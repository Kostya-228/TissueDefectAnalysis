using ConsoleApp;
using ConsoleApp.Models;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PackageAll
{
    public partial class MakeExperimentForm : Form
    {
        public MakeExperimentForm()
        {
            InitializeComponent();
        }

        private void MakeExperimentForm_Load(object sender, EventArgs e)
        {
            numericUpDownATo.ValueChanged += (sender_, e_) => { ReclalcOperationCount(); };
            numericUpDownAFrom.ValueChanged += (sender_, e_) => { ReclalcOperationCount(); };
            numericUpDownAStep.ValueChanged += (sender_, e_) => { ReclalcOperationCount(); };

            numericUpDownBTo.ValueChanged += (sender_, e_) => { ReclalcOperationCount(); };
            numericUpDownBFrom.ValueChanged += (sender_, e_) => { ReclalcOperationCount(); };
            numericUpDownBStep.ValueChanged += (sender_, e_) => { ReclalcOperationCount(); };

            numericUpDownPTo.ValueChanged += (sender_, e_) => { ReclalcOperationCount(); };
            numericUpDownPFrom.ValueChanged += (sender_, e_) => { ReclalcOperationCount(); };
            numericUpDownPStep.ValueChanged += (sender_, e_) => { ReclalcOperationCount(); };

            ReclalcOperationCount();

            this.comboBox1.Items.AddRange(DBConnector.GetList<ClothPattern>().Select(p => p.Name).ToArray());
        }

        private void ReclalcOperationCount()
        {
            labelIterCount.Text = (
                ((int)numericUpDownATo.Value - (int)numericUpDownAFrom.Value) / (int)numericUpDownAStep.Value *
                ((int)numericUpDownBTo.Value - (int)numericUpDownBFrom.Value) / (int)numericUpDownBStep.Value *
                ((int)numericUpDownPTo.Value - (int)numericUpDownPFrom.Value) / (int)numericUpDownPStep.Value).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var pattern = DBConnector.GetList<ClothPattern>().First(p => p.Name == (string)comboBox1.SelectedItem);
            string file_name = (string)comboBox2.SelectedItem;
            string root = pattern.Root;

            var thread = new Thread(() => {
                List<ImageArea> areas = DBConnector.GetList<ImageArea>().Where(area => area.FileName == file_name).ToList();
                var src = new Mat($"{root}\\{file_name}", ImreadModes.Grayscale);
                var indexer = src.GetGenericIndexer<Vec3b>();
                RunExperimenets(areas, indexer, DBConnector.GetList<Experement>().Max(ex => ex.ExperimentNumber) + 1);
            });

            thread.Start();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var pattern = DBConnector.GetList<ClothPattern>().First(p => p.Name == (string)comboBox1.SelectedItem);
            var imgs = DBConnector.GetList<ImageFile>().Where(img => img.PatternId == pattern.Id);

            comboBox2.Items.Clear();
            comboBox2.Items.AddRange(imgs.Select(img => img.FileName).ToArray());
        }

        void RunExperimenets(List<ImageArea> areas, Mat.Indexer<Vec3b> indexer, int exp_num = 1)
        {
            Param A = new Param() { 
                Min = (int)numericUpDownAFrom.Value,
                Max = (int)numericUpDownATo.Value,
                Step = (int)numericUpDownAStep.Value,
                Code = "1"
            };

            Param B = new Param()
            {
                Min = (int)numericUpDownBFrom.Value,
                Max = (int)numericUpDownBTo.Value,
                Step = (int)numericUpDownBStep.Value,
                Code = "2"
            };

            Param PCount = new Param()
            {
                Min = (int)numericUpDownPFrom.Value,
                Max = (int)numericUpDownPTo.Value,
                Step = (int)numericUpDownPStep.Value,
                Code = "3"
            };

            this.labelExpNum.Invoke(new Action(() => { labelExpNum.Text = exp_num.ToString(); }));
            int test_num = 1;
            foreach (var a in A.AsEnumerable())
                foreach (var b in B.AsEnumerable())
                    foreach (var c in PCount.AsEnumerable())
                    {
                        this.labelCurrentIteration.Invoke(new Action(() => { labelCurrentIteration.Text = test_num.ToString(); }));
                        var exp = new Experement() { ExperimentNumber = exp_num, TestNubmer = test_num };
                        DBConnector.CreateItem(exp);
                        DBConnector.CreateList(new List<ExperimentPlan>() {
                            new ExperimentPlan() { ExperimentNumber = exp.ExperimentNumber, TestNubmer = exp.TestNubmer, CodeParameter = A.Code, ValueParameter = a },
                            new ExperimentPlan() { ExperimentNumber = exp.ExperimentNumber, TestNubmer = exp.TestNubmer, CodeParameter = B.Code, ValueParameter = b },
                            new ExperimentPlan() { ExperimentNumber = exp.ExperimentNumber, TestNubmer = exp.TestNubmer, CodeParameter = PCount.Code, ValueParameter = c },
                        });
                        var results = ConsoleApp.Program.MakeExperemnt(a, b, c, areas, exp, indexer);
                        DBConnector.CreateList(results);
                        test_num++;
                    }
        }
    }
}
