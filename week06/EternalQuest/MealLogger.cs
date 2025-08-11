using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// MealLogger.cs
//
// This file defines the MealLogger class, which handles
// all meal and calorie-related functions for the app.
//
// It tracks a user's daily calorie intake, allows for
// logging of common or custom foods, and saves meal history.
// This class also handles saving and loading meal data.

public class MealLogger
{
    // A nested class to represent a single food item.
    public class FoodItem
    {
        public string Name { get; set; }
        public int Calories { get; set; }
        public double ServingSize { get; set; } // e.g., 1.0 for one serving
    }

    // A nested class to log a single meal entry.
    public class MealEntry
    {
        public DateTime Date { get; set; }
        public string MealType { get; set; } // e.g., Breakfast, Lunch, Dinner
        public string FoodName { get; set; }
        public double Servings { get; set; }
        public int TotalCalories { get; set; }
    }

    // Member variables for tracking food and meal data.
    private List<FoodItem> _foodItems;
    private List<MealEntry> _mealHistory;
    private int _caloriesToday;

    // Constructor to initialize the MealLogger.
    public MealLogger()
    {
        _foodItems = new List<FoodItem>();
        _mealHistory = new List<MealEntry>();
        _caloriesToday = 0;
        LoadFoodItems();
        LoadMealHistory();
    }

    // Displays the main menu for the meal log section.
    public void RunMealMenu()
    {
        bool isRunning = true;
        while (isRunning)
        {
            DisplayCalorieInfo();
            Console.WriteLine("\n--------------------------------------");
            Console.WriteLine("          Meal Log Menu");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("1. Log a meal");
            Console.WriteLine("2. Add a new custom food");
            Console.WriteLine("3. View meal history");
            Console.WriteLine("M. Return to Main Menu");
            Console.Write("Select an option: ");

            string input = Console.ReadLine().ToUpper();

            switch (input)
            {
                case "1":
                    LogMeal();
                    break;
                case "2":
                    AddCustomFood();
                    break;
                case "3":
                    ViewMealHistory();
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

    // Displays today's calorie intake.
    public void DisplayCalorieInfo()
    {
        Console.WriteLine("\n--- Today's Calorie Progress ---");
        Console.WriteLine($"Calories Consumed Today: {_caloriesToday} kcal");
    }

    // Guides the user through logging a meal.
    public void LogMeal()
    {
        Console.WriteLine("\n--- Log a Meal ---");
        Console.WriteLine("What type of meal is this?");
        Console.WriteLine("1. Breakfast");
        Console.WriteLine("2. Lunch");
        Console.WriteLine("3. Dinner");
        Console.WriteLine("4. Morning Snack");
        Console.WriteLine("5. Evening Snack");
        Console.Write("Select a meal type: ");

        string mealTypeChoice = Console.ReadLine();
        string mealType = "";
        switch (mealTypeChoice)
        {
            case "1": mealType = "Breakfast"; break;
            case "2": mealType = "Lunch"; break;
            case "3": mealType = "Dinner"; break;
            case "4": mealType = "Morning Snack"; break;
            case "5": mealType = "Evening Snack"; break;
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid meal type.");
                Console.ResetColor();
                return;
        }

        Console.WriteLine("\n--- Food Items ---");
        for (int i = 0; i < _foodItems.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_foodItems[i].Name} ({_foodItems[i].Calories} kcal per {_foodItems[i].ServingSize} serving)");
        }
        Console.Write("Select a food item number, or enter 'N' to add a new one: ");
        
        string foodInput = Console.ReadLine().ToUpper();
        FoodItem selectedFood = null;

        if (foodInput == "N")
        {
            AddCustomFood();
            selectedFood = _foodItems.LastOrDefault();
        }
        else if (int.TryParse(foodInput, out int foodChoice) && foodChoice > 0 && foodChoice <= _foodItems.Count)
        {
            selectedFood = _foodItems[foodChoice - 1];
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid food selection.");
            Console.ResetColor();
            return;
        }

        if (selectedFood != null)
        {
            Console.Write($"Enter the number of servings (e.g., 1.5): ");
            if (!double.TryParse(Console.ReadLine(), out double servings))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid number of servings.");
                Console.ResetColor();
                return;
            }

            int totalCalories = (int)(selectedFood.Calories * servings);
            _caloriesToday += totalCalories;

            _mealHistory.Add(new MealEntry
            {
                Date = DateTime.Now,
                MealType = mealType,
                FoodName = selectedFood.Name,
                Servings = servings,
                TotalCalories = totalCalories
            });

            Console.WriteLine("\nMeal logged successfully!");
            Console.WriteLine($"You consumed {totalCalories} kcal.");
            SaveMealHistory();
        }
    }

    // Allows the user to add a new food item to the list.
    public void AddCustomFood()
    {
        Console.WriteLine("\n--- Add a New Custom Food ---");
        Console.Write("Enter the name of the food: ");
        string name = Console.ReadLine();
        Console.Write("Enter the calories per serving: ");
        if (!int.TryParse(Console.ReadLine(), out int calories))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid calorie value.");
            Console.ResetColor();
            return;
        }
        Console.Write("Enter the serving size description (e.g., '1 cup' or '100g'): ");
        string servingSize = Console.ReadLine();

        _foodItems.Add(new FoodItem { Name = name, Calories = calories, ServingSize = 1.0 });
        Console.WriteLine("New food item added successfully!");
        SaveFoodItems();
    }

