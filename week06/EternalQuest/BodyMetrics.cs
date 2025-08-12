using System;
using System.Collections.Generic;
using System.IO;

// BodyMetrics.cs
//
// This file defines the BodyMetrics class, which handles all functionality
// related to tracking body measurements and health metrics.
//
// It includes a body fat calculator, a weight/height/distance unit converter,
// and methods for logging pressure and glucose readings.
public class BodyMetrics
{
    // A nested class to represent a single metric log entry.
    public class MetricEntry
    {
        public DateTime Date { get; set; }
        public string Type { get; set; } // e.g., "Pressure", "Glucose"
        public string Value { get; set; } 
    }

    // Member variables for logging.
    private List<MetricEntry> _metricsHistory;

    public BodyMetrics()
    {
        _metricsHistory = new List<MetricEntry>();
        LoadMetricsHistory();
    }

    // Displays the main menu for the body metrics section.
    public void RunBodyMetricsMenu()
    {
        bool isRunning = true;
        while (isRunning)
        {
            Console.WriteLine("\n--------------------------------------");
            Console.WriteLine("        Body Metrics Menu");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("1. Calculate Body Fat");
            Console.WriteLine("2. Use Unit Converter");
            Console.WriteLine("3. Log Pressure Reading");
            Console.WriteLine("4. Log Glucose Reading");
            Console.WriteLine("5. View Metrics History");
            Console.WriteLine("M. Return to Main Menu");
            Console.Write("Select an option: ");

            string input = Console.ReadLine().ToUpper();
            
            switch (input)
            {
                case "1":
                    CalculateBodyFat();
                    break;
                case "2":
                    UnitConverter();
                    break;
                case "3":
                    LogReading("Pressure");
                    break;
                case "4":
                    LogReading("Glucose");
                    break;
                case "5":
                    ViewMetricsHistory();
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

    // Calculates body fat using a simplified method.
    public void CalculateBodyFat()
    {
        Console.WriteLine("\n--- Body Fat Calculator ---");
        Console.Write("Enter your gender (male/female): ");
        string gender = Console.ReadLine().ToLower();

        Console.Write("Enter your weight in pounds (lbs): ");
        if (!double.TryParse(Console.ReadLine(), out double weight))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid weight value.");
            Console.ResetColor();
            return;
        }

        Console.Write("Enter your height in inches (in): ");
        if (!double.TryParse(Console.ReadLine(), out double height))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid height value.");
            Console.ResetColor();
            return;
        }

        Console.Write("Enter your waist circumference in inches (in): ");
        if (!double.TryParse(Console.ReadLine(), out double waist))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid waist value.");
            Console.ResetColor();
            return;
        }

        double bmi = (weight / (height * height)) * 703;
        double bodyFat;

        if (gender == "male")
        {
            bodyFat = (1.20 * bmi) + (0.23 * 30) - 10.8 - 5.4; 
        }
        else if (gender == "female")
        {
            bodyFat = (1.20 * bmi) + (0.23 * 30) - 5.4; 
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid gender. Calculation aborted.");
            Console.ResetColor();
            return;
        }

        Console.WriteLine($"\nYour estimated Body Fat is: {bodyFat:F2}%");
    }

