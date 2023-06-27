using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrepsCS.LeetCode.Arrays
{
    public class Solution
    {
        public bool ContainsDuplicate(int[] nums)
        {
            // Create a HashSet to store unique numbers
            HashSet<int> uniqueNums = new HashSet<int>();

            // Traverse through the array
            foreach (int num in nums)
            {
                // If the number already exists in the HashSet, it's a duplicate
                if (uniqueNums.Contains(num))
                {
                    return true;
                }

                // Add the number to the HashSet
                uniqueNums.Add(num);
            }

            // No duplicates found
            return false;
        }
    }
}
