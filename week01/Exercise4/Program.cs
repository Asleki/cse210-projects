// Program.cs
// This program asks the user for a series of numbers, stores them in a list,
// and then computes their sum, average, largest number, smallest positive number,
// and displays the sorted list.

using System;
using System.Collections.Generic; // Required for List<T>
using System.Linq; // Required for Min() method (for smallest positive number)

class Program
{
    static void Main(string[] args)
    {
        List<int> numbers = new List<int>(); // Create a new list to store integers

        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        int userNumber = -1; // Initialize with a non-zero value to enter the loop

        // Loop to get numbers from the user until 0 is entered
        // A do-while loop could also be used here as the body needs to run at least once.
        while (userNumber != 0)
        {
            Console.Write("Enter number: ");
            string? userResponse = Console.ReadLine(); // Read user input as a string

            // Safely parse the input using int.TryParse
            // Handles cases where input is null, empty, whitespace, or not a valid integer.
            if (string.IsNullOrWhiteSpace(userResponse) || !int.TryParse(userResponse, out userNumber))
            {
                Console.WriteLine("Invalid input. Please enter a whole number.");
                continue; // Skip the rest of this loop iteration and ask for input again
            }

            // Only add the number to the list if it is not 0
            if (userNumber != 0)
            {
                numbers.Add(userNumber);
            }
        }

        // Check if the list is empty before performing calculations
        if (numbers.Count == 0)
        {
            Console.WriteLine("No numbers were entered. Cannot compute sum, average, or max.");
            Console.WriteLine("\nPress Enter to exit.");
            Console.ReadLine();
            return; // Exit the program if the list is empty
        }

        // --- Core Requirement 1: Compute the sum ---
        int sum = 0;
        foreach (int number in numbers)
        {
            sum += number;
        }
        Console.WriteLine($"The sum is: {sum}");

        // --- Core Requirement 2: Compute the average ---
        // Cast sum to float to ensure floating-point division
        float average = ((float)sum) / numbers.Count;
        Console.WriteLine($"The average is: {average}");

        // --- Core Requirement 3: Find the largest number (maximum) ---
        // Initialize max with the first element (assuming list is not empty, checked above)
        int max = numbers[0]; 
        foreach (int number in numbers)
        {
            if (number > max)
            {
                max = number; // Update max if a larger number is found
            }
        }
        Console.WriteLine($"The largest number is: {max}");

        // --- Stretch Challenge 1: Find the smallest positive number ---
        int smallestPositive = int.MaxValue; // Initialize with the largest possible integer value
        bool foundPositive = false; // Flag to check if any positive number was found

        foreach (int number in numbers)
        {
            if (number > 0 && number < smallestPositive)
            {
                smallestPositive = number; // Update if a smaller positive number is found
                foundPositive = true;
            }
        }

        if (foundPositive)
        {
            Console.WriteLine($"The smallest positive number is: {smallestPositive}");
        }
        else
        {
            Console.WriteLine("No positive numbers were entered.");
        }


        // --- Stretch Challenge 2: Sort the numbers and display the sorted list ---
        // Create a new list for sorting to avoid modifying the original list if not desired,
        // or sort the original list directly. Here, we sort the original.
        numbers.Sort(); // Sorts the list in ascending order

        Console.WriteLine("The sorted list is:");
        foreach (int number in numbers)
        {
            Console.WriteLine(number);
        }

        // Keep the console window open until the user presses Enter
        Console.WriteLine("\nPress Enter to exit.");
        Console.ReadLine();
    }
}