    // A utility for converting units.
    public void UnitConverter()
    {
        Console.WriteLine("\n--- Unit Converter ---");
        Console.WriteLine("1. Convert Weight (lbs <-> kg)");
        Console.WriteLine("2. Convert Height (in <-> cm)");
        Console.WriteLine("3. Convert Distance (mi <-> km)");
        Console.Write("Select a conversion type: ");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                ConvertWeight();
                break;
            case "2":
                ConvertHeight();
                break;
            case "3":
                ConvertDistance();
                break;
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid option.");
                Console.ResetColor();
                break;
        }
    }

    private void ConvertWeight()
    {
        Console.Write("Enter weight and unit (e.g., '150 lbs' or '68 kg'): ");
        string input = Console.ReadLine();
        string[] parts = input.Split(' ');
        if (parts.Length != 2 || !double.TryParse(parts[0], out double value))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid input format.");
            Console.ResetColor();
            return;
        }

        if (parts[1].ToLower() == "lbs")
        {
            double result = value * 0.453592;
            Console.WriteLine($"{value} lbs is equal to {result:F2} kg.");
        }
        else if (parts[1].ToLower() == "kg")
        {
            double result = value / 0.453592;
            Console.WriteLine($"{value} kg is equal to {result:F2} lbs.");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid unit. Use 'lbs' or 'kg'.");
            Console.ResetColor();
        }
    }

    private void ConvertHeight()
    {
        Console.Write("Enter height and unit (e.g., '70 in' or '177 cm'): ");
        string input = Console.ReadLine();
        string[] parts = input.Split(' ');
        if (parts.Length != 2 || !double.TryParse(parts[0], out double value))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid input format.");
            Console.ResetColor();
            return;
        }

        if (parts[1].ToLower() == "in")
        {
            double result = value * 2.54;
            Console.WriteLine($"{value} in is equal to {result:F2} cm.");
        }
        else if (parts[1].ToLower() == "cm")
        {
            double result = value / 2.54;
            Console.WriteLine($"{value} cm is equal to {result:F2} in.");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid unit. Use 'in' or 'cm'.");
            Console.ResetColor();
        }
    }

    private void ConvertDistance()
    {
        Console.Write("Enter distance and unit (e.g., '5 mi' or '8 km'): ");
        string input = Console.ReadLine();
        string[] parts = input.Split(' ');
        if (parts.Length != 2 || !double.TryParse(parts[0], out double value))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid input format.");
            Console.ResetColor();
            return;
        }

        if (parts[1].ToLower() == "mi")
        {
            double result = value * 1.60934;
            Console.WriteLine($"{value} mi is equal to {result:F2} km.");
        }
        else if (parts[1].ToLower() == "km")
        {
            double result = value / 1.60934;
            Console.WriteLine($"{value} km is equal to {result:F2} mi.");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid unit. Use 'mi' or 'km'.");
            Console.ResetColor();
        }
    }

    // Logs a user's reading for pressure or glucose.
    public void LogReading(string type)
    {
        Console.WriteLine($"\n--- Log {type} Reading ---");
        Console.Write($"Enter your {type} reading: ");
        string reading = Console.ReadLine();

        _metricsHistory.Add(new MetricEntry
        {
            Date = DateTime.Now,
            Type = type,
            Value = reading
        });
        
        Console.WriteLine($"{type} reading logged successfully!");
        SaveMetricsHistory();
    }

    // Views the user's metrics history.
    public void ViewMetricsHistory()
    {
        Console.WriteLine("\n--- Metrics History ---");
        if (_metricsHistory.Count == 0)
        {
            Console.WriteLine("No metrics have been logged yet.");
            return;
        }
        
        foreach (var entry in _metricsHistory)
        {
            Console.WriteLine($"Date: {entry.Date.ToShortDateString()} - " +
                              $"Type: {entry.Type} - " +
                              $"Value: {entry.Value}");
        }
    }

    // Saves the metrics history to a file.
    public void SaveMetricsHistory()
    {
        try
        {
            using (StreamWriter writer = new StreamWriter("metrics_history.txt"))
            {
                foreach (var entry in _metricsHistory)
                {
                    writer.WriteLine($"{entry.Date},{entry.Type},{entry.Value}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error saving metrics history: {ex.Message}");
            Console.ResetColor();
        }
    }

    // Loads metrics history from a file.
    public void LoadMetricsHistory()
    {
        if (!File.Exists("metrics_history.txt")) return;
        
        try
        {
            string[] lines = File.ReadAllLines("metrics_history.txt");
            _metricsHistory.Clear();
            foreach (var line in lines)
            {
                string[] parts = line.Split(',');
                if (parts.Length == 3)
                {
                    _metricsHistory.Add(new MetricEntry
                    {
                        Date = DateTime.Parse(parts[0]),
                        Type = parts[1],
                        Value = parts[2]
                    });
                }
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error loading metrics history: {ex.Message}");
            Console.ResetColor();
        }
    }
}
