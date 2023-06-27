using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrepsCS.DynamicProgramming
{
    public class SolutionTwo
    {
        public int LengthOfLIS(int[] nums)
        {
            int n = nums.Length;
            int[] dp = new int[n]; // dp array to store the length of the longest increasing subsequence ending at each index
            int maxLength = 1; // Initialize the maximum length to 1

            // Initialize the dp array with 1, as the minimum length of any subsequence is 1
            for (int i = 0; i < n; i++)
            {
                dp[i] = 1;
            }

            // Calculate the length of the longest increasing subsequence for each index
            for (int i = 1; i < n; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (nums[i] > nums[j])
                    {
                        dp[i] = Math.Max(dp[i], dp[j] + 1);
                        maxLength = Math.Max(maxLength, dp[i]);
                    }
                }
            }

            return maxLength;
        }
    }
    
}
