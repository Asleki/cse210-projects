using System; // Provides fundamental classes and base types, including DateTime for date and time operations.

// This class represents a single journal entry.
// Its primary responsibility is to store all the specific details of one entry
// and to provide methods for presenting this data in a readable format
// and for preparing it to be saved persistently.
public class Entry
{
    // These are the core pieces of information that make up a journal entry.
    // They are accessible to other parts of the application, such as the Journal class,
    // to allow for easy management and display of entries.
    public string _date;       // Stores the date when the entry was created.
    public string _promptText; // Stores the specific prompt that the user responded to.
    public string _entryText;  // Stores the user's actual response or thoughts for the entry.
    public string _time;       // Stores the exact time of day when the entry was created, adding precision.
    public string _mood;       // Stores the user's reported mood at the time of writing, providing emotional context.

    // This method is responsible for displaying the entry's content to the console.
    // It formats the time, prompt, and the user's response in a readable way.
    // Date and Mood are intentionally excluded from this method, as they are handled
    // by the Journal class for daily grouping. All output will now be in blue.
    public void Display()
    {
        Console.ForegroundColor = ConsoleColor.Blue; // Set color for all entry display output

        Console.Write("🕒 Time: ");
        Console.WriteLine($"{_time}");

        Console.Write("Prompt: ");
        Console.WriteLine($"{_promptText}");

        Console.Write("Entry: ");
        Console.WriteLine($"{_entryText}");
        // No Console.ResetColor() as all output should be blue
    }

    // This method prepares the entry's data into a multi-line string for saving to a file.
    // The format is designed for human readability when viewing the raw text file,
    // with clear labels and line breaks between each piece of information.
    // A distinct header ("=== Journal Entry ===") marks the beginning of each new entry
    // to facilitate easier parsing when loading the data back into the application.
    public string GetStringRepresentationForSaving()
    {
        return "=== Journal Entry ===\n" +
               $"Date: {_date}\n" +
               $"Time: {_time}\n" +
               $"Mood: {_mood}\n" +
               $"Prompt: {_promptText}\n" +
               $"Entry: {_entryText}\n"; // Each field is on a new line, with a final newline for consistency.
    }
}
