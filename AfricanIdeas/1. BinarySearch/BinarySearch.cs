using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrepsCS.AfricanIdea.BinarySearch
{
    public  class BinarySearch
    {
        public static int PhoneSearch(string[] names, string target)
        {
            int left = 0;
            int right = names.Length - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                int result = string.Compare(names[mid], target);

                if (result == 0)
                    return mid;

                if (result < 0)
                    left = mid + 1;
                else
                    right = mid - 1;
            }
            // Target name not found
            return -1; 
        }

        public static string FindPhoneNumber(string[] names, string[] phoneNumbers, string targetName)
        {
            int index = PhoneSearch(names, targetName);

            if (index != -1)
                return phoneNumbers[index];

            // Target name not found
            return null; 
        }

        public static void Main()
        {
            // Assuming the names and phoneNumbers arrays are populated with data

            // Example data:
            string[] names = { "Shiloh", "Mael", "Zoe", "Michael", /* ... */ };
            string[] phoneNumbers = { "123-456-7890", "987-654-3210", "555-123-4567", "111-222-3333", /* ... */ };

            string targetName = "Zoe";
            string phoneNumber = FindPhoneNumber(names, phoneNumbers, targetName);

            if (phoneNumber != null)
                Console.WriteLine($"Phone number of {targetName}: {phoneNumber}");
            else
                Console.WriteLine($"{targetName} not found in the phone directory.");
        }
    }
}
