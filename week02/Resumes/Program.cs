// Program.cs
using System;

// This is the main program class.
// Its primary role is to control the flow of the application,
// display the user menu, get user input, and coordinate actions
// between the Journal, Entry, PromptGenerator, and now, SecurityManager classes.

// ***************************************************************************************************
// EXCEEDING CORE REQUIREMENTS - DETAILED DESCRIPTION
// ***************************************************************************************************
// This program significantly exceeds core requirements by implementing a robust
// user privacy and security feature. This addresses a common barrier to digital
// journaling: the concern about unauthorized access to personal thoughts.
//
// Key features of this enhancement include:
//
// 1.  **First-Time User Setup:**
//     - Upon the very first run, the program detects that no security preferences
//       have been set.
//     - It then greets the new user with a welcome message and offers the option
//       to set up a privacy feature (PIN or Password).
//
// 2.  **Choice of Security Method:**
//     - Users can choose between two security methods:
//         - **PIN:** Requires only digits.
//         - **Password:** Requires a combination of letters, digits, and special characters
//           for stronger security.
//     - The program includes basic validation for these formats.
//
// 3.  **Security Code Verification:**
//     - During setup, the user must repeat their chosen PIN/Password to verify it,
//       preventing typos from locking them out.
//
// 4.  **Persistent Security Settings:**
//     - The chosen security code (PIN or Password) is saved to a dedicated file (`privacy.txt`).
//     - This file is intended to be excluded from version control (e.g., via .gitignore)
//       to ensure user privacy if the code is shared on platforms like GitHub.
//
// 5.  **Mandatory Access Control:**
//     - Once a security code is set, the program requires it for **initial access**
//       immediately upon startup.
//     - It also requires the security code for critical operations like **loading**
//       and **saving** the journal, ensuring that sensitive data is only accessed
//       by an authorized user.
//
// This comprehensive security approach provides users with peace of mind,
// encouraging more consistent and private journaling, thereby addressing
// a key psychological barrier to journal keeping.
// ***************************************************************************************************

class Program
{
    // The Main method is the entry point of the program, where execution begins.
    static void Main(string[] args)
    {
        // Create an instance of the SecurityManager. This object will handle
        // all password/PIN related operations and check for existing security settings.
        SecurityManager securityManager = new SecurityManager();

        // --- Initial Program Access and Security Setup Logic ---
        // Check if a password has already been set for this user/installation.
        if (securityManager.IsPasswordSet())
        {
            Console.WriteLine("Welcome back! Please enter your security code to access My DigiJournal.");
            // If a password is set, require immediate verification before proceeding to the menu.
            if (!securityManager.VerifyAccess())
            {
                // If verification fails, exit the program.
                Console.WriteLine("Exiting program due to incorrect security code.");
                return; // Exit the Main method
            }
        }
        else
        {
            // This is likely the first time the user runs the program (or privacy.txt is missing/empty).
            Console.WriteLine("HELLO New User, welcome to My DigiJournal!");
            Console.WriteLine("For improved security, you may choose to set a privacy feature.");
            Console.Write("Would you like to set up security now? (yes/no): ");
            string setupChoice = Console.ReadLine().ToLower();

            if (setupChoice == "yes")
            {
                securityManager.SetNewSecurity();
                // After setting new security, we still need to verify it for initial access.
                if (!securityManager.VerifyAccess())
                {
                    Console.WriteLine("Exiting program due to incorrect security code after setup.");
                    return; // Exit the Main method
                }
            }
            else
            {
                Console.WriteLine("Skipping security setup. You can set it later if you wish.");
            }
        }
        // --- End of Initial Program Access and Security Setup Logic ---


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
            Console.WriteLine("\nPlease select one of the following choices:");
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
                        // Assign the current date, the chosen prompt, and the user's response to the new entry.
                        newEntry._date = DateTime.Now.ToShortDateString(); // Capture today's date
                        newEntry._promptText = randomPrompt;
                        newEntry._entryText = userResponse;

                        // Add the newly created entry to our journal.
                        myJournal.AddEntry(newEntry);
                        break;

                    case 2: // Handles the "Display" option: showing all entries in the journal.
                        myJournal.DisplayAllEntries();
                        break;

                    case 3: // Handles the "Load" option: loading entries from a file.
                        // Before loading, verify the user's access with the security manager.
                        if (securityManager.VerifyAccess())
                        {
                            Console.Write("What is the filename? ");
                            string loadFilename = Console.ReadLine();
                            // Call the journal's method to load entries, passing the filename.
                            myJournal.LoadFromFile(loadFilename);
                        }
                        break;

                    case 4: // Handles the "Save" option: saving current entries to a file.
                        // Before saving, verify the user's access with the security manager.
                        if (securityManager.VerifyAccess())
                        {
                            Console.Write("What is the filename? ");
                            string saveFilename = Console.ReadLine();
                            // Call the journal's method to save entries, passing the filename.
                            myJournal.SaveToFile(saveFilename);
                        }
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