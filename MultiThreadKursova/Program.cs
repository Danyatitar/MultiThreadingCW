using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
   
        Matrix matrix = new Matrix(10,10);

   
        matrix.GenerateRandomMatrix(1, 10);


        Console.WriteLine("Original Matrix:");
        matrix.PrintMatrix();
        Console.WriteLine();

        Stopwatch luStopwatch = Stopwatch.StartNew();
        try
        {
     
            MatrixSolver solver = new MatrixSolver(matrix);
            
            Matrix inverseMatrix = solver.GetInverseMatrix();
            bool isCorrect = solver.IsInverseMatrixCorrect(matrix,inverseMatrix);
             
            Console.WriteLine("Inverse Matrix:");
            inverseMatrix.PrintMatrix();
            Console.WriteLine();
            Console.WriteLine($"isCorrect: {isCorrect}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        luStopwatch.Stop();
        long luTime = luStopwatch.ElapsedMilliseconds;

         Stopwatch luParallelStopwatch = Stopwatch.StartNew();
        try
        {
    
            MatrixSolver solver = new MatrixSolver(matrix,2);

            Matrix inverseMatrix = solver.GetInverseMatrix();
            bool isCorrect = solver.IsInverseMatrixCorrect(matrix,inverseMatrix);
             
            Console.WriteLine("Inverse Matrix:");
            inverseMatrix.PrintMatrix();
            Console.WriteLine();
            Console.WriteLine($"isCorrect: {isCorrect}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        luParallelStopwatch.Stop();
        long luParallelTime = luParallelStopwatch.ElapsedMilliseconds;

        Console.WriteLine($"Time taken for InverseMatrixLU: {luTime} milliseconds");
        Console.WriteLine($"Time taken for InverseMatrixLUParallel: {luParallelTime} milliseconds");
        double acceleration = (double)luTime / luParallelTime;
        Console.WriteLine($"Acceleration for Parallel Algorithm: {acceleration}");
    }



  
}
