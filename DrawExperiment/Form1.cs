using ConsoleApp;
using ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.Linq;
using System.IO;
using System.Threading;

namespace DrawExperiment
{
    public partial class Form1 : Form
    {
        Graphics g;
        List<Experement> experements;

        List<ImageArea> areas;
        List<ExperimentResult> results;

        bool in_progress = false;

        int treashold { get { return (int)numericUpDown1.Value; } }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadExperiments();
            g = pictureBox1.CreateGraphics();
        }

        public void LoadExperiments()
        {
            experements = DBConnector.GetList<Experement>();
            comboBoxExperement.Items.Clear();
            comboBoxExperement.Items.AddRange(experements.Select(ex => ex.ExperimentNumber.ToString()).Distinct().ToArray());
        }

        public void LoadTests(int exp_num)
        {
            comboBoxTest.Items.Clear();
            comboBoxTest.Items.AddRange(experements.Where(ex => ex.ExperimentNumber == exp_num).Select(ex => ex.TestNubmer.ToString()).ToArray());
        }

        public void LoadImage(int experement, int test)
        {
            using (OleDbConnection connection = DBConnector.GetConnection())
            {
                var context = new DataContext(connection);
                results = context.GetTable<ExperimentResult>().Where(res =>
                    res.ExperimentNumber == experement &&
                    res.TestNubmer == test
                ).ToList();
                areas = context.GetTable<ImageArea>().Where(area => results.Select(res => res.IdArea).Contains(area.Id)).ToList();
            }

            SetPic(areas, results);
        }

        public void SetPic(List<ImageArea> areas, List<ExperimentResult> results)
        {
            SetStatistic(results);
            var quality = CalcQuality(areas, results, treashold);
            labelQuality.Text = quality.ToString();
            new Thread(() => {
                lock (g)
                {
                    var file = DBConnector.GetList<ImageFile>().First(img => img.FileName == areas[0].FileName);
                    var pattern = DBConnector.GetList<ClothPattern>().First(p => p.Id == file.PatternId);

                    lock (pictureBox1)
                    {
                        pictureBox1.WaitOnLoad = false;
                        pictureBox1.Image = new Bitmap(Path.Combine(pattern.Root, areas[0].FileName));
                        //SetStatistic(results);
                        Thread.Sleep(100);
                        DrawResults(areas, results);
                    }
                }
            }).Start();
        }

        public float CalcQuality(List<ImageArea> areas, List<ExperimentResult> results, int treshold, float kf1 = 1, float kf2 = 1)
        {
            int errorLevel1 = 0, errorsLevel2 = 0;
            foreach (var area in areas)
            {
                var is_defect = results.First(res => res.IdArea == area.Id).DefectPower <= treshold;
                if (area.IsDefect && !is_defect)
                    errorLevel1++;
                if (!area.IsDefect && is_defect)
                    errorsLevel2++;
            }
            return  (errorLevel1 * kf1 + errorsLevel2 * kf2) / areas.Count();
        }

        public void SetStatistic(List<ExperimentResult> results)
        {
            labelAvg.Text = ((int)results.Average(res => res.DefectPower)).ToString(); 
            labelMax.Text = ((int)results.Max(res => res.DefectPower)).ToString();
            labelMin.Text = ((int)results.Min(res => res.DefectPower)).ToString();
        }

        public void DrawResults(List<ImageArea> areas, List<ExperimentResult> results)
        {
            foreach (var area in areas)
            {
                var result = results.First(res => res.IdArea == area.Id);
                if (result.DefectPower <= treashold)
                    g.DrawRectangle(Pens.Green, ConvertImgToPicBox(area));
            }

            foreach (var area in areas)
            {
                var result = results.First(res => res.IdArea == area.Id);
                if (result.DefectPower >= (int)numericUpDown1.Value)
                    g.DrawRectangle(Pens.Red, ConvertImgToPicBox(area));
            }
        }

