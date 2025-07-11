// PromptGenerator.cs
using System; // Necessary for using the Random class
using System.Collections.Generic; // Necessary for using List<T> to store prompts

// This class is responsible for managing a list of journal prompts
// and providing a random prompt when requested.
public class PromptGenerator
{
    // This list stores all the potential prompts for the journal.
    public List<string> _prompts = new List<string>();

    // This object is used to generate random numbers, which helps in
    // selecting a random prompt from the list. It's initialized once.
    public Random _random = new Random();

    // This is a constructor for the PromptGenerator class.
    // When a new PromptGenerator object is created, this code runs
    // to populate the initial list of prompts.
    public PromptGenerator()
    {
        _prompts.Add("Who was the most interesting person I interacted with today?");
        _prompts.Add("What was the best part of my day?");
        _prompts.Add("How did I see the hand of the Lord in my life today?");
        _prompts.Add("What was the strongest emotion I felt today?");
        _prompts.Add("If I had one thing I could do over today, what would it be?");
        _prompts.Add("What is one thing I learned today?");
    }

    // This method selects and returns one random prompt from the list.
    public string GetRandomPrompt()
    {
        // Check if there are any prompts in the list to avoid errors.
        if (_prompts.Count == 0)
        {
            return "No prompts available.";
        }

        // Generate a random index within the bounds of the prompts list.
        int index = _random.Next(0, _prompts.Count);

        // Return the prompt at the randomly selected index.
        return _prompts[index];
    }

    // Optional: This method allows adding a new prompt to the list at runtime.
    public void AddPrompt(string newPrompt)
    {
        _prompts.Add(newPrompt);
        Console.WriteLine($"Added new prompt: \"{newPrompt}\"");
    }

    // Optional: This method displays all the prompts currently stored in the list.
    public void ListPrompts()
    {
        Console.WriteLine("--- Available Prompts ---");
        for (int i = 0; i < _prompts.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_prompts[i]}");
        }
        Console.WriteLine("-------------------------");
    }
}