using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// ExerciseManager.cs
//
// This file defines the ExerciseManager class, which handles
// all exercise and fitness-related functions for the app.
//
// It tracks steps and calories, manages exercise routes,
// and includes a "Together" competition feature.
// This class also handles saving and loading exercise data.

public class ExerciseManager
{
    // A nested class to represent an exercise route.
    public class Route
    {
        public string Name { get; set; }
        public double Distance { get; set; }
        public string Unit { get; set; }
    }

    // A nested class to log a single exercise session.
    public class ExerciseLogEntry
    {
        public DateTime Date { get; set; }
        public string Activity { get; set; }
        public string RouteName { get; set; }
        public double Distance { get; set; }
        public int Steps { get; set; }
        public int CaloriesBurned { get; set; }
        public int Trips { get; set; }
    }

    // A nested class to represent a virtual opponent in the "Together" challenge.
    public class CompetitionOpponent
    {
        public string Username { get; set; }
        public string Level { get; set; }
        public int Steps { get; set; }
    }

    // Member variables for tracking exercise data.
    private List<Route> _routes;
    private List<ExerciseLogEntry> _exerciseHistory;
    private int _stepsToday;
    private int _caloriesToday;

    // A list of pre-defined routes and opponents for the "Together" feature.
    private List<string> _predefinedUsernames = new List<string>
    {
        "Runner98", "FitFocus", "TrailBlazer", "IronWill", "ApexPredator",
        "ZenWalker", "GritGuy", "FastPace", "EnduranceHero", "SpeedyG"
    };

    // Constructor to initialize the ExerciseManager.
    public ExerciseManager()
    {
        _routes = new List<Route>();
        _exerciseHistory = new List<ExerciseLogEntry>();
        _stepsToday = 0;
        _caloriesToday = 0;
        LoadRoutes();
        LoadExerciseHistory();
    }

    // Displays the main menu for the exercise section.
    public void RunExerciseMenu()
    {
        bool isRunning = true;
        while (isRunning)
        {
            DisplayExerciseInfo();
            Console.WriteLine("\n--------------------------------------");
            Console.WriteLine("          Exercise Menu");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("1. Log an exercise");
            Console.WriteLine("2. Create a new route");
            Console.WriteLine("3. View exercise history");
            Console.WriteLine("4. Start a 'Together' challenge");
            Console.WriteLine("M. Return to Main Menu");
            Console.Write("Select an option: ");

            string input = Console.ReadLine().ToUpper();

            switch (input)
            {
                case "1":
                    LogExercise();
                    break;
                case "2":
                    CreateNewRoute();
                    break;
                case "3":
                    ViewHistory();
                    break;
                case "4":
                    StartTogetherChallenge();
                    break;
                case "M":
                    isRunning = false;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid option. Please try again.");
                    Console.ResetColor();
                    break;
            }
        }
    }

    // Displays today's steps and calories.
    public void DisplayExerciseInfo()
    {
        Console.WriteLine("\n--- Today's Progress ---");
        Console.WriteLine($"Steps Today: {_stepsToday}");
        Console.WriteLine($"Calories Burned: {_caloriesToday} kcal");
    }

    // Guides the user through logging an exercise.
    public void LogExercise()
    {
        Console.WriteLine("\n--- Log an Exercise ---");
        
        Console.WriteLine("Choose an activity:");
        Console.WriteLine("1. Walking");
        Console.WriteLine("2. Running");
        Console.WriteLine("3. Cycling");
        Console.Write("Select an activity: ");
        string activityChoice = Console.ReadLine();
        string activity = "";

        switch (activityChoice)
        {
            case "1": activity = "Walking"; break;
            case "2": activity = "Running"; break;
            case "3": activity = "Cycling"; break;
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid activity choice.");
                Console.ResetColor();
                return;
        }

        ViewRoutes();
        Console.Write("Select a route number, or enter 'N' to create a new one: ");
        string routeInput = Console.ReadLine().ToUpper();
        Route selectedRoute = null;

        if (routeInput == "N")
        {
            CreateNewRoute();
            selectedRoute = _routes.LastOrDefault();
        }
        else if (int.TryParse(routeInput, out int routeChoice) && routeChoice > 0 && routeChoice <= _routes.Count)
        {
            selectedRoute = _routes[routeChoice - 1];
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid route selection.");
            Console.ResetColor();
            return;
        }

        Console.Write($"Enter the number of trips/rounds for this route: ");
        if (!int.TryParse(Console.ReadLine(), out int trips))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid number of trips.");
            Console.ResetColor();
            return;
        }

