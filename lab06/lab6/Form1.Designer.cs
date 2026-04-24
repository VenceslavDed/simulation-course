namespace StochasticModeling
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage61 = new System.Windows.Forms.TabPage();
            this.tabPage62 = new System.Windows.Forms.TabPage();

            
            this.grpInput61 = new System.Windows.Forms.GroupBox();
            this.lblP1 = new System.Windows.Forms.Label();
            this.lblP2 = new System.Windows.Forms.Label();
            this.lblP3 = new System.Windows.Forms.Label();
            this.lblP4 = new System.Windows.Forms.Label();
            this.lblP5 = new System.Windows.Forms.Label();
            this.txtP1 = new System.Windows.Forms.TextBox();
            this.txtP2 = new System.Windows.Forms.TextBox();
            this.txtP3 = new System.Windows.Forms.TextBox();
            this.txtP4 = new System.Windows.Forms.TextBox();
            this.txtP5 = new System.Windows.Forms.TextBox();
            this.lblN61 = new System.Windows.Forms.Label();
            this.txtN61 = new System.Windows.Forms.TextBox();
            this.btnStart61 = new System.Windows.Forms.Button();
            this.picChart61 = new System.Windows.Forms.PictureBox();
            this.lblResult61 = new System.Windows.Forms.Label();

            
            this.grpInput62 = new System.Windows.Forms.GroupBox();
            this.lblMean = new System.Windows.Forms.Label();
            this.txtMean = new System.Windows.Forms.TextBox();
            this.lblVariance = new System.Windows.Forms.Label();
            this.txtVariance = new System.Windows.Forms.TextBox();
            this.lblN62 = new System.Windows.Forms.Label();
            this.txtN62 = new System.Windows.Forms.TextBox();
            this.btnStart62 = new System.Windows.Forms.Button();
            this.picChart62 = new System.Windows.Forms.PictureBox();
            this.lblResult62 = new System.Windows.Forms.Label();

            
            //  FORM
            this.SuspendLayout();
            this.Text = "Стохастическое моделирование — Лаб. 6.1 & 6.2";
            this.Size = new System.Drawing.Size(860, 560);
            this.MinimumSize = new System.Drawing.Size(860, 560);
            this.Font = new System.Drawing.Font("Segoe UI", 9f);
            this.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);

            
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 10f, System.Drawing.FontStyle.Regular);
            this.tabControl.TabPages.Add(this.tabPage61);
            this.tabControl.TabPages.Add(this.tabPage62);


            //  TAB PAGE 6.1
            this.tabPage61.Text = "Лаб. 6.1 — Дискретная СВ";
            this.tabPage61.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);


            this.grpInput61.Text = "Ряд распределения (xi = 1..5)";
            this.grpInput61.Location = new System.Drawing.Point(12, 12);
            this.grpInput61.Size = new System.Drawing.Size(220, 240);
            this.grpInput61.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            this.grpInput61.ForeColor = System.Drawing.Color.FromArgb(40, 80, 140);

            string[] probLabels = { "Prob 1", "Prob 2", "Prob 3", "Prob 4", "Prob 5" };
            System.Windows.Forms.Label[] lblArr = { lblP1, lblP2, lblP3, lblP4, lblP5 };
            System.Windows.Forms.TextBox[] txtArr = { txtP1, txtP2, txtP3, txtP4, txtP5 };

            for (int i = 0; i < 5; i++)
            {
                lblArr[i].Text = probLabels[i];
                lblArr[i].Location = new System.Drawing.Point(10, 28 + i * 36);
                lblArr[i].Size = new System.Drawing.Size(55, 22);
                lblArr[i].Font = new System.Drawing.Font("Segoe UI", 9f);
                lblArr[i].ForeColor = System.Drawing.Color.Black;
                grpInput61.Controls.Add(lblArr[i]);

                txtArr[i].Location = new System.Drawing.Point(72, 26 + i * 36);
                txtArr[i].Size = new System.Drawing.Size(70, 24);
                txtArr[i].Font = new System.Drawing.Font("Segoe UI", 9f);
                grpInput61.Controls.Add(txtArr[i]);
            }

            // Prob 5 
            txtP5.Text = "auto";
            txtP5.ForeColor = System.Drawing.Color.Gray;

            this.lblN61.Text = "Число экспер.:";
            this.lblN61.Location = new System.Drawing.Point(10, 212);
            this.lblN61.Size = new System.Drawing.Size(95, 20);
            this.lblN61.Font = new System.Drawing.Font("Segoe UI", 9f);
            this.grpInput61.Controls.Add(this.lblN61);

            this.txtN61.Location = new System.Drawing.Point(108, 209);
            this.txtN61.Size = new System.Drawing.Size(70, 24);
            this.txtN61.Text = "1000";
            this.grpInput61.Controls.Add(this.txtN61);

            this.btnStart61.Text = "Start";
            this.btnStart61.Location = new System.Drawing.Point(55, 265);
            this.btnStart61.Size = new System.Drawing.Size(110, 34);
            this.btnStart61.Font = new System.Drawing.Font("Segoe UI", 10f, System.Drawing.FontStyle.Bold);
            this.btnStart61.BackColor = System.Drawing.Color.FromArgb(70, 130, 180);
            this.btnStart61.ForeColor = System.Drawing.Color.White;
            this.btnStart61.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart61.FlatAppearance.BorderSize = 0;
            this.btnStart61.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStart61.Click += new System.EventHandler(this.btnStart61_Click);

            this.picChart61.Location = new System.Drawing.Point(248, 12);
            this.picChart61.Size = new System.Drawing.Size(580, 320);
            this.picChart61.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picChart61.BackColor = System.Drawing.Color.White;
            this.picChart61.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal;

            this.lblResult61.Location = new System.Drawing.Point(248, 345);
            this.lblResult61.Size = new System.Drawing.Size(580, 90);
            this.lblResult61.Font = new System.Drawing.Font("Consolas", 10f);
            this.lblResult61.ForeColor = System.Drawing.Color.DarkGreen;

            this.tabPage61.Controls.Add(this.grpInput61);
            this.tabPage61.Controls.Add(this.btnStart61);
            this.tabPage61.Controls.Add(this.picChart61);
            this.tabPage61.Controls.Add(this.lblResult61);


            //  TAB PAGE 6.2
            this.tabPage62.Text = "Лаб. 6.2 — Нормальная СВ";
            this.tabPage62.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);


            this.grpInput62.Text = "Параметры нормального распределения";
            this.grpInput62.Location = new System.Drawing.Point(12, 12);
            this.grpInput62.Size = new System.Drawing.Size(220, 200);
            this.grpInput62.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            this.grpInput62.ForeColor = System.Drawing.Color.FromArgb(40, 80, 140);

            this.lblMean.Text = "Mean (a):";
            this.lblMean.Location = new System.Drawing.Point(10, 30);
            this.lblMean.Size = new System.Drawing.Size(70, 22);
            this.lblMean.Font = new System.Drawing.Font("Segoe UI", 9f);
            this.lblMean.ForeColor = System.Drawing.Color.Black;

            this.txtMean.Location = new System.Drawing.Point(90, 27);
            this.txtMean.Size = new System.Drawing.Size(80, 24);
            this.txtMean.Text = "0";

            this.lblVariance.Text = "Variance (σ²):";
            this.lblVariance.Location = new System.Drawing.Point(10, 68);
            this.lblVariance.Size = new System.Drawing.Size(90, 22);
            this.lblVariance.Font = new System.Drawing.Font("Segoe UI", 9f);
            this.lblVariance.ForeColor = System.Drawing.Color.Black;

            this.txtVariance.Location = new System.Drawing.Point(105, 65);
            this.txtVariance.Size = new System.Drawing.Size(65, 24);
            this.txtVariance.Text = "1";

            this.lblN62.Text = "Sample size:";
            this.lblN62.Location = new System.Drawing.Point(10, 106);
            this.lblN62.Size = new System.Drawing.Size(85, 22);
            this.lblN62.Font = new System.Drawing.Font("Segoe UI", 9f);
            this.lblN62.ForeColor = System.Drawing.Color.Black;

            this.txtN62.Location = new System.Drawing.Point(100, 103);
            this.txtN62.Size = new System.Drawing.Size(70, 24);
            this.txtN62.Text = "1000";

            this.grpInput62.Controls.Add(this.lblMean);
            this.grpInput62.Controls.Add(this.txtMean);
            this.grpInput62.Controls.Add(this.lblVariance);
            this.grpInput62.Controls.Add(this.txtVariance);
            this.grpInput62.Controls.Add(this.lblN62);
            this.grpInput62.Controls.Add(this.txtN62);

            this.btnStart62.Text = "Start";
            this.btnStart62.Location = new System.Drawing.Point(55, 228);
            this.btnStart62.Size = new System.Drawing.Size(110, 34);
            this.btnStart62.Font = new System.Drawing.Font("Segoe UI", 10f, System.Drawing.FontStyle.Bold);
            this.btnStart62.BackColor = System.Drawing.Color.FromArgb(70, 130, 180);
            this.btnStart62.ForeColor = System.Drawing.Color.White;
            this.btnStart62.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart62.FlatAppearance.BorderSize = 0;
            this.btnStart62.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStart62.Click += new System.EventHandler(this.btnStart62_Click);

            this.picChart62.Location = new System.Drawing.Point(248, 12);
            this.picChart62.Size = new System.Drawing.Size(580, 320);
            this.picChart62.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picChart62.BackColor = System.Drawing.Color.White;
            this.picChart62.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal;

            this.lblResult62.Location = new System.Drawing.Point(248, 345);
            this.lblResult62.Size = new System.Drawing.Size(580, 90);
            this.lblResult62.Font = new System.Drawing.Font("Consolas", 10f);
            this.lblResult62.ForeColor = System.Drawing.Color.DarkGreen;

            this.tabPage62.Controls.Add(this.grpInput62);
            this.tabPage62.Controls.Add(this.btnStart62);
            this.tabPage62.Controls.Add(this.picChart62);
            this.tabPage62.Controls.Add(this.lblResult62);


            this.Controls.Add(this.tabControl);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage61;
        private System.Windows.Forms.TabPage tabPage62;

        private System.Windows.Forms.GroupBox grpInput61;
        private System.Windows.Forms.Label lblP1, lblP2, lblP3, lblP4, lblP5;
        private System.Windows.Forms.TextBox txtP1, txtP2, txtP3, txtP4, txtP5;
        private System.Windows.Forms.Label lblN61;
        private System.Windows.Forms.TextBox txtN61;
        private System.Windows.Forms.Button btnStart61;
        private System.Windows.Forms.PictureBox picChart61;
        private System.Windows.Forms.Label lblResult61;

        private System.Windows.Forms.GroupBox grpInput62;
        private System.Windows.Forms.Label lblMean, lblVariance, lblN62;
        private System.Windows.Forms.TextBox txtMean, txtVariance, txtN62;
        private System.Windows.Forms.Button btnStart62;
        private System.Windows.Forms.PictureBox picChart62;
        private System.Windows.Forms.Label lblResult62;
    }
}