        private void comboBoxTest_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!in_progress)
            LoadImage(
                int.Parse((string)comboBoxExperement.SelectedItem),
                int.Parse((string)comboBoxTest.SelectedItem)
                );
        }

        private void comboBoxExperement_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTests(int.Parse((string)comboBoxExperement.SelectedItem));
        }

        private Rectangle ConvertImgToPicBox(ImageArea area)
        {
            Point p = new Point(area.X1, area.Y1);
            //Point p = pictureBox1.PointToClient(Cursor.Position);
            Point unscaled_p = new Point();
            Size size = new Size();

            // image and container dimensions
            int w_i = pictureBox1.Image.Width;
            int h_i = pictureBox1.Image.Height;
            int w_c = pictureBox1.Width;
            int h_c = pictureBox1.Height;

            float imageRatio = w_i / (float)h_i; // image W:H ratio
            float containerRatio = w_c / (float)h_c; // container W:H ratio

            if (imageRatio >= containerRatio)
            {
                // horizontal image
                float scaleFactor = w_c / (float)w_i;
                float scaledHeight = h_i * scaleFactor;
                // calculate gap between top of container and top of image
                float filler = Math.Abs(h_c - scaledHeight) / 2;
                unscaled_p.X = (int)(p.X * scaleFactor);
                unscaled_p.Y = (int)((p.Y * scaleFactor + filler));

                size.Width = (int)(area.w * scaleFactor);
                size.Height = (int)((area.h * scaleFactor));
            }
            else
            {
                // vertical image
                float scaleFactor = h_c / (float)h_i;
                float scaledWidth = w_i * scaleFactor;
                float filler = Math.Abs(w_c - scaledWidth) / 2;
                unscaled_p.X = (int)(p.X * scaleFactor + filler);
                unscaled_p.Y = (int)(p.Y * scaleFactor);

                size.Width = (int)(area.w * scaleFactor);
                size.Height = (int)((area.h * scaleFactor));
            }



            return new Rectangle(unscaled_p, size);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (!in_progress)
            {
                labelQuality.Text = CalcQuality(areas, results, treashold).ToString();
                //SetPic(areas, results);
                DrawResults(areas, results);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var experement = int.Parse(comboBoxExperement.SelectedItem.ToString());
            new Thread(() =>
            {
                CalcBestParams(experement);
            }).Start();
        }

        public void CalcBestParams(int experement)
        {
            //List<ExperimentResult> results;
            //List<ImageArea> areas;
            in_progress = true;

            float max_quality = 0;
            int optimal_treashold = 0;
            int best_test_num = 0;


            using (OleDbConnection connection = DBConnector.GetConnection())
            {
                var context = new DataContext(connection);
                var tests = experements.Where(exp => exp.ExperimentNumber == experement).Select(exp => exp.TestNubmer);
                foreach (int test_num in tests)
                {
                    // костыль, чтобы не итерироватья слишком долго )))
                    if (test_num > 30)
                        break;

                    var test_result = CalcTest(experement, test_num, context, max_quality);
                    if (test_result is null)
                    {
                        continue;
                    }
                    if (max_quality < test_result.Item2)
                    {
                        max_quality = test_result.Item2;
                        optimal_treashold = test_result.Item1;
                        best_test_num = test_num;
                    }
                    labelProgress.Invoke(new Action(() => { labelProgress.Text = $"{test_num}/{tests.Max()}"; }));
                    //context.GetTable<Treashold>().InsertOnSubmit(new Treashold() { 
                    //    ExperimentNumber = experement, 
                    //    TestNubmer = test_num,
                    //    treashold = test_result.Item1,
                    //    Quality = test_result.Item2
                    //});
                    //context.SubmitChanges();
                }
            }

            var plans = DBConnector.GetList<ExperimentPlan>().Where(ex => ex.ExperimentNumber == experement && ex.TestNubmer == best_test_num).ToList();
            MessageBox.Show(
                $"Лучший результат на {best_test_num} тесте, с параметрами:\n" +
                $"Больший радиус: {plans.First(p => p.CodeParameter == "1").ValueParameter}\n" +
                $"Меньший радиус: {plans.First(p => p.CodeParameter == "2").ValueParameter}\n" +
                $"Количество точек радиус: {plans.First(p => p.CodeParameter == "3").ValueParameter}\n" +
                $"Пороговое значение: {optimal_treashold}\n" +
                $"Качество распознования: {max_quality}\n",
                "Результаты");
            in_progress = false;

        }

        public Tuple<int, float> CalcTest(int experement, int test_num, DataContext context, float global_quality, int trashold_step = 3)
        {
            results = context.GetTable<ExperimentResult>().Where(res =>
                        res.ExperimentNumber == experement &&
                        res.TestNubmer == test_num
                    ).ToList();
            if (results.Count == 0)
                return null;

            areas = context.GetTable<ImageArea>().Where(area => results.Select(res => res.IdArea).Contains(area.Id)).ToList();
            var avg = (int)results.Average(res => res.DefectPower);
            var max = (int)results.Max(res => res.DefectPower);

            float max_quality = 0;
            int optimal_treashold = 0;

            for (int i = avg; i < max; i += trashold_step)
            {
                float value = CalcQuality(areas.ToList(), results.ToList(), i);
                if (value > max_quality)
                {
                    max_quality = value;
                    optimal_treashold = i;


                    comboBoxTest.Invoke(new Action(() => { comboBoxTest.SelectedItem = test_num.ToString(); }));
                    numericUpDown1.Invoke(new Action(() => { numericUpDown1.Value = i; }));
                    labelQuality.Invoke(new Action(() => { labelQuality.Text = max_quality.ToString(); }));
                    //SetStatistic(results);
                    var quality = CalcQuality(areas, results, treashold);
                    if (max_quality > global_quality)
                        DrawResults(areas, results);
                }
            }
            return new Tuple<int, float>(optimal_treashold, max_quality);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var experement = int.Parse(comboBoxExperement.SelectedItem.ToString());
            var test = int.Parse(comboBoxTest.SelectedItem.ToString());
            new Thread(() =>
            {
                CalcPorog(experement, test);
            }).Start();
        }

        private void CalcPorog(int experement, int test_num, int trashold_step = 3)
        {
            List<ExperimentResult> results;
            List<ImageArea> areas;
            in_progress = true;
            using (OleDbConnection connection = DBConnector.GetConnection())
            {
                var context = new DataContext(connection);
                var tests = experements.Where(exp => exp.ExperimentNumber == experement).Select(exp => exp.TestNubmer);
                CalcTest(experement, test_num, context, 0);
            }
            in_progress = false;
        }
    }
}
