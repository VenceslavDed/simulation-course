using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace StochasticModeling
{
    public partial class Form1 : Form
    {
        private readonly Random _rng = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        //  ЛАБ. 6.1  

        private void btnStart61_Click(object sender, EventArgs e)
        {
            // Считываем вероятности из полей ввода
            double[] probInputs = new double[5];
            TextBox[] probBoxes = { txtP1, txtP2, txtP3, txtP4, txtP5 };

            int filledCount = 0;
            double sumFilled = 0;
            int autoIndex = -1;

            for (int i = 0; i < 5; i++)
            {
                string text = probBoxes[i].Text.Trim().Replace(',', '.');
                if (string.IsNullOrEmpty(text) || text.ToLower() == "auto")
                {
                    autoIndex = i;
                }
                else
                {
                    if (!double.TryParse(text, System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture, out probInputs[i])
                        || probInputs[i] < 0 || probInputs[i] > 1)
                    {
                        MessageBox.Show($"Prob {i + 1}: введите число от 0 до 1.", "Ошибка");
                        return;
                    }
                    sumFilled += probInputs[i];
                    filledCount++;
                }
            }

            if (autoIndex >= 0)
            {
                double autoVal = 1.0 - sumFilled;
                if (autoVal < 0 || autoVal > 1)
                {
                    MessageBox.Show("Сумма вероятностей превышает 1. Проверьте значения.", "Ошибка");
                    return;
                }
                probInputs[autoIndex] = autoVal;
            }
            else
            {
                if (Math.Abs(probInputs.Sum() - 1.0) > 1e-6)
                {
                    MessageBox.Show("Сумма вероятностей должна быть равна 1.", "Ошибка");
                    return;
                }
            }

            if (!int.TryParse(txtN61.Text.Trim(), out int N) || N <= 0)
            {
                MessageBox.Show("Введите корректное число экспериментов.", "Ошибка");
                return;
            }

            // Генерация выборки методом обратной функции (инверсный метод)
            int m = probInputs.Length;
            double[] theorMean = new double[1];
            double[] theorVar = new double[1];

            // Теоретическое среднее и дисперсия (значения 1..5)
            double mu = 0, sigma2 = 0;
            for (int i = 0; i < m; i++) { mu += (i + 1) * probInputs[i]; }
            for (int i = 0; i < m; i++) { sigma2 += probInputs[i] * Math.Pow(i + 1 - mu, 2); }

            int[] counts = new int[m];
            for (int n = 0; n < N; n++)
            {
                double u = _rng.NextDouble();
                double cumul = 0;
                for (int i = 0; i < m; i++)
                {
                    cumul += probInputs[i];
                    if (u < cumul) { counts[i]++; break; }
                }
            }

            double[] empProb = counts.Select(c => (double)c / N).ToArray();
            double empMean = 0, empVar = 0;
            for (int i = 0; i < m; i++) { empMean += (i + 1) * empProb[i]; }
            for (int i = 0; i < m; i++) { empVar += empProb[i] * Math.Pow(i + 1 - empMean, 2); }

            double errMean = mu != 0 ? Math.Abs(empMean - mu) / mu * 100 : 0;
            double errVar = sigma2 != 0 ? Math.Abs(empVar - sigma2) / sigma2 * 100 : 0;

            // Вычисляем статистику хи-квадрат
            double chi2 = 0;
            for (int i = 0; i < m; i++)
            {
                double expected = N * probInputs[i];
                if (expected > 0)
                    chi2 += Math.Pow(counts[i] - expected, 2) / expected;
            }

            // Критическое значение при df = m-1 = 4, уровень значимости α = 0.05  →  9.488
            int df = m - 1;
            double chiCrit = ChiSquaredCritical(df, 0.05);
            bool reject = chi2 > chiCrit;

            lblResult61.Text =
                $"Average: {empMean:F3} (error = {errMean:F0}%)\r\n" +
                $"Variance: {empVar:F3} (error = {errVar:F0}%)\r\n\r\n" +
                $"Chi-squared: {chi2:F3} > {chiCrit:F3}   is {(reject ? "true" : "false")}";
            lblResult61.ForeColor = reject ? Color.DarkRed : Color.DarkGreen;

            // Рисуем гистограмму
            DrawHistogram61(picChart61, empProb, probInputs);
        }

        private void DrawHistogram61(PictureBox pic, double[] emp, double[] theory)
        {
            int w = pic.Width, h = pic.Height;
            Bitmap bmp = new Bitmap(w, h);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);

                int padL = 40, padR = 15, padT = 20, padB = 35;
                int chartW = w - padL - padR;
                int chartH = h - padT - padB;
                int m = emp.Length;
                double maxVal = Math.Max(emp.Max(), theory.Max()) * 1.2;

                // Оси координат
                Pen axisPen = new Pen(Color.Black, 1.5f);
                g.DrawLine(axisPen, padL, padT, padL, padT + chartH);
                g.DrawLine(axisPen, padL, padT + chartH, padL + chartW, padT + chartH);

                Font fnt = new Font("Arial", 7f);
                Brush txtBrush = Brushes.Black;

                // Подписи по оси Y и горизонтальная сетка
                for (int i = 0; i <= 4; i++)
                {
                    double val = maxVal * i / 4;
                    int y = padT + chartH - (int)(chartH * val / maxVal);
                    g.DrawLine(Pens.LightGray, padL + 1, y, padL + chartW, y);
                    g.DrawString(val.ToString("F2"), fnt, txtBrush, 0, y - 6);
                }

                // Столбцы гистограммы
                float barW = (float)chartW / (m * 2.5f);
                float gap = barW * 0.3f;
                float groupW = (float)chartW / m;

                for (int i = 0; i < m; i++)
                {
                    float cx = padL + groupW * i + groupW / 2f;

                    // Эмпирический столбец (синий)
                    float bh = (float)(chartH * emp[i] / maxVal);
                    RectangleF rect = new RectangleF(cx - barW - gap / 2, padT + chartH - bh, barW, bh);
                    g.FillRectangle(new SolidBrush(Color.FromArgb(180, 70, 130, 180)), rect);
                    g.DrawRectangle(Pens.SteelBlue, rect.X, rect.Y, rect.Width, rect.Height);

                    // Теоретический столбец (оранжевый)
                    float th = (float)(chartH * theory[i] / maxVal);
                    RectangleF rect2 = new RectangleF(cx + gap / 2, padT + chartH - th, barW, th);
                    g.FillRectangle(new SolidBrush(Color.FromArgb(180, 210, 140, 50)), rect2);
                    g.DrawRectangle(Pens.DarkOrange, rect2.X, rect2.Y, rect2.Width, rect2.Height);

                    // Подпись по оси X
                    g.DrawString((i + 1).ToString(), fnt, txtBrush, cx - 4, padT + chartH + 4);

                    // Подписи значений над столбцами
                    g.DrawString(emp[i].ToString("F3"), new Font("Arial", 6f), Brushes.SteelBlue,
                        rect.X, rect.Y - 11);
                    g.DrawString(theory[i].ToString("F3"), new Font("Arial", 6f), Brushes.DarkOrange,
                        rect2.X, rect2.Y - 11);
                }

                g.FillRectangle(new SolidBrush(Color.FromArgb(180, 70, 130, 180)), padL + 2, padT, 12, 8);
                g.DrawString("эмпир.", fnt, Brushes.SteelBlue, padL + 16, padT - 1);
                g.FillRectangle(new SolidBrush(Color.FromArgb(180, 210, 140, 50)), padL + 65, padT, 12, 8);
                g.DrawString("теор.", fnt, Brushes.DarkOrange, padL + 79, padT - 1);

                axisPen.Dispose();
            }
            pic.Image?.Dispose();
            pic.Image = bmp;
        }


        //  ЛАБ 6.2 

        private void btnStart62_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(txtMean.Text.Trim().Replace(',', '.'), System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out double mean))
            {
                MessageBox.Show("Введите корректное среднее.", "Ошибка"); return;
            }
            if (!double.TryParse(txtVariance.Text.Trim().Replace(',', '.'), System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out double variance) || variance <= 0)
            {
                MessageBox.Show("Введите корректную дисперсию (> 0).", "Ошибка"); return;
            }
            if (!int.TryParse(txtN62.Text.Trim(), out int N) || N <= 0)
            {
                MessageBox.Show("Введите корректное число экспериментов.", "Ошибка"); return;
            }

            double sigma = Math.Sqrt(variance);

            // Генерируем выборку методом Бокса–Мюллера
            double[] samples = GenerateNormal(N, mean, sigma);

            double empMean = samples.Average();
            double empVar = samples.Select(x => Math.Pow(x - empMean, 2)).Average();

            double errMean = mean != 0 ? Math.Abs(empMean - mean) / Math.Abs(mean) * 100 : 0;
            double errVar = Math.Abs(empVar - variance) / variance * 100;

            // 7 интервалов гистограммы, центрированных на mean ± 3σ
            int bins = 7;
            double lo = mean - 3.5 * sigma;
            double hi = mean + 3.5 * sigma;
            double binW = (hi - lo) / bins;

            int[] binCounts = new int[bins];
            foreach (double s in samples)
            {
                int idx = (int)((s - lo) / binW);
                if (idx < 0) idx = 0;
                if (idx >= bins) idx = bins - 1;
                binCounts[idx]++;
            }

            double[] empFreq = binCounts.Select(c => (double)c / (N * binW)).ToArray();

            // Теоретические частоты — значение плотности нормального распределения в центре каждого интервала
            double[] theorFreq = new double[bins];
            for (int i = 0; i < bins; i++)
            {
                double x = lo + (i + 0.5) * binW;
                theorFreq[i] = NormalPDF(x, mean, sigma);
            }

            // Вычисляем статистику хи-квадрат
            double chi2 = 0;
            for (int i = 0; i < bins; i++)
            {
                double exp = N * binW * theorFreq[i];
                if (exp > 0)
                    chi2 += Math.Pow(binCounts[i] - exp, 2) / exp;
            }
            int df = bins - 3; // оцениваем среднее и дисперсию, поэтому вычитаем 3
            double chiCrit = ChiSquaredCritical(df, 0.05);
            bool reject = chi2 > chiCrit;

            lblResult62.Text =
                $"Average: {empMean:F3} (error = {errMean:F0}%)\r\n" +
                $"Variance: {empVar:F3} (error = {errVar:F0}%)\r\n\r\n" +
                $"Chi-squared: {chi2:F3} > {chiCrit:F3}   is {(reject ? "true" : "false")}";
            lblResult62.ForeColor = reject ? Color.DarkRed : Color.DarkGreen;

            DrawHistogram62(picChart62, bins, lo, binW, empFreq, theorFreq, mean, sigma);
        }

        private double[] GenerateNormal(int N, double mean, double sigma)
        {
            double[] samples = new double[N];
            for (int i = 0; i < N; i += 2)
            {
                double u1 = _rng.NextDouble();
                double u2 = _rng.NextDouble();
                double z0 = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2 * Math.PI * u2);
                double z1 = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2 * Math.PI * u2);
                samples[i] = mean + sigma * z0;
                if (i + 1 < N) samples[i + 1] = mean + sigma * z1;
            }
            return samples;
        }

        private void DrawHistogram62(PictureBox pic, int bins, double lo, double binW,
            double[] empFreq, double[] theorFreq, double mean, double sigma)
        {
            int w = pic.Width, h = pic.Height;
            Bitmap bmp = new Bitmap(w, h);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.Clear(Color.White);

                int padL = 45, padR = 15, padT = 20, padB = 40;
                int chartW = w - padL - padR;
                int chartH = h - padT - padB;

                double maxVal = Math.Max(empFreq.Max(), theorFreq.Max()) * 1.25;

                // Оси координат
                Pen axisPen = new Pen(Color.Black, 1.5f);
                g.DrawLine(axisPen, padL, padT, padL, padT + chartH);
                g.DrawLine(axisPen, padL, padT + chartH, padL + chartW, padT + chartH);

                Font fnt = new Font("Arial", 7f);
                Font fntSmall = new Font("Arial", 6f);
                Brush txtBrush = Brushes.Black;

                // Подписи по оси Y и горизонтальная сетка
                for (int i = 0; i <= 4; i++)
                {
                    double val = maxVal * i / 4;
                    int y = padT + chartH - (int)(chartH * val / maxVal);
                    g.DrawLine(Pens.LightGray, padL + 1, y, padL + chartW, y);
                    g.DrawString(val.ToString("F2"), fntSmall, txtBrush, 0, y - 6);
                }

                float bw = (float)chartW / bins;

                // Столбцы гистограммы
                for (int i = 0; i < bins; i++)
                {
                    float x = padL + i * bw;
                    float bh = (float)(chartH * empFreq[i] / maxVal);
                    RectangleF rect = new RectangleF(x + 1, padT + chartH - bh, bw - 2, bh);
                    g.FillRectangle(new SolidBrush(Color.FromArgb(160, 70, 130, 180)), rect);
                    g.DrawRectangle(new Pen(Color.SteelBlue), rect.X, rect.Y, rect.Width, rect.Height);

                    // Подпись по оси X (левая граница интервала)
                    double binCenter = lo + (i + 0.5) * binW;
                    g.DrawString($"{lo + i * binW:F1}", fntSmall, txtBrush, x - 2, padT + chartH + 3);
                }
                // Подпись правой границы последнего интервала
                g.DrawString($"{lo + bins * binW:F1}", fntSmall, txtBrush,
                    padL + bins * bw - 10, padT + chartH + 3);

                // Кривая нормального распределения
                using (Pen curvePen = new Pen(Color.DarkGreen, 2f))
                {
                    List<PointF> pts = new List<PointF>();
                    for (int px = 0; px <= chartW; px++)
                    {
                        double x = lo + (double)px / chartW * (bins * binW);
                        double pdf = NormalPDF(x, mean, sigma);
                        float py = padT + chartH - (float)(chartH * pdf / maxVal);
                        pts.Add(new PointF(padL + px, py));
                    }
                    g.DrawLines(curvePen, pts.ToArray());
                }

                axisPen.Dispose();
            }
            pic.Image?.Dispose();
            pic.Image = bmp;
        }


        //  Вспомогательные математические функции

        private static double NormalPDF(double x, double mean, double sigma)
        {
            return Math.Exp(-0.5 * Math.Pow((x - mean) / sigma, 2)) / (sigma * Math.Sqrt(2 * Math.PI));
        }


        /// Критическое значение χ² по аппроксимации Вилсона–Хильферти.

        private static double ChiSquaredCritical(int df, double alpha)
        {
            // Точные табличные значения для распространённых степеней свободы
            double[,] table = {
                { 1, 3.841 }, { 2, 5.991 }, { 3, 7.815 }, { 4, 9.488 },
                { 5, 11.070 }, { 6, 12.592 }, { 7, 14.067 }, { 8, 15.507 },
                { 9, 16.919 }, { 10, 18.307 }, { 11, 19.675 }, { 12, 21.026 },
                { 15, 24.996 }, { 20, 31.410 }, { 25, 37.652 }, { 30, 43.773 }
            };
            for (int i = 0; i < table.GetLength(0); i++)
                if ((int)table[i, 0] == df) return table[i, 1];

            // Аппроксимация Вилсона–Хильферти для значений не из таблицы
            double zp = 1.6449; // квантиль z для alpha = 0.05
            double x = df * Math.Pow(1 - 2.0 / (9 * df) + zp * Math.Sqrt(2.0 / (9 * df)), 3);
            return x;
        }
    }
}