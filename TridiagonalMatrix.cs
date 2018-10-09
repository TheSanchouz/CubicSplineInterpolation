using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubicSplineInterpolation
{
    class TridiagonalMatrix
    {
        //public double[] Solve(double[] upDiagonal, double[] diagonal, double[] downDiagonal, double[] d)
        //{
        //    int len = d.Length - 1;
        //    try
        //    {
        //        upDiagonal[0] = upDiagonal[0] / diagonal[0];
        //        d[0] = d[0] / diagonal[0];

        //        for (int i = 1; i < (len - 1); i++)
        //        {
        //            upDiagonal[i] = upDiagonal[i] / (diagonal[i] - downDiagonal[i] * upDiagonal[i - 1]);
        //            d[i] = (d[i] - downDiagonal[i] * d[i - 1]) / (diagonal[i] - downDiagonal[i] * upDiagonal[i - 1]);
        //        }
        //        d[len - 1] = (d[len - 1] - downDiagonal[len - 1] * d[len - 2]) 
        //            / (diagonal[len - 1] - downDiagonal[len - 1] * upDiagonal[len - 2]);

        //        for (int i = (len - 1); i-- > 0;)
        //        {
        //            d[i] = d[i] - upDiagonal[i] * d[i + 1];
        //        }

        //        return d;

        //    }
        //    catch (DivideByZeroException)  
        //    {
        //        return null;
        //    }
        //}

        //????
        public void Solve(double[] upDiagonal, double[] midDiagonal, double[] downDiagonal, double[] f, double[] solution)
        {
            int length = f.Length;

            double[] alpha = new double[length - 1];
            double[] beta = new double[length];
 
            alpha[0] = upDiagonal[0] / midDiagonal[0];
            beta[0] = f[0] / midDiagonal[0];

            for (int i = 1; i < length - 1; i++)
            {
                alpha[i] = upDiagonal[i] / (midDiagonal[i] - downDiagonal[i - 1] * alpha[i - 1]);
            }
            for (int i = 1; i < length; i++)
            {
                beta[i] = (f[i] + downDiagonal[i - 1] * beta[i - 1]) / (midDiagonal[i] - downDiagonal[i - 1] * alpha[i - 1]);
            }

            solution[solution.Length - 2] = beta[length - 1];
            for (int i = solution.Length - 3; i > 0; i--)
            {
                solution[i] = alpha[i + 1] * solution[i + 1] + beta[i + 1];
            }
        }

    }
}
