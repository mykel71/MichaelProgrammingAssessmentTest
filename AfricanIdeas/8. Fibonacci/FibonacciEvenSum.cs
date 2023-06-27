using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrepsCS.AfricanIdea.Fibonacci
{
    public class FibonacciEvenSum
    {
        public static int CalculateFibonacciEvenSum(int limit)
        {
            int sum = 0;
            int prevTerm = 1;
            int currTerm = 2;

            while (currTerm <= limit)
            {
                if (currTerm % 2 == 0)
                {
                    sum += currTerm;
                }

                int nextTerm = prevTerm + currTerm;
                prevTerm = currTerm;
                currTerm = nextTerm;
            }

            return sum;
        }

        public static void Main(string[] args)
        {
            int limit = 4000000;
            int evenSum = CalculateFibonacciEvenSum(limit);

            Console.WriteLine($"Sum of even-valued terms in the Fibonacci sequence below {limit}: {evenSum}");
        }
    }
}
