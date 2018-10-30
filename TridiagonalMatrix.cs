using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubicSplineInterpolation
{
    class TridiagonalMatrix
    {
        public double[] Solve(double[] downDiagonal, double[] midDiagonal, double[] upDiagonal, double[] f)
        {
            int length = midDiagonal.Length;

            if (length == downDiagonal.Length + 1 && length == upDiagonal.Length + 1 && length == f.Length)
            {
                bool diagonality = IsDiagonalDominance(downDiagonal, midDiagonal, upDiagonal);
                //Console.WriteLine($"Св-во диагонального преобладания = {diagonality}");

                double[] alpha = new double[length - 1];
                double[] beta = new double[length - 1];

                alpha[0] = -upDiagonal[0] / midDiagonal[0];
                beta[0] = f[0] / midDiagonal[0];

                //Console.WriteLine($"alpha[{1}]  = {alpha[0]}");
                //Console.WriteLine($"beta[{1}]   = {beta[0]}");

                for (int i = 1; i < length - 1; i++)
                {
                    double gamma = midDiagonal[i] + downDiagonal[i - 1] * alpha[i - 1];
                    alpha[i] = -upDiagonal[i] / gamma;
                    beta[i] = (f[i] - downDiagonal[i - 1] * beta[i - 1]) / gamma;

                    //Console.WriteLine($"alpha[{i + 1}]  = {alpha[i]}");
                    //Console.WriteLine($"beta[{i + 1}]   = {beta[i]}");
                }

                double[] solution = new double[length];
                solution[length - 1] = (f[length - 1] - downDiagonal[length - 2] * beta[length - 2])
                    / (midDiagonal[length - 1] + downDiagonal[length - 2] * alpha[length - 2]);

                //Console.WriteLine($"x[{length - 1}] = {solution[length - 1]}");

                for (int i = length - 2; i >= 0; i--)
                {
                    solution[i] = alpha[i] * solution[i + 1] + beta[i];
                    //Console.WriteLine($"x[{i}] = {solution[i]}");
                }

                return solution;
            }

            throw new ArgumentException("Матрица не является квадратной");
        }

        public double[] Solve(double[,] matrix, double[] f)
        {
            int length = f.Length;

            if (matrix.GetLength(0) == matrix.GetLength(1) && matrix.GetLength(1) == length)
            {
                bool diagonality = IsDiagonalDominance(matrix);
                //Console.WriteLine($"Св-во диагонального преобладания = {diagonality}");

                double[] alpha = new double[length - 1];
                double[] beta = new double[length - 1];

                alpha[0] = -matrix[0, 1] / matrix[0, 0];
                beta[0] = f[0] / matrix[0, 0];

                //Console.WriteLine($"alpha[{1}]  = {alpha[0]}");
                //Console.WriteLine($"beta[{1}]   = {beta[0]}");

                for (int i = 1; i < length - 1; i++)
                {
                    double gamma = matrix[i, i] + matrix[i, i - 1] * alpha[i - 1];
                    alpha[i] = -matrix[i, i + 1] / gamma;
                    beta[i] = (f[i] - matrix[i, i - 1] * beta[i - 1]) / gamma;

                    //Console.WriteLine($"alpha[{i + 1}]  = {alpha[i]}");
                    //Console.WriteLine($"beta[{i + 1}]   = {beta[i]}");
                }

                double[] solution = new double[length];
                solution[length - 1] = (f[length - 1] - matrix[length - 1, length - 2] * beta[length - 2])
                    / (matrix[length - 1, length - 1] + matrix[length - 1, length - 2] * alpha[length - 2]);

                //Console.WriteLine($"x[{length - 1}] = {solution[length - 1]}");
                for (int i = length - 2; i >= 0; i--)
                {
                    solution[i] = alpha[i] * solution[i + 1] + beta[i];
                    //Console.WriteLine($"x[{i}] = {solution[i]}");
                }

                return solution;
            }

            throw new ArgumentException("Матрица не является квадратной");
        }

        //проверка диагонального преобладания
        private bool IsDiagonalDominance(double[] downDiagonal, double[] midDiagonal, double[] upDiagonal)
        {
            int length = midDiagonal.Length;

            for (int i = 0; i < length; i++)
            {
                double downEl = (i == 0) ? 0.0 : downDiagonal[i - 1];
                double upEl = (i == length - 1) ? 0.0 : upDiagonal[i];

                if (Math.Abs(midDiagonal[i]) <= Math.Abs(downEl) + Math.Abs(upEl))
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsDiagonalDominance(double[,] matrix)
        {
            int length = matrix.GetLength(0);

            for (int i = 0; i < length; i++)
            {
                double downEl = (i == 0) ? 0.0 : matrix[i, i - 1];
                double upEl = (i == length - 1) ? 0.0 : matrix[i, i + 1];

                if (Math.Abs(matrix[i, i]) <= Math.Abs(downEl) + Math.Abs(upEl))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
