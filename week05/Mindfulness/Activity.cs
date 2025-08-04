using System;
using System.Threading;

// Base class for all mindfulness activities.
// It contains common attributes and behaviors shared by derived activity types.
public abstract class Activity
{
    // Protected member variables accessible by derived classes for activity details.
    protected string _activityName;
    protected string _description;
    protected int _duration;

    // Constructor to initialize common activity properties.
    public Activity(string name, string description)
    {
        _activityName = name;
        _description = description;
    }

    // Displays a common starting message for any activity.
    // It prompts the user for the duration and includes a preparation pause.
    protected void DisplayStartingMessage()
    {
        UIHelper.PrintHeader(_activityName, "Starting"); // Display activity specific header
        UIHelper.PrintColor($"Description: {_description}\n", ConsoleColor.Green);

        // Prompt user for activity duration and validate input.
        int parsedDuration;
        bool isValidInput = false;
        do
        {
            UIHelper.PrintColor("How long, in seconds, would you like for your session? ", ConsoleColor.Yellow);
            string durationInput = Console.ReadLine();
            isValidInput = int.TryParse(durationInput, out parsedDuration);

            if (!isValidInput || parsedDuration <= 0)
            {
                UIHelper.PrintColor("Invalid input. Please enter a positive number for duration.", ConsoleColor.Red);
            }
        } while (!isValidInput || parsedDuration <= 0);

        _duration = parsedDuration; // Set the activity duration

        UIHelper.PrintColor("\nPrepare to begin...", ConsoleColor.Cyan);
        UIHelper.ShowDottedPause(3); // Pause for 3 seconds with a dotted animation
        Console.WriteLine(); // Add a newline after the pause animation
    }

    // Displays a common ending message for any activity.
    // It congratulates the user and summarizes the completed activity and its duration.
    protected void DisplayEndingMessage()
    {
        Console.WriteLine(); // Add a newline for spacing before the ending message
        UIHelper.PrintColor("Well done!", ConsoleColor.Green);
        UIHelper.ShowDottedPause(3); // Pause for 3 seconds with a dotted animation

        UIHelper.PrintColor($"\nYou have completed the {_activityName} for {_duration} seconds.", ConsoleColor.Yellow);
        UIHelper.ShowDottedPause(3); // Pause for 3 seconds with a dotted animation
        Console.WriteLine(); // Add a newline after the pause animation
    }

    // Abstract method to be implemented by derived classes.
    // This method contains the specific logic for each type of mindfulness activity.
    public abstract void Run();
}