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


namespace CompMathHW2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        double a(double x,double h)
        {
            return 1 - ((0.5 / (x + 5.0)) * (h / 2.0));
        }

        double b(double x, double h)
        {
            return -2;
        }

        double c(double x, double h)
        {
            return 1 + ((0.5 / (x + 5)) * (h / 2.0));
        }

        double f(double x, double h)
        {
            return (h * h) / (Math.Sqrt(x + 5));
        }

        double findMax(Progonka var1, Progonka var2)
        {
            double max = 0;
            for (int i = 0; i < var1.x.Length; i++)
            {
                for (int j = 0; j < var2.x.Length; j++)
                {
                    if (var1.x[i] == var2.x[j])
                    {
                        if (Math.Abs(var1.y[i] - var2.y[j]) > max)
                        {
                            max = Math.Abs(var1.y[i] - var2.y[j]);
                        }
                    }
                }
            }
            return max;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int n = 8;
            double max;
            Progonka progonka;
            do
            {
                progonka = new Progonka(a, b, c, f, n, 0, 1, 3, -10, 5, 0, 1, Math.Sqrt(6));
                max = findMax(progonka, new Progonka(a, b, c, f, 2 * n, 0, 1, 3, -10, 5, 0, 1, Math.Sqrt(6)));
                n *= 2;
            }
            while (findMax(progonka,new Progonka(a, b, c, f, 2*n, 0, 1, 3, -10, 5, 0, 1, Math.Sqrt(6)))>1e-6);
            DrawFunction(progonka.y, progonka.x);
            MessageBox.Show("n=" + progonka.n.ToString() + " Максимальная разность=" + max.ToString());
        }

        public void DrawFunction(double[] y, double[] x)
        {
            try
            {
                Random random = new Random();
                string name;
                name = "Series for -" + "" + " n=" + (y.Length - 1).ToString();
                Series series = new Series(name);
                chart1.Series.Add(series);
                chart1.Series[name].ChartType = SeriesChartType.Spline;
                richTextBox1.Text += "Метод Прогонки\n";
                for (int i = 0; i <= x.Length; i++)
                {
                    chart1.Series[name].Points.AddXY(x[i], y[i]);
                    richTextBox1.Text += "x-" + x[i] + "  |  " + "y-" + y[i] + "\n";
                }
            }
            catch
            {

            }

        }
    }
}
