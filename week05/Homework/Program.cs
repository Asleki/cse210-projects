// Program.cs
using System;

class Program
{
    static void Main(string[] args)
    {
        // Test the base Assignment class
        Assignment assignment1 = new Assignment("Samuel Bennett", "Multiplication");
        Console.WriteLine(assignment1.GetSummary());
        Console.WriteLine(); // Add a blank line for readability

        

        // Test the MathAssignment class
        MathAssignment mathAssignment1 = new MathAssignment("Roberto Rodriguez", "Fractions", "7.3", "8-19");

        // Call inherited method GetSummary()
        Console.WriteLine(mathAssignment1.GetSummary());

        // Call method specific to MathAssignment
        Console.WriteLine(mathAssignment1.GetHomeworkList());
        Console.WriteLine(); // Add a blank line for readability
    }
}