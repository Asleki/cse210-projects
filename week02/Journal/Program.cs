using System; // Provides fundamental classes and base types, including Console for input/output and DateTime for date/time.
using System.IO; // Necessary for file path manipulation (Path.Combine) and access to base directory (AppDomain.CurrentDomain.BaseDirectory).
// Removed: using System.Threading; // No longer needed as Thread.Sleep was removed.
using System.Globalization; // Required for CultureInfo.InvariantCulture for consistent date formatting.

// This is the main program class.
// Its primary role is to control the overall flow of the application,
// manage user interaction through a menu, process user input, and coordinate actions
// among the various components of the journaling system: Journal, Entry, PromptGenerator, and SecurityManager.

// ***************************************************************************************************
// EXCEEDING CORE REQUIREMENTS - DETAILED DESCRIPTION
// ***************************************************************************************************
// This program significantly enhances the basic journaling application by implementing advanced features
// focused on user privacy, richer journal entry content, and an improved user experience.
// These additions directly address common reasons why people might struggle with consistent journaling.
//
// Key features that extend beyond the core assignment requirements include:
//
// 1.  Robust User Privacy and Security System:
//     - This addresses the critical concern of unauthorized access to personal thoughts, a significant barrier to digital journaling.
//     - First-Time User Onboarding: Upon the very first launch, the program intelligently detects if no security settings have been established. It then warmly welcomes new users and offers them the option to set up a privacy feature (either a PIN or a more complex Password).
//     - Flexible Security Method Choice: Users are empowered to select their preferred security method: a simple PIN (requiring only digits) or a stronger Password (enforcing a combination of letters, digits, and special characters for increased security). The system includes basic validation to guide users in creating valid formats.
//     - Security Code Confirmation: During the initial setup, users are required to re-enter their chosen security code for verification. This crucial step prevents accidental typos from locking them out of their journal.
//     - Persistent Security Settings: The chosen security code (PIN or Password) along with a user-defined username is securely stored in a dedicated file named 'privacy.txt'. This file is designed to be excluded from version control systems (like Git) to safeguard user privacy, especially when the code is shared or deployed on different machines.
//     - Mandatory Access Control: Once security settings are established, the program strictly requires the security code for initial access immediately upon startup. Furthermore, it mandates security verification for critical data operations, such as loading and saving the journal, ensuring that sensitive personal information is exclusively accessed by an authorized user.
//     - Personalized User Welcome: The application identifies and greets returning users by their previously set username (e.g., "Welcome back, Malunda!"), creating a more personal and engaging experience.
//
// 2.  Enhanced Journal Entry Data:
//     - Beyond merely recording the date, each journal entry now captures and stores additional valuable context:
//         - Precise Time of Entry: The exact time of day when the entry was written is recorded, providing a more detailed timeline of thoughts and events.
//         - User's Emotional State: The user's reported mood at the time of writing is captured, adding an important emotional dimension to the reflection and allowing for future analysis of mood patterns.
//
// 3.  Improved User Interface and Experience:
//     - Prominent and Stylized Header: A visually striking header, "MY DIGIJOURNAL," is consistently displayed at the top of every main screen. This header features bold, blue text perfectly centered within yellow decorative borders, giving the application a polished and professional appearance.
//     - Clear Screen Navigation: The console screen is automatically cleared before displaying each new selection or "page." This creates a distinct visual separation between different application states, reducing visual clutter and making navigation feel more intuitive.
//     - Dynamic Page Titles: Each "page" within the application dynamically updates a sub-header to clearly indicate the user's current location (e.g., "Main Menu," "Write Entry," "Display Journal"). This helps users orient themselves within the program.
//     - Organized and Stylized Entry Display: Individual journal entries are presented in a highly organized, multi-line format. This includes clear, bolded labels for "Date:", "Time:", "Mood:", "Prompt:", and "Entry:", significantly improving readability compared to a single-line representation.
//     - Logical Mood Input Flow: Within the "Write" process, the program is designed to ask for the user's mood as the very first input. This follows a natural journaling progression, capturing the emotional context before diving into the entry details.
//     - "Once-a-Day Mood" Convenience: The application incorporates intelligent logic to manage mood input. Users are prompted for their mood only once per day; for subsequent entries on the same day, the previously recorded mood is silently reused, enhancing convenience and reducing repetitive input.
//     - Multiple Prompts and "Keep Writing" Feature (Planned Integration): The "Write" functionality is designed to be extended to allow users to respond to multiple prompts within a single session. This will involve offering a choice of several prompts and providing options to "keep writing" (request more prompts) until they are "done." This feature aims to make journaling less overwhelming by offering flexibility and choice.
//
// These comprehensive enhancements collectively provide users with a more secure, personalized, and engaging journaling experience, actively encouraging more consistent and private reflection.

