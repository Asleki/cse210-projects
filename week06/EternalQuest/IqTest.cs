using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

// IqTest.cs
//
// This file defines the IqTest class, which provides a health-themed
// multiple-choice IQ test.
//
// It awards points based on the speed of the correct answer and logs
// the user's score to a history file.

public class IqTest
{
    // A nested class to represent a single test question.
    private class Question
    {
        public string Text { get; set; }
        public List<string> Options { get; set; }
        public int CorrectAnswerIndex { get; set; }
    }
    
    // A nested class to represent a log of a completed test.
    public class TestLog
    {
        public DateTime Date { get; set; }
        public int Score { get; set; }
    }

    // The list of questions for the test.
    private List<Question> _questions;
    private List<TestLog> _testHistory;
    private readonly Random _random = new Random();

    public IqTest()
    {
        _questions = new List<Question>();
        _testHistory = new List<TestLog>();
        LoadQuestions();
        LoadTestHistory();
    }

    // Loads the questions from a file.
    private void LoadQuestions()
    {
        // For now, hardcode some questions. In a full implementation,
        // this would be loaded from a file or database.
        _questions.Add(new Question
        {
            Text = "What is the primary function of red blood cells?",
            Options = new List<string> { "Fighting infections", "Carrying oxygen", "Clotting blood" },
            CorrectAnswerIndex = 1
        });
        _questions.Add(new Question
        {
            Text = "Which nutrient is essential for building and repairing tissues?",
            Options = new List<string> { "Carbohydrates", "Fats", "Proteins" },
            CorrectAnswerIndex = 2
        });
        _questions.Add(new Question
        {
            Text = "Which organ is responsible for detoxifying the blood?",
            Options = new List<string> { "Heart", "Lungs", "Liver" },
            CorrectAnswerIndex = 2
        });
        _questions.Add(new Question
        {
            Text = "What is the recommended number of hours of sleep for an adult?",
            Options = new List<string> { "4-6 hours", "7-9 hours", "10-12 hours" },
            CorrectAnswerIndex = 1
        });
    }
    
    // Loads test history from a file.
    public void LoadTestHistory()
    {
        if (!File.Exists("iq_test_history.txt")) return;
        
        try
        {
            string[] lines = File.ReadAllLines("iq_test_history.txt");
            _testHistory.Clear();
            foreach (var line in lines)
            {
                string[] parts = line.Split(',');
                if (parts.Length == 2)
                {
                    _testHistory.Add(new TestLog
                    {
                        Date = DateTime.Parse(parts[0]),
                        Score = int.Parse(parts[1])
                    });
                }
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error loading test history: {ex.Message}");
            Console.ResetColor();
        }
    }

    // Saves test history to a file.
    public void SaveTestHistory()
    {
        try
        {
            using (StreamWriter writer = new StreamWriter("iq_test_history.txt"))
            {
                foreach (var log in _testHistory)
                {
                    writer.WriteLine($"{log.Date},{log.Score}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error saving test history: {ex.Message}");
            Console.ResetColor();
        }
    }

    // Starts the IQ test and manages the scoring.
    public void RunTest()
    {
        Console.WriteLine("\n--------------------------------------");
        Console.WriteLine("           IQ Test");
        Console.WriteLine("--------------------------------------");
        Console.WriteLine("You will have 15 seconds to answer each question.");
        Console.WriteLine("The faster you answer, the more points you get!");
        Console.Write("Press Enter to begin...");
        Console.ReadLine();

        int totalScore = 0;
        int questionsToAsk = 3; // Let's ask 3 questions for a short test
        var randomQuestions = _questions.OrderBy(x => _random.Next()).Take(questionsToAsk).ToList();
        
        Stopwatch stopwatch = new Stopwatch();

        foreach (var question in randomQuestions)
        {
            Console.WriteLine($"\nQuestion: {question.Text}");
            for (int i = 0; i < question.Options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {question.Options[i]}");
            }
            
            stopwatch.Restart();
            Console.Write("Your answer (1-3): ");
            string userAnswer = Console.ReadLine();
            stopwatch.Stop();
            
            int pointsAwarded = 0;
            if (int.TryParse(userAnswer, out int choice) && choice - 1 == question.CorrectAnswerIndex)
            {
                if (stopwatch.Elapsed.TotalSeconds <= 5)
                {
                    pointsAwarded = 30;
                }
                else if (stopwatch.Elapsed.TotalSeconds <= 10)
                {
                    pointsAwarded = 20;
                }
                else if (stopwatch.Elapsed.TotalSeconds <= 15)
                {
                    pointsAwarded = 10;
                }
                
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Correct! You earned {pointsAwarded} points.");
                Console.ResetColor();
                totalScore += pointsAwarded;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Incorrect or invalid answer.");
                Console.ResetColor();
            }
        }

        Console.WriteLine($"\nTest complete! Your total score is: {totalScore}");
        _testHistory.Add(new TestLog { Date = DateTime.Now, Score = totalScore });
        SaveTestHistory();
        Console.WriteLine("Your score has been logged to history.");
    }
    
    // Views the user's test history.
    public void ViewHistory()
    {
        Console.WriteLine("\n--- IQ Test History ---");
        if (_testHistory.Count == 0)
        {
            Console.WriteLine("You have not taken any tests yet.");
            return;
        }
        
        foreach (var log in _testHistory)
        {
            Console.WriteLine($"Date: {log.Date.ToShortDateString()} - Score: {log.Score}");
        }
    }
}
