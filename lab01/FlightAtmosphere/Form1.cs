using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FlightAtmosphere
{
    public partial class Form1 : Form
    {
        const double g = 9.81;
        const double C = 0.15;
        const double rho = 1.29;
        public Form1()
        {
            InitializeComponent();
            chart1.ChartAreas[0].AxisX.Title = "x (м)";
            chart1.ChartAreas[0].AxisY.Title = "y (м)";

            dataGridView1.Columns.Add("dt", "dt");
            dataGridView1.Columns.Add("dist", "Дальность");
            dataGridView1.Columns.Add("hmax", "Макс. высота");
            dataGridView1.Columns.Add("vfinal", "Скорость в конце");
        }

        private void btLaunch_Click(object sender, EventArgs e)
        {
            double v0 = (double)inputSpeed.Value;
            double angle = (double)inputAngle.Value * Math.PI / 180.0;
            double h0 = (double)inputHeight.Value;
            double S = (double)inputSize.Value;
            double m = (double)inputWeight.Value;
            double dt = (double)inputDt.Value;

            double k = 0.5 * C * rho * S / m;

            Simulate(v0, angle, h0, k, dt);
        }
        private void Simulate(double v0, double angle, double h0, double k, double dt)
        {
            double x = 0;
            double y = h0;
            double vx = v0 * Math.Cos(angle);
            double vy = v0 * Math.Sin(angle);
            double maxHeight = y;

            Series series = new Series("dt=" + dt);
            series.ChartType = SeriesChartType.Line;
            series.BorderWidth = 2;
            chart1.Series.Add(series);

            while (y > 0)
            {
                series.Points.AddXY(x, y);

                RungeKuttaStep(ref x, ref y, ref vx, ref vy, k, dt);

                if (y > maxHeight)
                    maxHeight = y;
            }

            double finalSpeed = Math.Sqrt(vx * vx + vy * vy);

            dataGridView1.Rows.Add(
                dt,
                x.ToString("F2"),
                maxHeight.ToString("F2"),
                finalSpeed.ToString("F2")
            );
        }
        private void RungeKuttaStep(ref double x, ref double y,
                                    ref double vx, ref double vy,
                                    double k, double dt)
        {
            double ax(double vx_, double vy_)
            {
                double v = Math.Sqrt(vx_ * vx_ + vy_ * vy_);
                return -k * vx_ * v;
            }

            double ay(double vx_, double vy_)
            {
                double v = Math.Sqrt(vx_ * vx_ + vy_ * vy_);
                return -g - k * vy_ * v;
            }

            double k1x = vx;
            double k1y = vy;
            double k1vx = ax(vx, vy);
            double k1vy = ay(vx, vy);

            double k2x = vx + k1vx * dt / 2;
            double k2y = vy + k1vy * dt / 2;
            double k2vx = ax(vx + k1vx * dt / 2, vy + k1vy * dt / 2);
            double k2vy = ay(vx + k1vy * dt / 2, vy + k1vy * dt / 2);

            double k3x = vx + k2vx * dt / 2;
            double k3y = vy + k2vy * dt / 2;
            double k3vx = ax(vx + k2vx * dt / 2, vy + k2vy * dt / 2);
            double k3vy = ay(vx + k2vy * dt / 2, vy + k2vy * dt / 2);

            double k4x = vx + k3vx * dt;
            double k4y = vy + k3vy * dt;
            double k4vx = ax(vx + k3vx * dt, vy + k3vy * dt);
            double k4vy = ay(vx + k3vy * dt, vy + k3vy * dt);

            x += dt / 6 * (k1x + 2 * k2x + 2 * k3x + k4x);
            y += dt / 6 * (k1y + 2 * k2y + 2 * k3y + k4y);
            vx += dt / 6 * (k1vx + 2 * k2vx + 2 * k3vx + k4vx);
            vy += dt / 6 * (k1vy + 2 * k2vy + 2 * k3vy + k4vy);
        }

    }
}
