using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using ConsoleApp;
using ConsoleApp.Models;
using ConsoleApp.Utils;

namespace PrepareDataSet
{
    public partial class Form1 : Form
    {
        Graphics g;
        List<ImageFile> imgs;
        ImageFile cur_img;
        List<ImageArea> areas;
        public Form1()
        {
            InitializeComponent();
            g = pictureBox1.CreateGraphics();
        }


        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            Point p = GetRelativePoint();
            var clicked_area = areas.FirstOrDefault(area => area.Contains(p.X, p.Y));
            if (clicked_area == null)
                return;
            clicked_area.IsDefect = !clicked_area.IsDefect;
            g.DrawRectangle(clicked_area.IsDefect ? Pens.Red : Pens.Green, ConvertImgToPicBox(clicked_area));
            label2.Text = $"{p.X} {p.Y}";
            label3.Text = $"{clicked_area.Id}";
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

        private Point GetRelativePoint()
        {
            Point p = pictureBox1.PointToClient(Cursor.Position);
            Point unscaled_p = new Point();

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
                unscaled_p.X = (int)(p.X / scaleFactor);
                unscaled_p.Y = (int)((p.Y - filler) / scaleFactor);
            }
            else
            {
                // vertical image
                float scaleFactor = h_c / (float)h_i;
                float scaledWidth = w_i * scaleFactor;
                float filler = Math.Abs(w_c - scaledWidth) / 2;
                unscaled_p.X = (int)((p.X - filler) / scaleFactor);
                unscaled_p.Y = (int)(p.Y / scaleFactor);
            }

            return unscaled_p;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label1.Text = cur_img.FileName;
            Save();
            label1.Text = cur_img.FileName + " - Updated";
        }

        public void Save()
        {
            DBConnector.UpdateList(areas);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            imgs = DBConnector.GetList<ImageFile>();
            label1.Text = DBConnector.conn_str;
            comboBox1.Items.AddRange(imgs.Select(img => (object)img.FileName).ToArray());
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (areas != null)
                Save();
            cur_img = imgs.Where(img => img.FileName == (string)comboBox1.SelectedItem).First();
            pictureBox1.WaitOnLoad = false;
            pictureBox1.Image = new Bitmap(Path.Combine(ConsoleApp.Program.ImagesRoot, cur_img.FileName));
            label1.Text = cur_img.FileName;
            areas = DBConnector.GetList<ImageArea>().Where(area => area.FileName == cur_img.FileName).ToList();

            new Thread(() => {
                Thread.Sleep(100);
                foreach (var area in areas)
                {
                    g.DrawRectangle(area.IsDefect ? Pens.Red : Pens.Green, ConvertImgToPicBox(area));
                }
            }).Start();
        }
    }
}
