using System;
using static System.Console;
using System.Diagnostics;



namespace Task_10
{
    class Program
    {
        public static string elem;
        public static string maxelem;
        static int Maxvalue;
        static void Main()
        {

            bool Continue = true, cnt2 = true, cnt = true;
            string numInput;
            int N, M;
            int[,] Matrix = null;

            while (cnt2)
            {
                Write("N = ");
                try
                {
                    N = Convert.ToInt32(ReadLine());
                    if (N % 2 == 0)
                    {
                        cnt2 = false;
                        while (cnt)
                        {
                            Write("M = ");
                            try
                            {
                                M = Convert.ToInt32(ReadLine());
                                cnt = false;
                                WriteLine();

                                while (Continue)
                                {
                                    Write("Введiть '1', щоб згенерувати матрицю з псевдорандомними числами та '2', щоб контрольну: ");
                                    numInput = ReadLine();
                                    if (numInput == "1")
                                    {
                                        Continue = false;
                                        Matrix = generateMatrix(N, M);
                                        printMatrix(Matrix);
                                        WriteLine();
                                    }
                                    else if (numInput == "2")
                                    {
                                        Continue = false;
                                        Matrix = generatecontrolMatrix(N, M);
                                        printMatrix(Matrix);
                                        WriteLine();
                                    }
                                    else
                                        WriteLine("Введiть 1 або 2!");
                                }
                            }
                            catch
                            {
                                WriteLine("Введiть цiле натуральне число число!");
                            }
                        }
                    }
                    else
                        WriteLine("Введiть парне число!");
                }
                catch
                {
                    WriteLine("Введiть цiле натуральне число число!");
                }
               
            }
            Stopwatch sw = new Stopwatch();
            sw.Start();
            createArray(Matrix);
                WriteLine("Array of elem: " + elem);
                WriteLine();
                WriteLine("Max value = " + Maxvalue);
                WriteLine();
                if (maxelem != null)
                    WriteLine("Max elem: " + maxelem);
                else
                    WriteLine("Таких елементiв не iснує");
            sw.Stop();
            WriteLine();
            WriteLine(sw.Elapsed);
            
        }
    
        static void printMatrix(int[,] Matrix)
        {
            for (long i = 0; i < Matrix.GetLength(0); i++)
            {
                for (long j = 0; j < Matrix.GetLength(1); j++)
                    Write(Matrix[i, j].ToString() + "\t");
                WriteLine();
            }
        }
        static void createArray(int[,] Matrix)
        {
            int row = Matrix.GetLength(0);
            int col = Matrix.GetLength(1);
            int i1 = row - 1;
            int j1 = 0;
            while (true)
            {
                if (i1 == row - 1 && j1 == 0)
                {
                    elem += "a[" + i1 + ", " + j1 + "] = " + Matrix[i1, j1] + "; ";
                    Maxvalue = Matrix[i1, j1];
                   
                }

                if (j1 % 2 == 0 && j1 < col - 1)
                    j1++;
                else
                    i1--;

                while (j1 >= 0 && i1 >= row / 2)
                {
                    if (Matrix[i1, j1] > Maxvalue)
                    {
                        Maxvalue = Matrix[i1, j1];
                    }
                    elem += "a[" + i1 + ", " + j1 + "] = " + Matrix[i1, j1] + "; ";

                    if (j1 == 0)
                    {
                        break;
                    }
                    i1--;
                    j1--;
                    if (i1 == row / 2 - 1)
                    {
                        i1++;
                        j1++;
                        break;
                    }
                    if (j1 == col)
                        break;
                }
                if (i1 > row / 2)
                    i1--;
                else
                    j1++;
                while (i1 < row && j1 < col)
                {
                    if (Matrix[i1, j1] > Maxvalue)
                    {
                        Maxvalue = Matrix[i1, j1];
                    }
                    elem += "a[" + i1 + ", " + j1 + "] = " + Matrix[i1, j1] + "; ";
                   
                    i1++;
                    j1++;
                    if (j1 == col || i1 == row)
                    {
                        j1--;
                        i1--;
                        break;
                    }
                }
                if (j1 == col)
                    break;
            }
            
            j1 = col - 1;
            i1 = row / 2 - 1;
           
            while (true)
            {
                if (j1 == col - 1)
                {
                    while (j1 >= 0)
                    {
                        if(Matrix[i1,j1] > Maxvalue)
                            maxelem += "a[" + i1 + ", " + j1 + "] = " + Matrix[i1,j1] + "; ";
                        elem += "a[" + i1 + ", " + j1 + "] = " + Matrix[i1, j1] + "; ";
                       
                        j1--;
                        if (j1 < 0)
                        {
                            j1++;
                            i1--;
                            break;
                        }
                    }
                }
                else
                {

                    while (j1 < col)
                    {
                        if (Matrix[i1, j1] > Maxvalue)
                            maxelem += "a[" + i1 + ", " + j1 + "] = " + Matrix[i1, j1] + "; ";
                        elem += "a[" + i1 + ", " + j1 + "] = " + Matrix[i1, j1] + "; ";
                       
                        j1++;
                        if (j1 == col)
                        {
                            j1--;
                            i1--;
                            break;
                        }
                    }
                }
                if (i1 == -1)
                    break;
            }            
        }
        static int[,] generateMatrix(int Row, int Col)
        {
           
                    Random rnd = new Random();
                    int[,] Matrix = new int[Row, Col];
                    for (long i = 0; i < Row; i++)
                        for (long j = 0; j < Col; j++)
                            Matrix[i, j] = rnd.Next(0, 100);
                    return Matrix;
        }
        static int[,] generatecontrolMatrix(int Row, int Col)
        {

            Random rnd = new Random();
            int[,] Matrix = new int[Row, Col];
            int count = 0;
            for (long i = 0; i < Row; i++)
                for (long j = 0; j < Col; j++)
                {
                    Matrix[i, j] = count;
                    count++;
                }
            return Matrix;
        }

    }
}
