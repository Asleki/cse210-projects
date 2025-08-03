using System;
using System.Threading;
using System.Collections.Generic; // Required for Dictionary

/*
 * W05 Project: Mindfulness Program - DigiHealth App
 *
 * This program implements a mindfulness application called "DigiHealth" that offers
 * various activities to help users relax and reflect. It also includes several
 * exceeding requirements to enhance user experience and personalization.
 *
 * Exceeding Core Requirements:
 * 1.  Enhanced Application Launch Sequence:
 * - Displays a "We are getting you started..." message with a simple dot animation
 * for a few seconds before the main menu appears. This provides a smoother
 * transition and visual feedback to the user.
 * 2.  Custom UI Elements & Styling:
 * - Utilizes a `UIHelper` static class to centralize console UI operations.
 * - Features bold blue header text and bold yellow top/bottom borders.
 * - Menu option numbers (e.g., "1.") are displayed in green.
 * - Special action options (e.g., "N. Next", "M. More") are displayed in yellow.
 * - A yellow separator `--------------------------` is consistently used before input prompts.
 * - Employs basic emojis (e.g., ✅, ❌, ℹ️) for visual feedback (success, error, info).
 * 3.  Comprehensive Breathing Activity Introduction:
 * - Before the breathing exercise starts, the user is offered a choice to "Proceed reading"
 * for a fully detailed explanation of breathing mechanics, factors, and tips.
 * - This detailed information is presented in paragraphs using a **typewriter animation** effect.
 * - After reading, the user can choose to "Read again" or "Go back" to the exercise.
 * 4.  Persistent User Profile & Settings:
 * - Introduces a new "Settings" menu option.
 * - Allows users to set and save their `Full Name`, `Username`, and manage `Addresses` and `Health Info`.
 * - `UserProfile` data (including addresses and health details) is **saved to and loaded from `user_profile.txt`**
 * at application start and when leaving the settings menu, providing persistence.
 * - Users can add/remove multiple addresses (categorized by type: Home, Work, School, Other).
 * - Users can log detailed health information: medical conditions, blood type, allergies, current medications, and organ donor status.
 * 5.  Interactive Mood Check-in Feature:
 * - Adds a new "Mood Check-in" activity in the main menu.
 * - Guides the user through a series of questions: "How are you feeling?", "Which emotions?", "Where are you now?", and "Anything else to add?".
 * - The "Emotions" section allows cycling through "M. More" options for a richer selection of feelings.
 * - The "Where are you now?" question integrates directly with the `UserProfile`'s saved addresses:
 * If "Home", "Work", or "School" is selected, and multiple addresses of that type are saved,
 * the app will list them and prompt the user to choose a specific address.
 * - All mood check-in data (including the optional note) is saved to `mood_notes.txt`,
 * providing a personal log over time.
 * 6.  User Session Control:
 * - Although not explicitly implemented as a separate "cancel" button everywhere due to console limitations,
 * the structure allows for graceful exits from sub-menus (e.g., "0. Back to Main Menu" in Settings)
 * and activities conclude automatically based on duration, returning to the main menu.
 * The primary exit is via the main menu's "Quit" option.
 *
 * This comprehensive design significantly enhances the core mindfulness program by adding
 * personalization, persistent data storage, expanded content, and a more interactive
 * and visually engaging user interface, demonstrating a strong understanding of
 * object-oriented principles and exceeding the basic requirements.
 */

class Program
{
    private static UserProfile _currentUserProfile; // Store the user profile globally

    static void Main(string[] args)
    {
        // --- Application Launch Sequence (Exceeding Requirements) ---
        Console.WriteLine("\n\n\n\n"); // Add some space for the initial animation
        Console.Write("          " + UIHelper.GetColoredText("We are getting you started", ConsoleColor.White));
        UIHelper.ShowSimpleDotAnimation(4); // Use the reliable dot animation for 4 seconds

        _currentUserProfile = new UserProfile(); // Initialize and load user profile at startup

        // --- Main Application Loop ---
        RunMainMenu();
    }

    // --- Main Menu Logic ---
    static void RunMainMenu()
    {
        string choice = "";
        while (choice != "6") // Option 6 is "Quit"
        {
            // --- REMOVED Console.Clear() from here ---
            UIHelper.DisplayHeader("Home");

            // Use UIHelper to print colored menu options
            Console.WriteLine(UIHelper.GetColoredText("1. Breathing Activity", ConsoleColor.Green));
            Console.WriteLine(UIHelper.GetColoredText("2. Reflection Activity", ConsoleColor.Green));
            Console.WriteLine(UIHelper.GetColoredText("3. Listing Activity", ConsoleColor.Green));
            Console.WriteLine(UIHelper.GetColoredText("4. Mood Check-in", ConsoleColor.Green)); // Exceeding Requirement
            Console.WriteLine(UIHelper.GetColoredText("5. Settings", ConsoleColor.Green));     // Exceeding Requirement
            Console.WriteLine(UIHelper.GetColoredText("6. Quit", ConsoleColor.Green));

            Console.WriteLine(UIHelper.GetColoredText("--------------------------", ConsoleColor.Yellow)); // Separator
            Console.Write("Select a choice from the menu: ");
            choice = Console.ReadLine();

            // Handle user choice by instantiating and running the selected activity/feature
            switch (choice)
            {
                case "1":
                    // --- Added Console.WriteLine("\n") for spacing instead of clearing ---
                    Console.WriteLine("\n");
                    BreathingActivity breathingActivity = new BreathingActivity();
                    breathingActivity.Run();
                    break;
                case "2":
                    Console.WriteLine("\n");
                    ReflectionActivity reflectionActivity = new ReflectionActivity();
                    reflectionActivity.Run();
                    break;
                case "3":
                    Console.WriteLine("\n");
                    ListingActivity listingActivity = new ListingActivity();
                    listingActivity.Run();
                    break;
                case "4":
                    Console.WriteLine("\n");
                    MoodCheckIn moodCheckIn = new MoodCheckIn(_currentUserProfile); // Pass the user profile
                    moodCheckIn.StartCheckIn();
                    break;
                case "5":
                    Console.WriteLine("\n");
                    _currentUserProfile.DisplaySettingsMenu(); // Open the settings menu
                    break;
                case "6":
                    Console.WriteLine("\nExiting DigiHealth. Goodbye!");
                    // _currentUserProfile.SaveProfile(); // Save profile one last time on exit if changes were made outside settings
                    break;
                default:
                    Console.WriteLine(UIHelper.GetColoredText("❌ Invalid choice. Please try again.", ConsoleColor.Red));
                    Thread.Sleep(2000); // Pause to show error message
                    break;
            }

            // Pause before returning to the main menu, unless quitting
            if (choice != "6")
            {
                Console.WriteLine("\nPress any key to return to menu...");
                Console.ReadLine();
                
            }
        }
    }
}