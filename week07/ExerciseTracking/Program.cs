using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Create a list to hold all the activities
        List<Activity> activities = new List<Activity>();

        // Create instances of each activity type
        Running running1 = new Running(new DateTime(2025, 7, 25), 30, 4.8);
        Cycling cycling1 = new Cycling(new DateTime(2025, 7, 26), 45, 25.0);
        Swimming swimming1 = new Swimming(new DateTime(2025, 7, 27), 30, 20);

        // Add the activities to the list
        activities.Add(running1);
        activities.Add(cycling1);
        activities.Add(swimming1);

        // Iterate through the list and display the summary for each activity
        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}