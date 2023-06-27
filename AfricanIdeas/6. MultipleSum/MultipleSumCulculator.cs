using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrepsCS.AfricanIdea.MultipleSum
{
    public class MultipleSumCulculator
    {
        public static int CalculateMultiplesSum(int limit)
        {
            int sum = 0;

            for (int i = 1; i < limit; i++)
            {
                if (i % 3 == 0 || i % 5 == 0)
                {
                    sum += i;
                }
            }

            return sum;
        }

        public static void Main(string[] args)
        {
            int limit = 1000;
            int sum = CalculateMultiplesSum(limit);

            Console.WriteLine($"Sum of multiples of 3 or 5 below {limit}: {sum}");
        }
    }
}
