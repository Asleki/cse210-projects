// Program.cs
// This program calculates a letter grade based on a percentage input
// and includes logic for pass/fail and grade modifiers (+/-).

using System; // Required for Console input/output operations

class Program
{
    static void Main(string[] args)
    {
        // --- Core Requirements ---

        // 1. Ask the user for their grade percentage
        Console.Write("Please enter your grade percentage: ");
        // Read the user's input as a string. Console.ReadLine() can return null,
        // so we handle that possibility to avoid warnings and potential errors.
        string? gradeInput = Console.ReadLine(); 

        int gradePercentage;
        // Attempt to parse the input string into an integer.
        // int.TryParse is safer than int.Parse as it doesn't throw an exception
        // if the input is not a valid number or is null.
        if (string.IsNullOrWhiteSpace(gradeInput) || !int.TryParse(gradeInput, out gradePercentage))
        {
            Console.WriteLine("Invalid input. Please enter a valid number for your grade percentage.");
            // Exit the application if the input is invalid.
            // In a more complex application, you might loop and ask again.
            Console.WriteLine("\nPress any key to exit.");
            Console.ReadLine(); // Changed from Console.ReadKey()
            return; 
        }

        // Create a variable to store the letter grade
        string letter = "";

        // 2. Determine the letter grade using if-else if-else statements
        if (gradePercentage >= 90)
        {
            letter = "A";
        }
        else if (gradePercentage >= 80)
        {
            letter = "B";
        }
        else if (gradePercentage >= 70)
        {
            letter = "C";
        }
        else if (gradePercentage >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        // 3. Determine if the user passed the course
        // Assume that you must have at least a 70 to pass the class.
        if (gradePercentage >= 70)
        {
            Console.WriteLine("Congratulations! You passed the course.");
        }
        else
        {
            Console.WriteLine("Don't worry, you'll get it next time! Keep practicing.");
        }

        // --- Stretch Challenges ---

        // Variable to store the sign (+, -, or empty string)
        string sign = "";

        // Only apply signs if the grade is not F
        // and not an A grade that would result in A+ (handled later)
        if (gradePercentage < 97 && gradePercentage >= 60) // Grades B, C, D, and A-
        {
            // Get the last digit of the grade percentage
            int lastDigit = gradePercentage % 10;

            // Determine the sign based on the last digit
            if (lastDigit >= 7)
            {
                sign = "+";
            }
            else if (lastDigit < 3)
            {
                sign = "-";
            }
            // Otherwise, sign remains an empty string (no sign)
        }

        // Handle exceptional cases:
        // Recognize that there is no A+ grade, only A and A-.
        // If the letter is A and the grade percentage is 97 or higher,
        // it should just be an A, not A+.
        if (letter == "A" && gradePercentage >= 97)
        {
            sign = ""; // No A+
        }

        // Similarly, recognize that there is no F+ or F- grades, only F.
        if (letter == "F")
        {
            sign = ""; // F grades have no +/- sign
        }

        // 4. Single print statement for the letter grade (and sign)
        // This combines the letter and the sign, if any.
        Console.WriteLine($"Your letter grade is: {letter}{sign}");

        // Keep the console window open until a key is pressed
        Console.WriteLine("\nPress any key to exit.");
        Console.ReadLine(); // Changed from Console.ReadKey()
    }
}
