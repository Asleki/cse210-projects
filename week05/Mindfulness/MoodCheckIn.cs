// MoodCheckIn.cs
using System;
using System.Collections.Generic;
using System.IO; // For file saving
using System.Linq; // For .Any()
using System.Threading; // Added for Thread.Sleep

public class MoodCheckIn
{
    private UserProfile _userProfile; // To access user data like addresses
    private string _moodLogFilePath = "mood_notes.txt"; // File to save mood notes

    // Constructor to inject the UserProfile
    public MoodCheckIn(UserProfile userProfile)
    {
        _userProfile = userProfile;
    }

    // Main method to start the mood check-in process
    public void StartCheckIn()
    {
        Console.WriteLine("\n\n"); 
        UIHelper.DisplayHeader("Mood Check-in");

        string mood = GetMoodLevel();
        List<string> emotions = GetEmotions();
        string location = GetLocation();
        string note = GetAdditionalNote();

        SaveMoodData(mood, emotions, location, note);

        Console.WriteLine(UIHelper.GetColoredText("\nMood Check-in complete! Thank you for sharing.", ConsoleColor.Green));
        UIHelper.ShowSpinner(3); // Short pause before returning
    }

    // Step 1: Get how the user is feeling today
    private string GetMoodLevel()
    {
        Console.WriteLine("\nHow are you feeling today?");
        var moodOptions = new Dictionary<string, string>
        {
            {"1", "Awesome!"},
            {"2", "Good"},
            {"3", "Fine"},
            {"4", "Bad"},
            {"5", "Terrible"}
        };
        UIHelper.PrintMenuOptions(moodOptions, "Select your mood: ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1": return "Awesome!";
            case "2": return "Good";
            case "3": return "Fine";
            case "4": return "Bad";
            case "5": return "Terrible";
            default:
                Console.WriteLine(UIHelper.GetColoredText("Invalid selection. Defaulting to 'Fine'.", ConsoleColor.Yellow));
                return "Fine";
        }
    }

    // Step 2: Get emotions describing how the user feels
    private List<string> GetEmotions()
    {
        List<string> selectedEmotions = new List<string>();
        List<string> availableEmotions = new List<string>
        {
            "Calm", "Content", "Relaxed", "Excited", "Happy",
            "Anxious", "Stressed", "Sad", "Angry", "Tired",
            "Hopeful", "Grateful", "Inspired", "Bored", "Frustrated"
        }; // Expanded list

        string choice = "";
        int displayCount = 5; // Number of emotions to display at a time
        int startIndex = 0;

        while (choice.ToLower() != "n")
        {
            Console.WriteLine("\n\n"); 
            UIHelper.DisplayHeader("Mood Check-in");
            Console.WriteLine("\nWhich emotions best describe how you feel? (Type numbers, separated by commas, or select options below)");

            var currentOptions = new Dictionary<string, string>();
            for (int i = 0; i < displayCount && (startIndex + i) < availableEmotions.Count; i++)
            {
                currentOptions.Add((i + 1).ToString(), availableEmotions[startIndex + i]);
            }

            // Always add 'M. More' if there are more emotions to show
            if (startIndex + displayCount < availableEmotions.Count)
            {
                currentOptions.Add("M", "More emotions");
            }
            currentOptions.Add("N", "Next (done selecting)");

            UIHelper.PrintMenuOptions(currentOptions, "Your selection(s): ");
            string input = Console.ReadLine().ToLower().Trim();

            if (input == "m")
            {
                startIndex += displayCount; // Move to next set of emotions
                if (startIndex >= availableEmotions.Count)
                {
                    startIndex = 0; // Loop back to start if exhausted
                    Console.WriteLine(UIHelper.GetColoredText("Reached end of list. Restarting from beginning.", ConsoleColor.Yellow));
                    Thread.Sleep(1500);
                }
            }
            else if (input == "n")
            {
                choice = "n"; // Exit loop
            }
            else
            {
                string[] selections = input.Split(',');
                foreach (string s in selections)
                {
                    if (int.TryParse(s.Trim(), out int num) && num > 0 && num <= displayCount)
                    {
                        int actualIndex = startIndex + (num - 1);
                        if (actualIndex < availableEmotions.Count)
                        {
                            string emotion = availableEmotions[actualIndex];
                            if (!selectedEmotions.Contains(emotion))
                            {
                                selectedEmotions.Add(emotion);
                                Console.WriteLine(UIHelper.GetColoredText($"'{emotion}' added.", ConsoleColor.Green));
                            }
                            else
                            {
                                Console.WriteLine(UIHelper.GetColoredText($"'{emotion}' already selected.", ConsoleColor.Yellow));
                            }
                        }
                    }
                }
                Thread.Sleep(500); // Small pause for feedback
            }
        }
        return selectedEmotions;
    }

