using System;
using System.Collections.Generic;
using System.IO;

// BookOfMormonStudy.cs
//
// This class provides functionality for the Book of Mormon study feature.
// It allows users to read verses, highlight words or sentences,
// and save their notes to a file.
public class BookOfMormonStudy
{
    // A dictionary to store the books and their corresponding verses.
    // This allows for easy lookup by book title.
    private Dictionary<string, List<string>> _books;
    private string _notesFileName = "notes.txt";

    // The constructor initializes the dictionary and populates it with a hardcoded list of verses.
    public BookOfMormonStudy()
    {
        _books = new Dictionary<string, List<string>>();
        
        
        _books.Add("1 Nephi", new List<string>
        {
            "1 Nephi 1:1 - I, Nephi, having been born of goodly parents, therefore I was taught somewhat in all the learning of my father; and having seen many afflictions in the course of my days, nevertheless, having been highly favored of the Lord in all my days; yea, having had a great knowledge of the goodness and the mysteries of God, therefore I make a record of my proceedings in my days.",
            "1 Nephi 1:2 - Yea, I make a record in the language of my father, which consists of the learning of the Jews and the language of the Egyptians.",
            "1 Nephi 1:3 - And I know that the record which I make is true; and I make it with mine own hand; and I make it according to my knowledge.",
            "1 Nephi 1:4 - For it came to pass in the commencement of the first year of the reign of Zedekiah, king of Judah, my father Lehi having dwelt at Jerusalem in all his days; and in that same year there came many prophets, prophesying unto the people that they must repent, or the great city Jerusalem must be destroyed.",
            "1 Nephi 1:5 - Wherefore, it came to pass that my father, Lehi, as he went forth prayed unto the Lord, yea, even with all his heart, in behalf of his people.",
            "1 Nephi 1:6 - And it came to pass as he was thus praying unto the Lord, there came a pillar of fire and dwelt upon a rock before him; and he saw and heard much; and because of the things which he saw and heard he did quake and tremble exceedingly.",
            "1 Nephi 1:7 - And it came to pass that he returned to his own house at Jerusalem; and he cast himself upon his bed, overcome with the Spirit and the things which he had seen.",
            "1 Nephi 1:8 - And being thus overcome with the Spirit, he was carried away in a vision, even that he saw the heavens open, and he thought he saw God sitting upon his throne, surrounded with numberless concourses of angels in the attitude of singing and praising their God.",
            "1 Nephi 1:9 - And it came to pass that he saw One descending out of the midst of heaven, and he beheld that his luster was above that of the sun at noon-day.",
            "1 Nephi 1:10 - And he also saw twelve others following him, and their luster did exceed that of the stars in the firmament."
        });

        _books.Add("2 Nephi", new List<string>
        {
            "2 Nephi 2:25 - Adam fell that men might be; and men are, that they might have joy.",
            "2 Nephi 31:20 - Wherefore, ye must press forward with a steadfastness in Christ, having a perfect brightness of hope, and a love of God and of all men. Wherefore, if ye shall press forward, feasting upon the word of Christ, and endure to the end, behold, thus saith the Father: Ye shall have eternal life."
        });

        _books.Add("Alma", new List<string>
        {
            "Alma 32:21 - And now as I said concerning faith—faith is not to have a perfect knowledge of things; therefore if ye have faith ye hope for things which are not seen, which are true.",
            "Alma 37:37 - Counsel with the Lord in all thy doings, and he will direct thee for good; yea, when thou liest down at night lie down unto the Lord, that he may watch over you in your sleep; and when thou risest in the morning let thy heart be full of thanks unto God; and if ye do these things, ye shall be lifted up at the last day."
        });
    }

    // Displays the main study menu and handles user input.
    public void StudyMenu()
    {
        string choice = "";
        while (choice != "0")
        {
            Console.WriteLine("\n--- Book of Mormon Study Menu ---");
            Console.WriteLine("1. Read a verse");
            Console.WriteLine("2. View my highlighted notes");
            Console.WriteLine("0. Go back to Main Menu");
            Console.Write("Select an option: ");
            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ReadVerse();
                    break;
                case "2":
                    DisplayNotes();
                    break;
                case "0":
                    Console.WriteLine("Returning to Main Menu.");
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid option. Please try again.");
                    Console.ResetColor();
                    break;
            }
        }
    }

    // Allows the user to select a book and a verse to read and highlight.
    private void ReadVerse()
    {
        Console.WriteLine("\n--- Select a Book ---");
        int i = 1;
        foreach (var book in _books.Keys)
        {
            Console.WriteLine($"{i}. {book}");
            i++;
        }

        Console.Write("Enter the number of the book you want to read: ");
        if (int.TryParse(Console.ReadLine(), out int bookChoice) && bookChoice > 0 && bookChoice <= _books.Count)
        {
            string selectedBook = new List<string>(_books.Keys)[bookChoice - 1];
            List<string> verses = _books[selectedBook];

            Console.WriteLine($"\n--- Verses in {selectedBook} ---");
            for (int j = 0; j < verses.Count; j++)
            {
                Console.WriteLine($"{j + 1}. {verses[j]}");
            }
            
            Console.Write("Enter the number of the verse you want to study: ");
            if (int.TryParse(Console.ReadLine(), out int verseChoice) && verseChoice > 0 && verseChoice <= verses.Count)
            {
                string selectedVerse = verses[verseChoice - 1];
                Console.WriteLine("\n" + selectedVerse);
                
                Console.Write("Enter a word or sentence you would like to highlight: ");
                string highlightText = Console.ReadLine();
                
                if (!string.IsNullOrEmpty(highlightText) && selectedVerse.Contains(highlightText))
                {
                    SaveNote(selectedBook, selectedVerse, highlightText);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("The text you entered was not found in the verse or was empty. No note was saved.");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid verse number.");
                Console.ResetColor();
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid book number.");
            Console.ResetColor();
        }
    }

    // Saves a highlighted note to the notes.txt file.
    private void SaveNote(string book, string verse, string highlightedText)
    {
        try
        {
            // The note is appended to the file, so existing notes are not overwritten.
            using (StreamWriter writer = File.AppendText(_notesFileName))
            {
                writer.WriteLine($"Book: {book}");
                writer.WriteLine($"Verse: {verse}");
                writer.WriteLine($"Highlighted: \"{highlightedText}\"");
                writer.WriteLine("---");
            }
            Console.WriteLine("Note saved successfully!");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"An error occurred while saving the note: {ex.Message}");
            Console.ResetColor();
        }
    }

    // Reads and displays all saved notes from the notes.txt file.
    private void DisplayNotes()
    {
        Console.WriteLine("\n--- Your Highlighted Notes ---");
        if (!File.Exists(_notesFileName) || new FileInfo(_notesFileName).Length == 0)
        {
            Console.WriteLine("You have no notes saved yet.");
            return;
        }

        try
        {
            string[] notes = File.ReadAllLines(_notesFileName);
            foreach (string note in notes)
            {
                Console.WriteLine(note);
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"An error occurred while loading the notes: {ex.Message}");
            Console.ResetColor();
        }
    }
}
// This class provides functionality for the Book of Mormon study feature.