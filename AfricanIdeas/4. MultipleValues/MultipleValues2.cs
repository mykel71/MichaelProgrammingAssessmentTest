using System;

public class MultiValueStorage
{
    // Define constants for different values
    private const int ValueA = 1;     // Represented by the first bit
    private const int ValueB = 1 << 1; // Represented by the second bit
    private const int ValueC = 1 << 2; // Represented by the third bit

    public static void Main(string[] args)
    {
        int storage = 0; // Initialize the storage integer

        // Add values to the storage
        storage |= ValueA; // Set the first bit to represent ValueA
        storage |= ValueC; // Set the third bit to represent ValueC

        // Check if values are present in the storage
        bool hasValueA = (storage & ValueA) != 0; // Check if the first bit is set
        bool hasValueB = (storage & ValueB) != 0; // Check if the second bit is set
        bool hasValueC = (storage & ValueC) != 0; // Check if the third bit is set

        // Output the results
        Console.WriteLine($"ValueA present: {hasValueA}");
        Console.WriteLine($"ValueB present: {hasValueB}");
        Console.WriteLine($"ValueC present: {hasValueC}");
    }
}
