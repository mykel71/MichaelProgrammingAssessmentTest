using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrepsCS.LeetCode
{
    public class Solution
    {
        public int[] TwoSum(int[] nums, int target)
        {
            // Create a dictionary to store the complement value and its corresponding index
            Dictionary<int, int> complementIndexMap = new Dictionary<int, int>();

            // Traverse through the array
            for (int i = 0; i < nums.Length; i++)
            {
                int complement = target - nums[i];

                // If the complement value exists in the dictionary, return the indices
                if (complementIndexMap.ContainsKey(complement))
                {
                    return new int[] { complementIndexMap[complement], i };
                }

                // Add the current number and its index to the dictionary
                if (!complementIndexMap.ContainsKey(nums[i]))
                {
                    complementIndexMap.Add(nums[i], i);
                }
            }

            // No two numbers found that add up to the target
            return new int[0];
        }
    }
}
