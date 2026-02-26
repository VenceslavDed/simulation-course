using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeatEquation
{
    public partial class Form1 : Form
    {
        private double L = 0.1;    // длина пластины, м
        private double rho = 7800.0; // плотность, кг/м³ (сталь)
        private double c = 500.0;  // удельная теплоёмкость, Дж/(кг·°C)
        private double lambda = 50.0;   // теплопроводность, Вт/(м·°C)
        private double T_left = 100.0; // температура левой границы, °C
        private double T_right = 100.0; // температура правой границы, °C
        private double T_init = 0.0;   // начальная температура, °C
        private double t_end = 2.0;   // время моделирования, с

        private bool isRunning = false;

        private List<(string label, double[] T, double dx, Color color)> chartSeries
            = new List<(string, double[], double, Color)>();

        public Form1()
        {
            InitializeComponent();
            InitializeDataGridView();
        }

        private void InitializeDataGridView()
        {
            dgvResults.ColumnCount = 5;
            dgvResults.Columns[0].HeaderText = "dx \\ dt";
            dgvResults.Columns[1].HeaderText = "dt = 0.1";
            dgvResults.Columns[2].HeaderText = "dt = 0.01";
            dgvResults.Columns[3].HeaderText = "dt = 0.001";
            dgvResults.Columns[4].HeaderText = "dt = 0.0001";

            foreach (DataGridViewColumn col in dgvResults.Columns)
            {
                col.Width = 110;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            dgvResults.Columns[0].Width = 100;

            string[] rowHeaders = { "dx = 0.1", "dx = 0.01", "dx = 0.001", "dx = 0.0001" };
            foreach (var h in rowHeaders)
            {
                int idx = dgvResults.Rows.Add();
                dgvResults.Rows[idx].Cells[0].Value = h;
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (isRunning) return;
            RunSimulation();
        }

        private async void RunSimulation()
        {
            isRunning = true;
            btnRun.Enabled = false;
            btnRun.Text = "Вычисление...";
            progressBar.Value = 0;
            lblStatus.Text = "Статус: Запуск...";
            lblStatus.ForeColor = Color.FromArgb(90, 90, 100);
            chartSeries.Clear();
            pnlChart.Invalidate();

            try
            {
                var ci = CultureInfo.InvariantCulture;
                L = double.Parse(txtL.Text.Replace(',', '.'), ci);
                rho = double.Parse(txtRho.Text.Replace(',', '.'), ci);
                c = double.Parse(txtC.Text.Replace(',', '.'), ci);
                lambda = double.Parse(txtLambda.Text.Replace(',', '.'), ci);
                T_left = double.Parse(txtTLeft.Text.Replace(',', '.'), ci);
                T_right = double.Parse(txtTRight.Text.Replace(',', '.'), ci);
                T_init = double.Parse(txtTInit.Text.Replace(',', '.'), ci);
                t_end = double.Parse(txtTEnd.Text.Replace(',', '.'), ci);

                if (rho <= 0 || c <= 0 || lambda <= 0 || L <= 0 || t_end <= 0)
                    throw new Exception("Параметры должны быть положительными.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка ввода: " + ex.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRun.Enabled = true;
                btnRun.Text = "▶  Запустить";
                isRunning = false;
                return;
            }

            // a = lambda / (rho * c)
            double a = lambda / (rho * c);
            lblA.Text = $"a = λ/(ρ·c) = {a:G4} м²/с";

            for (int i = 0; i < 4; i++)
                for (int j = 1; j <= 4; j++)
                {
                    dgvResults.Rows[i].Cells[j].Value = "";
                    dgvResults.Rows[i].Cells[j].Style.ForeColor = Color.Black;
                }

            double[] dxValues = { 0.1, 0.01, 0.001, 0.0001 };
            double[] dtValues = { 0.1, 0.01, 0.001, 0.0001 };

            Color[] lineColors = {
                Color.FromArgb(  0, 114, 189),
                Color.FromArgb(217,  83,  25),
                Color.FromArgb(237, 177,  32),
                Color.FromArgb(126,  47, 142),
                Color.FromArgb( 77, 190,  80),
                Color.FromArgb( 77, 196, 214),
                Color.FromArgb(162,  20,  47),
                Color.FromArgb(  0, 158, 115),
                Color.FromArgb(204, 121,  16),
                Color.FromArgb( 86, 180, 233),
                Color.FromArgb(230, 159,   0),
                Color.FromArgb(  0, 114,  54),
                Color.FromArgb(213,  94,   0),
                Color.FromArgb(  0,  88, 163),
                Color.FromArgb(204,  51, 102),
                Color.FromArgb( 51, 153, 102),
            };
            int colorIdx = 0;
            int done = 0;

            await Task.Run(() =>
            {
                for (int idx = 0; idx < dxValues.Length; idx++)
                {
                    double dx = dxValues[idx];
                    for (int jdt = 0; jdt < dtValues.Length; jdt++)
                    {
                        double dt = dtValues[jdt];
                        // r = a * dt / dx²  — число Куранта
                        double r = a * dt / (dx * dx);
                        int iCopy = idx, jCopy = jdt;

                        if (r > 0.5)
                        {
                            this.Invoke((Action)(() =>
                            {
                                dgvResults.Rows[iCopy].Cells[jCopy + 1].Value = "неуст.";
                                dgvResults.Rows[iCopy].Cells[jCopy + 1].Style.ForeColor = Color.FromArgb(200, 50, 30);
                            }));
                        }
                        else
                        {
                            var (centerTemp, Tfinal, N) = Simulate(dx, dt, r);

                            var col = lineColors[colorIdx % lineColors.Length];
                            colorIdx++;
                            string lbl = $"dx={dx}, dt={dt}";
                            double[] copy = (double[])Tfinal.Clone();
                            this.Invoke((Action)(() =>
                            {
                                chartSeries.Add((lbl, copy, dx, col));
                                pnlChart.Invalidate();
                            }));

                            this.Invoke((Action)(() =>
                            {
                                dgvResults.Rows[iCopy].Cells[jCopy + 1].Value = $"{centerTemp:F4}";
                                dgvResults.Rows[iCopy].Cells[jCopy + 1].Style.ForeColor = Color.FromArgb(0, 130, 60);
                            }));
                        }

                        done++;
                        this.Invoke((Action)(() =>
                        {
                            progressBar.Value = (int)(done * 100.0 / 16);
                            lblStatus.Text = $"Статус: dx={dx}, dt={dt} — готово";
                        }));
                    }
                }
            });

            lblStatus.Text = "Статус: Завершено!";
            lblStatus.ForeColor = Color.FromArgb(0, 130, 60);
            pnlChart.Invalidate();
            btnRun.Enabled = true;
            btnRun.Text = "▶  Запустить";
            isRunning = false;
        }

        /// <summary>
        /// Явная схема МКР:
        /// ρ·c·(T[i,n+1] - T[i,n])/dt = λ·(T[i+1,n] - 2·T[i,n] + T[i-1,n])/dx²
        /// Что эквивалентно:
        /// T[i,n+1] = T[i,n] + r·(T[i+1,n] - 2·T[i,n] + T[i-1,n])
        /// где r = a·dt/dx², a = λ/(ρ·c)
        /// </summary>
        private (double centerTemp, double[] Tfinal, int N) Simulate(double dx, double dt, double r)
        {
            int N = (int)Math.Round(L / dx) + 1;
            double[] T = new double[N];
            double[] Tnew = new double[N];

            for (int i = 0; i < N; i++) T[i] = T_init;
            T[0] = T_left;
            T[N - 1] = T_right;

            int steps = (int)Math.Round(t_end / dt);
            for (int n = 0; n < steps; n++)
            {
                Tnew[0] = T_left;
                Tnew[N - 1] = T_right;

                // ρ·c·(T_new - T_old)/dt = λ·(T[i+1] - 2T[i] + T[i-1])/dx²
                for (int i = 1; i < N - 1; i++)
                    Tnew[i] = T[i] + r * (T[i + 1] - 2.0 * T[i] + T[i - 1]);

                Array.Copy(Tnew, T, N);
            }

            int center = N / 2;
            return (T[center], T, N);
        }

        private void pnlChart_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int W = pnlChart.Width;
            int H = pnlChart.Height;
            int padL = 60, padR = 20, padT = 15, padB = 45;
            int plotW = W - padL - padR;
            int plotH = H - padT - padB;

            g.FillRectangle(Brushes.White, 0, 0, W, H);
            g.FillRectangle(new SolidBrush(Color.FromArgb(248, 250, 253)), padL, padT, plotW, plotH);

            var penGrid = new Pen(Color.FromArgb(220, 225, 235), 1);
            var penAxis = new Pen(Color.FromArgb(160, 170, 185), 1);
            var fntSmall = new Font("Segoe UI", 7.5f);
            var fntBold = new Font("Segoe UI", 8.5f, FontStyle.Bold);
            var brushTxt = new SolidBrush(Color.FromArgb(60, 60, 75));

            int gridX = 10, gridY = 8;
            for (int i = 0; i <= gridX; i++)
            {
                int x = padL + i * plotW / gridX;
                g.DrawLine(penGrid, x, padT, x, padT + plotH);
                double xVal = i * L / gridX;
                g.DrawString(xVal.ToString("F3"), fntSmall, brushTxt, x - 12, padT + plotH + 5);
            }
            for (int i = 0; i <= gridY; i++)
                g.DrawLine(penGrid, padL, padT + i * plotH / gridY, padL + plotW, padT + i * plotH / gridY);

            g.DrawString("x, м", fntBold, brushTxt, padL + plotW / 2 - 15, padT + plotH + 26);

            var sf = new StringFormat { Alignment = StringAlignment.Center };
            var st = g.Save();
            g.TranslateTransform(12, padT + plotH / 2);
            g.RotateTransform(-90);
            g.DrawString("T, °C", fntBold, brushTxt, 0, 0, sf);
            g.Restore(st);

            if (chartSeries.Count == 0)
            {
                g.DrawString("Нажмите «Запустить» для отображения графика",
                    new Font("Segoe UI", 10f), Brushes.Silver, padL + 20, padT + plotH / 2 - 10);
                g.DrawRectangle(penAxis, padL, padT, plotW, plotH);
                return;
            }

            double yMin = double.MaxValue, yMax = double.MinValue;
            foreach (var (_, T, dx, _) in chartSeries)
                foreach (var v in T) { if (v < yMin) yMin = v; if (v > yMax) yMax = v; }

            yMin = Math.Min(yMin, T_init) - 5;
            yMax = Math.Max(yMax, Math.Max(T_left, T_right)) + 5;
            double yRange = yMax - yMin;

            for (int i = 0; i <= gridY; i++)
            {
                int y = padT + i * plotH / gridY;
                double val = yMax - i * yRange / gridY;
                g.DrawString(val.ToString("F0"), fntSmall, brushTxt, 2, y - 7);
            }

            foreach (var (label, T, dx, color) in chartSeries)
            {
                int N = T.Length;
                var pts = new PointF[N];
                for (int i = 0; i < N; i++)
                {
                    float px = padL + (float)(i * dx / L * plotW);
                    float py = padT + (float)((yMax - T[i]) / yRange * plotH);
                    pts[i] = new PointF(px, py);
                }
                if (pts.Length > 1)
                    g.DrawLines(new Pen(color, 2.5f), pts);
            }

            int itemH = 16;
            int cols = chartSeries.Count > 8 ? 2 : 1;
            int rows = (chartSeries.Count + cols - 1) / cols;
            int colW = 145;
            int legW = colW * cols + 8;
            int legH = rows * itemH + 8;
            int legX = padL + 10, legY0 = padT + 8;

            g.FillRectangle(new SolidBrush(Color.FromArgb(245, 247, 252)), legX - 4, legY0 - 4, legW, legH);
            g.DrawRectangle(new Pen(Color.FromArgb(190, 200, 220)), legX - 4, legY0 - 4, legW, legH);

            for (int ci = 0; ci < chartSeries.Count; ci++)
            {
                var (label, _, _, color) = chartSeries[ci];
                int col = ci / rows;
                int row2 = ci % rows;
                int ix = legX + col * colW;
                int iy = legY0 + row2 * itemH;
                g.FillRectangle(new SolidBrush(color), ix, iy + 5, 16, 3);
                g.DrawString(label, fntSmall, new SolidBrush(color), ix + 20, iy);
            }

            g.DrawRectangle(penAxis, padL, padT, plotW, plotH);
        }
    }
}
