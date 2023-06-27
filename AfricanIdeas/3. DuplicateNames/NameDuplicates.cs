using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrepsCS.AfricanIdea.DuplicateNames
{
    public class NameDuplicates
    {
        public static void FindDuplicates(List<string> names)
        {
            // HashSet to store unique names
            HashSet<string> uniqueNames = new HashSet<string>();
            // HashSet to store exact duplicates
            HashSet<string> duplicates = new HashSet<string>();
            // Dictionary to store potential misspellings
            Dictionary<string, List<string>> potentialDuplicates = new Dictionary<string, List<string>>(); 

            foreach (string name in names)
            {
                if (!uniqueNames.Contains(name))
                {
                    uniqueNames.Add(name);
                }
                else
                {
                    duplicates.Add(name);
                }
            }

            foreach (string name in uniqueNames)
            {
                // Normalize the name by removing spaces and converting to lowercase
                string normalizedName = NormalizeName(name); 

                if (!potentialDuplicates.ContainsKey(normalizedName))
                {
                    potentialDuplicates[normalizedName] = new List<string>();
                }

                potentialDuplicates[normalizedName].Add(name);
            }

            // Output exact duplicates
            Console.WriteLine("Exact duplicates:");
            foreach (string duplicate in duplicates)
            {
                Console.WriteLine(duplicate);
            }

            // Output potential misspellings
            Console.WriteLine("Potential misspellings:");
            foreach (var kvp in potentialDuplicates)
            {
                if (kvp.Value.Count > 1)
                {
                    Console.WriteLine(string.Join(", ", kvp.Value));
                }
            }
        }

        public static string NormalizeName(string name)
        {
            return name.Replace(" ", string.Empty).ToLower();
        }

        public static void Main(string[] args)
        {
            // Sample data
            List<string> names = new List<string>
            {
                "John Doe",
                "Jane Smith",
                "John Doe",
                "Jon Doe",
                "Jane Smtih",
                "Jan Smith"
            };

            FindDuplicates(names);
        }
    }
}
