// Program.cs
// This program demonstrates the use of functions in C#.
// It includes functions for displaying a welcome message,
// prompting for user name and favorite number,
// squaring a number, and displaying the final result.

using System; // Required for Console input/output operations

class Program
{
    // The Main function is the entry point of the program.
    static void Main(string[] args)
    {
        // Call the function to display the welcome message.
        DisplayWelcome();

        // Call the function to prompt for and get the user's name.
        string userName = PromptUserName();

        // Call the function to prompt for and get the user's favorite number.
        int userNumber = PromptUserNumber();

        // Call the function to square the user's number.
        int squaredNumber = SquareNumber(userNumber);

        // Call the function to display the final result.
        DisplayResult(userName, squaredNumber);

        // Keep the console window open until the user presses Enter.
        Console.WriteLine("\nPress Enter to exit.");
        Console.ReadLine();
    }

    // static void DisplayWelcome()
    // Purpose: Displays a welcome message to the console.
    // Parameters: None
    // Returns: void (nothing)
    static void DisplayWelcome()
    {
        Console.WriteLine("Welcome to the Program!");
    }

    // static string PromptUserName()
    // Purpose: Prompts the user for their name and returns it.
    // Parameters: None
    // Returns: string (the user's name)
    static string PromptUserName()
    {
        Console.Write("Please enter your name: ");
        // Read the user's input. Use null-coalescing to ensure a non-null string is returned.
        string name = Console.ReadLine() ?? "";
        return name;
    }

    // static int PromptUserNumber()
    // Purpose: Prompts the user for their favorite number and returns it as an integer.
    // Parameters: None
    // Returns: int (the user's favorite number)
    static int PromptUserNumber()
    {
        int number;
        while (true) // Loop until valid input is received
        {
            Console.Write("Please enter your favorite number: ");
            string? numberInput = Console.ReadLine(); // Read input, explicitly nullable

            // Safely parse the input using int.TryParse.
            // If parsing fails, inform the user and loop again.
            if (string.IsNullOrWhiteSpace(numberInput) || !int.TryParse(numberInput, out number))
            {
                Console.WriteLine("Invalid input. Please enter a whole number.");
            }
            else
            {
                break; // Exit the loop if input is valid
            }
        }
        return number;
    }

    // static int SquareNumber(int number)
    // Purpose: Accepts an integer and returns its square.
    // Parameters:
    //   number: An integer to be squared.
    // Returns: int (the squared number)
    static int SquareNumber(int number)
    {
        int square = number * number;
        return square;
    }

    // static void DisplayResult(string name, int square)
    // Purpose: Displays the user's name and the squared number.
    // Parameters:
    //   name: The user's name (string).
    //   square: The squared number (integer).
    // Returns: void (nothing)
    static void DisplayResult(string name, int square)
    {
        Console.WriteLine($"{name}, the square of your number is {square}");
    }
}
