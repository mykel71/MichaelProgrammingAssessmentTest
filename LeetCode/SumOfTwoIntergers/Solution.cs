using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrepsCS.LeetCode.SumOfTwoIntergers
{
    public class Solution
    {
        public int GetSum(int a, int b)
        {
            while (b != 0)
            {
                // Calculate the carry using bitwise AND
                int carry = a & b;
                // Calculate the sum without considering carry using bitwise XOR
                a = a ^ b;
                // Shift the carry to the left by 1 bit
                b = carry << 1; 
            }

            return a;
        }
    }
}
