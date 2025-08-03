// Activity.cs
using System;
using System.Threading;

public class Activity
{
    // --- Attributes ---
    private string _name;
    private string _description;
    protected int _duration; // Protected so derived classes can access directly if needed,
                             
    // --- Constructor ---
    public Activity(string name, string description)
    {
        _name = name;
        _description = description;
        _duration = 0; // Default duration
    }

    // --- Methods ---

    // Displays the common starting message for all activities.
    public void DisplayStartingMessage()
    {
        UIHelper.DisplayHeader("Activity Start"); // Use UIHelper for consistent header
        Console.WriteLine($"Welcome to the {UIHelper.GetColoredText(_name, ConsoleColor.Cyan)} Activity.");
        Console.WriteLine($"\n{_description}");

        Console.WriteLine(UIHelper.GetColoredText("\n--------------------------", ConsoleColor.Yellow));
        Console.Write("How long, in seconds, would you like for your session? ");
        string input = Console.ReadLine();
        if (int.TryParse(input, out int durationInput) && durationInput > 0)
        {
            _duration = durationInput;
        }
        else
        {
            Console.WriteLine(UIHelper.GetColoredText("Invalid duration. Setting to default 30 seconds.", ConsoleColor.Red));
            _duration = 30; // Default to 30 seconds on invalid input
            Thread.Sleep(2000);
        }

        Console.WriteLine("\nPrepare to begin...");
        UIHelper.ShowSpinner(5); // Pause with spinner for 5 seconds
    }

    // Displays the common ending message for all activities.
    public void DisplayEndingMessage()
    {
        Console.WriteLine("\nWell done!");
        UIHelper.ShowSpinner(3); // Pause with spinner for 3 seconds

        Console.WriteLine($"\nYou have completed the {UIHelper.GetColoredText(_name, ConsoleColor.Cyan)} for {UIHelper.GetColoredText(_duration.ToString(), ConsoleColor.Green)} seconds.");
        UIHelper.ShowSpinner(5); // Pause with spinner for 5 seconds before returning to menu
    }

    // Handles the generic spinner animation.
    public void ShowSpinner(int seconds)
    {
        UIHelper.ShowSpinner(seconds);
    }

    // Getter for the activity name (useful for ending message)
    public string GetName()
    {
        return _name;
    }

    // Getter for the duration
    public int GetDuration()
    {
        return _duration;
    }
}