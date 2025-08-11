// SimpleGoal.cs
//
// This file defines the SimpleGoal class, which inherits from the
// abstract Goal class. It represents a one-time goal
// that, once completed, cannot be recorded again.

public class SimpleGoal : Goal
{
    // A private member variable to track the completion status.
    private bool _isComplete;

    // Constructor to initialize a SimpleGoal object.
    // It calls the base class constructor and sets the initial
    // completion status to false.
    public SimpleGoal(string name, string description, int points) : base(name, description, points)
    {
        _isComplete = false;
    }
    
    // A secondary constructor for loading a goal from a saved file.
    // This constructor takes an additional parameter for the
    // completion status.
    public SimpleGoal(string name, string description, int points, bool isComplete) : base(name, description, points)
    {
        _isComplete = isComplete;
    }

    // Overrides the RecordEvent method from the base class.
    // This method marks the goal as complete.
    public override void RecordEvent()
    {
        _isComplete = true;
    }

    // Overrides the IsComplete method.
    // It returns the current completion status of the goal.
    public override bool IsComplete()
    {
        return _isComplete;
    }

    // Overrides the GetStringRepresentation method.
    // It formats the goal's data into a string for saving to a file.
    // The format includes the goal type and all necessary attributes.
    public override string GetStringRepresentation()
    {
        return $"SimpleGoal:{_shortName},{_description},{_points},{_isComplete}";
    }
}
