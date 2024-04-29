using System;

public class Matrix
{
    private double[,] data;
    private int rows;
    private int cols;
    private Random random;

    public Matrix(int rows, int cols)
    {
        this.rows = rows;
        this.cols = cols;
        data = new double[rows, cols];
        random = new Random();
    }

       public int Rows
    {
        get { return rows; }
    }


    public int Cols
    {
        get { return cols; }
    }


    public double this[int row, int col]
    {
        get { return data[row, col]; }
        set { data[row, col] = value; }
    }

     public double[,] getData()
    {
        return data;
       
    }

 
    public void GenerateRandomMatrix(int min,int max)
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                data[i, j] = random.Next(min, max + 1);
            }
        }
    }


    public void PrintMatrix()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write(data[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }


   
}