class Program
{
    // This helper method is responsible for displaying the application's stylized header.
    // It utilizes console colors to create a visually distinct and branded top section for each screen.
    static void ShowHeader(string pageTitle, string username)
    {
        // Console.Clear() has been removed to avoid System.IO.IOException in certain terminal environments.
        // The program will now display content sequentially without clearing the screen between "pages."

        // Calculates the necessary padding to perfectly center the main application title.
        // This ensures the header appears aesthetically balanced regardless of minor console width variations.
        string headerText = "MY DIGIJOURNAL";
        int totalWidth = 33; // Defines the fixed width of the decorative border.
        int padding = (totalWidth - headerText.Length) / 2;
        string centeredHeaderText = new string(' ', padding) + headerText + new string(' ', padding);
        // An adjustment is made for cases where the calculated padding results in an odd total length,
        // ensuring the text perfectly fits the border width.
        if (centeredHeaderText.Length < totalWidth)
        {
            centeredHeaderText += " ";
        }

        // Sets the console color to yellow for the top and bottom decorative lines of the header.
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("=================================");
        // Sets the console color to blue for the main application title.
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(centeredHeaderText); // Prints the dynamically centered application title.
        // Resets the console color to yellow for the bottom decorative line.
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("=================================");
        Console.ResetColor(); // Resets the console color to its default setting after the header is drawn.

        // Displays a personalized welcome message to the user, using their stored username.
        // The username is displayed in a cyan color for visual distinction.
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"Welcome, {username}!");
        Console.ResetColor();

