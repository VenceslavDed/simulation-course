using System.Drawing;
using System.Windows.Forms;

namespace HeatEquation
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private Panel panelLeft;
        private Panel panelRight;
        private GroupBox grpParams;
        private GroupBox grpTable;
        private GroupBox grpChart;

        private Label lblL, lblRho, lblC, lblLambda, lblTLeft, lblTRight, lblTInit, lblTEnd;
        private TextBox txtL, txtRho, txtC, txtLambda, txtTLeft, txtTRight, txtTInit, txtTEnd;

      /*  private Label lblA; */
        private Button btnRun;
        private ProgressBar progressBar;
        private Label lblStatus;
        private DataGridView dgvResults;
        private Panel pnlChart;

        private void InitializeComponent()
        {
            this.Text = "Метод конечных разностей — Уравнение теплопроводности";
            this.Size = new System.Drawing.Size(1200, 760);
            this.MinimumSize = new System.Drawing.Size(1000, 640);
            this.BackColor = Color.WhiteSmoke;
            this.ForeColor = Color.FromArgb(30, 30, 30);
            this.Font = new System.Drawing.Font("Segoe UI", 9f);
            this.StartPosition = FormStartPosition.CenterScreen;

            // ===== Левая панель =====
            panelLeft = new Panel
            {
                Dock = DockStyle.Left,
                Width = 285,
                BackColor = Color.FromArgb(240, 242, 245),
                Padding = new Padding(10)
            };
            panelLeft.Paint += (s, e) =>
                e.Graphics.DrawLine(new System.Drawing.Pen(Color.FromArgb(210, 210, 215), 1),
                    panelLeft.Width - 1, 0, panelLeft.Width - 1, panelLeft.Height);

            grpParams = new GroupBox
            {
                Text = "Параметры задачи",
                ForeColor = Color.FromArgb(0, 90, 160),
                Font = new System.Drawing.Font("Segoe UI", 9f, FontStyle.Bold),
                Location = new Point(10, 10),
                Size = new Size(260, 385),
                BackColor = Color.Transparent
            };

            int row = 25, rowH = 46;
            (lblL, txtL) = MakeField("Длина пластины L, м:", "0,1", grpParams, row); row += rowH;
            (lblRho, txtRho) = MakeField("Плотность ρ, кг/м³:", "7800", grpParams, row); row += rowH;
            (lblC, txtC) = MakeField("Теплоёмкость c, Дж/(кг·°C):", "500", grpParams, row); row += rowH;
            (lblLambda, txtLambda) = MakeField("Теплопроводность λ, Вт/(м·°C):", "50", grpParams, row); row += rowH;
            (lblTLeft, txtTLeft) = MakeField("T левой границы, °C:", "100", grpParams, row); row += rowH;
            (lblTRight, txtTRight) = MakeField("T правой границы, °C:", "100", grpParams, row); row += rowH;
            (lblTInit, txtTInit) = MakeField("Начальная T внутри, °C:", "0", grpParams, row); row += rowH;
            (lblTEnd, txtTEnd) = MakeField("Время моделирования, с:", "2", grpParams, row);

            grpParams.Size = new Size(260, row + 30);
            panelLeft.Controls.Add(grpParams);

       

            btnRun = new Button
            {
                Text = "▶  Запустить",
                Location = new Point(10, grpParams.Bottom + 8),
                Size = new Size(260, 42),
                BackColor = Color.FromArgb(0, 120, 212),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new System.Drawing.Font("Segoe UI", 11f, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnRun.FlatAppearance.BorderSize = 0;
            btnRun.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 100, 190);
            btnRun.Click += btnRun_Click;
            panelLeft.Controls.Add(btnRun);

            progressBar = new ProgressBar
            {
                Location = new Point(10, btnRun.Bottom + 8),
                Size = new Size(260, 14),
                Minimum = 0,
                Maximum = 100,
                Value = 0,
                Style = ProgressBarStyle.Continuous
            };
            panelLeft.Controls.Add(progressBar);

            lblStatus = new Label
            {
                Text = "Статус: ожидание",
                Location = new Point(10, progressBar.Bottom + 5),
                Size = new Size(260, 36),
                ForeColor = Color.FromArgb(90, 90, 100),
                Font = new System.Drawing.Font("Segoe UI", 8.5f)
            };
            panelLeft.Controls.Add(lblStatus);

            //  материалы
            var grpMaterials = new GroupBox
            {
                Text = "Примеры материалов",
                ForeColor = Color.FromArgb(0, 90, 160),
                Font = new System.Drawing.Font("Segoe UI", 8f, FontStyle.Bold),
                Location = new Point(10, lblStatus.Bottom + 6),
                Size = new Size(260, 120),
                BackColor = Color.Transparent
            };
            var lblMat = new Label
            {
                Text = "Сталь:     ρ=7800, c=500,  λ=50\n" +
                       "Алюминий:  ρ=2700, c=900,  λ=237\n" +
                       "Медь:      ρ=8900, c=385,  λ=401\n" +
                       "Бетон:     ρ=2300, c=880,  λ=1,7\n" +
                       "Дерево:    ρ=600,  c=1700, λ=0,15",
                Location = new Point(8, 18),
                Size = new Size(245, 94),
                ForeColor = Color.FromArgb(60, 60, 80),
                Font = new System.Drawing.Font("Consolas", 7.8f)
            };
            grpMaterials.Controls.Add(lblMat);
            panelLeft.Controls.Add(grpMaterials);

            // ===== Правая панель =====
            panelRight = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(10)
            };

            var lblTitle = new Label
            {
                Text = "Метод конечных разностей для уравнения теплопроводности",
                Font = new System.Drawing.Font("Segoe UI", 12f, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 90, 160),
                Location = new Point(10, 8),
                Size = new Size(880, 28),
                AutoSize = false
            };
            panelRight.Controls.Add(lblTitle);

        

            grpTable = new GroupBox
            {
                Text = "Температура в центре пластины после 2 с (°C)",
                ForeColor = Color.FromArgb(60, 60, 70),
                Font = new System.Drawing.Font("Segoe UI", 9f, FontStyle.Bold),
                Location = new Point(10, 58),
                Size = new Size(880, 175),
                BackColor = Color.Transparent
            };

            dgvResults = new DataGridView
            {
                Location = new Point(10, 22),
                Size = new Size(855, 140),
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                EnableHeadersVisualStyles = false,
                GridColor = Color.FromArgb(210, 215, 220),
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.White,
                    ForeColor = Color.FromArgb(30, 30, 30),
                    SelectionBackColor = Color.FromArgb(200, 220, 245),
                    SelectionForeColor = Color.FromArgb(20, 20, 20),
                    Font = new System.Drawing.Font("Consolas", 9f)
                },
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(0, 100, 180),
                    ForeColor = Color.White,
                    Font = new System.Drawing.Font("Segoe UI", 9f, FontStyle.Bold),
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                },
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(245, 248, 252)
                },
                ColumnHeadersHeight = 32,
                RowTemplate = { Height = 26 }
            };
            grpTable.Controls.Add(dgvResults);
            panelRight.Controls.Add(grpTable);

            grpChart = new GroupBox
            {
                Text = "Распределение температуры по толщине пластины",
                ForeColor = Color.FromArgb(60, 60, 70),
                Font = new System.Drawing.Font("Segoe UI", 9f, FontStyle.Bold),
                Location = new Point(10, 243),
                Size = new Size(880, 450),
                BackColor = Color.Transparent
            };

            pnlChart = new Panel
            {
                Location = new Point(10, 22),
                Size = new Size(855, 415),
                BackColor = Color.White
            };
            pnlChart.Paint += pnlChart_Paint;

            grpChart.Controls.Add(pnlChart);
            panelRight.Controls.Add(grpChart);

            this.Controls.Add(panelRight);
            this.Controls.Add(panelLeft);
        }

        private (Label lbl, TextBox txt) MakeField(string labelText, string defaultVal, Control parent, int y)
        {
            var lbl = new Label
            {
                Text = labelText,
                Location = new Point(8, y),
                Size = new Size(244, 16),
                ForeColor = Color.FromArgb(70, 70, 80),
                Font = new System.Drawing.Font("Segoe UI", 8f)
            };
            var txt = new TextBox
            {
                Text = defaultVal,
                Location = new Point(8, y + 18),
                Size = new Size(244, 22),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(20, 20, 20),
                BorderStyle = BorderStyle.FixedSingle,
                Font = new System.Drawing.Font("Consolas", 9f)
            };
            parent.Controls.Add(lbl);
            parent.Controls.Add(txt);
            return (lbl, txt);
        }
    }
}