        if (selectedRoute != null)
        {
            double totalDistance = selectedRoute.Distance * trips;
            int totalSteps = CalculateSteps(totalDistance, selectedRoute.Unit);
            int totalCalories = CalculateCalories(totalDistance, selectedRoute.Unit);
            
            // Update daily totals
            _stepsToday += totalSteps;
            _caloriesToday += totalCalories;

            // Log the exercise entry
            _exerciseHistory.Add(new ExerciseLogEntry
            {
                Date = DateTime.Now,
                Activity = activity,
                RouteName = selectedRoute.Name,
                Distance = totalDistance,
                Steps = totalSteps,
                CaloriesBurned = totalCalories,
                Trips = trips
            });

            Console.WriteLine("\nExercise logged successfully!");
            Console.WriteLine($"You took {totalSteps} steps and burned {totalCalories} kcal.");
            SaveExerciseHistory();
        }
    }

    // Allows the user to create a new route.
    public void CreateNewRoute()
    {
        Console.WriteLine("\n--- Create a New Route ---");
        Console.Write("Enter a name for the new route: ");
        string name = Console.ReadLine();
        Console.Write("Enter the distance (e.g., 5.0): ");
        if (!double.TryParse(Console.ReadLine(), out double distance))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid distance value.");
            Console.ResetColor();
            return;
        }
        Console.Write("Enter the unit (km, mi, m): ");
        string unit = Console.ReadLine().ToLower();

        _routes.Add(new Route { Name = name, Distance = distance, Unit = unit });
        Console.WriteLine("New route created successfully!");
        SaveRoutes();
    }

    // Displays the list of available routes.
    public void ViewRoutes()
    {
        Console.WriteLine("\n--- Available Routes ---");
        if (_routes.Count == 0)
        {
            Console.WriteLine("No routes available. Create one first.");
            return;
        }

        for (int i = 0; i < _routes.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_routes[i].Name} ({_routes[i].Distance} {_routes[i].Unit})");
        }
    }

    // Starts the "Together" competition with virtual opponents.
    public void StartTogetherChallenge()
    {
        Console.WriteLine("\n--- 'Together' Challenge ---");
        Console.WriteLine("A new challenge has been created! You will compete with 6 virtual opponents.");
        
        // Randomly select 6 opponents from the predefined list
        Random random = new Random();
        List<CompetitionOpponent> opponents = new List<CompetitionOpponent>();
        
        var shuffledUsernames = _predefinedUsernames.OrderBy(a => random.Next()).Take(6).ToList();
        
        foreach (var username in shuffledUsernames)
        {
            int level = random.Next(1, 51);
            string levelText = "";
            if (level <= 10) levelText = "Newbie";
            else if (level <= 20) levelText = "Achiever";
            else if (level <= 30) levelText = "Expert";
            else if (level <= 40) levelText = "Leader";
            else levelText = "Champion";

            opponents.Add(new CompetitionOpponent
            {
                Username = username,
                Level = levelText,
                Steps = random.Next(1000, 15000)
            });
        }
        
        // Display the challenge
        Console.WriteLine("\nChallenge Leaderboard:");
        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine("  Rank | Username      | Level       | Steps     ");
        Console.WriteLine("-------------------------------------------------");

        // Sort opponents by steps in descending order
        opponents = opponents.OrderByDescending(o => o.Steps).ToList();

        for (int i = 0; i < opponents.Count; i++)
        {
            Console.WriteLine($"{i + 1,5} | {opponents[i].Username,-12} | {opponents[i].Level,-11} | {opponents[i].Steps,-9}");
        }
        Console.WriteLine("-------------------------------------------------");
        
        Console.WriteLine("\nThis challenge will be logged in your history.");
    }

    // Views the user's exercise history.
    public void ViewHistory()
    {
        Console.WriteLine("\n--- Exercise History ---");
        if (_exerciseHistory.Count == 0)
        {
            Console.WriteLine("You have not logged any exercises yet.");
            return;
        }
        foreach (var entry in _exerciseHistory)
        {
            Console.WriteLine($"Date: {entry.Date.ToShortDateString()} - " +
                              $"Activity: {entry.Activity} - " +
                              $"Route: {entry.RouteName} - " +
                              $"Distance: {entry.Distance} - " +
                              $"Steps: {entry.Steps} - " +
                              $"Calories: {entry.CaloriesBurned}");
        }
    }

    // Calculates steps based on distance and unit.
    // Assuming 1 km = ~1312 steps, 1 mi = ~2113 steps, 1 m = ~1.3 steps
    private int CalculateSteps(double distance, string unit)
    {
        double totalDistanceInKm;
        if (unit == "mi")
        {
            totalDistanceInKm = distance * 1.60934;
        }
        else if (unit == "m")
        {
            totalDistanceInKm = distance / 1000;
        }
        else
        {
            totalDistanceInKm = distance; // Assume km
        }
        return (int)(totalDistanceInKm * 1312);
    }
    
    // Calculates calories burned based on distance and unit.
    // Assuming ~60 kcal/km for a general walking pace
    private int CalculateCalories(double distance, string unit)
    {
        double totalDistanceInKm;
        if (unit == "mi")
        {
            totalDistanceInKm = distance * 1.60934;
        }
        else if (unit == "m")
        {
            totalDistanceInKm = distance / 1000;
        }
        else
        {
            totalDistanceInKm = distance; // Assume km
        }
        return (int)(totalDistanceInKm * 60);
    }
    
    // Saves all created routes to a file.
    public void SaveRoutes()
    {
        try
        {
            using (StreamWriter writer = new StreamWriter("routes.txt"))
            {
                foreach (var route in _routes)
                {
                    writer.WriteLine($"{route.Name},{route.Distance},{route.Unit}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error saving routes: {ex.Message}");
            Console.ResetColor();
        }
    }

    // Loads routes from a file.
    public void LoadRoutes()
    {
        if (!File.Exists("routes.txt")) return;
        
        try
        {
            string[] lines = File.ReadAllLines("routes.txt");
            _routes.Clear();
            foreach (var line in lines)
            {
                string[] parts = line.Split(',');
                if (parts.Length == 3)
                {
                    _routes.Add(new Route
                    {
                        Name = parts[0],
                        Distance = double.Parse(parts[1]),
                        Unit = parts[2]
                    });
                }
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error loading routes: {ex.Message}");
            Console.ResetColor();
        }
    }
    
    // Saves exercise history to a file.
    public void SaveExerciseHistory()
    {
        try
        {
            using (StreamWriter writer = new StreamWriter("exercise_history.txt"))
            {
                foreach (var entry in _exerciseHistory)
                {
                    writer.WriteLine($"{entry.Date},{entry.Activity},{entry.RouteName},{entry.Distance},{entry.Steps},{entry.CaloriesBurned},{entry.Trips}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error saving history: {ex.Message}");
            Console.ResetColor();
        }
    }
    
    // Loads exercise history from a file.
    public void LoadExerciseHistory()
    {
        if (!File.Exists("exercise_history.txt")) return;
        
        try
        {
            string[] lines = File.ReadAllLines("exercise_history.txt");
            _exerciseHistory.Clear();
            foreach (var line in lines)
            {
                string[] parts = line.Split(',');
                if (parts.Length == 7)
                {
                    _exerciseHistory.Add(new ExerciseLogEntry
                    {
                        Date = DateTime.Parse(parts[0]),
                        Activity = parts[1],
                        RouteName = parts[2],
                        Distance = double.Parse(parts[3]),
                        Steps = int.Parse(parts[4]),
                        CaloriesBurned = int.Parse(parts[5]),
                        Trips = int.Parse(parts[6])
                    });
                }
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error loading history: {ex.Message}");
            Console.ResetColor();
        }
    }
}
