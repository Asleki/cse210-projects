// Program.cs
using System;
// The System.Collections.Generic namespace is not explicitly needed here for lists,
// but it's a common inclusion in C# projects.

// This is the main program class.
// Its primary role is to control the flow of the application,
// display the user menu, get user input, and coordinate actions
// between the Journal, Entry, and PromptGenerator classes.
class Program
{
    // The Main method is the entry point of the program, where execution begins.
    static void Main(string[] args)
    {
        // Create an instance of the Journal class. This object will manage
        // all the journal entries and handle saving/loading.
        Journal myJournal = new Journal();

        // Create an instance of the PromptGenerator class. This object
        // will provide random prompts for new entries.
        PromptGenerator promptGenerator = new PromptGenerator();

        // This variable holds the user's selected option from the menu.
        // The main program loop will continue as long as the user doesn't choose to quit.
        int choice = 0;
        while (choice != 5) // Assuming '5' is the 'Quit' option
        {
            // Display the main menu to the user, listing all available actions.
            Console.WriteLine("Please select one of the following choices:");
            Console.WriteLine("1. Write");
            Console.WriteLine("2. Display");
            Console.WriteLine("3. Load");
            Console.WriteLine("4. Save");
            Console.WriteLine("5. Quit");
            Console.Write("What would you like to do? ");

            // Read the user's input from the console.
            string input = Console.ReadLine();

            // Attempt to convert the user's input (which is a string) into an integer.
            // This is safer than direct conversion, preventing errors if non-numeric input is given.
            if (int.TryParse(input, out choice))
            {
                // A switch statement is used to execute different code blocks
                // based on the integer value of the user's choice.
                switch (choice)
                {
                    case 1: // Handles the "Write" option: creating a new journal entry.
                        // Get a random prompt from our prompt generator object.
                        string randomPrompt = promptGenerator.GetRandomPrompt();
                        Console.WriteLine($"Prompt: {randomPrompt}");
                        Console.Write("> "); // A visual cue for the user to type their response.
                        string userResponse = Console.ReadLine();

                        // Create a new Entry object to store this specific entry's data.
                        Entry newEntry = new Entry();
                        // Assign the current date, the prompt, and the user's response to the new entry.
                        newEntry._date = DateTime.Now.ToShortDateString(); // Capture today's date
                        newEntry._promptText = randomPrompt;
                        newEntry._entryText = userResponse;

                        // Add the newly created entry to our journal.
                        myJournal.AddEntry(newEntry);
                        break;

                    case 2: // Handles the "Display" option: showing all entries in the journal.
                        // Call the journal's method to display all its stored entries.
                        myJournal.DisplayAllEntries();
                        break;

                    case 3: // Handles the "Load" option: loading entries from a file.
                        Console.Write("What is the filename? ");
                        string loadFilename = Console.ReadLine();
                        // Call the journal's method to load entries, passing the filename.
                        myJournal.LoadFromFile(loadFilename);
                        break;

                    case 4: // Handles the "Save" option: saving current entries to a file.
                        Console.Write("What is the filename? ");
                        string saveFilename = Console.ReadLine();
                        // Call the journal's method to save entries, passing the filename.
                        myJournal.SaveToFile(saveFilename);
                        break;

                    case 5: // Handles the "Quit" option: exiting the program.
                        Console.WriteLine("Exiting program. Goodbye!");
                        break;

                    default: // Handles any valid number input that isn't a recognized menu option.
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                        break;
                }
            }
            else // Handles cases where the user's input was not a valid number at all.
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
            // Print a blank line after each operation for better visual separation in the console.
            Console.WriteLine();
        }
    }
}