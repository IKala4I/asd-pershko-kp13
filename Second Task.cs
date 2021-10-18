using System;
using static System.Console;

namespace Block___diagram
{
    class Program
    {
        static void Main()
        {
            int n, m;
            Write("n = ");
            n = Convert.ToInt32(ReadLine());
            Write("m = ");
            m = Convert.ToInt32(ReadLine());
            int s1 ;
            int s2 ;
            for (int ch = n; ch <= m; ch++)
            {
                s1 = 0;
                for (int i = 1; i < ch; i++)
                {
                    if (ch % i == 0)
                        s1 += i;
                }
               
                s2 = 0;
                for (int k = 1; k < s1; k++)
                {
                    if (s1 % k == 0)
                        s2 += k;
                }
                if (s2 == ch && s1 != ch && s1 > ch)
                    WriteLine($"Числа {ch} та {s1} дружнi");

            }
        }
    }
}
