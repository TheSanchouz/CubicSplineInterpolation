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

        public CubicSplineInterpolation(double[] x, double[] y)
        {
            if (x.Length == y.Length)
            {
                //Sort(x, y);

                int length = x.Length - 1;

                double[] b = new double[length];
                double[] c = new double[length + 1];
                double[] d = new double[length];
                double[] h = new double[length];

                double[] downDiagonal = new double[length - 2];
                double[] midDiagonal = new double[length - 1];
                double[] upDiagonal = new double[length - 2];
                double[] f = new double[length - 1];

                for (int i = 0; i < length; i++)
                {
                    h[i] = x[i + 1] - x[i];
                    Console.WriteLine($"h[{i + 1}]   = {h[i]}");
                }

                for (int i = 0; i < length - 2; i++)
                {
                    downDiagonal[i] = h[i];
                    upDiagonal[i] = h[i];

                    Console.WriteLine($"downD[{i + 1}]   = {downDiagonal[i]}");
                    Console.WriteLine($"upD[{i + 1}]   = {upDiagonal[i]}");
                }

                for (int i = 0; i < length - 1; i++)
                {
                    midDiagonal[i] = 2 * (h[i] + h[i + 1]);

                    Console.WriteLine($"midD[{i + 1}]   = {midDiagonal[i]}");
                }

                for (int i = 0; i < length - 1; i++)
                {
                    f[i] = 3 * ((y[i + 2] - y[i + 1]) / h[i + 1] - (y[i + 1] - y[i]) / h[i]);
                    Console.WriteLine($"f[{i + 1}]   = {f[i]}");
                }

                TridiagonalMatrix tridiagonalMatrix = new TridiagonalMatrix();
                double[] solution = new double[length - 1];
                solution = tridiagonalMatrix.Solve(downDiagonal, midDiagonal, upDiagonal, f);

                c[0] = 0;
                c[length] = 0;

                Console.WriteLine($"c[{0}]   = {c[0]}");
                for (int i = 1; i < length; i++)
                {
                    c[i] = solution[i - 1];
                    Console.WriteLine($"c[{i}]   = {c[i]}");
                }
                Console.WriteLine($"c[{length}]   = {c[length]}");

                for (int i = 0; i < length; i++)
                {
                    d[i] = (c[i + 1] - c[i]) / (3 * h[i]);
                    Console.WriteLine($"d[{i + 1}]   = {d[i]}");
                }

                for (int i = 0; i < length; i++)
                {
                    b[i] = (y[i + 1] - y[i]) / h[i] + +(2 * c[i + 1] + c[i]) * h[i] / 3;
                    //b[i] = c[i + 1] * h[i] / 3 + c[i] * h[i] / 6 + (y[i + 1] - y[i]) / h[i];
                    Console.WriteLine($"b[{i + 1}]   = {b[i]}");
                }

                size = length;
                splines = new CubicSpline[size];
                for (int i = 0; i < size; i++)
                {
                    splines[i] = new CubicSpline(x[i], x[i + 1], y[i], b[i], c[i], d[i]);
                }
            }
        }

        public CubicSpline this[int number]
        {
            get
            {
                return splines[number];
            }
        }

        public string GetFormulaString()
        {
            string str = "";

            for (int i = 0; i < size; i++)
            {
                str += "P" + (i + 1).ToString() + " = " + splines[i].GetFormulaString() + "\n";
            }

            return str;
        }

        public string GetVarValues()
        {
            string str = "";

            for (int i = 0; i < size; i++)
            {
                str += "P" + (i + 1).ToString() + ":" + "\n" + splines[i].GetVarValues() + "\n";
            }

            return str;
        }

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
    }
}
