// UIHelper.cs
using System;
using System.Threading;
using System.Collections.Generic; // Required for List

public static class UIHelper
{
    // --- Header Display Method ---
    public static void DisplayHeader(string subheader)
    {
        Console.WriteLine(GetColoredText("==========================", ConsoleColor.Yellow));
        Console.WriteLine(GetColoredText("          DigiHealth          ", ConsoleColor.Blue));
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

    // --- Spinner Animation ---
    public static void ShowSpinner(int seconds)
    {
        List<string> spinnerFrames = new List<string> { "|", "/", "-", "\\" };
        DateTime startTime = DateTime.Now;
        int i = 0;
        while ((DateTime.Now - startTime).TotalSeconds < seconds)
        {
            Console.Write(spinnerFrames[i]);
            Thread.Sleep(250);
            Console.Write("\b"); // Attempt to move cursor back (though it may not work in all terminals)
            i++;
            if (i >= spinnerFrames.Count)
            {
                i = 0;
            }
        }
        // Ensure the spinner character is cleared
        Console.Write(" "); // Overwrite last spinner char with a space
        Console.Write("\b"); // Move cursor back again to hide the space
    }

    // --- Countdown Timer ---
    public static void ShowCountdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write($"{i} ");
            Thread.Sleep(1000);
            // Clear the number by overwriting with spaces and backspacing
            if (i < 10) Console.Write("\b\b"); // For single digit numbers
            else Console.Write("\b\b\b"); // For double digit numbers
        }
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