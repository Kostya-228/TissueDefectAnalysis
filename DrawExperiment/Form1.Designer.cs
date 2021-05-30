namespace DrawExperiment
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBoxExperement = new System.Windows.Forms.ComboBox();
            this.comboBoxTest = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.labelMax = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.labelMin = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.labelAvg = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelQuality = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.labelProgress = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxExperement
            // 
            this.comboBoxExperement.FormattingEnabled = true;
            this.comboBoxExperement.Location = new System.Drawing.Point(34, 47);
            this.comboBoxExperement.Name = "comboBoxExperement";
            this.comboBoxExperement.Size = new System.Drawing.Size(121, 21);
            this.comboBoxExperement.TabIndex = 0;
            this.comboBoxExperement.SelectedIndexChanged += new System.EventHandler(this.comboBoxExperement_SelectedIndexChanged);
            // 
            // comboBoxTest
            // 
            this.comboBoxTest.FormattingEnabled = true;
            this.comboBoxTest.Location = new System.Drawing.Point(34, 132);
            this.comboBoxTest.Name = "comboBoxTest";
            this.comboBoxTest.Size = new System.Drawing.Size(121, 21);
            this.comboBoxTest.TabIndex = 1;
            this.comboBoxTest.SelectedIndexChanged += new System.EventHandler(this.comboBoxTest_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Эксперимент";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Тест";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(35, 172);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 4;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Порог";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(307, 47);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(650, 241);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 209);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Макс";
            // 
            // labelMax
            // 
            this.labelMax.AutoSize = true;
            this.labelMax.Location = new System.Drawing.Point(87, 209);
            this.labelMax.Name = "labelMax";
            this.labelMax.Size = new System.Drawing.Size(13, 13);
            this.labelMax.TabIndex = 8;
            this.labelMax.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(31, 233);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Мин";
            // 
            // labelMin
            // 
            this.labelMin.AutoSize = true;
            this.labelMin.Location = new System.Drawing.Point(87, 233);
            this.labelMin.Name = "labelMin";
            this.labelMin.Size = new System.Drawing.Size(13, 13);
            this.labelMin.TabIndex = 10;
            this.labelMin.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(31, 257);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Среднее";
            // 
            // labelAvg
            // 
            this.labelAvg.AutoSize = true;
            this.labelAvg.Location = new System.Drawing.Point(87, 257);
            this.labelAvg.Name = "labelAvg";
            this.labelAvg.Size = new System.Drawing.Size(13, 13);
            this.labelAvg.TabIndex = 12;
            this.labelAvg.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 330);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(161, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Оценка кач-ва распознования";
            // 
            // labelQuality
            // 
            this.labelQuality.AutoSize = true;
            this.labelQuality.Location = new System.Drawing.Point(207, 330);
            this.labelQuality.Name = "labelQuality";
            this.labelQuality.Size = new System.Drawing.Size(13, 13);
            this.labelQuality.TabIndex = 14;
            this.labelQuality.Text = "0";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(34, 74);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 39);
            this.button1.TabIndex = 15;
            this.button1.Text = "поиск оптимального теста";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // labelProgress
            // 
            this.labelProgress.AutoSize = true;
            this.labelProgress.Location = new System.Drawing.Point(32, 103);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(0, 13);
            this.labelProgress.TabIndex = 16;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(161, 132);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(120, 44);
            this.button2.TabIndex = 17;
            this.button2.Text = "поиск оптимального порога";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 389);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.labelProgress);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labelQuality);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelAvg);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.labelMin);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.labelMax);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxTest);
            this.Controls.Add(this.comboBoxExperement);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxExperement;
        private System.Windows.Forms.ComboBox comboBoxTest;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelMax;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labelMin;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label labelAvg;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelQuality;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labelProgress;
        private System.Windows.Forms.Button button2;
    }
}

