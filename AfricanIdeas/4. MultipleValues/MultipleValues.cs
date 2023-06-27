using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrepsCS.AfricanIdea.MultipleValues
{
    public class MultipleValues
    {
        // Define constants for value positions
        // Least significant bits
        private const int Value1Position = 0;
        // Number of bits for Value 1
        private const int Value1Length = 8;
        // Next bits after Value 1
        private const int Value2Position = Value1Length;
        // Number of bits for Value 2
        private const int Value2Length = 12;
        // Next bits after Value 2
        private const int Value3Position = Value1Length + Value2Length;
        // Number of bits for Value 3
        private const int Value3Length = 12;
        // Maximum value that can be stored for Value 1
        private const int MaxValue1 = (1 << Value1Length) - 1;
        // Maximum value that can be stored for Value 2
        private const int MaxValue2 = (1 << Value2Length) - 1;
        // Maximum value that can be stored for Value 3
        private const int MaxValue3 = (1 << Value3Length) - 1; 

        public static void StoreValues(ref int storage, int value1, int value2, int value3)
        {
            // Perform bitwise operations to store values in the integer
            int storedValue1 = value1 & MaxValue1;
            int storedValue2 = (value2 & MaxValue2) << Value2Position;
            int storedValue3 = (value3 & MaxValue3) << Value3Position;

            storage = storedValue1 | storedValue2 | storedValue3;
        }

        public static void RetrieveValues(int storage, out int value1, out int value2, out int value3)
        {
            // Retrieve values from the stored integer using bitwise operations
            value1 = storage & MaxValue1;
            value2 = (storage >> Value2Position) & MaxValue2;
            value3 = (storage >> Value3Position) & MaxValue3;
        }

        public static void Main(string[] args)
        {
            int storage = 0;

            int value1 = 100;
            int value2 = 200;
            int value3 = 300;

            StoreValues(ref storage, value1, value2, value3);
            RetrieveValues(storage, out int retrievedValue1, out int retrievedValue2, out int retrievedValue3);

            Console.WriteLine($"Stored Values: {retrievedValue1}, {retrievedValue2}, {retrievedValue3}");
        }
    }
}
