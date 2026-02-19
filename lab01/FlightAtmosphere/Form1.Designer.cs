namespace FlightAtmosphere
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.inputSpeed = new System.Windows.Forms.NumericUpDown();
            this.inputAngle = new System.Windows.Forms.NumericUpDown();
            this.inputHeight = new System.Windows.Forms.NumericUpDown();
            this.inputSize = new System.Windows.Forms.NumericUpDown();
            this.inputWeight = new System.Windows.Forms.NumericUpDown();
            this.inputDt = new System.Windows.Forms.NumericUpDown();
            this.btLaunch = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputWeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputDt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Top;
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Name = "chart1";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(1112, 300);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // inputSpeed
            // 
            this.inputSpeed.AccessibleDescription = "Скорость";
            this.inputSpeed.Location = new System.Drawing.Point(435, 306);
            this.inputSpeed.Name = "inputSpeed";
            this.inputSpeed.Size = new System.Drawing.Size(120, 20);
            this.inputSpeed.TabIndex = 1;
            // 
            // inputAngle
            // 
            this.inputAngle.AccessibleDescription = "Угол";
            this.inputAngle.Location = new System.Drawing.Point(435, 332);
            this.inputAngle.Name = "inputAngle";
            this.inputAngle.Size = new System.Drawing.Size(120, 20);
            this.inputAngle.TabIndex = 2;
            // 
            // inputHeight
            // 
            this.inputHeight.AccessibleDescription = "Высота";
            this.inputHeight.Location = new System.Drawing.Point(435, 362);
            this.inputHeight.Name = "inputHeight";
            this.inputHeight.Size = new System.Drawing.Size(120, 20);
            this.inputHeight.TabIndex = 3;
            // 
            // inputSize
            // 
            this.inputSize.AccessibleDescription = "Площадь";
            this.inputSize.Location = new System.Drawing.Point(435, 396);
            this.inputSize.Name = "inputSize";
            this.inputSize.Size = new System.Drawing.Size(120, 20);
            this.inputSize.TabIndex = 4;
            // 
            // inputWeight
            // 
            this.inputWeight.AccessibleDescription = "Масса";
            this.inputWeight.Location = new System.Drawing.Point(435, 422);
            this.inputWeight.Name = "inputWeight";
            this.inputWeight.Size = new System.Drawing.Size(120, 20);
            this.inputWeight.TabIndex = 5;
            // 
            // inputDt
            // 
            this.inputDt.AccessibleDescription = "Шаг dt";
            this.inputDt.DecimalPlaces = 4;
            this.inputDt.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.inputDt.Location = new System.Drawing.Point(435, 462);
            this.inputDt.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.inputDt.Name = "inputDt";
            this.inputDt.Size = new System.Drawing.Size(120, 20);
            this.inputDt.TabIndex = 6;
            this.inputDt.Value = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            // 
            // btLaunch
            // 
            this.btLaunch.Location = new System.Drawing.Point(728, 358);
            this.btLaunch.Name = "btLaunch";
            this.btLaunch.Size = new System.Drawing.Size(75, 23);
            this.btLaunch.TabIndex = 7;
            this.btLaunch.Text = "запуск";
            this.btLaunch.UseVisualStyleBackColor = true;
            this.btLaunch.Click += new System.EventHandler(this.btLaunch_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView1.Location = new System.Drawing.Point(0, 502);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1112, 150);
            this.dataGridView1.TabIndex = 8;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(573, 306);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 9;
            this.textBox1.Text = "скорость";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(573, 332);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 10;
            this.textBox2.Text = "угол";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(573, 361);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 20);
            this.textBox3.TabIndex = 11;
            this.textBox3.Text = "высота";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(573, 395);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 20);
            this.textBox4.TabIndex = 12;
            this.textBox4.Text = "площадь";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(573, 421);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(100, 20);
            this.textBox5.TabIndex = 13;
            this.textBox5.Text = "масса";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(573, 461);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(100, 20);
            this.textBox6.TabIndex = 14;
            this.textBox6.Text = "шаг dt";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1112, 652);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btLaunch);
            this.Controls.Add(this.inputDt);
            this.Controls.Add(this.inputWeight);
            this.Controls.Add(this.inputSize);
            this.Controls.Add(this.inputHeight);
            this.Controls.Add(this.inputAngle);
            this.Controls.Add(this.inputSpeed);
            this.Controls.Add(this.chart1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputWeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputDt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.NumericUpDown inputSpeed;
        private System.Windows.Forms.NumericUpDown inputAngle;
        private System.Windows.Forms.NumericUpDown inputHeight;
        private System.Windows.Forms.NumericUpDown inputSize;
        private System.Windows.Forms.NumericUpDown inputWeight;
        private System.Windows.Forms.NumericUpDown inputDt;
        private System.Windows.Forms.Button btLaunch;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
    }
}

