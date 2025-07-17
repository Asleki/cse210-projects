
// The main entry point for the Scripture Memorizer application.
public class Program
{
    // --- Exceeding Requirements ---
    // 1. Multiple Scripture Sources: Includes scriptures from Book of Mormon, Old Testament, New Testament, and Doctrine and Covenants.
    // 2. User Selection: Allows the user to choose a scripture source at the start.
    // 3. Bible Testament Selection: If "Bible" is chosen, prompts for Old or New Testament.
    // 4. Bordered Header: Displays "========= SCRIPTURE MEMORIZER =========" at the top.
    // 5. Random Selection: A random scripture is chosen from the selected category.
    // 6. Hiding Logic: Only hides words that are not already hidden (stretch challenge from previous activity).
    // -----------------------------

    private static Random _globalRandom = new Random(); // Use a single Random instance

    public static void Main(string[] args)
    {
        // Initialize the scripture library
        Dictionary<string, List<Scripture>> scriptureLibrary = InitializeScriptureLibrary();

        Scripture currentScripture = null;

        while (currentScripture == null)
        {
            Console.Clear();
            DisplayHeader();
            Console.WriteLine("\nSelect a scripture source:");
            Console.WriteLine("1. Book of Mormon");
            Console.WriteLine("2. Bible");
            Console.WriteLine("3. Doctrine and Covenants");
            Console.Write("Enter your choice (1-3): ");
            string sourceChoice = Console.ReadLine();

            List<Scripture> selectedScriptures = null;

            switch (sourceChoice)
            {
                case "1": // Book of Mormon
                    selectedScriptures = scriptureLibrary["Book of Mormon"];
                    break;
                case "2": // Bible
                    Console.Clear();
                    DisplayHeader();
                    Console.WriteLine("\nSelect a Bible Testament:");
                    Console.WriteLine("1. Old Testament");
                    Console.WriteLine("2. New Testament");
                    Console.Write("Enter your choice (1-2): ");
                    string testamentChoice = Console.ReadLine();

                    if (testamentChoice == "1")
                    {
                        selectedScriptures = scriptureLibrary["Old Testament"];
                    }
                    else if (testamentChoice == "2")
                    {
                        selectedScriptures = scriptureLibrary["New Testament"];
                    }
                    else
                    {
                        Console.WriteLine("Invalid testament choice. Please try again.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        continue; // Go back to main source selection
                    }
                    break;
                case "3": // Doctrine and Covenants
                    selectedScriptures = scriptureLibrary["Doctrine and Covenants"];
                    break;
                default:
                    Console.WriteLine("Invalid source choice. Please try again.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    continue; // Go back to main source selection
            }

            if (selectedScriptures != null && selectedScriptures.Any())
            {
                // Select a random scripture from the chosen category
                int randomIndex = _globalRandom.Next(0, selectedScriptures.Count);
                currentScripture = selectedScriptures[randomIndex];
            }
            else
            {
                Console.WriteLine("No scriptures found for the selected category. Please try again.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }


        string userInput = "";
        int wordsToHidePerTurn = 3; // Define how many words to hide per turn.

        // Loop until the user types 'quit' or all words are hidden.
        while (userInput.ToLower() != "quit" && !currentScripture.IsCompletelyHidden())
        {
            // Clear the console screen.
            Console.Clear();

            // Display the header.
            DisplayHeader();

            // Display the complete scripture, including the reference and the text.
            Console.WriteLine("\n" + currentScripture.GetDisplayText());
            Console.WriteLine(); // Add a blank line for better readability.

            // Prompt the user to press the enter key or type quit.
            Console.WriteLine("Press Enter to hide words, or type 'quit' to exit.");
            userInput = Console.ReadLine();

            // If the user presses Enter (without typing quit), hide words.
            if (userInput.ToLower() != "quit")
            {
                currentScripture.HideRandomWords(wordsToHidePerTurn);
            }
        }

        // Final display when all words are hidden or user quits.
        Console.Clear();
        DisplayHeader();
        Console.WriteLine("\n" + currentScripture.GetDisplayText());
        Console.WriteLine("\nProgram ended. All words hidden or 'quit' entered.");
    }

    // Displays the bordered header.
    private static void DisplayHeader()
    {
        Console.WriteLine("========= SCRIPTURE MEMORIZER =========");
    }

    // Initializes and returns the scripture library.
    private static Dictionary<string, List<Scripture>> InitializeScriptureLibrary()
    {
        var library = new Dictionary<string, List<Scripture>>();

        // Book of Mormon Scriptures
        library["Book of Mormon"] = new List<Scripture>
        {
            new Scripture(new Reference("1 Nephi", 3, 7), "And it came to pass that I, Nephi, said unto my father: I will go and do the things which the Lord hath commanded, for I know that the Lord giveth no commandments unto the children of men, save he shall prepare a way for them that they may accomplish the thing which he commandeth them."),
            new Scripture(new Reference("Alma", 32, 21), "And now as I said concerning faith—faith is not to have a perfect knowledge of things; therefore if ye have faith ye hope for things which are not seen, which are true."),
            new Scripture(new Reference("Moroni", 10, 4, 5), "And when ye shall receive these things, I would exhort you that ye would ask God, the Eternal Father, in the name of Christ, if these things are not true; and if ye shall ask with a sincere heart, with real intent, having faith in Christ, he will manifest the truth of it unto you, by the power of the Holy Ghost. And by the power of the Holy Ghost ye may know the truth of all things.")
        };

        // Old Testament Scriptures
        library["Old Testament"] = new List<Scripture>
        {
            new Scripture(new Reference("Proverbs", 3, 5, 6), "Trust in the Lord with all thine heart; and lean not unto thine own understanding. In all thy ways acknowledge him, and he shall direct thy paths."),
            new Scripture(new Reference("Psalm", 23, 1), "The Lord is my shepherd; I shall not want."),
            new Scripture(new Reference("Isaiah", 1, 18), "Come now, and let us reason together, saith the Lord: though your sins be as scarlet, they shall be as white as snow; though they be red like crimson, they shall be as wool.")
        };

        // New Testament Scriptures
        library["New Testament"] = new List<Scripture>
        {
            new Scripture(new Reference("John", 3, 16), "For God so loved the world, that he gave his only begotten Son, that whosoever believeth in him should not perish, but have everlasting life."),
            new Scripture(new Reference("Philippians", 4, 13), "I can do all things through Christ which strengtheneth me."),
            new Scripture(new Reference("Matthew", 11, 28, 30), "Come unto me, all ye that labour and are heavy laden, and I will give you rest. Take my yoke upon you, and learn of me; for I am meek and lowly in heart: and ye shall find rest unto your souls. For my yoke is easy, and my burden is light.")
        };

        // Doctrine and Covenants Scriptures
        library["Doctrine and Covenants"] = new List<Scripture>
        {
            new Scripture(new Reference("D&C", 1, 30), "And also those to whom these commandments were given, might have power to lay the foundation of this church, and to bring it forth out of obscurity and out of darkness, the only true and living church upon the face of the whole earth, with which I, the Lord, am well pleased, speaking unto the church collectively and not individually."),
            new Scripture(new Reference("D&C", 8, 2, 3), "Yea, behold, I will tell you in your mind and in your heart, by the Holy Ghost, which shall come upon you and which shall dwell in your heart. Now, behold, this is the spirit of revelation; behold, this is the spirit by which Moses brought the children of Israel through the Red Sea on dry ground."),
            new Scripture(new Reference("D&C", 19, 16, 17), "For behold, I, God, have suffered these things for all, that they might not suffer if they would repent; But if they would not repent they must suffer even as I; Which suffering caused myself, even God, the greatest of all, to tremble because of pain, and to bleed at every pore, and to suffer both body and spirit—and would that I might not drink the bitter cup, and shrink—Nevertheless, glory be to the Father, and I partook and finished my preparations unto the children of men.")
        };

        return library;
    }
}
