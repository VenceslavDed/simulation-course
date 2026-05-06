using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.AxHost;

namespace lab7
{
    public partial class Form1 : Form
    {
        Random random = new Random();

        int currentState = 0;
        double[,] Lambda = new double[3, 3];

        string[] stateNames = { "Ясно", "Облачно", "Пасмурно" };
 

        public Form1()
        {
            InitializeComponent();
            InitializeChart();
        }

        private void InitializeChart()
        {
            chart1.Series.Clear();
            chart1.Series.Add("Погода");
            chart1.Series[0].ChartType = SeriesChartType.StepLine;
            chart1.Series[0].BorderWidth = 3;
            chart1.Series[0].Color = System.Drawing.Color.DarkBlue;

            chart1.ChartAreas[0].AxisX.Title = "Время (дни)";
            chart1.ChartAreas[0].AxisY.Title = "Состояние погоды";
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Maximum = 2;
            chart1.ChartAreas[0].AxisY.Interval = 1;

            chart1.ChartAreas[0].AxisY.CustomLabels.Clear();
            chart1.ChartAreas[0].AxisY.CustomLabels.Add(-0.5, 0.5, "Ясно");
            chart1.ChartAreas[0].AxisY.CustomLabels.Add(0.5, 1.5, "Облачно");
            chart1.ChartAreas[0].AxisY.CustomLabels.Add(1.5, 2.5, "Пасмурно");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        double GetExpRV(double lambda)
        {
            if (lambda <= 0) return double.PositiveInfinity; 
            return -Math.Log(random.NextDouble()) / lambda;
        }

        int GetNextState(int state)
        {
            double[] p = new double[] { 0, 0, 0 };
            double s = 0;
            for (int j = 0; j < 3; j++)
            {
                if (j != state)
                {
                    p[j] = -Lambda[state, j] / Lambda[state, state];
                }                   
                else
                {
                    p[j] = 0;
                    continue;
                }
            }           
            double a = random.NextDouble();

            for (int j = 0; j < 3; j++)
            {
                if (j == state) continue;
                s += p[j];
                if (a <= s)
                    return j;
            }
            return state;
        }

        // Вычисление теоретического стационарного распределения
        private double[] GetStationaryDistribution()
        {
            double λ01 = Lambda[0, 1], λ02 = Lambda[0, 2];
            double λ10 = Lambda[1, 0], λ12 = Lambda[1, 2];
            double λ20 = Lambda[2, 0], λ21 = Lambda[2, 1];
            double out0 = λ01 + λ02; 
            double out1 = λ10 + λ12; 
            double out2 = λ20 + λ21; 

            double coeff = (out1 * out0 - λ10 * λ01) / (out1 * λ20 + λ10 * λ21);
            double n = out0 * out1 - λ10 * λ01;
            double d = λ20 * out1 + λ10 * λ21;
            double π2_over_π0 = (d != 0) ? n / d : 0;
            double π1_over_π0 = (λ10 != 0) ? (out0 - λ20 * π2_over_π0) / λ10 : 0;

            double sumCoef = 1 + π1_over_π0 + π2_over_π0;
            double π0 = 1.0 / sumCoef;
            double π1 = π1_over_π0 * π0;
            double π2 = π2_over_π0 * π0;
            double total = π0 + π1 + π2;
            if (total > 0)
            {
                π0 /= total;
                π1 /= total;
                π2 /= total;
            }
            return new double[] { π0, π1, π2 };
        }
        private void button2_Click(object sender, EventArgs e)
        {
            double totalDays = (double)numericUpDown1.Value;
            Lambda[0, 0] = (double)n00.Value;
            Lambda[0, 1] = (double)n01.Value;
            Lambda[0, 2] = (double)n02.Value;
            Lambda[1, 0] = (double)n10.Value;
            Lambda[1, 1] = (double)n11.Value;
            Lambda[1, 2] = (double)n12.Value;
            Lambda[2, 0] = (double)n20.Value;
            Lambda[2, 1] = (double)n21.Value;
            Lambda[2, 2] = (double)n22.Value;
            listBox1.Items.Clear();
            
            double[] Dur = new double[3];   
            double t = 0;
            double[] theoretical = GetStationaryDistribution();
            int minIndex = 0;
            for (int i = 1; i < 3; i++)
                if (theoretical[i] > theoretical[minIndex])
                    minIndex = i;
            currentState = minIndex;

            int[,] trans = new int[3, 3];
            chart1.Series[0].Points.Clear();
            chart1.Series[0].Points.AddXY(0, currentState);

            int steps = 0; 

            while (t < totalDays) 
            {
                double lambdaSum = 0;
                for (int j = 0; j < 3; j++)
                    if (j != currentState)
                        lambdaSum += Lambda[currentState, j];

                double dt = GetExpRV(lambdaSum);
                if (t + dt >= totalDays)
                {
                    double r = totalDays - t;
                    Dur[currentState] += r;
                    t = totalDays;
                    chart1.Series[0].Points.AddXY(totalDays, currentState);
                    break;
                }
                else
                {
                    Dur[currentState] += dt;
                    t += dt;
                    chart1.Series[0].Points.AddXY(t, currentState);

                    int nextState = GetNextState(currentState);
                    trans[currentState, nextState]++;
                    currentState = nextState;
                    steps++;
                }
            }

            double[] empirical = new double[3];
            for (int i = 0; i < 3; i++)
                empirical[i] = Dur[i] / totalDays;

            
            double[] error = new double[3];
            for (int i = 0; i < 3; i++)
                error[i] = Math.Abs(empirical[i] - theoretical[i]);

            listBox1.Items.Add("Сравнение вероятностей");
            for (int i = 0; i < 3; i++)
            {
                listBox1.Items.Add($"{stateNames[i]}:");
                listBox1.Items.Add($"  Эмпирическая: {empirical[i]:F6}");
                listBox1.Items.Add($"  Теоретическая: {theoretical[i]:F6}");
                listBox1.Items.Add($"  Ошибка:       {error[i]:F6}");
            }
            listBox1.Items.Add("");
            listBox1.Items.Add($"Количество переходов: {steps}");
            listBox1.Items.Add("");

            string fileName = "weather_statistics.csv";
            using (StreamWriter sw = new StreamWriter(fileName, false, System.Text.Encoding.UTF8))
            {
                sw.WriteLine("Состояние;Время (дни);Эмпирическая вероятность;Теоретическая вероятность;Абсолютная ошибка");
                for (int i = 0; i < 3; i++)
                {
                    sw.WriteLine($"{stateNames[i]};{Dur[i]:F4};{empirical[i]:F6};{theoretical[i]:F6};{error[i]:F6}");
                }
                sw.WriteLine();
                sw.WriteLine("Переходы (из -> в):");
                sw.WriteLine("Из\\В;Ясно;Облачно;Пасмурно");
                for (int i = 0; i < 3; i++)
                {
                    sw.Write($"{stateNames[i]};");
                    for (int j = 0; j < 3; j++)
                        sw.Write($"{trans[i, j]};");
                    sw.WriteLine();
                }
                sw.WriteLine();
                sw.WriteLine($"Количество переходов: {steps}");
            }

            listBox1.Items.Add($"Статистика сохранена в файл: {fileName}");
        }
    }
}
