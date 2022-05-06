using System;
using System.Collections;
using System.Collections.Generic;

namespace ASD_Lab_6
{
    class Exercise
    {
        static void Main(string[] args)
        {
            bool flag = true;

            while (flag)
            {

                Console.WriteLine("Converter of mathematical expressions in reverse Polish notation\n");

                Console.WriteLine("Press 1 if you want to see a control example\nPress 2 if you want to enter data from the keyboard yourself\n");

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1:

                        Console.Clear();

                        Example();

                        flag = false;

                        break;

                    case ConsoleKey.D2:

                        Console.Clear();

                        OwnOption();

                        flag = false;

                        break;

                    default:

                        Console.WriteLine("\n\nWrite 1 or 2!\n");

                        break;
                }
            }
        }

        private static void OwnOption()
        {
            bool flag = true;

            string example = "";

            while (flag)
            {
                Console.WriteLine("Write your mathematical example and press Enter to see it in reverse Polish notation:\n");

                example = Console.ReadLine();

                for (int i = 0; i < example.Length; i++)
                {
                    if ((int)example[i] < 32 || (((int)example[i] > 32 && (int)example[i] < 40)) || (int)example[i] == 44 || (int)example[i] == 46 || (((int)example[i] > 57 && (int)example[i] < 65)) || (((int)example[i] > 90 && (int)example[i] < 97)) || (int)example[i] > 122)
                    {
                        Console.WriteLine("\nInvalid input character!");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    }
                    else if (i > 0 && (((int)example[i] > 64 && (int)example[i] < 91 || (int)example[i] > 96 && (int)example[i] < 123) && ((int)example[i - 1] > 64 && (int)example[i - 1] < 91 || (int)example[i - 1] > 96 && (int)example[i - 1] < 123)))
                    {
                        Console.WriteLine("\nThere must be an arithmetic operation between Latin letters!");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    }
                    else if (i > 0 && ((int)example[i] == 40 && ((int)example[i - 1] == 32 || ((int)example[i - 1] >= 65 && (int)example[i - 1] <= 90) || ((int)example[i - 1] >= 97 && (int)example[i - 1] <= 122))))
                    {
                        Console.WriteLine("\nIn front \"(\" must be a sign of the arithmetic operation!");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    }
                        
                    if (i == example.Length - 1)
                        flag = false;
                }
            }

            Con_to_reverse_Pl_not(example);

        }

        static void Example()
        {
            string example = "25 - 43 * (a+b) * c + 234 - 54";

            Con_to_reverse_Pl_not(example);
        }

        static void Con_to_reverse_Pl_not(string example)
        {
            Console.Clear();

            Console.WriteLine("Example: " + example + "\n");

            List<string> postfix_form = new List<string>();

            Stack<string> stack = new Stack<string>();

            for (int i = 0; i < example.Length; i++)
            {
                if (example[i].ToString() != "*" && example[i].ToString() != "/" && example[i].ToString() != "+" && example[i].ToString() != "-" && example[i].ToString() != "(" && example[i].ToString() != ")" && example[i].ToString() != " ")
                {
                    if (Char.IsDigit(example[i]))
                    {
                        while (Char.IsDigit(example[i]))
                        {
                            postfix_form.Add(example[i].ToString());
                            if (i != example.Length - 1)
                                i++;
                            else
                            {
                                i++;
                                break;
                            }
                        }
                        i--;
                        postfix_form.Add(" ");
                    }
                    else
                    {
                        postfix_form.Add(example[i].ToString());
                        postfix_form.Add(" ");
                    }
                }
                else if (example[i].ToString() != " " && example[i].ToString() != ")")
                {
                    stack.Push(example[i].ToString());

                    stack.PrintStack();
                }
                else if (example[i].ToString() == ")")
                {
                    while (stack.Front() != "(")
                    {
                        postfix_form.Add(stack.Pop());

                        postfix_form.Add(" ");

                        stack.PrintStack();
                    }
                    stack.Pop();

                    stack.PrintStack();
                }
            }

            while (stack.Front() != null)
            {
                postfix_form.Add(stack.Pop());

                postfix_form.Add(" ");

                stack.PrintStack();
            }

            Console.WriteLine("\nReverse Polish notation:");

            foreach (var item in postfix_form)
            {
                Console.Write(item);
            }
        }
    }
}
