using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompMathHW2
{
    delegate double workFunc(double x,double h);

    class Progonka
    {
        workFunc a;
        workFunc b;
        workFunc c;
        workFunc f;
        public double[] x;
        public double[] y;
        double h;
        public double n;
        double start;
        double end;
        double alfa0;
        double beta0;
        double gamma0;
        double alfa1;
        double beta1;
        double gamma1;

        public Progonka(workFunc a, workFunc b, workFunc c, workFunc f, double n, double start, double end,double alfa0,double beta0,double gamma0, double alfa1, double beta1, double gamma1)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.f = f;
            this.n = n;
            this.x = new double[(int)n + 1];
            this.y = new double[(int)n + 1];
            this.h = (end - start) / n;
            this.start = start;
            this.end = end;
            this.alfa0 = alfa0;
            this.beta0 = beta0;
            this.gamma0 = gamma0;
            this.alfa1 = alfa1;
            this.beta1 = beta1;
            this.gamma1 = gamma1;
            setVals();
        }

        public double[] rArr;
        public double[] sArr;

        private void setVals()
        {
            
            for (int i = 0; i < x.Length; i++)
            {
                x[i] = start + i * h;
            }

            rArr = new double[x.Length];
            sArr = new double[x.Length];

            rArr[1] = -mu0(x[1]);
            sArr[1] = F0(x[1]);


            for (int i = 1; i < rArr.Length-1; i++)
            {
                rArr[i+1] = r(x[i], rArr[i]);
                sArr[i+1] = s(x[i], rArr[i], sArr[i]);           
            }


            double Fm1 = (f(x[(int)n - 1],h) * beta1 - c(x[(int)n - 1], h) * gamma1 * 2.0 * h) / ((a(x[(int)n - 1], h) + c(x[(int)n - 1], h)) * beta1);
            double Mum1 = (b(x[(int)n - 1], h) * beta1 - c(x[(int)n - 1], h) * alfa1 * 2.0 * h) / ((a(x[(int)n - 1], h) + c(x[(int)n - 1], h)) * beta1);    

            y = new double[x.Length];

            y[x.Length - 1] = (Fm1 - sArr[(int)n]) / (Mum1 + rArr[(int)n]);

            for (int i = x.Length-2; i >= 0 ; i--)
            {
                y[i] = findY(y[i + 1], rArr[i + 1], sArr[i + 1]);
            }
        }

        private double mu0(double x)
        {
            return ((a(x, h) + c(x, h)) * beta0) / (b(x, h) * beta0 + a(x, h) * alfa0 * 2 * h);
        }

        private double F0(double x)
        {
            return (f(x, h) * beta0 + a(x, h) * gamma0 * 2 * h) / (b(x, h) * beta0 + a(x, h) * alfa0 * 2 * h);
        }



        private double r(double x,double prevR)
        {
            return -(c(x, h)) / (a(x, h) * prevR + b(x, h));
        }

        private double s(double x, double prevR,double prevS)
        {
            return (f(x, h) - a(x, h) * prevS) / (a(x, h) * prevR + b(x, h));
        }

        private double findY(double prevY,double r,double s)
        {
            return r * prevY + s;
        }
    }
}
