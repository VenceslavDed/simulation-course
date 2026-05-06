namespace lab7
{
    partial class Form1
    {
        
        private System.ComponentModel.IContainer components = null;

        
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            numericUpDown1 = new NumericUpDown();
            label1 = new Label();
            chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            button1 = new Button();
            button2 = new Button();
            listBox1 = new ListBox();
            label2 = new Label();
            n00 = new NumericUpDown();
            n11 = new NumericUpDown();
            n22 = new NumericUpDown();
            n01 = new NumericUpDown();
            n02 = new NumericUpDown();
            n10 = new NumericUpDown();
            n12 = new NumericUpDown();
            n20 = new NumericUpDown();
            n21 = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chart1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)n00).BeginInit();
            ((System.ComponentModel.ISupportInitialize)n11).BeginInit();
            ((System.ComponentModel.ISupportInitialize)n22).BeginInit();
            ((System.ComponentModel.ISupportInitialize)n01).BeginInit();
            ((System.ComponentModel.ISupportInitialize)n02).BeginInit();
            ((System.ComponentModel.ISupportInitialize)n10).BeginInit();
            ((System.ComponentModel.ISupportInitialize)n12).BeginInit();
            ((System.ComponentModel.ISupportInitialize)n20).BeginInit();
            ((System.ComponentModel.ISupportInitialize)n21).BeginInit();
            SuspendLayout();
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(78, 17);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(150, 27);
            numericUpDown1.TabIndex = 0;
            numericUpDown1.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(27, 19);
            label1.Name = "label1";
            label1.Size = new Size(45, 20);
            label1.TabIndex = 1;
            label1.Text = "Дней";
            // 
            // chart1
            // 
            chartArea2.Name = "ChartArea1";
            chart1.ChartAreas.Add(chartArea2);
            legend2.Enabled = false;
            legend2.Name = "Legend1";
            chart1.Legends.Add(legend2);
            chart1.Location = new Point(410, 54);
            chart1.Name = "chart1";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.IsVisibleInLegend = false;
            series2.LabelFormat = "F3";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            chart1.Series.Add(series2);
            chart1.Size = new Size(718, 384);
            chart1.TabIndex = 2;
            chart1.Text = "chart1";
            // 
            // button1
            // 
            button1.Location = new Point(1034, 17);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 3;
            button1.Text = "Закрыть";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(234, 17);
            button2.Name = "button2";
            button2.Size = new Size(156, 29);
            button2.TabIndex = 4;
            button2.Text = "Прогнозирование";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(36, 460);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(1092, 184);
            listBox1.TabIndex = 5;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(27, 67);
            label2.Name = "label2";
            label2.Size = new Size(326, 20);
            label2.TabIndex = 6;
            label2.Text = "Матрица интенсивности переходов";
            // 
            // n00
            // 
            n00.DecimalPlaces = 3;
            n00.Location = new Point(33, 107);
            n00.Maximum = new decimal(new int[] { 0, 0, 0, 0 });
            n00.Minimum = new decimal(new int[] { 100, 0, 0, int.MinValue });
            n00.Name = "n00";
            n00.Size = new Size(68, 27);
            n00.TabIndex = 7;
            n00.Value = new decimal(new int[] { 8, 0, 0, -2147418112 });
            // 
            // n11
            // 
            n11.DecimalPlaces = 3;
            n11.Location = new Point(107, 140);
            n11.Maximum = new decimal(new int[] { 0, 0, 0, 0 });
            n11.Minimum = new decimal(new int[] { 100, 0, 0, int.MinValue });
            n11.Name = "n11";
            n11.Size = new Size(68, 27);
            n11.TabIndex = 8;
            n11.Value = new decimal(new int[] { 4, 0, 0, -2147418112 });
            // 
            // n22
            // 
            n22.DecimalPlaces = 3;
            n22.Location = new Point(181, 173);
            n22.Maximum = new decimal(new int[] { 0, 0, 0, 0 });
            n22.Minimum = new decimal(new int[] { 100, 0, 0, int.MinValue });
            n22.Name = "n22";
            n22.Size = new Size(68, 27);
            n22.TabIndex = 9;
            n22.Value = new decimal(new int[] { 9, 0, 0, -2147418112 });
            // 
            // n01
            // 
            n01.DecimalPlaces = 3;
            n01.Location = new Point(107, 107);
            n01.Name = "n01";
            n01.Size = new Size(68, 27);
            n01.TabIndex = 10;
            n01.Value = new decimal(new int[] { 45, 0, 0, 131072 });
            // 
            // n02
            // 
            n02.DecimalPlaces = 3;
            n02.Location = new Point(181, 107);
            n02.Name = "n02";
            n02.Size = new Size(68, 27);
            n02.TabIndex = 11;
            n02.Value = new decimal(new int[] { 35, 0, 0, 131072 });
            // 
            // n10
            // 
            n10.DecimalPlaces = 3;
            n10.Location = new Point(33, 140);
            n10.Name = "n10";
            n10.Size = new Size(68, 27);
            n10.TabIndex = 12;
            n10.Value = new decimal(new int[] { 3, 0, 0, 65536 });
            // 
            // n12
            // 
            n12.DecimalPlaces = 3;
            n12.Location = new Point(181, 140);
            n12.Name = "n12";
            n12.Size = new Size(68, 27);
            n12.TabIndex = 13;
            n12.Value = new decimal(new int[] { 1, 0, 0, 65536 });
            // 
            // n20
            // 
            n20.DecimalPlaces = 3;
            n20.Location = new Point(33, 173);
            n20.Name = "n20";
            n20.Size = new Size(68, 27);
            n20.TabIndex = 14;
            n20.Value = new decimal(new int[] { 45, 0, 0, 131072 });
            // 
            // n21
            // 
            n21.DecimalPlaces = 3;
            n21.Location = new Point(107, 173);
            n21.Name = "n21";
            n21.Size = new Size(68, 27);
            n21.TabIndex = 15;
            n21.Value = new decimal(new int[] { 45, 0, 0, 131072 });
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1140, 656);
            Controls.Add(n21);
            Controls.Add(n20);
            Controls.Add(n12);
            Controls.Add(n10);
            Controls.Add(n02);
            Controls.Add(n01);
            Controls.Add(n22);
            Controls.Add(n11);
            Controls.Add(n00);
            Controls.Add(label2);
            Controls.Add(listBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(chart1);
            Controls.Add(label1);
            Controls.Add(numericUpDown1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)chart1).EndInit();
            ((System.ComponentModel.ISupportInitialize)n00).EndInit();
            ((System.ComponentModel.ISupportInitialize)n11).EndInit();
            ((System.ComponentModel.ISupportInitialize)n22).EndInit();
            ((System.ComponentModel.ISupportInitialize)n01).EndInit();
            ((System.ComponentModel.ISupportInitialize)n02).EndInit();
            ((System.ComponentModel.ISupportInitialize)n10).EndInit();
            ((System.ComponentModel.ISupportInitialize)n12).EndInit();
            ((System.ComponentModel.ISupportInitialize)n20).EndInit();
            ((System.ComponentModel.ISupportInitialize)n21).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NumericUpDown numericUpDown1;
        private Label label1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private Button button1;
        private Button button2;
        private ListBox listBox1;
        private Label label2;
        private NumericUpDown n00;
        private NumericUpDown n11;
        private NumericUpDown n22;
        private NumericUpDown n01;
        private NumericUpDown n02;
        private NumericUpDown n10;
        private NumericUpDown n12;
        private NumericUpDown n20;
        private NumericUpDown n21;
    }
}
