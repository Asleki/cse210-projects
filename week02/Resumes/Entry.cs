// Entry.cs
using System;

// This class represents a single journal entry.
// It is responsible for holding the data associated with one entry
// and providing methods to display or prepare that data.
public class Entry
{
    // These are the member variables (attributes) that store the data for each entry.
    // They are public to allow easy access from other classes like Journal.
    public string _date;
    public string _promptText;
    public string _entryText;

    // This method is responsible for displaying the entry's content to the console.
    // It formats the date, prompt, and the user's response in a readable way.
    public void Display()
    {
        Console.WriteLine($"Date: {_date} - Prompt: {_promptText}");
        Console.WriteLine($"Entry: {_entryText}");
        Console.WriteLine(); // Adds a blank line for better readability between entries
    }

    // This method prepares the entry's data into a single string.
    // This format is suitable for writing to a file, using a chosen separator.
    public string GetStringRepresentationForSaving()
    {
        // We're using the pipe character '|' as a separator.
        // This is a common practice to keep data organized when saving to plain text files.
        return $"{_date}|{_promptText}|{_entryText}";
    }
}