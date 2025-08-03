// BreathingActivity.cs
using System;
using System.Threading;

public class BreathingActivity : Activity
{
    // --- Constructor ---
    public BreathingActivity() : base("Breathing Activity", "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    {
        // No specific attributes beyond inherited ones
    }

    // --- Specific Behaviors ---

    // This is the main method to run the breathing activity.
    public void Run()
    {
        DisplayStartingMessage(); // Call base class method

        // --- Exceeding Requirements: Enhanced Breathing Activity Intro ---
        ShowEnhancedBreathingIntro();

        // --- Core Breathing Exercise ---
        Console.WriteLine("\nNow, let's begin the breathing exercise.");
        UIHelper.ShowSpinner(3); // Brief pause before starting

        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddSeconds(_duration);

        while (DateTime.Now < endTime)
        {
            Console.Write("\nBreathe in...");
            UIHelper.ShowCountdown(4); // Breathe in for 4 seconds

            Console.Write("Breathe out...");
            UIHelper.ShowCountdown(6); // Breathe out for 6 seconds
        }

        DisplayEndingMessage(); // Call base class method
    }

    // Handles the enhanced introductory content for the Breathing Activity.
    private void ShowEnhancedBreathingIntro()
    {
        Console.WriteLine("\n\n"); // Added for spacing instead of Clear()
        UIHelper.DisplayHeader("Breathing Intro");
        Console.WriteLine("Breathing exercises are a simple yet powerful way to calm your mind and body.");
        Console.WriteLine("They can help reduce stress, improve focus, and promote relaxation.");

        // Options for user
        var options = new System.Collections.Generic.Dictionary<string, string>
        {
            { "1", "Start exercise now" },
            { "2", "Proceed reading (Learn more)" }
        };
        UIHelper.PrintMenuOptions(options, "Your choice: ");
        string choice = Console.ReadLine();

        if (choice == "2")
        {
            Console.WriteLine("\n\n"); 
            UIHelper.DisplayHeader("Detailed Breathing Info");

            // Paragraph 1
            UIHelper.AnimateTypewriter("Normal breathing patterns involve a cycle of inhalation (breathing in) and exhalation (breathing out). Typically, an adult takes about 12 to 20 breaths per minute at rest. Inhalation is an active process where the diaphragm contracts, drawing air into the lungs. Exhalation is usually passive, as the diaphragm relaxes and air leaves the lungs.");
            Thread.Sleep(2000); // Pause before next section

            Console.WriteLine("\n\n"); 
            UIHelper.DisplayHeader("Detailed Breathing Info");

            // Paragraph 2
            UIHelper.AnimateTypewriter("Factors affecting breathing include physical activity, stress, emotions, and underlying health conditions. Mindful breathing aims to regulate these patterns, often by extending exhalation, to stimulate the parasympathetic nervous system, which promotes a state of rest and digestion.");
            Thread.Sleep(2000); // Pause before next section

            Console.WriteLine("\n\n"); 
            UIHelper.DisplayHeader("Tips for Breathing Exercises");

            // Tips
            UIHelper.AnimateTypewriter("Tip 1: Find a quiet space where you won't be disturbed. Sit or lie down in a comfortable position. Close your eyes gently if you feel comfortable doing so.");
            Thread.Sleep(1500);
            UIHelper.AnimateTypewriter("Tip 2: Focus on your breath. Notice the sensation of air entering and leaving your body. Don't try to change anything initially, just observe.");
            Thread.Sleep(1500);
            UIHelper.AnimateTypewriter("Tip 3: Practice diaphragmatic (belly) breathing. Place one hand on your chest and the other on your belly. As you breathe in, your belly should rise, and as you breathe out, it should fall. Your chest should remain relatively still.");
            Thread.Sleep(1500);
            UIHelper.AnimateTypewriter("Tip 4: Count your breaths. Try inhaling for a count of four, holding for a count of two, and exhaling for a count of six. Adjust the counts to what feels comfortable for you.");
            Thread.Sleep(1500);
            UIHelper.AnimateTypewriter("Tip 5: Incorporate it into your daily routine. Even a few minutes of mindful breathing can make a difference. Practice before bed, during breaks, or whenever you feel stressed.");
            Thread.Sleep(2000);

            // After all detailed info, allow user to repeat or go back
            Console.WriteLine(UIHelper.GetColoredText("\n--------------------------", ConsoleColor.Yellow));
            var readOptions = new System.Collections.Generic.Dictionary<string, string>
            {
                { "1", "Read again" },
                { "2", "Go back to Breathing Activity" }
            };
            UIHelper.PrintMenuOptions(readOptions, "Your choice: ");
            string readChoice = Console.ReadLine();

            if (readChoice == "1")
            {
                Console.WriteLine("\n\n");
                ShowEnhancedBreathingIntro(); // Recursively call to read again
            }
            Console.WriteLine("\n\n"); 
        }
        else
        {
            Console.WriteLine("\n\n"); 
        }
    }
}