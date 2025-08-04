using System;
using System.Threading;
using System.Collections.Generic;

// Derived class representing the Reflection Activity.
// It extends the base Activity class and implements the specific reflection exercise logic.
class ReflectionActivity : Activity
{
    // A list of prompts for the reflection activity.
    private List<string> _prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless.",
        "Think of a time when you overcame a significant challenge."
    };

    // A list of questions for deeper reflection related to the chosen prompt.
    private List<string> _questions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    // Random number generator for selecting prompts and questions.
    private Random _random = new Random();

    // Constructor initializes the activity with its specific name and description.
    public ReflectionActivity() : base("Reflection Activity", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
    {
    }

    // Overrides the abstract Run() method to execute the Reflection Activity.
    public override void Run()
    {
        // Display the standard starting message (gets duration, prepares to begin).
        DisplayStartingMessage();

        // Get a random prompt for the reflection.
        string prompt = GetRandomPrompt();
        UIHelper.PrintColor($"\nConsider the following prompt:", ConsoleColor.Cyan);
        UIHelper.PrintColor($"--- {prompt} ---\n", ConsoleColor.Yellow);

        // Give the user a moment to begin reflecting on the prompt.
        UIHelper.PrintColor("When you have thought about the prompt, press enter to continue with questions.", ConsoleColor.Yellow);
        Console.ReadLine(); // Wait for user to press Enter

        // Initialize variables for timing.
        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddSeconds(_duration);

        UIHelper.PrintColor("\nNow ponder on each of the following questions as they relate to this experience.", ConsoleColor.Cyan);
        UIHelper.ShowDottedPause(3); // Pause for 3 seconds with a dotted animation

        // Loop to display random questions until the duration is met.
        while (DateTime.Now < endTime)
        {
            string question = GetRandomQuestion();
            UIHelper.PrintColor($"> {question} ", ConsoleColor.Green); // Display question
            UIHelper.ShowDottedPause(5); // Pause for 5 seconds with a dotted animation for reflection
        }

        // Display the standard ending message for the activity.
        DisplayEndingMessage();
    }

    // Selects a random prompt from the list of prompts.
    private string GetRandomPrompt()
    {
        int index = _random.Next(_prompts.Count);
        return _prompts[index];
    }

    // Selects a random question from the list of questions.
    private string GetRandomQuestion()
    {
        int index = _random.Next(_questions.Count);
        return _questions[index];
    }
}