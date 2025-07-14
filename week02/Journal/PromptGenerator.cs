using System; // Provides fundamental classes and base types, including Random for generating random numbers.
using System.Collections.Generic; // Enables the use of generic collections like List<T> for storing prompts.
using System.Linq; // Provides LINQ (Language Integrated Query) methods, useful for filtering and ordering collections.

// This class is designed to manage and provide journal prompts.
// Its primary role is to offer various ways to retrieve prompts,
// supporting both single random prompts and a selection of unique prompts for extended journaling.
public class PromptGenerator
{
    // This list holds all the predefined journal prompts that the application can offer to the user.
    // It serves as the central repository for all possible journaling starting points.
    public List<string> _prompts = new List<string>();

    // This object is used to generate random numbers. It is essential for
    // selecting prompts unpredictably, ensuring a fresh experience for the user
    // each time they request a prompt. It is initialized once to maintain randomness across calls.
    public Random _random = new Random();

    // The constructor for the PromptGenerator class.
    // When a new instance of the PromptGenerator is created, this code automatically runs
    // to populate the initial set of prompts. This ensures the application has prompts ready
    // to offer to the user from the moment it starts.
    public PromptGenerator()
    {
        _prompts.Add("Who was the most interesting person encountered today?");
        _prompts.Add("What was the most positive experience of the day?");
        _prompts.Add("How was a challenge handled today, and what was learned from it?");
        _prompts.Add("What emotion was most prominent today, and why?");
        _prompts.Add("If one decision could be re-evaluated from today, what would it be?");
        _prompts.Add("What new insight or piece of information was gained today?");
        _prompts.Add("Describe a moment today that brought a sense of joy or contentment.");
        _prompts.Add("What small victory or accomplishment was achieved today?");
        _prompts.Add("Reflect on a personal goal: what progress was made today?");
        _prompts.Add("What aspects of life evoke a feeling of gratitude right now?");
    }

    // This method provides a single, randomly selected prompt from the available list.
    // It is useful when the user simply wants a quick, unexpected starting point for their entry.
    public string GetRandomPrompt()
    {
        // A check is performed to ensure the list of prompts is not empty,
        // preventing errors if no prompts are available.
        if (_prompts.Count == 0)
        {
            return "No prompts currently available. Please add some.";
        }

        // A random index is generated within the valid range of the prompts list.
        int index = _random.Next(0, _prompts.Count);

        // The prompt located at the randomly chosen index is returned.
        return _prompts[index];
    }

    // This method generates and returns a specified number of unique prompts.
    // It is designed to support the "multiple prompts" feature, allowing users
    // to choose from a selection or request more options. It prevents the same
    // prompt from being offered again within a single journaling session if already used.
    public List<string> GetMultiplePrompts(int count, List<string> excludedPrompts = null)
    {
        // Initializes the list of prompts to exclude if none are provided,
        // ensuring the method can always operate without null reference issues.
        if (excludedPrompts == null)
        {
            excludedPrompts = new List<string>();
        }

        // Filters the main list of prompts, removing any that are present in the
        // excluded list. This ensures that only fresh, unused prompts are considered.
        // A HashSet is used for efficient lookup of excluded prompts.
        HashSet<string> excludedSet = new HashSet<string>(excludedPrompts);
        List<string> availablePrompts = _prompts.Where(p => !excludedSet.Contains(p)).ToList();

        // If the number of available unique prompts is less than the requested count,
        // all remaining available prompts are returned, shuffled randomly.
        // This prevents requesting more prompts than exist.
        if (availablePrompts.Count < count)
        {
            return availablePrompts.OrderBy(p => _random.Next()).ToList(); // Shuffles the list randomly
        }

        // Selects the requested 'count' of unique random prompts.
        List<string> selectedPrompts = new List<string>();
        while (selectedPrompts.Count < count && availablePrompts.Any())
        {
            // A random index is chosen from the currently available prompts.
            int index = _random.Next(0, availablePrompts.Count);
            string prompt = availablePrompts[index];
            selectedPrompts.Add(prompt);
            // The selected prompt is removed from the available list to ensure it's not
            // selected again, guaranteeing uniqueness within the returned set.
            availablePrompts.RemoveAt(index);
        }

        return selectedPrompts;
    }

    // This method provides a way to add a new prompt to the internal list of prompts
    // while the application is running. This enhances flexibility, allowing the prompt
    // set to be expanded without requiring a code change and recompile.
    public void AddPrompt(string newPrompt)
    {
        _prompts.Add(newPrompt);
        Console.WriteLine($"Added new prompt: \"{newPrompt}\"");
    }

    // This method displays all the prompts currently stored in the list to the console.
    // It is useful for debugging or for a potential future feature where users can
    // view or manage the available prompts.
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
