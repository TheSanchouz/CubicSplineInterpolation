using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubicSplineInterpolation
{
    class CubicSplineInterpolation
    {
        public int size { get; }
        private CubicSpline[] splines;

        public CubicSplineInterpolation(double[] x, double[] y)
        {
            if (x.Length == y.Length)
            {
                Sort(x, y);

                int length = x.Length - 1;

                double[] b = new double[length];
                double[] c = new double[length + 1];
                double[] d = new double[length];
                double[] h = new double[length];

                double[] downDiagonal = length - 2 > 0 ? new double[length - 2] : null;
                double[] midDiagonal = new double[length - 1];
                double[] upDiagonal = length - 2 > 0 ? new double[length - 2] : null;
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
                if (length >= 2)
                {
                    solution = tridiagonalMatrix.Solve(downDiagonal, midDiagonal, upDiagonal, f);
                }

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
                    b[i] = (y[i + 1] - y[i]) / h[i] - (c[i + 1] + 2 * c[i]) * h[i] / 3;
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
                if (number < size)
                    return splines[number];
                else throw new ArgumentException("Выход за предел массива сплайнов");
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

        public void SaveInterpolation(string path)
        {
            StreamWriter streamWriter = new StreamWriter(path, false, System.Text.Encoding.Default);

            streamWriter.WriteLine($"#INTERPOLATION FOR {size}");
            for (int i = 0; i < size; i++)
            {
                streamWriter.WriteLine($"{splines[i].xLeft} {splines[i].xRight} {splines[i].a} {splines[i].b} {splines[i].c} {splines[i].d}");
            }
            streamWriter.WriteLine("#END");

            streamWriter.Close();
        }
        public CubicSplineInterpolation(string path)
        {
            StreamReader streamReader = new StreamReader(path);
            int count = 0;

            while (!streamReader.EndOfStream)
            {
                string line = streamReader.ReadLine();

                if (line.IndexOf("#INTERPOLATION FOR") == 0)
                {
                    string[] tmp = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    this.size = int.Parse(tmp[2]);
                    splines = new CubicSpline[size];
                }
                else if (line.IndexOf("#END") == 0)
                {
                    break;
                }
                else
                {
                    string[] coeff = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    splines[count++] = new CubicSpline(double.Parse(coeff[0]),
                            double.Parse(coeff[1]),
                            double.Parse(coeff[2]),
                            double.Parse(coeff[3]),
                            double.Parse(coeff[4]),
                            double.Parse(coeff[5]));
                }
            }

            streamReader.Close();
        }
    }
}
