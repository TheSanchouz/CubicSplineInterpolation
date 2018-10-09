using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubicSplineInterpolation
{
    class CubicSpline
    {
        private readonly double xLeft, xRight;

        private readonly double a, b, c, d;

        private double Function(double x)
        {
            return a +
                    b * (x - xRight) +
                    c * (x - xRight) * (x - xRight) +
                    d * (x - xRight) * (x - xRight) * (x - xRight);
        }


        public CubicSpline(double xLeft, double xRight, 
            double a, double b, double c, double d) 
        {
            this.xLeft = xLeft;
            this.xRight = xRight;

            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
        }

        public void DrawCubicSpline(int alpha, System.Windows.Forms.PaintEventArgs e)
        {
            //int count = (int)(xRight - xLeft) / alpha;

            //double[] y = new double[count];
            //Point[] points = new Point[count + 2];

            ////занесение левой точки
            //points[0].X = (int)xLeft;
            //points[0].Y = (int)Function(xLeft);
            ////???
            //for (int i = 1; i < count; i++)
            //{
            //    y[i] = Function(xLeft + i * alpha);

            //    points[i].X = (int)(xLeft + i * alpha);
            //    points[i].Y = (int)y[i];
            //}

            //e.Graphics.DrawCurve(new Pen(Color.FromArgb(255, 0, 0, 255)), points);
            Point[] points = {
                new Point((int)xLeft, (int)Function(xLeft)),
                new Point((int)xRight, (int)Function(xRight))};

            e.Graphics.DrawCurve(new Pen(Color.FromArgb(255, 0, 0, 255)), points);
        }

        public string GetFormulaString()
        {
            string str = "";
            string var = "";

            if (xRight != 0)
            {
                if (xRight > 0)
                {
                    var = "(x" + "-" + xRight.ToString() + ")";
                }
                else
                {
                    var = "(x" + "+" + xRight.ToString() + ")";
                }
            }
            else
            {
                var = "x";
            }


            if (a != 0)
            {
                str += a.ToString("-#");
            }

            if (b != 0)
            {
                str += b.ToString("+#;-#");
                str += var;
            }

            if (c != 0)
            {
                str += c.ToString("+#;-#");
                str += var + "^" + "2";
            }

            if (d != 0)
            {
                str += d.ToString("+#;-#");
                str += var + "^" + "3";
            }

            return str;
        }

        public string GetVarValues()
        {
            return "x = " + xRight.ToString() + "\n" + 
                "a = " + a.ToString() + "\n" +
                "b = " + b.ToString() + "\n" +
                "c = " + c.ToString() + "\n" +
                "d = " + d.ToString() + "\n";
        }
    }
}
