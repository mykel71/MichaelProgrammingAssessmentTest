using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrepsCS.LeetCode.MissingNumber
{
    public class Solution
    {
        public int MissingNumber(int[] nums)
        {
            int n = nums.Length;
            // Calculate the sum of numbers from 0 to n using the formula n * (n + 1) / 2
            int expectedSum = n * (n + 1) / 2; 

            // Calculate the actual sum of the numbers in the array
            int actualSum = 0;
            foreach (int num in nums)
            {
                actualSum += num;
            }

            // The missing number is the difference between the expected sum and the actual sum
            return expectedSum - actualSum;
        }
    }
}
