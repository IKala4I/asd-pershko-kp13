using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;


namespace Lab_5_ASD
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array;

            bool flag = true;

            while (flag)
            {
                WriteLine("Select the array creation mode to sort\nPress 1 if you want to create your own\nPress 2 if you want the array to be generated automatically\nPress 3 if you want to see a control example, M = 4");

                switch (ReadKey().Key)
                {
                    case ConsoleKey.D1:

                        Clear();

                        int count;

                        WriteLine("Write how many elements should be in the array:");

                        while (!int.TryParse(ReadLine(), out count) || (count % 2 != 0) || (count < 5))
                        {
                            Write("wrong count, try one more time. The number must be even and > 5: ");
                        }

                        Clear();

                        int M;

                        WriteLine("Write a number to sort by inserting a sequence that is less than this length:");

                        while (!int.TryParse(ReadLine(), out M) || (M < 2) || (M > count / 2))
                        {
                            Write("wrong numb, try one more time. The number must be > 2 and < count / 2 : ");
                        }

                        array = ReadArray(count);

                        Print_your_array(array);

                        array =  QuickSort(array, M);

                        Print_sorted_array(array);

                        flag = false;

                        break;

                    case ConsoleKey.D2:

                        Clear();

                        WriteLine("Write how many elements should be in the array:");

                        while (!int.TryParse(ReadLine(), out count) || (count % 2 != 0) || (count < 5))
                        {
                            Write("wrong count, try one more time. The number must be even and > 5 : ");
                        }

                        Clear();

                        WriteLine("Write a number to sort by inserting a sequence that is less than this length:");

                        while (!int.TryParse(ReadLine(), out M) || (M < 2) || (M > count / 2))
                        {
                            Write("wrong numb, try one more time. The number must be > 2 and < count / 2: ");
                        }

                        array = GenerateArray(count);

                        Print_your_array(array);

                        array = QuickSort(array, M);

                        Print_sorted_array(array);

                        flag = false;

                        break;

                    case ConsoleKey.D3:

                        Clear();

                        array = GetArray();

                        Print_your_array(array);

                        array = QuickSort(array, 4);

                        Print_sorted_array(array);

                        flag = false;

                        break;

                    default:

                        WriteLine("\nWrite 1 or 2!\n");

                        break;
                }
            }
        }

        private static int[] GenerateArray(int count)
        {
            Random rnd = new Random();

            List<int> list = new List<int>();
            List<int> even_numbers = new List<int>();
            List<int> odd_numbers = new List<int>();

            for (int i = -200, k = -199; i <= 200 & k <= 199; i += 2, k += 2)
            {
                even_numbers.Add(i);

                odd_numbers.Add(k);
            }

            int[] array = new int[count];

            int counter = 0;

            while (counter < count)
            {
                int k = rnd.Next(even_numbers.Count);

                int j = rnd.Next(odd_numbers.Count);

                list.Add(even_numbers[k]);

                list.Add(odd_numbers[j]);

                counter += 2;

                if (list.Distinct().ToArray().Length != counter)
                {
                    list.Clear();

                    counter = 0;
                }

            }

            for (int i = 0; i < list.Count; i++)
            {
                array[i] = list[i];
            }

            return array;
        }

        private static int[] ReadArray(int count)
        {
            Clear();

            List<int> list = new List<int>();

            int[] array = new int[count];

            int counter = 0;

            while (counter < count)
            {
                WriteLine($"Write {counter + 1} even number and press Enter:");

                int number;

                while (!int.TryParse(ReadLine(), out number) || (number % 2 != 0))
                {
                    Write("wrong number, try one more time. The number must be even : ");
                }

                list.Add(number);

                WriteLine();

                WriteLine($"Write {counter + 2} odd number and press Enter:");

                int number2;

                while (!int.TryParse(ReadLine(), out number2) || (number2 % 2 == 0))
                {
                    Write("wrong number, try one more time. The number must be odd: ");
                }

                list.Add(number2);

                counter += 2;

                if (list.Distinct().ToArray().Length != counter)
                {

                    list.Clear();

                    WriteLine("values cannot be repeated!");

                    ReadLine();

                    Clear();

                    counter = 0;
                }

                WriteLine();
            }

            for (int i = 0; i < list.Count; i++)
            {
                array[i] = list[i];
            }
            return array;
        }

        static int[] QuickSort(int[] array, int M)
        {
            int[] even_numb = new int[array.Length / 2];
            int[] odd_numb = new int[array.Length / 2];

            int[] arr = new int[array.Length];

            int counter_e = 0, counter_o = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (i % 2 == 0)
                {
                    even_numb[counter_e] = array[i];

                    counter_e++;
                }
                else if (i % 2 == 1)
                {
                    odd_numb[counter_o] = array[i];

                    counter_o++;
                }
            }

            even_numb = QuickSort_numbers(0, even_numb.Length - 1, even_numb, M);

            odd_numb = QuickSort_numbers(0, odd_numb.Length - 1, odd_numb, M, false);

            for (int i = 0; i < array.Length; i++)
            {
                if (i < odd_numb.Length)
                    arr[i] = odd_numb[i];
                else
                    arr[i] = even_numb[i - even_numb.Length];
            }

            return arr;
        }

        private static int[] QuickSort_numbers(int start, int end, int[] array, int M, bool flag = true)
        {
            WriteLine("\n\nquick sort even numbers (Goar split)");

            int X = array[(start + end) / 2];

            int i = start; int j = end;

            while (i <= j)
            {
                if (flag)
                {
                    while (array[i] < X)
                        i++;

                    while (array[j] > X)
                        j--;
                }
                else
                {
                    while (array[i] > X)
                        i++;

                    while (array[j] < X)
                        j--;
                }

                if (i <= j)
                {
                    Print_goara(start, end, i, j, array, ConsoleColor.Red);

                    int temp = array[i];

                    array[i] = array[j];

                    array[j] = temp;

                    Print_goara(start, end, i, j, array, ConsoleColor.Blue);

                    i++; j--;

                }
            }
            if (start < j && ((j - start + 1) >= M))
                QuickSort_numbers(start, j, array, M, flag);
            else
            {
                if (flag)
                {
                    Sorting_insert_numbers(start, j, array);
                }
                else
                {
                    Sorting_insert_numbers(start, j, array, false);
                }
            }

            if (i < end && ((end - i + 1) >= M))
                QuickSort_numbers(i, end, array, M, flag);
            else
            {
                if (flag)
                {
                    Sorting_insert_numbers(i, end, array);
                }
                else
                {
                    Sorting_insert_numbers(i, end, array, false);
                }
            }

            return array;
        }

        static void Sorting_insert_numbers(int start, int end, int[] array, bool flag2 = true)
        {
            int tmp = 0;

            bool flag = false;

            for (int i = start + 1; i <= end; i++)
            {
                int counter = 0;

                tmp = array[i];

                int j = i - 1;
                if (flag2)
                {
                    while (j >= start && array[j] > tmp)
                    {
                        flag = true;

                        if (counter == 0)
                        {
                            WriteLine("\n\n Sorting insert");

                            Print_insert(start, end, j + 1, array, ConsoleColor.Green);
                        }

                        array[j + 1] = array[j];

                        j--;

                        counter++;
                    }
                }
                else
                {
                    while (j >= start && array[j] < tmp)
                    {
                        flag = true;

                        if (counter == 0)
                        {
                            WriteLine("\n\n Sorting insert");

                            Print_insert(start, end, j + 1, array, ConsoleColor.Green);
                        }

                        array[j + 1] = array[j];

                        j--;

                        counter++;
                    }
                }

                array[j + 1] = tmp;

                if(flag)
                    Print_insert(start,end, j + 1, array, ConsoleColor.Yellow);

                flag = false;
            }
        }

        private static int[] GetArray()
        {
            int[] array = { 2, 3, -150, 7, -4, -23, -24, 95, 98, -135, 56, 123, 142, 99 };
            return array;
        }
        private static void Print_goara(int start, int end, int i, int j, int[] array, ConsoleColor cl)
        {
            WriteLine();

            for (int k = start; k <= end; k++)
            {
                if (k == i || k == j)
                {
                    ForegroundColor = cl;
                    Write(array[k] + "; ");
                    ForegroundColor = ConsoleColor.White;
                }
                else
                    Write(array[k] + "; ");
            }
            
        }
        private static void Print_insert(int start, int end, int j, int[] array, ConsoleColor cl)
        {
            WriteLine();

            for (int k = start; k <= end; k++)
            {
                if (k == j)
                {
                    ForegroundColor = cl;
                    Write(array[k] + "; ");
                    ForegroundColor = ConsoleColor.White;
                }
                else
                    Write(array[k] + "; ");
            }
        }
        private static void Print_sorted_array(int[] array)
        {

            WriteLine("\n\nSorted array:");

            foreach (var item in array)
            {
                Write(item + " ");
            }
        }

        private static void Print_your_array(int[] array)
        {
            Clear();

            WriteLine("Your array:");

            foreach (var item in array)
            {
                Write(item + " ");
            }

            Write("\n\nRed and Green - before swap\nBlue and Yellow - after swap");
        }
    }
}
