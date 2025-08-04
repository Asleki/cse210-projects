using System;
using System.Threading;
using System.Collections.Generic;

// Derived class representing the Breathing Activity.
// It extends the base Activity class and implements the specific breathing exercise logic.
class BreathingActivity : Activity
{
    // Constructor initializes the activity with its specific name and description.
    public BreathingActivity() : base("Breathing Activity", "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    {
    }

    // Overrides the abstract Run() method to execute the Breathing Activity.
    // It includes an optional introductory content display and the main breathing cycle loop.
    public override void Run()
    {
        // Display the introductory content for the breathing activity.
        // This includes the "Read More" exceeding requirement feature.
        DisplayBreathingIntro();

        // Display the standard starting message (gets duration, prepares to begin).
        DisplayStartingMessage();

        // Calculate the end time for the activity based on the user-defined duration.
        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddSeconds(_duration);

        // Loop to guide the user through breathing in and out until the duration is met.
        while (DateTime.Now < endTime)
        {
            // Guide user to breathe in with a countdown.
            UIHelper.PrintColor("\nBreathe in...", ConsoleColor.Cyan);
            UIHelper.ShowCountdown(4); // Inhale for 4 seconds

            // Add a brief dotted pause between inhale and exhale phases for better flow.
            UIHelper.ShowDottedPause(1); // One dot pause after inhale

            // Guide user to breathe out with a countdown.
            UIHelper.PrintColor("\nBreathe out...", ConsoleColor.Cyan);
            UIHelper.ShowCountdown(6); // Exhale for 6 seconds
        }

        // Display the standard ending message for the activity.
        DisplayEndingMessage();
    }

    // Displays a brief introduction to breathing exercises and offers to show more detailed information.
    // This method implements an exceeding requirement feature.
    private void DisplayBreathingIntro()
    {
        UIHelper.PrintHeader("Breathing Activity", "Introduction");
        Console.WriteLine("Breathing exercises are a simple way to relax and calm your mind.");
        
        // Prompt the user if they want to read more details or start the exercise immediately.
        UIHelper.PrintColor("\nWould you like to (R)ead More about breathing or (Y)ield and Start the exercise? (R/Y): ", ConsoleColor.Yellow);
        string choice = Console.ReadLine()?.ToLower();
        
        // If the user chooses 'r', display the detailed information.
        if (choice == "r")
        {
            DisplayDetailedBreathingInfo();
            // After viewing detailed info, return to this prompt to allow the user
            // to explicitly choose 'Y' to start or 'R' again.
            DisplayBreathingIntro(); 
        }
        // If choice is 'y' or anything else, the method exits and the Run() method
        // will proceed to DisplayStartingMessage(), effectively starting the activity.
    }

    // Provides detailed educational content about breathing exercises using a typewriter animation.
    // This method implements an exceeding requirement feature.
    private void DisplayDetailedBreathingInfo()
    {
        // Define educational paragraphs about breathing techniques and benefits.
        string paragraph1 = "Deep, conscious breathing is a powerful tool for managing stress and improving overall well-being. Unlike shallow chest breathing, diaphragmatic (belly) breathing engages the diaphragm, leading to more efficient oxygen intake and activation of the parasympathetic nervous system, which promotes relaxation.";
        string paragraph2 = "Regular practice of controlled breathing can lower heart rate, reduce blood pressure, decrease muscle tension, and calm the mind. It helps to break the 'fight or flight' response often triggered by daily stressors, bringing your body and mind into a state of balance and peace. This practice fosters mindfulness, helping you stay present and grounded.";
        string subheader = "Key Considerations & Tips for Effective Breathing:";
        
        // Define a list of practical tips for performing breathing exercises.
        List<string> tips = new List<string>
        {
            "Find a quiet, distraction-free space where you won't be interrupted.",
            "Sit or lie down in a comfortable position, ensuring your back is supported and your body is relaxed.",
            "Place one hand on your chest and the other on your belly. As you breathe, try to make your belly rise more than your chest.",
            "Inhale slowly through your nose, feeling your abdomen expand. Count slowly to four as you inhale.",
            "Hold your breath briefly for a count of one or two.",
            "Exhale slowly through your mouth (or nose), gently pursing your lips, for a count of six or more, feeling your belly fall.",
            "Focus entirely on the sensation of your breath. If your mind wanders, gently bring it back to your breathing.",
            "Don't force your breath; let it be a natural, gentle, and continuous rhythm. Consistency is key, even short sessions are beneficial."
        };

        // Display the header for the detailed information section.
        UIHelper.PrintHeader("Breathing Activity", "Detailed Information");
        Console.WriteLine(); // Add a newline for spacing after header

        // Animate the first paragraph with a typewriter effect and pause.
        UIHelper.AnimateTypewriter(paragraph1, 30); // 30ms delay per character
        Thread.Sleep(1500); // Pause for 1.5 seconds after paragraph

        Console.WriteLine(); // Add a newline between paragraphs for better readability

        // Animate the second paragraph with a typewriter effect and pause.
        UIHelper.AnimateTypewriter(paragraph2, 30); // 30ms delay per character
        Thread.Sleep(1500); // Pause for 1.5 seconds after paragraph

        // Print the subheader for tips in a distinct color.
        UIHelper.PrintColor($"\n{subheader}\n", ConsoleColor.Cyan);

        // Animate and display each tip with a bullet point.
        foreach (var tip in tips)
        {
            UIHelper.AnimateTypewriter($"- {tip}", 25); // 25ms delay per character, add bullet point
            Thread.Sleep(300); // Shorter pause between tips
        }
        
        // Prompt user to press Enter to continue back to the activity menu.
        UIHelper.PrintColor("\nPress Enter to return to the Breathing Activity menu...", ConsoleColor.Yellow);
        Console.ReadLine(); // Wait for user input
    }
}