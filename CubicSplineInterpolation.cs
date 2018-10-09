using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubicSplineInterpolation
{
    class CubicSplineInterpolation
    {
        private readonly int size;
        private readonly CubicSpline[] splines;

        private void Sort(double[] x, double[] y)
        {
            if (x.GetLength(0) != y.GetLength(0))
            {
                throw new ArgumentException("Несовпадение длин введенных массивов");
            }

            int length = x.Length;

            bool flag = false;
            for (int i = 0; i < length; i++)
            {
                for (int j = 1; j < length; j++)
                {
                    if (x[j] < x[j - 1])
                    {
                        double tmpX = x[j];
                        double tmpY = y[j];

                        x[j] = x[j - 1];
                        y[j] = y[j - 1];

                        x[j - 1] = tmpX;
                        y[j - 1] = tmpY;

                        flag = true;
                    }
                }

                if (!flag)
                {
                    break;
                }
            }
        }

        public CubicSplineInterpolation(double[] x, double[] y)
        {
            Sort(x, y);

            int length = x.Length;

            double[] c = new double[length];
            c[0] = c[length - 1] = 0;

            double[] h = new double[length - 1];
            for (int i = 0; i < length - 1; i++)
            {
                h[i] = x[i + 1] - x[i];
            }

            double[] upDiagonal = new double[length - 2];
            double[] downDiagonal = new double[length - 2];

            double[] midDiagonal = new double[length - 1];

            for (int i = 0; i < length - 2; i++)
            {
                upDiagonal[i] = h[i + 1];
                downDiagonal[i] = h[i + 1];
            }
            for (int i = 0; i < length - 1; i++)
            {
                midDiagonal[i] = 2 * (h[i + 1] + h[i]);
            }

            double[] f = new double[length - 1];
            for (int i = 0; i < length - 1; i++)
            {
                f[i] = 3 * ((y[i + 2] - y[i + 1]) / h[i + 1] - (y[i + 1] - y[i]) / h[i]);
            }

            TridiagonalMatrix tridiagonalMatrix = new TridiagonalMatrix();
            tridiagonalMatrix.Solve(upDiagonal, midDiagonal, downDiagonal, f, c);

            double[] b = new double[length - 1];
            double[] d = new double[length - 1];
            
            for (int i = 0; i < length - 1; i++)
            {
                b[i] = (y[i + 1] - y[i]) / h[i] + (2 * c[i + 1] + c[i]) / 3 * h[i];
                d[i] = (c[i + 1] - c[i]) / (3 * h[i]);
            }

            this.size = length - 1;
            CubicSpline[] cubicSplines = new CubicSpline[size];
            for (int i = 0; i < size; i++)
            {
                cubicSplines[i] = new CubicSpline(x[i], x[i + 1], y[i + 1], b[i], c[i], d[i]);
            }
        }

        public string GetFormulaInterpolation()
        {
            string str = "";

            for (int i = 0; i < size; i++)
            {
                str += "P" + (i + 1).ToString() + "=" + splines[i].GetFormulaString() + "\n";
            }

            return str;
        }

        public void DrawInterpolation(int alpha, System.Windows.Forms.PaintEventArgs e)
        {
            for (int i = 0; i < size; i++)
            {
                splines[i].DrawCubicSpline(alpha, e);
            }
        }
    }
}
