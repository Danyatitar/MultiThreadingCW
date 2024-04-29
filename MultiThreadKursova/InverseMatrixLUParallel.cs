using System;

public class InverseMatrixLUParallel
{
    // LU decomposition
    
    public static Matrix Inverse(Matrix matrix, int threadsCount)
{
    int n = matrix.Rows;
    Matrix lower, upper;
    InverseMatrixLU.LUDecomposition(matrix, out lower, out upper);
    Matrix inverse = new Matrix(n, n);
    Matrix identity = new Matrix(n, n);
    for (int i = 0; i < n; i++)
    {
        identity[i, i] = 1;
    }

    int columnsForThreads = 1;

    if (threadsCount >= n)
    {
        threadsCount = n;
    }
    else if (threadsCount < n)
    {
        columnsForThreads = n / threadsCount;
        int rest = n % threadsCount;
        if (n != 0)
        {
            threadsCount++;
        }
    }

    Task[] tasks = new Task[threadsCount];
    for (int index = 0; index < threadsCount; index++)
    {
        int start = index * columnsForThreads;
        int end = start + columnsForThreads;
        if (end > n)
        {
            end = n;
        }
        tasks[index] = Task.Run(() =>
        {
            for (int col = start; col < end; col++)
            {
                double[] b = new double[n];
                for (int i = 0; i < n; i++)
                {
                    b[i] = identity[i, col];
                }

                double[] x = InverseMatrixLU.SolveLower(lower, b);
                double[] y = InverseMatrixLU.SolveUpper(upper, x);

                for (int i = 0; i < n; i++)
                {
                    inverse[i, col] = y[i];
                }
            }
        });
    }

    Task.WaitAll(tasks);

    return inverse;
}

   
}

