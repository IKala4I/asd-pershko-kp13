using System;
using static System.Console;

namespace Task_12
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] Matrix = null;
           

            int n, m;
            Write("Input n :");
            while (!int.TryParse(ReadLine(), out n) || (n <= 0))
            {
                Write("wrong n, try one more time : ");
            }
            Write("Input m :");
            while (!int.TryParse(ReadLine(), out m) || (m != n))
            {
                Write("wrong m, m must be equal to n : ");
            }
            WriteLine("\nWhere red is to be sorted\n");
            Matrix = generateMatrix(n, m);
            printMatrix(Matrix);
            Matrix = AlgoritmShella(Matrix);
            WriteLine();
            printMatrix(Matrix);


           

        }
        static int[,] generateMatrix(int Row, int Col)
        {

            Random rnd = new Random();
            int[,] Matrix = new int[Row, Col];
            for (int i = 0; i < Row; i++)
                for (int j = 0; j < Col; j++)
                    Matrix[i, j] = rnd.Next(0, 100);
            return Matrix;
        }
        static void printMatrix(int[,] Matrix)
        {
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    if (i > j)
                    {
                        ForegroundColor = ConsoleColor.DarkRed;
                        Write(Matrix[i, j].ToString() + "\t");
                        ForegroundColor = ConsoleColor.White;
                    }
                    else
                        Write(Matrix[i, j].ToString() + "\t");

                }
                WriteLine();
       
            }
            
        }
        static int[,] AlgoritmShella( int[,] Matrix)
        {
            int number = 0;
            for (int i = 0; i < Matrix.GetLength(0); i++)
                number += i;
            int[] array = new int[number];
            int x = 0;
            for (int i = 0; i < Matrix.GetLength(0); i++)
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    if (i == j && j != Matrix.GetLength(1)-1)
                    {
                        for (int k = i + 1; k < Matrix.GetLength(0); k++)
                        {
                            array[x] = Matrix[k,i];
                            x++;
                        }
                    }
                }
            array = ShellSort(array);
            x = 0;
            for (int i = 0; i < Matrix.GetLength(0); i++)
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    if (i == j && j != Matrix.GetLength(1) - 1)
                    {
                        for (int k = i + 1; k < Matrix.GetLength(0); k++)
                        {
                            Matrix[k, i] = array[x];
                            x++;
                        }
                    }
                }
                       
            return Matrix;
        }
        static int[] ShellSort(int[] array)
        {

            int step = array.Length / 2;
            while (step >= 1)
            {
                for (int i = step; i < array.Length; i++)
                {
                    int j = i;
                    while ((j >= step) && (array[j - step] > array[j]))
                    {
                        
                        int t = array[j];
                        array[j] =  array[j - step];
                        array[j - step] = t;
                        j = j - step;
                    }
                }

                step = step / 2;
            }

            return array;
        }
    }
}
