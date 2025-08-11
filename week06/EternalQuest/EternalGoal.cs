// EternalGoal.cs
//
// This file defines the EternalGoal class, which inherits from the
// abstract Goal class. It represents a goal that is never completed,
// but awards points each time an event is recorded.

public class EternalGoal : Goal
{
    // Constructor to initialize an EternalGoal object.
    // It calls the base class constructor.
    public EternalGoal(string name, string description, int points) : base(name, description, points)
    {
        // No additional member variables or logic needed for this class.
    }
    
    // A secondary constructor for loading a goal from a saved file.
    // This constructor simply calls the base constructor.
    public EternalGoal(string name, string description, int points, bool isComplete) : base(name, description, points)
    {
        // The 'isComplete' parameter is included for consistency with the
        // other goal types when loading, but it is not used here.
    }

    // Overrides the RecordEvent method from the base class.
    // This method does not change the state of the goal,
    // as it is never "completed."
    public override void RecordEvent()
    {
        // For an eternal goal, recording an event just means points are awarded.
        // The state of the goal itself does not change.
    }

    // Overrides the IsComplete method.
    // An eternal goal is never complete, so this method always returns false.
    public override bool IsComplete()
    {
        return false;
    }

    // Overrides the GetStringRepresentation method.
    // It formats the goal's data into a string for saving to a file.
    // The format includes the goal type and all necessary attributes.
    public override string GetStringRepresentation()
    {
        return $"EternalGoal:{_shortName},{_description},{_points}";
    }
}
