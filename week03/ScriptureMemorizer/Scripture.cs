
// Represents the entire scripture, including its reference and text,
// managing the hiding and displaying of words.
public class Scripture
{
    private Reference _reference;   // The reference object for the scripture.
    private List<Word> _words;      // A list of Word objects representing the scripture text.
    private Random _random;         // Random number generator for hiding words.

    // Constructor to initialize a new Scripture object.
    // It takes a Reference object and the scripture text as a string.
    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = new List<Word>();
        _random = new Random();

        // Split the text into words and create Word objects.
        // Using StringSplitOptions.RemoveEmptyEntries to handle multiple spaces.
        string[] textWords = text.Split(new char[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string wordText in textWords)
        {
            _words.Add(new Word(wordText));
        }
    }

    // Hides a specified number of random words that are not already hidden.
    // This implements the stretch challenge requirement.
    public void HideRandomWords(int count)
    {
        // Get all words that are currently not hidden.
        List<Word> unhiddenWords = _words.Where(word => !word.IsHidden()).ToList();

        // If there are no unhidden words, or we've requested to hide more than available,
        // just hide all remaining unhidden words.
        int wordsToHide = Math.Min(count, unhiddenWords.Count);

        // Randomly select and hide words from the unhidden list.
        for (int i = 0; i < wordsToHide; i++)
        {
            // Select a random index from the remaining unhidden words.
            int indexToHide = _random.Next(0, unhiddenWords.Count);
            unhiddenWords[indexToHide].Hide();

            // Remove the hidden word from the temporary list to avoid re-hiding it in the same turn.
            unhiddenWords.RemoveAt(indexToHide);
        }
    }

    // Returns the full display text of the scripture, including the reference
    // and the text with hidden words replaced by underscores.
    public string GetDisplayText()
    {
        // Build the scripture text by getting the display text for each word.
        string scriptureText = string.Join(" ", _words.Select(word => word.GetDisplayText()));
        return $"{_reference.GetDisplayText()} {scriptureText}";
    }

    // Checks if all words in the scripture are hidden.
    public bool IsCompletelyHidden()
    {
        // Returns true if all words in the _words list are hidden.
        return _words.All(word => word.IsHidden());
    }
}

// This class represents a scripture reference, including the book, chapter, and verse(s).
// It provides methods to format the reference for display.