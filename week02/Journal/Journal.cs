// Journal.cs
// This file defines the Journal class, which manages a collection of journal entries.
// It allows adding, displaying, saving, and loading entries from a file.
using System;
using System.Collections.Generic; // Necessary for using List<T> to store entries
using System.IO;                  // Necessary for file operations (reading and writing files)

// This class represents the user's journal.
// Its main responsibility is to manage a collection of individual journal entries.
public class Journal
{
    // This is the list where all the journal entries will be stored.
    // It's initialized as an empty list when a Journal object is created.
    public List<Entry> _entries = new List<Entry>();

    // This method is used to add a new journal entry to the list of entries.
    public void AddEntry(Entry newEntry)
    {
        _entries.Add(newEntry);
    }

    // This method goes through all the entries currently in the journal
    // and displays them one by one to the console.
    public void DisplayAllEntries()
    {
        Console.WriteLine("--- Journal Entries ---");
        // Loop through each entry in the list and tell it to display itself.
        foreach (Entry entry in _entries)
        {
            entry.Display();
        }
        Console.WriteLine("-----------------------");
    }

    // This method saves all the current journal entries to a text file.
    // It takes the desired filename as an input.
    public void SaveToFile(string filename)
    {
        // A 'using' statement ensures the file is properly closed and resources are released
        // even if an error occurs during writing.
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            // Iterate through each entry and write its formatted string to the file.
            foreach (Entry entry in _entries)
            {
                outputFile.WriteLine(entry.GetStringRepresentationForSaving());
            }
        }
        Console.WriteLine($"Journal saved to {filename}");
    }

    // This method loads journal entries from a specified text file.
    // Any entries currently in memory will be replaced by the loaded ones.
    public void LoadFromFile(string filename)
    {
        // First, clear any existing entries to start fresh with the loaded data.
        _entries.Clear();

        // Check if the file actually exists before attempting to read it.
        if (File.Exists(filename))
        {
            // Read all lines from the file into an array of strings.
            string[] lines = System.IO.File.ReadAllLines(filename);

            // Process each line to reconstruct an Entry object.
            foreach (string line in lines)
            {
                // Split the line using the pipe character '|' as a separator,
                // as this is what we used when saving the entries.
                string[] parts = line.Split('|');

                // Ensure the line has the expected number of parts (date, prompt, response).
                if (parts.Length == 3)
                {
                    // Create a new Entry object and populate its attributes
                    // with the data parsed from the file.
                    Entry loadedEntry = new Entry();
                    loadedEntry._date = parts[0];
                    loadedEntry._promptText = parts[1];
                    loadedEntry._entryText = parts[2];
                    _entries.Add(loadedEntry);
                }
            }
            Console.WriteLine($"Journal loaded from {filename}");
        }
        else
        {
            Console.WriteLine($"File not found: {filename}");
        }
    }
}