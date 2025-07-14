using System; // Provides fundamental classes and base types, including DateTime for date and time operations.
using System.Collections.Generic; // Enables the use of generic collections like List<T> for storing entries.
using System.IO;                   // Necessary for file operations (reading and writing files) and path manipulation.
using System.Linq;                 // Provides LINQ (Language Integrated Query) methods, such as OrderBy and LastOrDefault, for data manipulation.
using System.Globalization;        // Required for CultureInfo.InvariantCulture for consistent date parsing.

// This class represents the user's journal, serving as the central manager for all journal entries.
// Its core responsibilities include maintaining a collection of individual entries,
// and providing methods to add new entries, display existing ones in a structured format,
// and handle the persistent saving and loading of the journal data from a file.
public class Journal
{
    // This list holds all the individual journal entries currently loaded into memory.
    // It acts as the in-memory database for the user's journal content.
    public List<Entry> _entries = new List<Entry>();

    // This field stores the full, absolute path to the default journal data file (myjournal.txt).
    // The path is dynamically calculated to ensure the file is always located in the project's root directory,
    // making file access consistent regardless of where the application is run from.
    private string _journalFilePath;

    // These fields are specifically designed to support the "once-a-day mood" feature.
    // They keep track of the last mood the user entered and the date that mood was recorded.
    // This allows the application to reuse the same mood for multiple entries on the same day,
    // reducing repetitive input for the user.
    private string _lastRecordedMood = "";
    private DateTime _lastMoodRecordedDate = DateTime.MinValue; // Initialized to a very old date to ensure mood is asked on first run.

    // The constructor for the Journal class.
    // When a new Journal object is created, this code automatically runs to set up
    // the journal's file path and attempts to load any existing journal entries.
    // This automatic loading on startup ensures that the user's previous entries
    // are immediately available when the application begins.
    public Journal()
    {
        // Calculates the path to the project's root directory.
        // AppDomain.CurrentDomain.BaseDirectory points to the executable's location (e.g., bin/Debug/net8.0/).
        // Path.Combine with ".." is used to navigate up three directory levels to reach the project root.
        string projectRootPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", ".."));
        _journalFilePath = Path.Combine(projectRootPath, "myjournal.txt"); // Sets the full path for the default journal file.

        // Automatically attempts to load entries from the default journal file when the Journal object is created.
        LoadFromFile("myjournal.txt");
    }

    // This method adds a new, complete journal entry object to the internal list of entries.
    // It is the primary way to incorporate new user reflections into the journal.
    public void AddEntry(Entry newEntry)
    {
        _entries.Add(newEntry);
    }

    // This method manages the user's mood input, implementing the "once-a-day mood" feature.
    // It checks if a mood has already been provided for the current calendar day.
    // If a mood exists for today, it is silently reused. Otherwise, the user is prompted
    // to enter their mood, and this new mood is then stored for the rest of the day.
    public string GetMoodForToday()
    {
        // Compares the date of the last recorded mood with the current date.
        // It also checks if a mood value actually exists.
        if (_lastMoodRecordedDate.Date == DateTime.Today.Date && !string.IsNullOrWhiteSpace(_lastRecordedMood))
        {
            // If the mood was recorded today, the previously stored mood is returned without prompting the user.
            return _lastRecordedMood;
        }
        else
        {
            // If it's a new day or no mood was recorded yet, the user is prompted for their current mood.
            Console.Write("How are you feeling today? (e.g., Happy, Sad, Grateful): ");
            string userMood = Console.ReadLine();
            // The newly entered mood and the current date are stored for future entries made on the same day.
            _lastRecordedMood = userMood;
            _lastMoodRecordedDate = DateTime.Today;
            return userMood;
        }
    }


