using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubicSplineInterpolation
{
    class CubicSpline
    {
        private readonly double xLeft, xRight;
        private readonly double a, b, c, d;

        public double Function(double x)
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

        public string GetFormulaString()
        {
            string str = "";
            string var = "";

            if (xRight != 0)
                var = "(x" + xRight.ToString("-0.##;+0.##", CultureInfo.InvariantCulture) + ")";
            else
                var = "x";


            if (a != 0)
            {
                str += a.ToString();
            }

            if (b != 0)
            {
                str += b.ToString("+0.##;-0.##", CultureInfo.InvariantCulture);
                str += var;
            }

            if (c != 0)
            {
                str += c.ToString("+0.##;-0.##", CultureInfo.InvariantCulture);
                str += var + "^" + "2";
            }

            if (d != 0)
            {
                str += d.ToString("+0.##;-0.##", CultureInfo.InvariantCulture);
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

        public double XLeft
        {
            get
            {
                return xLeft;
            }
        }

        public double XRight
        {
            get
            {
                return xRight;
            }
        }

        public double A
        {
            get
            {
                return a;
            }
        }

        public double B
        {
            get
            {
                return b;
            }
        }

        public double C
        {
            get
            {
                return c;
            }
        }

        public double D
        {
            get
            {
                return d;
            }
        }
    }
}
