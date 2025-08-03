// ReflectionActivity.cs
using System;
using System.Collections.Generic; // Required for List<T>
using System.Threading;

public class ReflectionActivity : Activity
{
    // --- Attributes ---
    private List<string> _prompts;
    private List<string> _questions;
    private Random _random;

    // --- Constructor ---
    public ReflectionActivity() : base("Reflection Activity", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
    {
        _prompts = new List<string>
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        _questions = new List<string>
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

        _random = new Random();
    }

    // --- Specific Behaviors ---

    // The main method to run the reflection activity.
    public void Run()
    {
        DisplayStartingMessage(); // Call base class method

        Console.WriteLine("\n\n"); 
        UIHelper.DisplayHeader("Reflection Activity");

        string prompt = GetRandomPrompt();
        Console.WriteLine("\nConsider the following prompt:");
        Console.WriteLine(UIHelper.GetColoredText($"--- {prompt} ---", ConsoleColor.Cyan));
        Console.WriteLine("\nWhen you have thought about the prompt, press enter to continue.");
        Console.ReadLine();

        Console.WriteLine("\nNow ponder on each of the following questions as they relate to this experience.");
        UIHelper.ShowCountdown(5); // Give a few seconds to prepare for questions

        Console.WriteLine("\n\n"); 
        UIHelper.DisplayHeader("Reflection Activity");

        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddSeconds(_duration);

        while (DateTime.Now < endTime)
        {
            string question = GetRandomQuestion();
            Console.Write($"\n> {question} ");
            UIHelper.ShowSpinner(10); // Pause for 10 seconds for each question
            // We don't clear the question, allowing user to keep it in mind
        }

        DisplayEndingMessage(); // Call base class method
    }

    // Selects a random prompt from the _prompts list.
    private string GetRandomPrompt()
    {
        int index = _random.Next(_prompts.Count);
        return _prompts[index];
    }

    // Selects a random question from the _questions list.
    private string GetRandomQuestion()
    {
        int index = _random.Next(_questions.Count);
        return _questions[index];
    }
}