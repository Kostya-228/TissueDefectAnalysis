﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PrepareDataSet;

namespace PackageAll
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            (new PrepareDataSet.Form1()).Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            (new MakeExperimentForm()).Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            (new DrawExperiment.Form1()).Show();
        }
    }
}
