// This program tests the Fraction class by creating instances using different constructors,
// manipulating the fraction values using getters and setters, and displaying the results.
using System;

public class Program
{
    public static void Main(string[] args)
    {
        // Verify that you can create fractions using all three of these constructors.
        Console.WriteLine("--- Testing Constructors ---");

        // Create an instance for 1/1 (using the first constructor)
        Fraction fraction1 = new Fraction();
        Console.WriteLine($"Fraction 1 (no args): {fraction1.GetFractionString()} = {fraction1.GetDecimalValue()}"); // Expected: 1/1 = 1

        // Create an instance for 6/1 (using the second constructor)
        Fraction fraction2 = new Fraction(6);
        Console.WriteLine($"Fraction 2 (one arg): {fraction2.GetFractionString()} = {fraction2.GetDecimalValue()}"); // Expected: 6/1 = 6

        // Create an instance for 6/7 (using the third constructor)
        Fraction fraction3 = new Fraction(6, 7);
        Console.WriteLine($"Fraction 3 (two args): {fraction3.GetFractionString()} = {fraction3.GetDecimalValue()}"); // Expected: 6/7 = 0.857...

        Console.WriteLine("\n--- Testing Getters and Setters ---");

        // Create a new fraction to test getters and setters
        Fraction testFraction = new Fraction(3, 5);
        Console.WriteLine($"Original test fraction: {testFraction.GetFractionString()}"); // Expected: 3/5

        // Use setter to change the top value
        testFraction.SetTop(8);
        Console.WriteLine($"After SetTop(8): {testFraction.GetFractionString()}"); // Expected: 8/5

        // Use getter to retrieve the new top value
        Console.WriteLine($"Retrieved top value using GetTop(): {testFraction.GetTop()}"); // Expected: 8

        // Use setter to change the bottom value
        testFraction.SetBottom(9);
        Console.WriteLine($"After SetBottom(9): {testFraction.GetFractionString()}"); // Expected: 8/9

        // Use getter to retrieve the new bottom value
        Console.WriteLine($"Retrieved bottom value using GetBottom(): {testFraction.GetBottom()}"); // Expected: 9

        // Test setter with invalid denominator (0)
        testFraction.SetBottom(0);
        Console.WriteLine($"After SetBottom(0): {testFraction.GetFractionString()}"); // Expected: Error message, then 8/1

        Console.WriteLine("\n--- Sample Output Verification ---");

        // Verify that you can call each constructor and that you can retrieve and display the different representations for a few different fractions.
        // 1
        Fraction f1 = new Fraction(1);
        Console.WriteLine(f1.GetFractionString());
        Console.WriteLine(f1.GetDecimalValue());

        // 5
        Fraction f5 = new Fraction(5);
        Console.WriteLine(f5.GetFractionString());
        Console.WriteLine(f5.GetDecimalValue());

        // 3/4
        Fraction f3_4 = new Fraction(3, 4);
        Console.WriteLine(f3_4.GetFractionString());
        Console.WriteLine(f3_4.GetDecimalValue());

        // 1/3
        Fraction f1_3 = new Fraction(1, 3);
        Console.WriteLine(f1_3.GetFractionString());
        Console.WriteLine(f1_3.GetDecimalValue());
    }
}