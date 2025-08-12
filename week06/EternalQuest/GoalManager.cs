using System;
using System.Collections.Generic;
using System.IO;

// GoalManager.cs
//
// This file defines the GoalManager class, which is responsible for
// all goal management functionality in the Digihealth App.
//
// It manages a list of goals, the user's score, and the daily streak.
// The class provides methods for interacting with these goals,
// as well as saving and loading the goal data from a file.
public class GoalManager
{
    // Member variables to hold the list of goals, the user's score, and the streak.
    private List<Goal> _goals;
    private int _score;
    private int _streak;
    private DateTime _lastRecordedDate;

    // Constructor to initialize the GoalManager.
    public GoalManager()
    {
        _goals = new List<Goal>();
        _score = 0;
        _streak = 0;
        // The last recorded date is initialized to a minimum value,
        // which helps to check if an event has ever been recorded.
        _lastRecordedDate = DateTime.MinValue;
    }

    // Displays the current score and daily streak.
    public void DisplayPlayerInfo()
    {
        Console.WriteLine($"\nYour current score is: {_score} points.");
        Console.WriteLine($"Your daily streak is: {_streak} day(s).");
    }

    // Provides the menu for creating a new goal and calls the appropriate
    // creation method.
    public void CreateGoal()
    {
        Console.WriteLine("\n--- Create a New Goal ---");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.Write("Enter the type of goal you want to create: ");

        string goalType = Console.ReadLine();
        
        // Collect common goal details
        Console.Write("Enter the short name for the goal: ");
        string name = Console.ReadLine();

        Console.Write("Enter a description of the goal: ");
        string description = Console.ReadLine();

        Console.Write("Enter the points for this goal: ");
        int points = int.Parse(Console.ReadLine());
        
        Goal newGoal = null;

        switch (goalType)
        {
            case "1":
                newGoal = new SimpleGoal(name, description, points);
                break;
            case "2":
                newGoal = new EternalGoal(name, description, points);
                break;
            case "3":
                Console.Write("Enter the target number of times to complete this goal: ");
                int target = int.Parse(Console.ReadLine());
                Console.Write("Enter the bonus points for completing the goal: ");
                int bonus = int.Parse(Console.ReadLine());
                newGoal = new ChecklistGoal(name, description, points, target, bonus);
                break;
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid goal type. No goal was created.");
                Console.ResetColor();
                return;
        }

        if (newGoal != null)
        {
            _goals.Add(newGoal);
            Console.WriteLine("Goal created successfully!");
        }
    }

    // Lists the details of all goals in the _goals list.
    public void ListGoalDetails()
    {
        Console.WriteLine("\n--- Your Goals ---");
        if (_goals.Count == 0)
        {
            Console.WriteLine("You have no goals set yet.");
        }
        else
        {
            for (int i = 0; i < _goals.Count; i++)
            {
                // Polymorphism is used here, as GetDetailsString() will call
                // the correct method for each specific goal type.
                string status = _goals[i].IsComplete() ? "[X]" : "[ ]";
                Console.WriteLine($"{i + 1}. {status} {_goals[i].GetDetailsString()}");
            }
        }
    }

    // Prompts the user to select a goal and then records an event for it.
    public void RecordEvent()
    {
        Console.WriteLine("\n--- Record an Event ---");
        ListGoalDetails();

        Console.Write("Select the number of the goal you accomplished: ");
        if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= _goals.Count)
        {
            Goal selectedGoal = _goals[choice - 1];
            
            if (selectedGoal.IsComplete())
            {
                Console.WriteLine("This goal is already complete.");
            }
            else
            {
                selectedGoal.RecordEvent();
                
                // The daily streak logic is now handled in this section.
                // It checks the last recorded date against today's date.
                DateTime today = DateTime.Today;
                if (_lastRecordedDate == DateTime.MinValue || _lastRecordedDate < today.AddDays(-1))
                {
                    // The streak resets to 1 if this is the first day or if a day was missed.
                    _streak = 1;
                }
                else if (_lastRecordedDate == today.AddDays(-1))
                {
                    // The streak increments if the event is recorded on a consecutive day.
                    _streak++;
                }
                // The streak is not changed if an event is recorded more than once in a single day.
                
                // The last recorded date is updated to today.
                _lastRecordedDate = today;

                // The user's score is updated with the points from the selected goal.
                _score += selectedGoal.Points;
                Console.WriteLine($"You received {selectedGoal.Points} points!");
                
                // The code checks if a bonus needs to be awarded for a ChecklistGoal.
                if (selectedGoal is ChecklistGoal checklistGoal && checklistGoal.IsComplete())
                {
                    _score += checklistGoal.Bonus;
                    Console.WriteLine($"Congratulations! You earned a bonus of {checklistGoal.Bonus} points!");
                }
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid selection.");
            Console.ResetColor();
        }
    }
    
    // Saves the goals and score to a file.
    public void SaveGoals()
    {
        Console.Write("Enter the filename to save your goals (e.g., goals.txt): ");
        string filename = Console.ReadLine();

        try
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine($"{_score}");
                writer.WriteLine($"{_streak}");
                // The last recorded date is now saved to the file for persistence.
                writer.WriteLine($"{_lastRecordedDate.ToShortDateString()}");
                foreach (Goal goal in _goals)
                {
                    writer.WriteLine(goal.GetStringRepresentation());
                }
            }
            Console.WriteLine("Goals and score saved successfully!");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"An error occurred while saving the goals: {ex.Message}");
            Console.ResetColor();
        }
    }

    // Loads the goals and score from a file.
    public void LoadGoals()
    {
        Console.Write("Enter the filename to load your goals (e.g., goals.txt): ");
        string filename = Console.ReadLine();

        if (!File.Exists(filename))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("File not found.");
            Console.ResetColor();
            return;
        }

        try
        {
            string[] lines = File.ReadAllLines(filename);
            _score = int.Parse(lines[0]);
            _streak = int.Parse(lines[1]);
            // The last recorded date is now loaded from the file.
            _lastRecordedDate = DateTime.Parse(lines[2]);
            _goals.Clear();

            for (int i = 3; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(':');
                string goalType = parts[0];
                string[] details = parts[1].Split(',');

                Goal loadedGoal = null;
                string name = details[0];
                string description = details[1];
                int points = int.Parse(details[2]);
                
                switch (goalType)
                {
                    case "SimpleGoal":
                        bool isComplete = bool.Parse(details[3]);
                        loadedGoal = new SimpleGoal(name, description, points, isComplete);
                        break;
                    case "EternalGoal":
                        loadedGoal = new EternalGoal(name, description, points);
                        break;
                    case "ChecklistGoal":
                        int bonus = int.Parse(details[3]);
                        int target = int.Parse(details[4]);
                        int amountCompleted = int.Parse(details[5]);
                        loadedGoal = new ChecklistGoal(name, description, points, amountCompleted, target, bonus);
                        break;
                }
                if (loadedGoal != null)
                {
                    _goals.Add(loadedGoal);
                }
            }
            Console.WriteLine("Goals and score loaded successfully!");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"An error occurred while loading the goals: {ex.Message}");
            Console.ResetColor();
        }
    }
}
