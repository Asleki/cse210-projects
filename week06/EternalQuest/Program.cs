using System;
using System.IO;

// Program.cs
//
// This is the main entry point for the Digihealth App.
// It handles the initial user experience, including profile setup
// and the main program loop.
//
// Features exceeding the basic requirements:
//  - Gamification and Leveling System:
//    Includes a user leveling system based on points earned.
//  - "Together" Competition:
//    A multiplayer feature for exercise challenges with virtual opponents.
//  - Comprehensive Health Metrics:
//    Tracks a wide range of body metrics and includes a body fat calculator
//    and a unit converter.
//  - Cognitive and Spiritual Development:
//    Features a Book of Mormon study tool and a health-themed IQ test.
//  - Enhanced User Experience:
//    Tracks a daily streak, logs meals with custom food options,
//    and remembers common meals.
//
// The Main method will manage the welcome message, check for an existing user
// profile, and hand off control to a GoalManager or similar class
// to display the main menu.
public class Program
{
    public static void Main(string[] args)
    {
        // Display the header with yellow color
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("======================================");
        Console.WriteLine("          Digihealth App");
        Console.WriteLine("======================================");
        Console.ResetColor();

        // The name of the file to store user profile data
        string profileFileName = "profile.txt";
        bool isFirstTimeUser = !File.Exists(profileFileName);

        if (isFirstTimeUser)
        {
            Console.WriteLine("\nHello! It looks like you're a first-time user.");
            Console.WriteLine("Let's set up your profile.");
            
            // Collect profile information from the user
            Console.Write("Enter your full name or username: ");
            string username = Console.ReadLine();

            Console.Write("Enter your email address: ");
            string email = Console.ReadLine();

            Console.WriteLine("\n--- Address Information ---");
            Console.Write("Home address: ");
            string homeAddress = Console.ReadLine();
            
            Console.Write("Work address: ");
            string workAddress = Console.ReadLine();

            Console.Write("Church address: ");
            string churchAddress = Console.ReadLine();

            Console.Write("\nEnter any health conditions (e.g., 'allergies, diabetes'): ");
            string healthConditions = Console.ReadLine();

            // Attempt to save the profile data to a file
            try
            {
                using (StreamWriter writer = new StreamWriter(profileFileName))
                {
                    writer.WriteLine($"Username: {username}");
                    writer.WriteLine($"Email: {email}");
                    writer.WriteLine($"Home Address: {homeAddress}");
                    writer.WriteLine($"Work Address: {workAddress}");
                    writer.WriteLine($"Church Address: {churchAddress}");
                    writer.WriteLine($"Health Conditions: {healthConditions}");
                }

                Console.WriteLine("\nProfile saved successfully! You can now access the main menu.");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nError: An error occurred while saving your profile: {ex.Message}");
                Console.ResetColor();
                return; // Exit if file saving fails
            }
        }
        else
        {
            Console.WriteLine("\nWelcome back to the Digihealth App!");
        }

        GoalManager goalManager = new GoalManager();
        ExerciseManager exerciseManager = new ExerciseManager();
        MealLogger mealLogger = new MealLogger();
        BodyMetrics bodyMetrics = new BodyMetrics();
        IqTest iqTest = new IqTest();
        // A new instance of the BookOfMormonStudy class is created to handle the study feature.
        BookOfMormonStudy bOMStudy = new BookOfMormonStudy();

        // Main menu loop
        bool isRunning = true;
        while (isRunning)
        {
            // Display player info before each menu loop
            goalManager.DisplayPlayerInfo();
            Console.WriteLine("\n--------------------------------------");
            Console.WriteLine("      Main Menu");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("1. Goal Management");
            Console.WriteLine("2. Exercise");
            Console.WriteLine("3. Meal Log");
            Console.WriteLine("4. Body Metrics");
            Console.WriteLine("5. IQ Test");
            Console.WriteLine("6. Book of Mormon Study");
            Console.WriteLine("0. Quit");
            Console.Write("Select an option: ");
            
            string input = Console.ReadLine();
            
            switch (input)
            {
                case "1":
                    RunGoalMenu(goalManager);
                    break;
                case "2":
                    exerciseManager.RunExerciseMenu();
                    break;
                case "3":
                    mealLogger.RunMealMenu();
                    break;
                case "4":
                    bodyMetrics.RunBodyMetricsMenu();
                    break;
                case "5":
                    RunIqTestMenu(iqTest);
                    break;
                case "6":
                    // The main menu calls the StudyMenu method from the new BookOfMormonStudy class.
                    bOMStudy.StudyMenu();
                    break;
                case "0":
                    isRunning = false;
                    Console.WriteLine("\nThank you for using the Digihealth App. Goodbye!");
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid option. Please try again.");
                    Console.ResetColor();
                    break;
            }
        }
    }

    // A method to run the goal management menu
    public static void RunGoalMenu(GoalManager goalManager)
    {
        bool isGoalMenuRunning = true;
        while (isGoalMenuRunning)
        {
            goalManager.DisplayPlayerInfo();
            Console.WriteLine("\n--------------------------------------");
            Console.WriteLine("      Goal Management");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Record Event");
            Console.WriteLine("4. Save Goals");
            Console.WriteLine("5. Load Goals");
            Console.WriteLine("M. Return to Main Menu");
            Console.Write("Select an option: ");

            string input = Console.ReadLine();
            
            switch (input.ToUpper())
            {
                case "1":
                    goalManager.CreateGoal();
                    break;
                case "2":
                    goalManager.ListGoalDetails();
                    break;
                case "3":
                    goalManager.RecordEvent();
                    break;
                case "4":
                    goalManager.SaveGoals();
                    break;
                case "5":
                    goalManager.LoadGoals();
                    break;
                case "M":
                    isGoalMenuRunning = false;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid option. Please try again.");
                    Console.ResetColor();
                    break;
            }
        }
    }
    
    // A method to run the IQ Test menu
    public static void RunIqTestMenu(IqTest iqTest)
    {
        bool isTestMenuRunning = true;
        while (isTestMenuRunning)
        {
            Console.WriteLine("\n--------------------------------------");
            Console.WriteLine("          IQ Test Menu");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("1. Start a New Test");
            Console.WriteLine("2. View Test History");
            Console.WriteLine("M. Return to Main Menu");
            Console.Write("Select an option: ");
            
            string input = Console.ReadLine().ToUpper();

            switch (input)
            {
                case "1":
                    iqTest.RunTest();
                    break;
                case "2":
                    iqTest.ViewHistory();
                    break;
                case "M":
                    isTestMenuRunning = false;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid option. Please try again.");
                    Console.ResetColor();
                    break;
            }
        }
    }
}