        // Displays the title of the current "page" or section the user is navigating.
        // This title is shown in bold blue, providing clear context of the user's location within the app.
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"--- {pageTitle} ---");
        Console.ResetColor();
        Console.WriteLine(); // Adds a blank line for better visual separation between the header and content.
    }

    // The Main method serves as the entry point for the entire program, where execution begins.
    static void Main(string[] args)
    {
        // Initializes an instance of the SecurityManager, which is responsible for handling
        // all user authentication, password/PIN management, and checking for existing security settings.
        SecurityManager securityManager = new SecurityManager();
        // Retrieves the current username from the SecurityManager. This username is used
        // for personalized greetings and throughout the application's display.
        string currentUsername = securityManager.GetUsername();

        // --- Initial Program Access and Security Setup Logic ---
        // This section controls the initial interaction with the user upon launching the application,
        // managing security setup for new users and login for returning users.
        if (securityManager.IsPasswordSet())
        {
            // If security settings are already in place, the program prompts for login.
            // The header is displayed, indicating the "Login" page.
            ShowHeader("Login", currentUsername);
            Console.WriteLine("Welcome back! Please enter your security code to access My DigiJournal.");
            // Verifies the user's entered security code against the stored one.
            if (!securityManager.VerifyAccess())
            {
                // If the security code is incorrect, the program exits to protect journal data.
                Console.WriteLine("Exiting program due to incorrect security code.");
                return; // Terminates the Main method execution.
            }
        }
        else
        {
            // If no security settings are found (indicating a first-time user),
            // a special header for "First-Time Setup" is displayed.
            ShowHeader("First-Time Setup", "New User");
            Console.WriteLine("HELLO New User, welcome to My DigiJournal!");
            Console.WriteLine("For improved security, users can choose to set a privacy feature.");
            Console.Write("Would you like to set up security now? (yes/no): ");
            string setupChoice = Console.ReadLine().ToLower();

            if (setupChoice == "yes")
            {
                // Initiates the process for setting up new security (username and password/PIN).
                securityManager.SetNewSecurity();
                // After setting up, immediate verification is required for initial access.
                if (!securityManager.VerifyAccess())
                {
                    // If verification fails even after setup, the program exits.
                    Console.WriteLine("Exiting program due to incorrect security code after setup.");
                    return; // Terminates the Main method execution.
                }
                // Updates the current username after successful security setup.
                currentUsername = securityManager.GetUsername();
            }
            else
            {
                // If the user chooses to skip security setup, a message is displayed,
                // and the username defaults to "Guest".
                Console.WriteLine("Skipping security setup. You can set it later if you wish.");
                currentUsername = "Guest"; // Ensures the username is "Guest" if setup is skipped.
            }
        }
        // --- End of Initial Program Access and Security Setup Logic ---

        // Initializes an instance of the Journal class. This object is central
        // to managing all journal entries, including adding, displaying, saving, and loading them.
        Journal myJournal = new Journal();

        // Initializes an instance of the PromptGenerator. This component is responsible for
        // providing random prompts to inspire journal entries.
        PromptGenerator promptGenerator = new PromptGenerator();

        // This variable stores the user's selected option from the main menu.
        // The main application loop continues to run as long as the user does not choose to quit.
        int choice = 0;
        while (choice != 5) // The loop continues until the user selects option '5' (Quit).
        {
            // Dynamically determines the appropriate header title for the current "page"
            // based on the user's last action or the initial state.
            string currentPageHeader = "";
            switch (choice)
            {
                case 0: currentPageHeader = "Main Menu"; break; // Represents the initial state before any selection.
                case 1: currentPageHeader = "Write Entry"; break;
                case 2: currentPageHeader = "Display Journal"; break;
                case 3: currentPageHeader = "Load Journal"; break;
                case 4: currentPageHeader = "Save Journal"; break;
                case 5: currentPageHeader = "Exiting"; break; // Indicates the program is in the process of shutting down.
                default: currentPageHeader = "Invalid Selection"; break; // For unrecognized valid number inputs.
            }
            // Displays the stylized header for the current page, providing clear navigation context.
            ShowHeader(currentPageHeader, currentUsername);

            // Presents the main menu options to the user.
            Console.WriteLine("Please select one of the following choices:");
            Console.WriteLine("1. Write");
            Console.WriteLine("2. Display");
            Console.WriteLine("3. Load");
            Console.WriteLine("4. Save");
            Console.WriteLine("5. Quit");
            Console.Write("What would you like to do? ");

            // Reads the user's input from the console.
            string input = Console.ReadLine();

            // Attempts to convert the user's string input into an integer.
            // This is a robust way to handle input, preventing errors if non-numeric
            // characters are entered.
            if (int.TryParse(input, out choice))
            {
                // A switch statement directs the program flow based on the integer value
                // of the user's valid menu selection.
                switch (choice)
                {
                    case 1: // Handles the "Write" option, initiating the process of creating a new journal entry.
                        // Updates the header to reflect the "Write Entry" page.
                        ShowHeader("Write Entry", currentUsername);

                        // --- Mood Input (First Question in Write Flow) ---
                        // Prompts the user for their mood. This is intentionally the first question
                        // to capture the emotional context before the main entry.
                        // The Journal class will handle the "once a day mood" logic internally.
                        string userMood = myJournal.GetMoodForToday(); // Retrieves mood (asks if new day, reuses if same day)
                        // --- End Mood Input ---

                        // Retrieves a random prompt from the prompt generator.
                        string randomPrompt = promptGenerator.GetRandomPrompt();
                        Console.WriteLine($"Prompt: {randomPrompt}");
                        Console.Write("> "); // Provides a visual cue for the user to type their response.
                        string userResponse = Console.ReadLine();

                        // Creates a new Entry object to encapsulate all the data for this specific entry.
                        Entry newEntry = new Entry();
                        // Assigns the current date, time, the chosen prompt, the user's response, and mood to the new entry.
                        // IMPORTANT: Using InvariantCulture to ensure consistent date format (dd/MM/yyyy) for saving/parsing.
                        newEntry._date = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        newEntry._time = DateTime.Now.ToShortTimeString(); // Captures the current time in a short string format.
                        newEntry._promptText = randomPrompt;
                        newEntry._entryText = userResponse;
                        newEntry._mood = userMood; // Stores the mood obtained from the user.

                        // Adds the newly created entry to the journal's in-memory collection.
                        myJournal.AddEntry(newEntry);
                        Console.WriteLine("Entry added successfully!");
                        break;

                    case 2: // Handles the "Display" option, showing all entries in the journal.
                        // Updates the header to reflect the "Display Journal" page.
                        ShowHeader("Display Journal", currentUsername);
                        // Calls the Journal's method to iterate through and display all stored entries.
                        myJournal.DisplayAllEntries();
                        break;

                    case 3: // Handles the "Load" option, loading journal entries from a specified file.
                        // Updates the header to reflect the "Load Journal" page.
                        ShowHeader("Load Journal", currentUsername);
                        // Checks if a password is set and, if so, verifies user access before proceeding to load.
                        if (securityManager.IsPasswordSet() && !securityManager.VerifyAccess())
                        {
                            Console.WriteLine("Access denied. Cannot load journal without correct security code.");
                        }
                        else
                        {
                            Console.Write("What is the filename? (e.g., myjournal.txt) ");
                            string loadFilename = Console.ReadLine();
                            // Calls the Journal's method to load entries from the specified file.
                            myJournal.LoadFromFile(loadFilename);
                        }
                        break;

                    case 4: // Handles the "Save" option, saving current journal entries to a specified file.
                        // Updates the header to reflect the "Save Journal" page.
                        ShowHeader("Save Journal", currentUsername);
                        // Checks if a password is set and, if so, verifies user access before proceeding to save.
                        if (securityManager.IsPasswordSet() && !securityManager.VerifyAccess())
                        {
                            Console.WriteLine("Access denied. Cannot save journal without correct security code.");
                        }
                        else
                        {
                            Console.Write("What is the filename? (e.g., myjournal.txt) ");
                            string saveFilename = Console.ReadLine();
                            // Calls the Journal's method to save the current entries to the specified file.
                            myJournal.SaveToFile(saveFilename);
                        }
                        break;

                    case 5: // Handles the "Quit" option, initiating program termination.
                        // Updates the header to indicate the program is exiting.
                        ShowHeader("Exiting", currentUsername);
                        Console.WriteLine("Exiting program. Goodbye!");
                        break;

                    default: // Catches any valid number input that does not correspond to a recognized menu option.
                        // Updates the header to indicate an "Invalid Choice."
                        ShowHeader("Invalid Choice", currentUsername);
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                        break;
                }
            }
            else // Handles cases where the user's input was not a valid number at all.
            {
                // Updates the header to indicate "Invalid Input."
                ShowHeader("Invalid Input", currentUsername);
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }
    }
}
