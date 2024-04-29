public class InverseMatrixLU
{
    // LU decomposition
 public static void LUDecomposition(Matrix matrix, out Matrix lower, out Matrix upper)
    {
        int n = matrix.Rows;
        lower = new Matrix(n, n);
        upper = new Matrix(n, n);

        for (int i = 0; i < n; i++)
        {
            lower[i, i] = 1;
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = i; j < n; j++)
            {
                double sum = 0;
                for (int k = 0; k < i; k++)
                {
                    sum += lower[i, k] * upper[k, j];
                }
                upper[i, j] = matrix[i, j] - sum;
            }

            for (int j = i; j < n; j++)
            {
                double sum = 0;
                for (int k = 0; k < i; k++)
                {
                    sum += lower[j, k] * upper[k, i];
                }
                lower[j, i] = (matrix[j, i] - sum) / upper[i, i];
            }
        }
    }

    // Finding the inverse of a matrix using LU decomposition
    public static Matrix Inverse(Matrix matrix)
    {
        Matrix lower, upper;
        LUDecomposition(matrix, out lower, out upper);
        int n = matrix.Rows;
        Matrix inverse = new Matrix(n, n);
        Matrix identity = new Matrix(n, n);
        for (int i = 0; i < n; i++)
        {
            identity[i, i] = 1;
        }

        for (int col = 0; col < n; col++)
        {
            double[] b = new double[n];
            for (int i = 0; i < n; i++)
            {
                b[i] = identity[i, col];
            }

            double[] y = SolveLower(lower, b);
            double[] x = SolveUpper(upper, y);

            for (int i = 0; i < n; i++)
            {
                inverse[i, col] = x[i];
            }
        }

        return inverse;
    }

    public static double[] SolveLower(Matrix lower, double[] b)
    {
        int n = lower.Rows;
        double[] y = new double[n];
        for (int i = 0; i < n; i++)
        {
            double sum = 0;
            for (int j = 0; j < i; j++)
            {
                sum += lower[i, j] * y[j];
            }
            y[i] = (b[i] - sum) / lower[i, i];
        }
        return y;
    }

    public static double[] SolveUpper(Matrix upper, double[] b)
    {
        int n = upper.Rows;
        double[] x = new double[n];
        for (int i = n - 1; i >= 0; i--)
        {
            double sum = 0;
            for (int j = i + 1; j < n; j++)
            {
                sum += upper[i, j] * x[j];
            }
            x[i] = (b[i] - sum) / upper[i, i];
        }
        return x;
    }
}



    