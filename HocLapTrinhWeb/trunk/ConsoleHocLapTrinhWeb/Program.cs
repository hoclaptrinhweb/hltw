using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleHocLapTrinhWeb
{
    public class Program
    {
        static void Main()
        {
            UseYield();
        }

        public static void UseYield()
        {
            //
            // Compute two with the exponent of 30.
            //
            foreach (int value in ComputePower(2, 30))
            {
                Console.Write(value);
                Console.Write(" ");
            }
            Console.WriteLine();
            Console.Read();

        }

        public static IEnumerable<int> ComputePower(int number, int exponent)
        {
            int exponentNum = 0;
            int numberResult = 1;
            //
            // Continue loop until the exponent count is reached.
            //
            while (exponentNum < exponent)
            {
                //
                // Multiply the result.
                //
                numberResult *= number;
                exponentNum++;
                //
                // Return the result with yield.
                //
                yield return numberResult;
            }
        }

       

    }
}
