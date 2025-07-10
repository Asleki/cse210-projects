// Resume.cs
// This file defines the Resume class, which contains a list of Job objects and a method to display them.
using System;
using System.Collections.Generic;

public class Resume
{
    public string _name;

    // This is a list that holds Job objects.
    // It allows us to store multiple jobs in a single resume.
    public List<Job> _jobs = new List<Job>();

    public void Display()
    {
        Console.WriteLine($"Name: {_name}");
        Console.WriteLine("Jobs:");

        // Loop through each job in the _jobs list
        // and call the Display method on each job.
        foreach (Job job in _jobs)
        {
            // This calls the Display method on each job
            job.Display();
        }
    }
}