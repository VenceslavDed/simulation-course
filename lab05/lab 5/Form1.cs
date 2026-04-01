using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace lab_5
{
    public partial class Form1 : Form
    {
        
        private long _sensorX;
        private const long A = 16807L;
        private const long M = 2147483647L;

        private void SeedSensor()
        {
            long seed = DateTime.Now.Ticks % M;
            _sensorX = seed == 0 ? 1 : seed;
        }

        // Возвращает α ∈ [0; 1)
        private double SensorNext()
        {
            _sensorX = (A * _sensorX) % M;
            return (double)_sensorX / M;
        }

        
        private bool GenerateEvent(double p)
        {
            return SensorNext() < p;
        }

        
        private int GenerateGroupEvent(double[] probs)
        {
            double alpha = SensorNext();
            double cumulative = 0.0;
            for (int k = 0; k < probs.Length - 1; k++)
            {
                cumulative += probs[k];
                if (alpha < cumulative)
                    return k;
            }
            return probs.Length - 1;
        }


        //  ДАННЫЕ — 8 ответов о погоде
        private static readonly string[] BallTexts =
        {
            "Будет ясно и солнечно!",        // p = 0.15  позитивный
            "Отличная погода",               // p = 0.15  позитивный
            "Возможен лёгкий ветерок",       // p = 0.10  нейтральный
            "Облачно, но без осадков",       // p = 0.10  нейтральный
            "Спроси позже — небо меняется",  // p = 0.10  нейтральный
            "Возьми зонт на всякий случай",  // p = 0.15  негативный
            "Дождь весь день",               // p = 0.15  негативный
            "Гроза! Лучше не выходить",      // p = 0.10  негативный
        };

        private static readonly double[] BallProbs =
        {
            0.15, 0.15,          // позитивные
            0.10, 0.10, 0.10,    // нейтральные
            0.15, 0.15, 0.10     // негативные
        };

        // Цвета ответов
        private static readonly Color[] BallColors =
        {
            Color.FromArgb(0, 140, 0),
            Color.FromArgb(0, 140, 0),
            Color.FromArgb(160, 110, 0),
            Color.FromArgb(160, 110, 0),
            Color.FromArgb(160, 110, 0),
            Color.FromArgb(180, 0, 0),
            Color.FromArgb(180, 0, 0),
            Color.FromArgb(180, 0, 0),
        };

        // ── Статистика ──
        private int _total1, _yes1;
        private int[] _stats2 = new int[8];
        private int _lastBallIndex = -1;

        
        public Form1()
        {
            InitializeComponent();
            SeedSensor();
            FillBallProbsLabel();
            UpdateStats1();
        }

        private void FillBallProbsLabel()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < BallTexts.Length; i++)
                sb.AppendLine(string.Format("p{0} = {1:F2}  →  {2}", i + 1, BallProbs[i], BallTexts[i]));
            lblBallProbs.Text = sb.ToString();
        }

        //  ЧАСТЬ 1 — «Да или нет»
        private void btnAsk1_Click(object sender, EventArgs e)
        {
            double p = (double)numProbability.Value / 100.0;
            bool yes = GenerateEvent(p);

            _total1++;
            if (yes) _yes1++;

            lblAnswer1.Text = yes ? "ДА!" : "НЕТ!";
            lblAnswer1.ForeColor = yes ? Color.FromArgb(0, 140, 0) : Color.FromArgb(180, 0, 0);

            UpdateStats1();
        }

        private void btnReset1_Click(object sender, EventArgs e)
        {
            _total1 = _yes1 = 0;
            lblAnswer1.Text = "...";
            lblAnswer1.ForeColor = Color.FromArgb(64, 64, 64);
            UpdateStats1();
        }

        private void numProbability_ValueChanged(object sender, EventArgs e)
        {
            lblProbValue.Text = string.Format("p = {0}%", numProbability.Value);
            UpdateStats1();
        }

        private void UpdateStats1()
        {
            if (_total1 == 0)
            {
                lblStats1.Text = "Статистика: ещё нет данных";
                return;
            }
            double freq = (double)_yes1 / _total1;
            double p = (double)numProbability.Value / 100.0;
            lblStats1.Text = string.Format(
                "Попыток: {0}   «Да»: {1}   Эмпирическая вероятность: {2:F3}   (задано p = {3:F2})",
                _total1, _yes1, freq, p);
        }

        //  ЧАСТЬ 2 — Magic 8-Ball (погода)
        private void btnAsk2_Click(object sender, EventArgs e)
        {
            int k = GenerateGroupEvent(BallProbs);
            _stats2[k]++;
            _lastBallIndex = k;

            lblBallAnswer.Text = BallTexts[k];
            lblBallAnswer.ForeColor = BallColors[k];

            // Перерисовать шар с новым ответом
            panelBall.Invalidate();

            UpdateStats2();
        }

        private void btnReset2_Click(object sender, EventArgs e)
        {
            _stats2 = new int[BallTexts.Length];
            _lastBallIndex = -1;
            lblBallAnswer.Text = "Задайте вопрос о погоде...";
            lblBallAnswer.ForeColor = Color.FromArgb(64, 64, 64);
            lblStats2.Text = "Статистика: ещё нет данных";
            panelBall.Invalidate();
        }

        private void UpdateStats2()
        {
            int total = 0;
            foreach (int c in _stats2) total += c;
            if (total == 0) return;

            var sb = new StringBuilder(string.Format("Попыток: {0}\r\n", total));
            for (int i = 0; i < BallTexts.Length; i++)
            {
                double emp = (double)_stats2[i] / total;
                sb.AppendLine(string.Format(
                    "  [{0:F2} → {1:F3}]  {2}  (n={3})",
                    BallProbs[i], emp, BallTexts[i], _stats2[i]));
            }
            lblStats2.Text = sb.ToString();
        }

        
        //  РИСОВАНИЕ ШАРА
        private void panelBall_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            int w = panelBall.Width;
            int h = panelBall.Height;
            int diameter = Math.Min(w, h) - 10;
            int cx = w / 2;
            int cy = h / 2;
            int r = diameter / 2;
            Rectangle ballRect = new Rectangle(cx - r, cy - r, diameter, diameter);

            
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(ballRect);

                using (PathGradientBrush pgb = new PathGradientBrush(path))
                {
                    pgb.CenterColor = Color.FromArgb(60, 60, 130);
                    pgb.SurroundColors = new Color[] { Color.FromArgb(5, 5, 30) };
                    pgb.CenterPoint = new PointF(cx - r * 0.2f, cy - r * 0.2f);
                    g.FillEllipse(pgb, ballRect);
                }
            }

            
            int blikW = (int)(diameter * 0.38);
            int blikH = (int)(diameter * 0.22);
            int blikX = cx - r + (int)(diameter * 0.18);
            int blikY = cy - r + (int)(diameter * 0.10);
            using (GraphicsPath blikPath = new GraphicsPath())
            {
                blikPath.AddEllipse(blikX, blikY, blikW, blikH);
                using (PathGradientBrush blikBrush = new PathGradientBrush(blikPath))
                {
                    blikBrush.CenterColor = Color.FromArgb(200, 255, 255, 255);
                    blikBrush.SurroundColors = new Color[] { Color.FromArgb(0, 255, 255, 255) };
                    g.FillEllipse(blikBrush, blikX, blikY, blikW, blikH);
                }
            }

            // Внутренний синий круг (иконка «8» или ответ)
            int innerR = (int)(r * 0.52);
            Rectangle innerRect = new Rectangle(cx - innerR, cy - innerR + (int)(r * 0.05),
                                                innerR * 2, innerR * 2);
            using (SolidBrush ib = new SolidBrush(Color.FromArgb(200, 10, 10, 60)))
                g.FillEllipse(ib, innerRect);

            // Текст внутри шара
            string innerText = _lastBallIndex < 0 ? "8" : "?";
            using (Font f = new Font("Segoe UI", r * 0.38f, FontStyle.Bold, GraphicsUnit.Pixel))
            using (SolidBrush tb = new SolidBrush(Color.FromArgb(180, 150, 180, 255)))
            {
                StringFormat sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                g.DrawString(innerText, f, tb, innerRect, sf);
            }

            // Обводка шара
            using (Pen pen = new Pen(Color.FromArgb(20, 20, 60), 2.5f))
                g.DrawEllipse(pen, ballRect);
        }
    }
}