// ListingActivity.cs
using System;
using System.Collections.Generic; // Required for List<T>
using System.Threading;

public class ListingActivity : Activity
{
    // --- Attributes ---
    private List<string> _prompts;
    private Random _random;

    // --- Constructor ---
    public ListingActivity() : base("Listing Activity", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
        _prompts = new List<string>
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };
        _random = new Random();
    }

    // --- Specific Behaviors ---

    // The main method to run the listing activity.
    public void Run()
    {
        DisplayStartingMessage(); // Call base class method

        Console.WriteLine("\n\n"); 
        UIHelper.DisplayHeader("Listing Activity");

        string prompt = GetRandomPrompt();
        Console.WriteLine("\nList as many responses you can to the following prompt:");
        Console.WriteLine(UIHelper.GetColoredText($"--- {prompt} ---", ConsoleColor.Cyan));

        Console.Write("You may begin in: ");
        UIHelper.ShowCountdown(5); // Give a few seconds to think

        Console.WriteLine(); // New line for listing input
        Console.WriteLine("Start listing items:");

        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddSeconds(_duration);
        int itemCount = 0;

        // Collect items until the duration runs out
        while (DateTime.Now < endTime)
        {
            
            string listItem = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(listItem)) // Only count non-empty entries
            {
                itemCount++;
            }
        }

        Console.WriteLine($"\nYou listed {UIHelper.GetColoredText(itemCount.ToString(), ConsoleColor.Green)} items!");
        Thread.Sleep(2000); // Pause briefly to show the count

        DisplayEndingMessage(); // Call base class method
    }

    // Selects a random prompt from the _prompts list.
    private string GetRandomPrompt()
    {
        int index = _random.Next(_prompts.Count);
        return _prompts[index];
    }
}