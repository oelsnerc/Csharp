using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PimeNumbers
{
    class Program
    {
        static bool Test(uint Number)
        {
            if ((Number % 2) == 0) return false;
            uint cnt = Number / 2;
            for (uint n = 3; n < Number; n += 2)
                if (Number % n == 0) return false;
            return true;
        }

        static void Main(string[] args)
        {
            uint Count = 0;
            uint Sum = 0;
            for (uint Number = 3; Number < 500000; Number++)
                if (Test(Number))
                {
                    // Console.Write(Number);
                    // Console.Write(' ');
                    Sum += Number;
                    Count++;
                };
            Console.Write(Count);
            Console.Write(' ');
            Console.WriteLine(Sum);
        }
    }
}