    // This method iterates through all the journal entries currently in memory and displays them to the console.
    // It now applies "pretty printing" rules for grouped display by date, as per the user's request:
    // - Entries are sorted chronologically by date and then by time.
    // - A single "=== Journal Entry ===" header is displayed per day.
    // - Date and Mood are displayed only once per day, under the daily header.
    // - Subsequent entries for the same day only display Time, Prompt, and Entry.
    // - A separator line is added between entries from different days.
    public void DisplayAllEntries()
    {
        Console.WriteLine("--- Journal Entries ---");
        // Provides a message if the journal is empty, guiding the user to create entries.
        if (_entries.Count == 0)
        {
            Console.WriteLine("No entries to display. Write something first!");
            Console.WriteLine("-----------------------");
            return;
        }

        // Sorts the entries chronologically by date and then by time.
        // Uses InvariantCulture for consistent date parsing during sorting.
        var sortedEntries = _entries
            .OrderBy(e => DateTime.TryParse(e._date, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date) ? date : DateTime.MinValue)
            .ThenBy(e => DateTime.TryParse(e._time, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime time) ? time : DateTime.MinValue)
            .ToList();

        DateTime? lastDisplayedDate = null; // Tracks the date of the last entry whose header was displayed.

        // Loops through each entry in the sorted list to display it according to the new grouping rules.
        for (int i = 0; i < sortedEntries.Count; i++)
        {
            Entry currentEntry = sortedEntries[i];
            DateTime currentEntryDate;

            // Safely parse the date for comparison, using InvariantCulture.
            if (!DateTime.TryParse(currentEntry._date, CultureInfo.InvariantCulture, DateTimeStyles.None, out currentEntryDate))
            {
                currentEntryDate = DateTime.MinValue; // Default to min value if parsing fails.
            }

            // Check if this is a new day or the very first entry.
            if (!lastDisplayedDate.HasValue || currentEntryDate.Date != lastDisplayedDate.Value.Date)
            {
                // If it's not the very first entry overall, and it's a new day,
                // print the separator for the previous day's group.
                if (lastDisplayedDate.HasValue)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("-----------------------------------------------------");
                    Console.ResetColor();
                    Console.WriteLine(); // Add a blank line after the separator
                }

                // Print the daily header and date/mood for the first entry of the new day.
                Console.WriteLine("=== Journal Entry ===");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Date: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"{currentEntry._date}");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Mood: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"{currentEntry._mood}");
                Console.ResetColor();
                Console.WriteLine(); // Blank line after Mood
                lastDisplayedDate = currentEntryDate; // Update the last displayed date.
            }

            // Explicitly print Time, Prompt, and Entry for the current entry directly in this method.
            // This ensures these fields are always printed for each entry, but the Date/Mood are not repeated.
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("🕒 Time: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{currentEntry._time}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Prompt: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{currentEntry._promptText}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Entry: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{currentEntry._entryText}");
            Console.ResetColor();

            // Add a blank line after each individual entry within the same day for spacing.
            // Only add if it's not the last entry and the next entry is on the same date.
            if (i < sortedEntries.Count - 1)
            {
                DateTime nextEntryDate;
                if (DateTime.TryParse(sortedEntries[i+1]._date, CultureInfo.InvariantCulture, DateTimeStyles.None, out nextEntryDate))
                {
                    if (currentEntryDate.Date == nextEntryDate.Date)
                    {
                        Console.WriteLine();
                    }
                }
            }
        }

        // After the loop, if there were any entries, print the final separator.
        if (_entries.Count > 0)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("-----------------------------------------------------");
            Console.ResetColor();
        }
        Console.WriteLine("-----------------------"); // Overall journal footer
    }

    // This method saves all the current journal entries from memory to a specified text file.
    // It constructs the full file path dynamically to ensure the file is saved in the project's root directory.
    public void SaveToFile(string filename)
    {
        // Constructs the complete file path for saving, ensuring it's relative to the project's root.
        // This allows the user to specify just a filename (e.g., "myjournal.txt"), and the application
        // handles placing it in the correct location.
        string filePathToSave = Path.Combine(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..")), filename);

        try
        {
            // Uses a 'using' statement to ensure the StreamWriter resource is properly managed:
            // the file is automatically closed and system resources are released, even if errors occur during writing.
            using (StreamWriter outputFile = new StreamWriter(filePathToSave))
            {
                // Iterates through each journal entry in the in-memory list.
                // For each entry, it calls GetStringRepresentationForSaving() (from Entry.cs),
                // which now provides a multi-line, human-readable string.
                // Each complete multi-line entry is written to the file.
                foreach (Entry entry in _entries)
                {
                    outputFile.WriteLine(entry.GetStringRepresentationForSaving());
                }
            }
            Console.WriteLine($"Journal saved to {filePathToSave}"); // Confirms successful saving to the user.
        }
        catch (Exception ex)
        {
            // Catches any errors that occur during the file saving process (e.g., permission issues).
            // An informative error message is displayed to the user.
            Console.WriteLine($"Error saving journal to {filePathToSave}: {ex.Message}");
        }
    }

    // This method loads journal entries from a specified text file into the application's memory.
    // It is now designed to parse the new multi-line, human-readable format.
    public void LoadFromFile(string filename)
    {
        // Constructs the complete file path for loading, ensuring it's relative to the project's root.
        string filePathToLoad = Path.Combine(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..")), filename);

        // Clears the existing list of entries to prevent duplication and ensure only loaded data is present.
        _entries.Clear();
        // Resets the mood tracking variables when a new journal is loaded,
        // as the mood context might change with different journal content.
        _lastRecordedMood = "";
        _lastMoodRecordedDate = DateTime.MinValue;

        try
        {
            // Checks if the specified file actually exists before attempting to read its contents.
            if (File.Exists(filePathToLoad))
            {
                string[] lines = System.IO.File.ReadAllLines(filePathToLoad);
                Entry currentEntry = null; // Stores the entry being built from multiple lines.
                string currentEntryTextBuffer = ""; // Buffer to accumulate multi-line entry text.

                // Iterates through each line read from the file to reconstruct individual Entry objects.
                // This parsing logic now handles the multi-line format.
                foreach (string line in lines)
                {
                    string trimmedLine = line.Trim();

                    // Checks for the start of a new journal entry.
                    if (trimmedLine == "=== Journal Entry ===")
                    {
                        // If a new entry header is found, and a previous entry was being built,
                        // finalize and add the previous entry to the list.
                        if (currentEntry != null)
                        {
                            currentEntry._entryText = currentEntryTextBuffer.Trim(); // Assign buffered text
                            _entries.Add(currentEntry);
                        }
                        currentEntry = new Entry(); // Start a new, empty Entry object.
                        currentEntryTextBuffer = ""; // Reset the entry text buffer for the new entry.
                    }
                    // If an entry is currently being built, parse the line based on its label.
                    else if (currentEntry != null)
                    {
                        if (trimmedLine.StartsWith("Date:", StringComparison.OrdinalIgnoreCase))
                        {
                            currentEntry._date = trimmedLine.Substring("Date:".Length).Trim();
                        }
                        else if (trimmedLine.StartsWith("Time:", StringComparison.OrdinalIgnoreCase))
                        {
                            currentEntry._time = trimmedLine.Substring("Time:".Length).Trim();
                        }
                        else if (trimmedLine.StartsWith("Mood:", StringComparison.OrdinalIgnoreCase))
                        {
                            currentEntry._mood = trimmedLine.Substring("Mood:".Length).Trim();
                        }
                        else if (trimmedLine.StartsWith("Prompt:", StringComparison.OrdinalIgnoreCase))
                        {
                            currentEntry._promptText = trimmedLine.Substring("Prompt:".Length).Trim();
                        }
                        else if (trimmedLine.StartsWith("Entry:", StringComparison.OrdinalIgnoreCase))
                        {
                            // Start accumulating entry text.
                            currentEntryTextBuffer = trimmedLine.Substring("Entry:".Length).Trim();
                        }
                        else if (!string.IsNullOrWhiteSpace(trimmedLine))
                        {
                            // If it's not a new label and not an empty line, and we are within an entry,
                            // it's considered a continuation of the entry text.
                            // A newline is added to preserve the original line breaks.
                            if (!string.IsNullOrEmpty(currentEntryTextBuffer))
                            {
                                currentEntryTextBuffer += "\n" + trimmedLine;
                            }
                            else
                            {
                                currentEntryTextBuffer = trimmedLine; // Should not happen if "Entry:" is always first.
                            }
                        }
                    }
                }

                // After the loop finishes, add the very last entry that was being built.
                if (currentEntry != null)
                {
                    currentEntry._entryText = currentEntryTextBuffer.Trim(); // Assign buffered text for the last entry
                    _entries.Add(currentEntry);
                }

                // After successfully loading entries, this block attempts to initialize the
                // "once-a-day mood" tracking based on the last entry in the loaded journal.
                // This ensures that if the user loads a journal from today, the mood feature
                // behaves correctly for subsequent entries.
                if (_entries.Any())
                {
                    Entry lastEntry = _entries.LastOrDefault(); // Retrieves the last entry in the loaded list.
                    // Checks if the last entry exists and its date can be successfully parsed.
                    if (lastEntry != null && DateTime.TryParse(lastEntry._date, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime lastDate))
                    {
                        _lastRecordedMood = lastEntry._mood; // Sets the last recorded mood from the loaded entry.
                        _lastMoodRecordedDate = lastDate;    // Sets the date of the last recorded mood.
                    }
                }
            }
            else
            {
                // If the specified file does not exist, the program will simply not load anything.
                // The message "File not found" is intentionally not printed for cleaner output.
            }
        }
        catch (Exception ex)
        {
            // Catches any unexpected errors that occur during the file loading process.
            // An informative error message is displayed to the user.
            Console.WriteLine($"Error loading journal from {filePathToLoad}: {ex.Message}");
        }
    }
}
