// Program.cs
// This program prompts the user for their first and last name
// and then displays it back in the "last-name, first-name last-name" format.

using System; // Required for Console input/output operations

class Program
{
    static void Main(string[] args)
    {
        // Prompt the user for their first name
        Console.Write("What is your first name? ");
        // Read the first name from the console. Console.ReadLine() returns a string.
        string firstName = Console.ReadLine() ?? ""; // Use null-coalescing to handle potential null, defaulting to empty string

        // Prompt the user for their last name
        Console.Write("What is your last name? ");
        // Read the last name from the console.
        string lastName = Console.ReadLine() ?? ""; // Use null-coalescing to handle potential null, defaulting to empty string

        // Display the name back in the specified format using string interpolation.
        // The format is "Your name is last-name, first-name last-name."
        // Ensure precise spacing, comma, and period as shown in the examples.
        Console.WriteLine($"Your name is {lastName}, {firstName} {lastName}.");

        // Keep the console window open until the user presses Enter
        Console.WriteLine("\nPress Enter to exit.");
        Console.ReadLine();
    }
}
