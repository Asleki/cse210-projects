using System;
using System.IO; // Required for file operations
using System.Collections.Generic; // Required for List

// Class to handle the Mood Check-in activity.
// This is an exceeding requirement feature, allowing users to log their mood.
class MoodCheckIn
{
    // Private field to store a reference to the user's profile.
    private UserProfile _userProfile;

    // Constructor to initialize MoodCheckIn with the current user profile.
    public MoodCheckIn(UserProfile userProfile)
    {
        _userProfile = userProfile;
    }

    // Starts the mood check-in process, guiding the user through several steps.
    public void StartCheckIn()
    {
        UIHelper.PrintHeader("Mood Check-in", "Start");
        Console.WriteLine("Let's check in with how you're feeling today.");

        // Step 1: Get user's general feeling.
        string feeling = GetFeeling();

        // Step 2: Get specific emotions.
        List<string> emotions = GetEmotions();

        // Step 3: Get location.
        string location = GetLocation();
        string specificAddress = ""; // To store specific address if chosen

        // If location is home, work, or school, prompt for specific address from user profile.
        if (location == "home" || location == "work" || location == "school")
        {
            specificAddress = GetSpecificAddress(location);
        }

        // Step 4: Get additional notes.
        string note = GetNotes();

        // Display a summary of the check-in.
        UIHelper.PrintHeader("Mood Check-in", "Summary");
        UIHelper.PrintColor($"Feeling: {feeling}\n", ConsoleColor.Green);
        UIHelper.PrintColor($"Emotions: {string.Join(", ", emotions)}\n", ConsoleColor.Green);
        UIHelper.PrintColor($"Location: {location}", ConsoleColor.Green);
        if (!string.IsNullOrEmpty(specificAddress))
        {
            Console.WriteLine($" ({specificAddress})");
        }
        else
        {
            Console.WriteLine(); // Add newline if no specific address
        }
        UIHelper.PrintColor($"Notes: {note}\n", ConsoleColor.Green);

        // Prompt user to save the mood data.
        UIHelper.PrintColor("\nDo you want to save this mood check-in? (Y/N): ", ConsoleColor.Yellow);
        string saveChoice = Console.ReadLine()?.ToLower();

        if (saveChoice == "y")
        {
            SaveMoodData(feeling, emotions, location, specificAddress, note);
            UIHelper.PrintColor("\nMood check-in saved!", ConsoleColor.Green);
            UIHelper.ShowDottedPause(2); // Short pause with dots
        }
        else
        {
            UIHelper.PrintColor("\nMood check-in not saved.", ConsoleColor.Yellow);
            UIHelper.ShowDottedPause(2); // Short pause with dots
        }
    }

