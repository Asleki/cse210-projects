
public class Fraction
{
    // Attributes
    // Make sure the attributes are private.
    private int _top;
    private int _bottom;

    // Constructors

    // Constructor that has no parameters that initializes the number to 1/1.
    public Fraction()
    {
        _top = 1;
        _bottom = 1;
    }

    // Constructor that has one parameter for the top and that initializes the denominator to 1.
    // So that if you pass in the number 5, the fraction would be initialized to 5/1.
    public Fraction(int wholeNumber)
    {
        _top = wholeNumber;
        _bottom = 1;
    }

    // Constructor that has two parameters, one for the top and one for the bottom.
    public Fraction(int top, int bottom)
    {
        _top = top;
        _bottom = bottom;
    }

    // Getters and Setters

    // Getter for _top
    public int GetTop()
    {
        return _top;
    }

    // Setter for _top
    public void SetTop(int top)
    {
        _top = top;
    }

    // Getter for _bottom
    public int GetBottom()
    {
        return _bottom;
    }

    // Setter for _bottom
    public void SetBottom(int bottom)
    {
        // Optional: Add validation to prevent division by zero
        if (bottom != 0)
        {
            _bottom = bottom;
        }
        else
        {
            // Handle error, e.g., throw an exception or set a default
            System.Console.WriteLine("Error: Denominator cannot be zero. Setting to 1.");
            _bottom = 1;
        }
    }

    // Methods to return representations

    // Create a method called GetFractionString that returns the fraction in the form 3/4.
    public string GetFractionString()
    {
        return $"{_top}/{_bottom}";
    }

    // Create a method called GetDecimalValue that returns a double that is the result of dividing the top number by the bottom number, such as 0.75.
    public double GetDecimalValue()
    {
        // Cast _top to double to ensure floating-point division
        return (double)_top / _bottom;
    }
}