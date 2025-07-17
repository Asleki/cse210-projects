using System;
using System.Collections.Generic;
using System.Linq;


// Represents a single word in the scripture.
public class Word
{
    private string _text;       // The actual text of the word.
    private bool _isHidden;     // A flag indicating if the word is currently hidden.

    // Constructor to initialize a new Word object.
    public Word(string text)
    {
        _text = text;
        _isHidden = false; // Words are visible by default.
    }

    // Hides the word by setting the _isHidden flag to true.
    public void Hide()
    {
        _isHidden = true;
    }

    // Checks if the word is currently hidden.
    public bool IsHidden()
    {
        return _isHidden;
    }

    // Returns the display text of the word.
    // If hidden, it returns underscores matching the length of the word.
    // Otherwise, it returns the original word text.
    public string GetDisplayText()
    {
        if (_isHidden)
        {
            // Create a string of underscores with the same length as the word.
            return new string('_', _text.Length);
        }
        else
        {
            return _text;
        }
    }
}
