using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace lab6
{
    partial class Form1
    {

        private System.ComponentModel.IContainer components = null;


        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            Start1 = new Button();
            button3 = new Button();
            panel1 = new Panel();
            NumSample = new NumericUpDown();
            label1 = new Label();
            dataGridView1 = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            Start2 = new Button();
            VarRes = new Label();
            N_experiment = new NumericUpDown();
            label2 = new Label();
            labelMean = new Label();
            labelVar = new Label();
            label_Chi = new Label();
            label4 = new Label();
            label5 = new Label();
            label7 = new Label();
            label9 = new Label();
            label10 = new Label();
            labelErrorMean = new Label();
            labelErrorVar = new Label();
            label3 = new Label();
            mean2 = new NumericUpDown();
            label6 = new Label();
            var2 = new NumericUpDown();
            chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            label8 = new Label();
            N2_experiment = new NumericUpDown();
            labelVarError2 = new Label();
            labelMeanError2 = new Label();
            label13 = new Label();
            label14 = new Label();
            label15 = new Label();
            label16 = new Label();
            label_Chi_2 = new Label();
            labelVar2 = new Label();
            labelMean2 = new Label();
            ((System.ComponentModel.ISupportInitialize)NumSample).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chart1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)N_experiment).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mean2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)var2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chart2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)N2_experiment).BeginInit();
            SuspendLayout();
            // 
            // Start1
            // 
            Start1.BackColor = Color.ForestGreen;
            Start1.FlatStyle = FlatStyle.Popup;
            Start1.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 204);
            Start1.ForeColor = SystemColors.Window;
            Start1.Location = new Point(851, 679);
            Start1.Name = "Start1";
            Start1.Size = new Size(112, 42);
            Start1.TabIndex = 0;
            Start1.Text = "Старт";
            Start1.UseVisualStyleBackColor = false;
            Start1.Click += Start1_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.Red;
            button3.DialogResult = DialogResult.Cancel;
            button3.FlatStyle = FlatStyle.Popup;
            button3.Font = new Font("Segoe UI Black", 10F, FontStyle.Bold, GraphicsUnit.Point, 204);
            button3.ForeColor = SystemColors.Window;
            button3.Location = new Point(1803, 12);
            button3.Name = "button3";
            button3.Size = new Size(40, 40);
            button3.TabIndex = 2;
            button3.Text = "X";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaptionText;
            panel1.Location = new Point(977, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(2, 1000);
            panel1.TabIndex = 3;
            // 
            // NumSample
            // 
            NumSample.Location = new Point(171, 73);
            NumSample.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            NumSample.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            NumSample.Name = "NumSample";
            NumSample.Size = new Size(108, 31);
            NumSample.TabIndex = 4;
            NumSample.Value = new decimal(new int[] { 5, 0, 0, 0 });
            NumSample.ValueChanged += numRowsCount_ValueChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(7, 75);
            label1.Name = "label1";
            label1.Size = new Size(151, 25);
            label1.TabIndex = 5;
            label1.Text = "Кол-во значений";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2 });
            dataGridView1.Location = new Point(28, 175);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(229, 547);
            dataGridView1.TabIndex = 6;
            dataGridView1.CellValueChanged += CellValueChanged;
            // 
            // Column1
            // 
            Column1.HeaderText = "Значение";
            Column1.MinimumWidth = 8;
            Column1.Name = "Column1";
            Column1.Width = 150;
            // 
            // Column2
            // 
            Column2.HeaderText = "Вероятность";
            Column2.MinimumWidth = 8;
            Column2.Name = "Column2";
            Column2.Width = 120;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chart1.Legends.Add(legend1);
            chart1.Location = new Point(285, 27);
            chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chart1.Series.Add(series1);
            chart1.Size = new Size(686, 532);
            chart1.TabIndex = 7;
            chart1.Text = "chart1";
            // 
            // Start2
            // 
            Start2.BackColor = Color.ForestGreen;
            Start2.FlatStyle = FlatStyle.Popup;
            Start2.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 204);
            Start2.ForeColor = SystemColors.Window;
            Start2.Location = new Point(1721, 679);
            Start2.Name = "Start2";
            Start2.Size = new Size(112, 42);
            Start2.TabIndex = 8;
            Start2.Text = "Старт";
            Start2.UseVisualStyleBackColor = false;
            Start2.Click += Start2_Click;
            // 
            // VarRes
            // 
            VarRes.AutoSize = true;
            VarRes.Location = new Point(91, 147);
            VarRes.Name = "VarRes";
            VarRes.Size = new Size(96, 25);
            VarRes.TabIndex = 9;
            VarRes.Text = "Остаток: 1";
            // 
            // N_experiment
            // 
            N_experiment.Location = new Point(171, 27);
            N_experiment.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            N_experiment.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            N_experiment.Name = "N_experiment";
            N_experiment.Size = new Size(108, 31);
            N_experiment.TabIndex = 10;
            N_experiment.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(7, 29);
            label2.Name = "label2";
            label2.Size = new Size(162, 25);
            label2.TabIndex = 11;
            label2.Text = "Кол-во испытаний";
            // 
            // labelMean
            // 
            labelMean.AutoSize = true;
            labelMean.Location = new Point(434, 589);
            labelMean.Name = "labelMean";
            labelMean.Size = new Size(59, 25);
            labelMean.TabIndex = 13;
            labelMean.Text = "label2";
            // 
            // labelVar
            // 
            labelVar.AutoSize = true;
            labelVar.Location = new Point(434, 637);
            labelVar.Name = "labelVar";
            labelVar.Size = new Size(59, 25);
            labelVar.TabIndex = 14;
            labelVar.Text = "label2";
            // 
            // label_Chi
            // 
            label_Chi.AutoSize = true;
            label_Chi.Location = new Point(313, 689);
            label_Chi.Name = "label_Chi";
            label_Chi.Size = new Size(59, 25);
            label_Chi.TabIndex = 15;
            label_Chi.Text = "label2";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(313, 589);
            label4.Name = "label4";
            label4.Size = new Size(85, 25);
            label4.TabIndex = 16;
            label4.Text = "Cреднее:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(313, 637);
            label5.Name = "label5";
            label5.Size = new Size(103, 25);
            label5.TabIndex = 17;
            label5.Text = "Дисперсия:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(526, 589);
            label7.Name = "label7";
            label7.Size = new Size(82, 25);
            label7.TabIndex = 19;
            label7.Text = "Ошибка:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(876, 368);
            label9.Name = "label9";
            label9.Size = new Size(0, 25);
            label9.TabIndex = 21;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(526, 637);
            label10.Name = "label10";
            label10.Size = new Size(82, 25);
            label10.TabIndex = 22;
            label10.Text = "Ошибка:";
            // 
            // labelErrorMean
            // 
            labelErrorMean.AutoSize = true;
            labelErrorMean.Location = new Point(630, 589);
            labelErrorMean.Name = "labelErrorMean";
            labelErrorMean.Size = new Size(59, 25);
            labelErrorMean.TabIndex = 23;
            labelErrorMean.Text = "label2";
            // 
            // labelErrorVar
            // 
            labelErrorVar.AutoSize = true;
            labelErrorVar.Location = new Point(630, 637);
            labelErrorVar.Name = "labelErrorVar";
            labelErrorVar.Size = new Size(59, 25);
            labelErrorVar.TabIndex = 24;
            labelErrorVar.Text = "label2";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(1011, 639);
            label3.Name = "label3";
            label3.Size = new Size(81, 25);
            label3.TabIndex = 28;
            label3.Text = "Среднее";
            // 
            // mean2
            // 
            mean2.DecimalPlaces = 3;
            mean2.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            mean2.Location = new Point(1175, 637);
            mean2.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            mean2.Minimum = new decimal(new int[] { 100000, 0, 0, int.MinValue });
            mean2.Name = "mean2";
            mean2.Size = new Size(108, 31);
            mean2.TabIndex = 27;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(1011, 685);
            label6.Name = "label6";
            label6.Size = new Size(99, 25);
            label6.TabIndex = 26;
            label6.Text = "Дисперсия";
            // 
            // var2
            // 
            var2.DecimalPlaces = 3;
            var2.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            var2.Location = new Point(1175, 683);
            var2.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            var2.Minimum = new decimal(new int[] { 10000, 0, 0, int.MinValue });
            var2.Name = "var2";
            var2.Size = new Size(108, 31);
            var2.TabIndex = 25;
            var2.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // chart2
            // 
            chartArea2.Name = "ChartArea1";
            chart2.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            chart2.Legends.Add(legend2);
            chart2.Location = new Point(1011, 27);
            chart2.Name = "chart2";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            chart2.Series.Add(series2);
            chart2.Size = new Size(686, 532);
            chart2.TabIndex = 29;
            chart2.Text = "chart2";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(1011, 591);
            label8.Name = "label8";
            label8.Size = new Size(162, 25);
            label8.TabIndex = 31;
            label8.Text = "Кол-во испытаний";
            // 
            // N2_experiment
            // 
            N2_experiment.Location = new Point(1175, 589);
            N2_experiment.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            N2_experiment.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            N2_experiment.Name = "N2_experiment";
            N2_experiment.Size = new Size(108, 31);
            N2_experiment.TabIndex = 30;
            N2_experiment.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // labelVarError2
            // 
            labelVarError2.AutoSize = true;
            labelVarError2.Location = new Point(1638, 639);
            labelVarError2.Name = "labelVarError2";
            labelVarError2.Size = new Size(59, 25);
            labelVarError2.TabIndex = 40;
            labelVarError2.Text = "label2";
            // 
            // labelMeanError2
            // 
            labelMeanError2.AutoSize = true;
            labelMeanError2.Location = new Point(1638, 591);
            labelMeanError2.Name = "labelMeanError2";
            labelMeanError2.Size = new Size(59, 25);
            labelMeanError2.TabIndex = 39;
            labelMeanError2.Text = "label2";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(1534, 639);
            label13.Name = "label13";
            label13.Size = new Size(82, 25);
            label13.TabIndex = 38;
            label13.Text = "Ошибка:";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(1534, 591);
            label14.Name = "label14";
            label14.Size = new Size(82, 25);
            label14.TabIndex = 37;
            label14.Text = "Ошибка:";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(1321, 639);
            label15.Name = "label15";
            label15.Size = new Size(103, 25);
            label15.TabIndex = 36;
            label15.Text = "Дисперсия:";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(1321, 591);
            label16.Name = "label16";
            label16.Size = new Size(85, 25);
            label16.TabIndex = 35;
            label16.Text = "Cреднее:";
            // 
            // label_Chi_2
            // 
            label_Chi_2.AutoSize = true;
            label_Chi_2.Location = new Point(1321, 691);
            label_Chi_2.Name = "label_Chi_2";
            label_Chi_2.Size = new Size(59, 25);
            label_Chi_2.TabIndex = 34;
            label_Chi_2.Text = "label2";
            // 
            // labelVar2
            // 
            labelVar2.AutoSize = true;
            labelVar2.Location = new Point(1442, 639);
            labelVar2.Name = "labelVar2";
            labelVar2.Size = new Size(59, 25);
            labelVar2.TabIndex = 33;
            labelVar2.Text = "label2";
            // 
            // labelMean2
            // 
            labelMean2.AutoSize = true;
            labelMean2.Location = new Point(1442, 591);
            labelMean2.Name = "labelMean2";
            labelMean2.Size = new Size(59, 25);
            labelMean2.TabIndex = 32;
            labelMean2.Text = "label2";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1855, 1000);
            Controls.Add(labelVarError2);
            Controls.Add(labelMeanError2);
            Controls.Add(label13);
            Controls.Add(label14);
            Controls.Add(label15);
            Controls.Add(label16);
            Controls.Add(label_Chi_2);
            Controls.Add(labelVar2);
            Controls.Add(labelMean2);
            Controls.Add(label8);
            Controls.Add(N2_experiment);
            Controls.Add(chart2);
            Controls.Add(label3);
            Controls.Add(mean2);
            Controls.Add(label6);
            Controls.Add(var2);
            Controls.Add(labelErrorVar);
            Controls.Add(labelErrorMean);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(label7);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label_Chi);
            Controls.Add(labelVar);
            Controls.Add(labelMean);
            Controls.Add(label2);
            Controls.Add(N_experiment);
            Controls.Add(VarRes);
            Controls.Add(Start2);
            Controls.Add(chart1);
            Controls.Add(dataGridView1);
            Controls.Add(label1);
            Controls.Add(NumSample);
            Controls.Add(panel1);
            Controls.Add(button3);
            Controls.Add(Start1);
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Maximized;
            MinimumSize = new Size(1200, 700);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)NumSample).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)chart1).EndInit();
            ((System.ComponentModel.ISupportInitialize)N_experiment).EndInit();
            ((System.ComponentModel.ISupportInitialize)mean2).EndInit();
            ((System.ComponentModel.ISupportInitialize)var2).EndInit();
            ((System.ComponentModel.ISupportInitialize)chart2).EndInit();
            ((System.ComponentModel.ISupportInitialize)N2_experiment).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Start1;
        private Button button3;
        private Panel panel1;
        private NumericUpDown NumSample;
        private Label label1;
        private DataGridView dataGridView1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private Button Start2;
        private Label VarRes;
        private NumericUpDown N_experiment;
        private Label label2;
        private Label labelMean;
        private Label labelVar;
        private Label label_Chi;
        private Label label4;
        private Label label5;
        private Label label7;
        private Label label9;
        private Label label10;
        private Label labelErrorMean;
        private Label labelErrorVar;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private Label label3;
        private NumericUpDown mean2;
        private Label label6;
        private NumericUpDown var2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private Label label8;
        private NumericUpDown N2_experiment;
        private Label labelVarError2;
        private Label labelMeanError2;
        private Label label13;
        private Label label14;
        private Label label15;
        private Label label16;
        private Label label_Chi_2;
        private Label labelVar2;
        private Label labelMean2;
    }
}