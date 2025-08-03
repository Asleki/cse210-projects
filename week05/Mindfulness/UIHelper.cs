// UIHelper.cs
using System;
using System.Threading;
using System.Collections.Generic; // Required for List
using System.Linq; // Required for LINQ, though not explicitly used in this snippet, good practice to have if other parts might use it.

public static class UIHelper
{
    // --- Header Display Method ---
    public static void DisplayHeader(string subheader)
    {
        Console.WriteLine(GetColoredText("==========================", ConsoleColor.Yellow));
        Console.WriteLine(GetColoredText("         DigiHealth         ", ConsoleColor.Blue));
        Console.WriteLine(GetColoredText("==========================", ConsoleColor.Yellow));
        Console.WriteLine($"   {subheader}\n");
    }

    // --- Helper for Coloring Text ---
    public static string GetColoredText(string text, ConsoleColor color)
    {
        ConsoleColor originalColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        string coloredText = text;
        Console.ForegroundColor = originalColor; // Reset color immediately
        return coloredText;
    }

    
    public static void ShowSpinner(int seconds)
    {
        Console.WriteLine("  Preparing activity:"); // Initial message before spinner starts
        string[] frames = { "⏰ .", "⏰ ..", "⏰ ..." }; // Visual frames for the "spinner"
        DateTime startTime = DateTime.Now;
        int frameIndex = 0;
        int animationInterval = 400; // milliseconds between frame updates

        while (DateTime.Now - startTime < TimeSpan.FromSeconds(seconds))
        {
            Console.WriteLine($"  {frames[frameIndex]}"); // Each frame prints on a new line
            frameIndex = (frameIndex + 1) % frames.Length;
            Thread.Sleep(animationInterval);
        }
        Console.WriteLine(" "); // Add an extra newline for separation after the spinner animation
    }

        public static void ShowCountdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.WriteLine($"  {i}"); // Print each number on a new line, with some indentation
            Thread.Sleep(1000); // 1-second pause
        }
        Console.WriteLine(" "); // Add an extra newline for separation after countdown
    }

    // --- Simple Dot Loading Animation (for initial startup, most compatible) ---
        public static void ShowSimpleDotAnimation(int seconds)
    {
        DateTime startTime = DateTime.Now;
        while ((DateTime.Now - startTime).TotalSeconds < seconds)
        {
            Console.Write(".");
            Thread.Sleep(500);
        }
        Console.WriteLine(); // New line after animation
    }

    // --- Typewriter Animation (for detailed descriptions in Breathing Activity) ---
    public static void AnimateTypewriter(string text, int delayMilliseconds = 50)
    {
        foreach (char c in text)
        {
            Console.Write(c);
            Thread.Sleep(delayMilliseconds);
        }
        Console.WriteLine(); // New line after typing
    }

    // --- Print options with green/yellow numbering/text and separator ---
    public static void PrintMenuOptions(Dictionary<string, string> options, string prompt)
    {
        foreach (var option in options)
        {
            if (int.TryParse(option.Key, out _)) // Check if key is a number
            {
                Console.WriteLine(GetColoredText($"{option.Key}. {option.Value}", ConsoleColor.Green));
            }
            else
            {
                Console.WriteLine(GetColoredText($"{option.Key}. {option.Value}", ConsoleColor.Yellow));
            }
        }
        Console.WriteLine(GetColoredText("--------------------------", ConsoleColor.Yellow));
        Console.Write(prompt);
    }
}