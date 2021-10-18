using System;
using static System.Console;
using static System.Math;

namespace Exercise_3
{
    class Program
    {
        static void Main()
        {
            double x, y, z;
            double a, b;
            Write("x = ");
            x = Convert.ToDouble(ReadLine());
            Write("y = ");
            y = Convert.ToDouble(ReadLine());
            Write("z = ");
            z = Convert.ToDouble(ReadLine());
            if (Abs(x + z) == 0 || Abs(y - x) == 0 || Log(Abs(y - x)) == -2)
                WriteLine("а та б не iснує");
            else
            {
                a = Log10(Abs(x + z)) / (1 + Log(Abs(y - x)) / 2) + 2 * y;
                if (a == 0 || a + z <= 0 || x < 0)
                    WriteLine("a = " + a + "; b не iснує");
                else
                {
                   
                    b = Log(a + z) / (a * a) + Pow(x, -a);
                    WriteLine($"a = {a}; b = {b}");
                }
            }

        }

    }
}
