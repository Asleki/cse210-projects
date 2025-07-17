
// Reference.cs
// Represents the scripture reference (e.g., "John 3:16" or "Proverbs 3:5-6").
public class Reference
{
    private string _book;        // The book of the scripture (e.g., "John").
    private int _chapter;        // The chapter number.
    private int _startVerse;     // The starting verse number.
    private int _endVerse;       // The ending verse number (optional, for verse ranges).

    // Constructor for a single verse scripture reference (e.g., "John 3:16").
    public Reference(string book, int chapter, int verse)
    {
        _book = book;
        _chapter = chapter;
        _startVerse = verse;
        _endVerse = verse; // For a single verse, start and end are the same.
    }

    // Constructor for a scripture reference with a verse range (e.g., "Proverbs 3:5-6").
    public Reference(string book, int chapter, int startVerse, int endVerse)
    {
        _book = book;
        _chapter = chapter;
        _startVerse = startVerse;
        _endVerse = endVerse;
    }

    // Returns the display text of the scripture reference.
    // Formats as "Book Chapter:Verse" or "Book Chapter:StartVerse-EndVerse".
    public string GetDisplayText()
    {
        if (_startVerse == _endVerse)
        {
            // Single verse format
            return $"{_book} {_chapter}:{_startVerse}";
        }
        else
        {
            // Verse range format
            return $"{_book} {_chapter}:{_startVerse}-{_endVerse}";
        }
    }
}

