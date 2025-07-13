// Program.cs
// This program implements the "Guess My Number" game.
// It includes core requirements for loops and random numbers,
// as well as stretch challenges for guess counting and multiple rounds.

using System; // Required for Console input/output and Random class

class Program
{
    static void Main(string[] args)
    {
        // Variable to control the outer loop for playing multiple rounds (Stretch Challenge 2)
        string playAgain = "yes";

        // Outer loop: Keep playing as long as the user wants to
        while (playAgain.ToLower() == "yes")
        {
            // --- Core Requirement 3: Generate a random magic number ---
            Random randomGenerator = new Random();
            // Generates a random number between 1 (inclusive) and 101 (exclusive),
            // effectively giving a range from 1 to 100.
            int magicNumber = randomGenerator.Next(1, 101);

            int guess = -1; // Initialize guess to a value that won't match magicNumber initially
            int guessCount = 0; // --- Stretch Challenge 1: Initialize guess counter ---

            Console.WriteLine("\n--- Welcome to Guess My Number! ---");
            Console.WriteLine("I'm thinking of a number between 1 and 100.");

            // --- Core Requirement 2: Add a loop that keeps looping as long as the guess does not match ---
            while (guess != magicNumber)
            {
                Console.Write("What is your guess? ");
                string? guessInput = Console.ReadLine(); // Read input, explicitly nullable

                // Increment guess count for each valid attempt (Stretch Challenge 1)
                guessCount++; 

                // Safely parse the input using int.TryParse
                // This handles invalid input (non-numbers, empty strings) gracefully.
                if (string.IsNullOrWhiteSpace(guessInput) || !int.TryParse(guessInput, out guess))
                {
                    Console.WriteLine("Invalid input. Please enter a whole number.");
                    // Decrement guessCount because this was not a valid guess attempt
                    guessCount--; 
                    continue; // Skip the rest of this loop iteration and ask for input again
                }

                // --- Core Requirement 1: Use if statements to determine higher/lower/guessed ---
                if (magicNumber > guess)
                {
                    Console.WriteLine("Higher");
                }
                else if (magicNumber < guess)
                {
                    Console.WriteLine("Lower");
                }
                else // guess == magicNumber
                {
                    // --- Core Requirement 1 & Stretch Challenge 1: Inform of guess count ---
                    Console.WriteLine($"You guessed it! It took you {guessCount} guesses.");
                }
            }

            // --- Stretch Challenge 2: Ask if the user wants to play again ---
            Console.Write("Do you want to play again? (yes/no): ");
            // Read input, convert to lowercase for consistent comparison,
            // and use null-coalescing to default to "no" if input is null.
            playAgain = Console.ReadLine()?.ToLower() ?? "no"; 
        }

        Console.WriteLine("\nThanks for playing! Goodbye.");
        // Keep the console window open until the user presses Enter
        Console.WriteLine("Press Enter to exit.");
        Console.ReadLine();
    }
}
