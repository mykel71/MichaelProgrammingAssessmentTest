using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrepsCS.DynamicProgramming
{
    public class SolutionOne
    {
        public int ClimbStairs(int n)
        {
            if (n <= 2)
            {
                return n;
            }

            // Initialize the base cases for n = 1 and n = 2
            int prev1 = 1;
            int prev2 = 2;

            // Calculate the distinct ways for n > 2
            for (int i = 3; i <= n; i++)
            {
                int current = prev1 + prev2;
                prev1 = prev2;
                prev2 = current;
            }

            return prev2;
        }
    }
}
