using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace lab6
{
    public partial class Form1 : Form
    {
        private readonly double[] chiTable =
        {
            3.841, 5.991, 7.815, 9.488, 11.070,
            12.592, 14.067, 15.507, 16.919, 18.307,
            19.675, 21.026, 22.362, 23.685, 24.996,
            26.296, 27.587, 28.869, 30.144, 31.410
        };

        
        private const ulong _M = (1UL << 63);       
        private const ulong _Beta = (1UL << 32) + 3UL; 
        private static ulong _X = (ulong)(uint)Environment.TickCount;

        private static ulong MulMod(ulong a, ulong b)
        {
            ulong aHi = a >> 32;
            ulong aLo = a & 0xFFFFFFFFUL;
            return ((aHi * b << 32) + aLo * b) & 0x7FFFFFFFFFFFFFFFUL;
        }

        private static double NextDouble()
        {
            _X = MulMod(_Beta, _X);
            return (double)_X / (double)_M;
        }

        public Form1()
        {
            InitializeComponent();

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Columns[0].ReadOnly = true;

            chart1.Legends[0].Enabled = false;
            chart1.Series[0].ChartType = SeriesChartType.Column;
            chart1.Series[0].IsValueShownAsLabel = true;
            chart1.Series[0]["PointWidth"] = "0.75";
            chart1.ChartAreas[0].AxisX.Title = "Значение X";
            chart1.ChartAreas[0].AxisY.Title = "Эмпирическая вероятность";
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;

            chart2.Legends[0].Enabled = false;
            chart2.Series[0].ChartType = SeriesChartType.Column;
            chart2.Series[0]["PointWidth"] = "0.85";
            chart2.ChartAreas[0].AxisX.Title = "Интервалы";
            chart2.ChartAreas[0].AxisY.Title = "Относительная частота";

            if (chart2.Series.IndexOf("Density") < 0)
            {
                chart2.Series.Add("Density");
            }

            chart2.Series["Density"].ChartType = SeriesChartType.Spline;
            chart2.Series["Density"].BorderWidth = 3;
            chart2.Series["Density"].Color = Color.Red;

            NumSample.Value = 5;
            N_experiment.Value = 1000;
            N2_experiment.Value = 1000;
            mean2.Value = 0;
            var2.Value = 1;

            FillDefaultProbabilities();
            ClearResults();
            UpdateProbabilitySum();
        }

        private void FillDefaultProbabilities()
        {
            double[] p = { 0.10, 0.20, 0.40, 0.20, 0.10 };
            dataGridView1.RowCount = (int)NumSample.Value;

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = i + 1;
                dataGridView1.Rows[i].Cells[1].Value = i < p.Length ? p[i].ToString(CultureInfo.InvariantCulture) : "0";
            }
        }

        private void ClearResults()
        {
            labelMean.Text = "-";
            labelVar.Text = "-";
            labelErrorMean.Text = "-";
            labelErrorVar.Text = "-";
            label_Chi.Text = "-";

            labelMean2.Text = "-";
            labelVar2.Text = "-";
            labelMeanError2.Text = "-";
            labelVarError2.Text = "-";
            label_Chi_2.Text = "-";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Start1_Click(object sender, EventArgs e)
        {
            int m = (int)NumSample.Value;
            int N = (int)N_experiment.Value;

            if (!ReadProbabilities(m, out double[] probabilities))
            {
                MessageBox.Show("Введите корректные вероятности. Сумма вероятностей должна быть равна 1.");
                return;
            }

            int[] frequencies = new int[m];

            for (int i = 0; i < N; i++)
            {
                int index = GenerateDiscrete(probabilities);
                frequencies[index]++;
            }

            double[] empiricalProbabilities = new double[m];
            for (int i = 0; i < m; i++)
            {
                empiricalProbabilities[i] = (double)frequencies[i] / N;
            }

            double theoreticalMean = 0;
            for (int i = 0; i < m; i++)
            {
                theoreticalMean += (i + 1) * probabilities[i];
            }

            double theoreticalVariance = 0;
            for (int i = 0; i < m; i++)
            {
                theoreticalVariance += Math.Pow(i + 1, 2) * probabilities[i];
            }
            theoreticalVariance -= Math.Pow(theoreticalMean, 2);

            double empiricalMean = 0;
            for (int i = 0; i < m; i++)
            {
                empiricalMean += (i + 1) * empiricalProbabilities[i];
            }

            double empiricalVariance = 0;
            for (int i = 0; i < m; i++)
            {
                empiricalVariance += Math.Pow(i + 1, 2) * empiricalProbabilities[i];
            }
            empiricalVariance -= Math.Pow(empiricalMean, 2);

            double meanError = RelativeError(empiricalMean, theoreticalMean);
            double varianceError = RelativeError(empiricalVariance, theoreticalVariance);

            double chiSquare = 0;
            for (int i = 0; i < m; i++)
            {
                chiSquare += Math.Pow(frequencies[i], 2) / (N * probabilities[i]);
            }
            chiSquare -= N;

            int degreesOfFreedom = m - 1;
            double criticalValue = GetCriticalChiSquare(degreesOfFreedom);

            labelMean.Text = empiricalMean.ToString("F4");
            labelVar.Text = empiricalVariance.ToString("F4");
            labelErrorMean.Text = (meanError * 100).ToString("F2") + "%";
            labelErrorVar.Text = (varianceError * 100).ToString("F2") + "%";
            ShowChiResult(label_Chi, chiSquare, criticalValue);

            DrawDiscreteChart(empiricalProbabilities);
        }

        private bool ReadProbabilities(int m, out double[] probabilities)
        {
            probabilities = new double[m];
            double sum = 0;

            for (int i = 0; i < m; i++)
            {
                object value = dataGridView1.Rows[i].Cells[1].Value;

                if (value == null || !TryParseDouble(value.ToString(), out double p))
                {
                    return false;
                }

                if (p <= 0 || p > 1)
                {
                    return false;
                }

                probabilities[i] = p;
                sum += p;
            }

            return Math.Abs(sum - 1.0) < 0.0001;
        }

        private int GenerateDiscrete(double[] probabilities)
        {
            double alpha = NextDouble();
            double s = alpha;

            for (int i = 0; i < probabilities.Length; i++)
            {
                s -= probabilities[i];
                if (s <= 0)
                {
                    return i;
                }
            }

            return probabilities.Length - 1;
        }

        private void DrawDiscreteChart(double[] empiricalProbabilities)
        {
            chart1.Series[0].Points.Clear();

            for (int i = 0; i < empiricalProbabilities.Length; i++)
            {
                int point = chart1.Series[0].Points.AddXY(i + 1, empiricalProbabilities[i]);
                chart1.Series[0].Points[point].Label = empiricalProbabilities[i].ToString("F3");
            }
        }

        private void CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 1)
            {
                Start1.Enabled = UpdateProbabilitySum();
            }
        }

        private void numRowsCount_ValueChanged(object sender, EventArgs e)
        {
            int rows = (int)NumSample.Value;
            dataGridView1.RowCount = rows;

            for (int i = 0; i < rows; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = i + 1;
                if (dataGridView1.Rows[i].Cells[1].Value == null)
                {
                    dataGridView1.Rows[i].Cells[1].Value = "0";
                }
            }

            Start1.Enabled = UpdateProbabilitySum();
        }

        private bool UpdateProbabilitySum()
        {
            double sum = 0;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                object value = dataGridView1.Rows[i].Cells[1].Value;

                if (value != null && TryParseDouble(value.ToString(), out double p))
                {
                    sum += p;
                }
            }

            double rest = 1.0 - sum;
            VarRes.Text = $"Остаток: {rest:F3}";

            bool isCorrect = Math.Abs(rest) < 0.0001;
            VarRes.ForeColor = isCorrect ? Color.Green : Color.Red;

            return isCorrect;
        }

        private void Start2_Click(object sender, EventArgs e)
        {
            double mean = (double)mean2.Value;
            double variance = (double)var2.Value;
            int N = (int)N2_experiment.Value;

            if (variance <= 0)
            {
                MessageBox.Show("Дисперсия должна быть больше нуля.");
                return;
            }

            double sigma = Math.Sqrt(variance);
            double[] data = new double[N];

            double sum = 0;
            for (int i = 0; i < N; i++)
            {
                data[i] = GenerateNormal(mean, sigma);
                sum += data[i];
            }

            double empiricalMean = sum / N;

            double varianceSum = 0;
            for (int i = 0; i < N; i++)
            {
                varianceSum += Math.Pow(data[i] - empiricalMean, 2);
            }

            double empiricalVariance = varianceSum / N;

            double meanError = RelativeError(empiricalMean, mean);
            double varianceError = RelativeError(empiricalVariance, variance);

            int intervalCount = (int)Math.Floor(1 + Math.Log(N, 2));
            if (intervalCount < 5) intervalCount = 5;
            if (intervalCount > 20) intervalCount = 20;

            double min = data.Min();
            double max = data.Max();
            double h = (max - min) / intervalCount;

            if (h <= 0)
            {
                MessageBox.Show("Не удалось построить интервалы для гистограммы.");
                return;
            }

            int[] frequencies = new int[intervalCount];
            for (int i = 0; i < N; i++)
            {
                int index = (int)((data[i] - min) / h);
                if (index >= intervalCount) index = intervalCount - 1;
                if (index < 0) index = 0;
                frequencies[index]++;
            }

            double chiSquare = 0;
            chart2.Series[0].Points.Clear();
            chart2.Series["Density"].Points.Clear();

            for (int i = 0; i < intervalCount; i++)
            {
                double left = min + i * h;
                double right = left + h;
                double middle = (left + right) / 2.0;

                double pTheoretical = NormalDensity(middle, mean, sigma) * h;

                if (pTheoretical > 0)
                {
                    chiSquare += Math.Pow(frequencies[i] - N * pTheoretical, 2) / (N * pTheoretical);
                }

                double empiricalProbability = (double)frequencies[i] / N;
                string intervalName = $"[{left:F2}; {right:F2}]";
                chart2.Series[0].Points.AddXY(intervalName, empiricalProbability);
            }

            int degreesOfFreedom = intervalCount - 1;
            double criticalValue = GetCriticalChiSquare(degreesOfFreedom);

            labelMean2.Text = empiricalMean.ToString("F4");
            labelVar2.Text = empiricalVariance.ToString("F4");
            labelMeanError2.Text = (meanError * 100).ToString("F2") + "%";
            labelVarError2.Text = (varianceError * 100).ToString("F2") + "%";
            ShowChiResult(label_Chi_2, chiSquare, criticalValue);

            DrawNormalDensity(min, max, h, mean, sigma);
        }

        private double GenerateNormal(double mean, double sigma)
        {
            double u1 = 1.0 - NextDouble();
            double u2 = 1.0 - NextDouble();

            double z = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2);
            return mean + sigma * z;
        }

        private double NormalDensity(double x, double mean, double sigma)
        {
            return 1.0 / (sigma * Math.Sqrt(2.0 * Math.PI)) *
                   Math.Exp(-Math.Pow(x - mean, 2) / (2.0 * sigma * sigma));
        }

        private void DrawNormalDensity(double min, double max, double h, double mean, double sigma)
        {
            double step = (max - min) / 100.0;
            if (step <= 0) return;

            for (double x = min; x <= max; x += step)
            {
                chart2.Series["Density"].Points.AddXY(x, NormalDensity(x, mean, sigma) * h);
            }

            chart2.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            chart2.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            chart2.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
        }

        private double RelativeError(double empirical, double theoretical)
        {
            if (Math.Abs(theoretical) < 0.0000001)
            {
                return Math.Abs(empirical - theoretical);
            }

            return Math.Abs(empirical - theoretical) / Math.Abs(theoretical);
        }

        private double GetCriticalChiSquare(int degreesOfFreedom)
        {
            if (degreesOfFreedom < 1) degreesOfFreedom = 1;
            if (degreesOfFreedom > chiTable.Length) degreesOfFreedom = chiTable.Length;
            return chiTable[degreesOfFreedom - 1];
        }

        private void ShowChiResult(Label label, double chiSquare, double criticalValue)
        {
            if (chiSquare < criticalValue)
            {
                label.Text = $"Гипотеза верна: χ² = {chiSquare:F3} < {criticalValue:F3}";
                label.ForeColor = Color.Green;
            }
            else
            {
                label.Text = $"Гипотеза неверна: χ² = {chiSquare:F3} > {criticalValue:F3}";
                label.ForeColor = Color.Red;
            }
        }

        private bool TryParseDouble(string text, out double value)
        {
            text = text.Replace(',', '.');
            return double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out value);
        }
    }
}