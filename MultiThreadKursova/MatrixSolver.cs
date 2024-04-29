
public class MatrixSolver
{
  
    private Matrix inverseMatrix;

    public MatrixSolver(Matrix matrix)
    {
         if (Math.Abs(Determinant(matrix)) < 1e-10)
        {
            throw new InvalidOperationException("Inverse does not exist for a singular matrix.");
        }
        inverseMatrix = InverseMatrixLU.Inverse(matrix);
    }

    public MatrixSolver(Matrix matrix, int threadCounter)
    {
         if (Math.Abs(Determinant(matrix)) < 1e-10)
        {
            throw new InvalidOperationException("Inverse does not exist for a singular matrix.");
        }
        inverseMatrix = InverseMatrixLUParallel.Inverse(matrix, threadCounter);
    }

    public Matrix GetInverseMatrix()
    {
        return inverseMatrix;
    }
     private double Determinant(Matrix matrix)
    {
        int n = matrix.Rows;
        if (n == 1)
        {
            return matrix[0, 0];
        }
        else if (n == 2)
        {
            return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
        }
        else
        {
            double determinant = 0;
            for (int j = 0; j < n; j++)
            {
                determinant += matrix[0, j] * Cofactor(matrix, 0, j);
            }
            return determinant;
        }
    }

    // Method to calculate the cofactor of a matrix element
    private double Cofactor(Matrix matrix, int row, int col)
    {
        return Math.Pow(-1, row + col) * Minor(matrix, row, col);
    }

    private double Minor(Matrix matrix, int row, int col)
    {
        int n = matrix.Rows;
        Matrix minorMatrix = new Matrix(n - 1, n - 1);
        int minorRow = 0;
        for (int i = 0; i < n; i++)
        {
            if (i == row) continue;
            int minorCol = 0;
            for (int j = 0; j < n; j++)
            {
                if (j == col) continue;
                minorMatrix[minorRow, minorCol] = matrix[i, j];
                minorCol++;
            }
            minorRow++;
        }
        return Determinant(minorMatrix);
    }

 public bool IsInverseMatrixCorrect(Matrix originalMatrix, Matrix inverseMatrix)
{

    double[,] original = ConvertTo2DArray(originalMatrix);
    double[,] inverse = ConvertTo2DArray(inverseMatrix);


    int rowsA = original.GetLength(0);
    int colsA = original.GetLength(1);
    int colsB = inverse.GetLength(1);

    double[,] result = new double[rowsA, colsB];

    for (int i = 0; i < rowsA; i++)
    {
        for (int j = 0; j < colsB; j++)
        {
            for (int k = 0; k < colsA; k++)
            {
                result[i, j] += original[i, k] * inverse[k, j];
            }
        }
    }

   
    for (int i = 0; i < rowsA; i++)
    {
        for (int j = 0; j < colsB; j++)
        {
            if (Math.Abs(result[i, j] - ((i == j) ? 1.0 : 0.0)) > 1e-10)
            {
                return false; 
            }
        }
    }
    return true; 
}

private double[,] ConvertTo2DArray(Matrix matrix)
{

    double[,] data = matrix.getData(); 
    return data;
}
}