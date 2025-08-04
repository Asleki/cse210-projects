// This program provides various mindfulness activities (Breathing, Reflection, Listing)
// to help users manage stress and practice mindfulness.
//
// Exceeding Requirements / Creativity Features:
// ---------------------------------------------
// This application goes beyond the core requirements in several significant ways 
//to enhance user experience and functionality:
//
// 1.  Comprehensive User Profile & Settings Management:
//     - UserProfile Class: A dedicated UserProfile class is 
//implemented to store personal information (name, username),
//         health details (blood type, organ donor, conditions, 
//allergies, medications), and multiple addresses.
//     - Address Class: A separate Address class is used to model different
// address types (Home, Work, School, etc.).
//     - Persistent Data Storage: User profile data is automatically saved to 
//and loaded from user_profile.json
//         using JSON serialization, ensuring user preferences and information
// persist across application sessions.
//     - Interactive Settings Menu: A full "Settings" option (menu choice '5')
// is added to the main menu,
//         allowing users to view, edit personal info, edit health info, and manage 
//(add / remove) their saved addresses.
//
// 2.  Mood Check-in Activity:
//     - New Activity Type: An entirely new "Mood Check-in" activity (menu choice '4')
// is integrated into the main menu.
//         This allows users to log their current mood, specific emotions, location,
// and add a personal note.
//     - Integration with User Profile: The Mood Check-in dynamically uses addresses
// saved in the UserProfile,
//         prompting users to select a specific saved address if they choose a location 
//type (e.g., 'Home', 'Work')
//         for which they have addresses stored.
//     - Persistent Mood Log: Mood check-in data is saved to a mood_log.txt file, providing a simple
//         text-based history of the user's emotional state over time.
//
// 3.  Enhanced UI and Animations:
//     - Animated Welcome Screen: A visually engaging simple dots animation
//  is displayed upon application startup,
//         providing a more polished and welcoming experience.
//     - Advanced Countdown Animation: For all countdowns (e.g., in Breathing Activity), 
//a clear clock emoji (🕰️)
//         is used before the seconds count, making the timer more intuitive and visually appealing.
//     - Typewriter Effect for Detailed Info: In the Breathing Activity, an optional
// "Read More" feature is added.
//         When selected, detailed educational content about breathing exercises is displayed with a
//         smooth, character-by-character "typewriter" animation, enhancing readability and engagement.
//     - Dotted Pause Animation: Simple, universal dotted animations (...) are used for general pauses, 
//providing
//         a clear indication of activity without requiring complex cursor manipulation.
//

// These features collectively aim to create a more personalized, informative, 
//and engaging mindfulness application.
//
using System;
using System.Threading;
using System.Text; // Required for Encoding

public class Program
{
    // The Main method serves as the entry point of the application.
    // It initializes core components and manages the main application loop.
    public static void Main(string[] args)
    {
        // Set console output encoding to UTF-8 to ensure proper display of special characters and emojis.
        Console.OutputEncoding = Encoding.UTF8;

        // Display the initial application header.
        UIHelper.PrintHeader("DigiHealth", "");

        // Show a welcome animation at the start of the application.
        // This is an exceeding requirement feature.
        UIHelper.AnimateWelcome(4); // Animates for 4 seconds

        // Display a message indicating the application is starting up.
        Console.WriteLine("\nWe are getting you started...");
        Thread.Sleep(2000); // Pause for 2 seconds

        // Initialize the UserProfile object and attempt to load saved profile data.
        // This supports persistent user settings, an exceeding requirement feature.
        UserProfile userProfile = new UserProfile();
        userProfile.LoadProfile();

        // Start the main menu loop, passing the userProfile for access in other features.
        RunMainMenu(userProfile);
    }

    // RunMainMenu manages the main application menu and user interaction.
    // It continuously displays options until the user chooses to quit.
    private static void RunMainMenu(UserProfile userProfile)
    {
        string choice = "";
        while (choice != "0")
        {
            // Display the main application header with "Home" as the current section.
            UIHelper.PrintHeader("DigiHealth", "Home");

            // Display all the available options in the main menu.
            UIHelper.PrintMainMenuOptions();

            // Prompt the user to enter their choice and read the input.
            UIHelper.PrintColor("Enter your choice: ", ConsoleColor.Yellow);
            choice = Console.ReadLine();

            // Use a switch statement to handle different user choices.
            switch (choice)
            {
                case "1":
                    // Option 1: Start the Breathing Activity.
                    BreathingActivity breathingActivity = new BreathingActivity();
                    breathingActivity.Run();
                    break;
                case "2":
                    // Option 2: Start the Reflection Activity.
                    ReflectionActivity reflectionActivity = new ReflectionActivity();
                    reflectionActivity.Run();
                    break;
                case "3":
                    // Option 3: Start the Listing Activity.
                    ListingActivity listingActivity = new ListingActivity();
                    listingActivity.Run();
                    break;
                case "4":
                    // Option 4: Start the Mood Check-in Activity.
                    // This is an exceeding requirement feature.
                    MoodCheckIn moodCheckIn = new MoodCheckIn(userProfile);
                    moodCheckIn.StartCheckIn();
                    break;
                case "5":
                    // Option 5: Display the Settings menu.
                    // This is an exceeding requirement feature, managed by UserProfile.
                    userProfile.DisplaySettingsMenu();
                    break;
                case "0":
                    // Option 0: Quit the application.
                    Console.WriteLine("Goodbye!");
                    break;
                default:
                    // Handle invalid input.
                    UIHelper.PrintColor("\nInvalid choice. Please enter a number from the menu.", ConsoleColor.Red);
                    UIHelper.ShowDottedPause(2); // Pause briefly with dots for user to read message
                    break;
            }

            // After an activity or invalid input (unless quitting), pause before redisplaying the menu.
            if (choice != "0")
            {
                Console.WriteLine("\n"); // Add some spacing
                UIHelper.PrintColor("Press Enter to continue to the main menu...", ConsoleColor.Yellow);
                Console.ReadLine(); // Wait for user to press Enter
            }
        }
    }
}