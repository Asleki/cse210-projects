using System;
using System.Threading;
using System.Collections.Generic;

// Derived class representing the Listing Activity.
// It extends the base Activity class and implements the specific listing exercise logic.
class ListingActivity : Activity
{
    // A list of prompts for the listing activity.
    private List<string> _prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    // Random number generator for selecting prompts.
    private Random _random = new Random();

    // Constructor initializes the activity with its specific name and description.
    public ListingActivity() : base("Listing Activity", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
    }

    // Overrides the abstract Run() method to execute the Listing Activity.
    public override void Run()
    {
        // Display the standard starting message (gets duration, prepares to begin).
        DisplayStartingMessage();

        // Get a random prompt for the activity.
        string prompt = GetRandomPrompt();
        UIHelper.PrintColor($"\nList as many responses you can to the following prompt:", ConsoleColor.Cyan);
        UIHelper.PrintColor($"--- {prompt} ---\n", ConsoleColor.Yellow);

        // Give the user a countdown to prepare their thoughts.
        UIHelper.PrintColor("You may begin in: ", ConsoleColor.Cyan);
        UIHelper.ShowCountdown(5); // Countdown for 5 seconds

        Console.WriteLine(); // Add a newline for spacing

        // Initialize variables for timing and item counting.
        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddSeconds(_duration);
        int itemCount = 0;

        // Loop to allow the user to list items until the duration is met.
        while (DateTime.Now < endTime)
        {
            UIHelper.PrintColor(">", ConsoleColor.DarkGreen); // Prompt character
            string item = Console.ReadLine();

            // Only count items if they are not empty.
            if (!string.IsNullOrWhiteSpace(item))
            {
                itemCount++;
            }
        }

        // Display the total number of items entered by the user.
        UIHelper.PrintColor($"\nYou listed {itemCount} items!", ConsoleColor.Green);
        UIHelper.ShowDottedPause(2); // Pause briefly with dots

        // Display the standard ending message for the activity.
        DisplayEndingMessage();
    }

    // Selects a random prompt from the list of available prompts.
    private string GetRandomPrompt()
    {
        int index = _random.Next(_prompts.Count);
        return _prompts[index];
    }
}