    // Views the user's meal history.
    public void ViewMealHistory()
    {
        Console.WriteLine("\n--- Meal History ---");
        if (_mealHistory.Count == 0)
        {
            Console.WriteLine("You have not logged any meals yet.");
            return;
        }
        foreach (var entry in _mealHistory)
        {
            Console.WriteLine($"Date: {entry.Date.ToShortDateString()} - " +
                              $"Meal: {entry.MealType} - " +
                              $"Food: {entry.FoodName} - " +
                              $"Servings: {entry.Servings} - " +
                              $"Calories: {entry.TotalCalories}");
        }
    }

    // Saves all created food items to a file.
    public void SaveFoodItems()
    {
        try
        {
            using (StreamWriter writer = new StreamWriter("foods.txt"))
            {
                foreach (var food in _foodItems)
                {
                    writer.WriteLine($"{food.Name},{food.Calories},{food.ServingSize}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error saving foods: {ex.Message}");
            Console.ResetColor();
        }
    }

    // Loads food items from a file.
    public void LoadFoodItems()
    {
        // Add some default foods if the file doesn't exist
        if (!File.Exists("foods.txt"))
        {
            _foodItems.Add(new FoodItem { Name = "Chicken Breast", Calories = 165, ServingSize = 1.0 });
            _foodItems.Add(new FoodItem { Name = "Brown Rice", Calories = 123, ServingSize = 1.0 });
            _foodItems.Add(new FoodItem { Name = "Broccoli", Calories = 55, ServingSize = 1.0 });
            SaveFoodItems();
            return;
        }
        
        try
        {
            string[] lines = File.ReadAllLines("foods.txt");
            _foodItems.Clear();
            foreach (var line in lines)
            {
                string[] parts = line.Split(',');
                if (parts.Length == 3)
                {
                    _foodItems.Add(new FoodItem
                    {
                        Name = parts[0],
                        Calories = int.Parse(parts[1]),
                        ServingSize = double.Parse(parts[2])
                    });
                }
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error loading foods: {ex.Message}");
            Console.ResetColor();
        }
    }

    // Saves meal history to a file.
    public void SaveMealHistory()
    {
        try
        {
            using (StreamWriter writer = new StreamWriter("meal_history.txt"))
            {
                foreach (var entry in _mealHistory)
                {
                    writer.WriteLine($"{entry.Date},{entry.MealType},{entry.FoodName},{entry.Servings},{entry.TotalCalories}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error saving meal history: {ex.Message}");
            Console.ResetColor();
        }
    }

    // Loads meal history from a file.
    public void LoadMealHistory()
    {
        if (!File.Exists("meal_history.txt")) return;
        
        try
        {
            string[] lines = File.ReadAllLines("meal_history.txt");
            _mealHistory.Clear();
            _caloriesToday = 0; // Reset daily calorie count
            foreach (var line in lines)
            {
                string[] parts = line.Split(',');
                if (parts.Length == 5)
                {
                    var entry = new MealEntry
                    {
                        Date = DateTime.Parse(parts[0]),
                        MealType = parts[1],
                        FoodName = parts[2],
                        Servings = double.Parse(parts[3]),
                        TotalCalories = int.Parse(parts[4])
                    };
                    _mealHistory.Add(entry);

                    // Add calories for today's date only
                    if (entry.Date.Date == DateTime.Today)
                    {
                        _caloriesToday += entry.TotalCalories;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error loading meal history: {ex.Message}");
            Console.ResetColor();
        }
    }
}
