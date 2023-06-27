using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrepsCS.AfricanIdea.PrimeNumFinder
{
    public class PrimeNumFinder
    {
        public static bool IsPrime(int number)
        {
            if (number <= 1)
            {
                return false;
            }

            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public static int FindNthPrime(int n)
        {
            int primeCount = 0;
            int number = 2;

            while (primeCount < n)
            {
                if (IsPrime(number))
                {
                    primeCount++;
                }

                number++;
            }

            return number - 1;
        }

        public static void Main(string[] args)
        {
            int n = 10001;
            int nthPrime = FindNthPrime(n);

            Console.WriteLine($"The {n}th prime number is: {nthPrime}");
        }
    }
}