    // Step 3: Get the user's current location
    private string GetLocation()
    {
        Console.WriteLine("\n\n"); 
        UIHelper.DisplayHeader("Mood Check-in");
        Console.WriteLine("\nWhere are you now?");

        var locationOptions = new Dictionary<string, string>
        {
            {"1", "Work"},
            {"2", "Home"},
            {"3", "School"},
            {"4", "Outdoors"},
            {"5", "Other"}
        };
        UIHelper.PrintMenuOptions(locationOptions, "Select your current location: ");
        string choice = Console.ReadLine();
        string selectedLocationType = "";

        switch (choice)
        {
            case "1": selectedLocationType = "Work"; break;
            case "2": selectedLocationType = "Home"; break;
            case "3": selectedLocationType = "School"; break;
            case "4": return "Outdoors"; // Outdoors doesn't require address selection
            case "5":
                Console.Write("Please specify 'Other' location: ");
                return Console.ReadLine();
            default:
                Console.WriteLine(UIHelper.GetColoredText("Invalid selection. Defaulting to 'Other'.", ConsoleColor.Yellow));
                Console.Write("Please specify 'Other' location: ");
                return Console.ReadLine();
        }

        // If a specific address type was chosen, let the user select from their saved addresses
        if (!string.IsNullOrEmpty(selectedLocationType))
        {
            List<Address> addresses = _userProfile.GetAddressesByType(selectedLocationType);
            if (addresses.Any())
            {
                Console.WriteLine("\n\n"); 
                UIHelper.DisplayHeader("Mood Check-in");
                Console.WriteLine($"\nWhich {selectedLocationType} address are you at?");
                for (int i = 0; i < addresses.Count; i++)
                {
                    Console.WriteLine(UIHelper.GetColoredText($"{i + 1}. {addresses[i].GetFullAddress()}", ConsoleColor.Green));
                }
                Console.WriteLine(UIHelper.GetColoredText("--------------------------", ConsoleColor.Yellow));
                Console.Write("Select an address number: ");
                if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= addresses.Count)
                {
                    return addresses[index - 1].GetFullAddress(); // Return the selected full address
                }
                else
                {
                    Console.WriteLine(UIHelper.GetColoredText("Invalid selection. Using generic location type.", ConsoleColor.Yellow));
                    return selectedLocationType; // Fallback to just the type
                }
            }
            else
            {
                Console.WriteLine(UIHelper.GetColoredText($"No '{selectedLocationType}' addresses found in your profile. Using generic '{selectedLocationType}'.", ConsoleColor.Yellow));
                Thread.Sleep(1500);
                return selectedLocationType; // Return the type if no specific addresses
            }
        }
        return selectedLocationType; 
    }

    // Step 4: Get any additional notes from the user
    private string GetAdditionalNote()
    {
        Console.WriteLine("\n\n");
        UIHelper.DisplayHeader("Mood Check-in");
        Console.WriteLine("\nAnything else to add? (e.g., specific reasons for your mood, what happened, etc.)");
        Console.WriteLine("(Press Enter if nothing to add)");
        Console.Write("Your note: ");
        return Console.ReadLine();
    }

    // Saves the mood check-in data to a file (mood_notes.txt)
    private void SaveMoodData(string mood, List<string> emotions, string location, string note)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(_moodLogFilePath, true)) // true for append mode
            {
                writer.WriteLine($"Timestamp: {DateTime.Now}");
                writer.WriteLine($"Mood: {mood}");
                writer.WriteLine($"Emotions: {string.Join(", ", emotions)}");
                writer.WriteLine($"Location: {location}");
                writer.WriteLine($"Note: {note}");
                writer.WriteLine("---"); // Separator for entries
            }
            Console.WriteLine(UIHelper.GetColoredText("\n✅ Mood check-in saved successfully to mood_notes.txt!", ConsoleColor.Green));
        }
        catch (Exception ex)
        {
            Console.WriteLine(UIHelper.GetColoredText($"❌ Error saving mood check-in: {ex.Message}", ConsoleColor.Red));
        }
        Thread.Sleep(2000);
    }
}