using System;
using System.Threading;
using System.Text;

// Static utility class for all User Interface (UI) related operations.
// It handles displaying formatted text, colors, headers, and various animations.
public static class UIHelper
{
    // A helper method to safely control console cursor visibility.
    // Catches IOException which can occur in certain terminal environments (e.g., some IDEs).
    private static void SetCursorVisibility(bool visible)
    {
        try
        {
            Console.CursorVisible = visible;
        }
        catch (System.IO.IOException)
        {
            // Ignore the error if cursor visibility cannot be changed.
        }
    }

    // A helper method to safely get the console window width.
    // Catches IOException and provides a fallback default width.
    private static int GetConsoleWindowWidth()
    {
        try
        {
            return Console.WindowWidth;
        }
        catch (System.IO.IOException)
        {
            // Fallback to a common default width if console width cannot be obtained.
            return 80; // Default width for layout purposes
        }
    }

    // A helper method to safely set the console cursor position.
    // Catches IOException and ArgumentOutOfRangeException if setting position is not supported or invalid.
    private static void SetCursorPosition(int left, int top)
    {
        try
        {
            Console.SetCursorPosition(left, top);
        }
        catch (System.IO.IOException)
        {
            // Ignore the error if cursor position cannot be set.
        }
        catch (ArgumentOutOfRangeException)
        {
            // Ignore if the specified position is out of the console's bounds.
        }
    }

    // Prints a message to the console in a specified color.
    public static void PrintColor(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.Write(message);
        Console.ResetColor(); // Reset color to default after printing
    }

    // Displays a formatted header with a main title and an optional sub-header (like pagination).
    // This method no longer uses Console.Clear() to avoid IOExceptions.
    public static void PrintHeader(string mainTitle, string subHeader)
    {
        // Add a few newlines to push previous content up, simulating a "clear" without Console.Clear().
        Console.WriteLine("\n\n\n"); 
        
        int consoleWidth = GetConsoleWindowWidth();

        // Print top border
        PrintColor(new string('=', consoleWidth), ConsoleColor.DarkYellow);
        Console.WriteLine();

        // Print main title centered
        string paddedMainTitle = mainTitle.PadLeft((consoleWidth + mainTitle.Length) / 2).PadRight(consoleWidth);
        PrintColor(paddedMainTitle, ConsoleColor.Blue);
        Console.WriteLine();

        // Print sub-header if provided
        if (!string.IsNullOrEmpty(subHeader))
        {
            string paddedSubHeader = subHeader.PadLeft((consoleWidth + subHeader.Length) / 2).PadRight(consoleWidth);
            PrintColor(paddedSubHeader, ConsoleColor.Green);
            Console.WriteLine();
        }
        
        // Print bottom border
        PrintColor(new string('=', consoleWidth), ConsoleColor.DarkYellow);
        Console.WriteLine("\n"); // Add some spacing after the header
    }

    // Displays the main menu options for the application.
    public static void PrintMainMenuOptions()
    {
        Console.WriteLine("Select an activity:");
        PrintColor("  1. ", ConsoleColor.Green); Console.WriteLine("Start Breathing Activity");
        PrintColor("  2. ", ConsoleColor.Green); Console.WriteLine("Start Reflection Activity");
        PrintColor("  3. ", ConsoleColor.Green); Console.WriteLine("Start Listing Activity");
        PrintColor("  4. ", ConsoleColor.Green); Console.WriteLine("Mood Check-in"); // Exceeding requirement
        PrintColor("  5. ", ConsoleColor.Green); Console.WriteLine("Settings"); // Exceeding requirement
        PrintColor("  0. ", ConsoleColor.Green); Console.WriteLine("Quit");
        Console.WriteLine(); // Add spacing
    }

    // Shows a general dotted pause animation for a given number of seconds.
    // This is a simplified animation that is robust across various terminals.
    public static void ShowDottedPause(int seconds)
    {
        SetCursorVisibility(false); // Hide cursor during animation

        for (int i = 0; i < seconds; i++)
        {
            Console.Write(".");
            Thread.Sleep(1000); // Wait 1 second per dot
        }
        SetCursorVisibility(true); // Show cursor again
    }

    // Shows a countdown timer with a clock emoji prefix.
    // Uses carriage return (\r) for in-place updates, which is more robust than backspace for varied lengths.
    // This is an exceeding requirement feature for enhanced animation.
    public static void ShowCountdown(int seconds)
    {
        Console.OutputEncoding = Encoding.UTF8; // Ensure emoji display
        SetCursorVisibility(false); // Hide cursor during animation

        // Determine max length of message to ensure proper clearing
        // e.g., "🕰️ 10 " vs "🕰️ 1 "
        int maxLength = $"🕰️ {seconds} ".Length + 2; // Add a bit extra for safety

        for (int i = seconds; i > 0; i--)
        {
            string message = $"🕰️ {i} "; 
            // Use \r to return to start of line, then overwrite with spaces to clear, then return \r, then write new message
            Console.Write($"\r{new string(' ', maxLength)}\r{message}");
            Thread.Sleep(1000);
        }
        // Clear the final countdown message from the line
        Console.Write($"\r{new string(' ', maxLength)}\r"); 
        
        SetCursorVisibility(true); // Show cursor again
    }

    // Displays a welcome animation using simple dots.
    // This ensures consistency with the user's preference for dotted animations.
    public static void AnimateWelcome(int durationSeconds)
    {
        Console.WriteLine("\n"); // Add spacing
        UIHelper.PrintColor("Loading", ConsoleColor.Blue);
        ShowDottedPause(durationSeconds); // Use the simple dotted pause for welcome animation
        Console.WriteLine("\n"); // Move to next line after animation
    }

    // Displays text with a typewriter effect, character by character.
    // This is an exceeding requirement feature for engaging content presentation.
    public static void AnimateTypewriter(string text, int delayMs)
    {
        foreach (char c in text)
        {
            Console.Write(c);
            Thread.Sleep(delayMs); // Pause after each character
        }
        Console.WriteLine(); // Move to next line after text is fully displayed
    }
}