    // Guides the user to select their general feeling.
    private string GetFeeling()
    {
        string feeling = "";
        bool isValidChoice = false;
        do
        {
            Console.WriteLine("\nHow are you feeling right now?");
            Console.WriteLine("  1. Awesome!");
            Console.WriteLine("  2. Good");
            Console.WriteLine("  3. Fine");
            Console.WriteLine("  4. Bad");
            Console.WriteLine("  5. Terrible");
            UIHelper.PrintColor("Enter choice (1-5): ", ConsoleColor.Yellow);
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": feeling = "Awesome!"; isValidChoice = true; break;
                case "2": feeling = "Good"; isValidChoice = true; break;
                case "3": feeling = "Fine"; isValidChoice = true; break;
                case "4": feeling = "Bad"; isValidChoice = true; break;
                case "5": feeling = "Terrible"; isValidChoice = true; break;
                default: UIHelper.PrintColor("Invalid choice. Please enter a number from 1 to 5.\n", ConsoleColor.Red); break;
            }
        } while (!isValidChoice);
        return feeling;
    }

    // Guides the user to select emotions that describe their feeling.
    private List<string> GetEmotions()
    {
        List<string> selectedEmotions = new List<string>();
        List<string> commonEmotions = new List<string> { "Calm", "Content", "Relaxed", "Excited", "Happy", "Sad", "Anxious", "Stressed", "Frustrated", "Angry", "Tired", "Hopeful" };
        
        UIHelper.PrintColor("\nWhich emotions best describe how you feel? (Type 'done' when finished or 'more' for more options):", ConsoleColor.Cyan);
        
        bool finishedSelecting = false;
        while (!finishedSelecting)
        {
            Console.WriteLine("Available emotions:");
            for (int i = 0; i < commonEmotions.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {commonEmotions[i]}");
            }
            Console.WriteLine("  Type 'more' for custom input.");
            Console.WriteLine("  Type 'done' to finish selecting.");

            UIHelper.PrintColor("Enter emotion number or command: ", ConsoleColor.Yellow);
            string input = Console.ReadLine()?.ToLower();

            if (input == "done")
            {
                finishedSelecting = true;
            }
            else if (input == "more")
            {
                UIHelper.PrintColor("Enter custom emotion(s) (comma-separated if multiple): ", ConsoleColor.Yellow);
                string customInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(customInput))
                {
                    selectedEmotions.AddRange(customInput.Split(',', StringSplitOptions.RemoveEmptyEntries));
                }
            }
            else if (int.TryParse(input, out int emotionIndex) && emotionIndex > 0 && emotionIndex <= commonEmotions.Count)
            {
                string emotion = commonEmotions[emotionIndex - 1];
                if (!selectedEmotions.Contains(emotion))
                {
                    selectedEmotions.Add(emotion);
                    UIHelper.PrintColor($"'{emotion}' added. Current: {string.Join(", ", selectedEmotions)}\n", ConsoleColor.Green);
                }
                else
                {
                    UIHelper.PrintColor($"'{emotion}' already selected.\n", ConsoleColor.Yellow);
                }
            }
            else
            {
                UIHelper.PrintColor("Invalid input. Please enter a valid number, 'done', or 'more'.\n", ConsoleColor.Red);
            }
            Console.WriteLine(); // Spacing
        }
        return selectedEmotions;
    }

    // Guides the user to select their current location.
    private string GetLocation()
    {
        string location = "";
        bool isValidChoice = false;
        do
        {
            Console.WriteLine("\nWhere are you right now?");
            Console.WriteLine("  1. Home");
            Console.WriteLine("  2. Work");
            Console.WriteLine("  3. School");
            Console.WriteLine("  4. Outdoors");
            Console.WriteLine("  5. Other");
            UIHelper.PrintColor("Enter choice (1-5): ", ConsoleColor.Yellow);
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": location = "home"; isValidChoice = true; break;
                case "2": location = "work"; isValidChoice = true; break;
                case "3": location = "school"; isValidChoice = true; break;
                case "4": location = "outdoors"; isValidChoice = true; break;
                case "5": location = "other"; isValidChoice = true; break;
                default: UIHelper.PrintColor("Invalid choice. Please enter a number from 1 to 5.\n", ConsoleColor.Red); break;
            }
        } while (!isValidChoice);
        return location;
    }

    // Prompts the user to select a specific address if available in their profile for the given location type.
    // This integrates with the UserProfile, an exceeding requirement feature.
    private string GetSpecificAddress(string locationType)
    {
        List<Address> addresses = _userProfile.Addresses.FindAll(a => a.AddressType.Equals(locationType, StringComparison.OrdinalIgnoreCase));

        if (addresses.Count == 0)
        {
            Console.WriteLine($"No saved {locationType} addresses in your profile.");
            return "";
        }

        Console.WriteLine($"\nSelect a specific {locationType} address:");
        for (int i = 0; i < addresses.Count; i++)
        {
            Console.WriteLine($"  {i + 1}. {addresses[i].ToString()}");
        }
        Console.WriteLine("  0. Skip selecting a specific address");

        int choice;
        bool isValidChoice = false;
        do
        {
            UIHelper.PrintColor($"Enter choice (0-{addresses.Count}): ", ConsoleColor.Yellow);
            string input = Console.ReadLine();
            isValidChoice = int.TryParse(input, out choice) && choice >= 0 && choice <= addresses.Count;

            if (!isValidChoice)
            {
                UIHelper.PrintColor("Invalid choice. Please enter a valid number.\n", ConsoleColor.Red);
            }
        } while (!isValidChoice);

        return choice == 0 ? "" : addresses[choice - 1].ToString();
    }

    // Allows the user to enter additional notes for the mood check-in.
    private string GetNotes()
    {
        UIHelper.PrintColor("\nAny additional notes? (Optional): ", ConsoleColor.Yellow);
        return Console.ReadLine() ?? ""; // Return empty string if input is null
    }

    // Saves the mood check-in data to a text file (mood_log.txt).
    // This contributes to the persistent mood log exceeding requirement.
    private void SaveMoodData(string feeling, List<string> emotions, string location, string specificAddress, string note)
    {
        string filePath = "mood_log.txt";
        try
        {
            // Append data to the file, creating it if it doesn't exist.
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine($"Timestamp: {DateTime.Now}");
                writer.WriteLine($"Feeling: {feeling}");
                writer.WriteLine($"Emotions: {string.Join(", ", emotions)}");
                writer.WriteLine($"Location: {location}{(string.IsNullOrEmpty(specificAddress) ? "" : $" ({specificAddress})")}");
                writer.WriteLine($"Notes: {note}");
                writer.WriteLine(new string('-', 30)); // Separator
            }
        }
        catch (Exception ex)
        {
            UIHelper.PrintColor($"Error saving mood data: {ex.Message}", ConsoleColor.Red);
        }
    }
}