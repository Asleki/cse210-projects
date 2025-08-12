// ChecklistGoal.cs
//
// This file defines the ChecklistGoal class, which inherits from the
// abstract Goal class. It represents a goal that must be
// completed a set number of times to be finished.
// It awards a bonus upon reaching the target completion count.

public class ChecklistGoal : Goal
{
    // Private member variables to track the progress and bonus.
    private int _amountCompleted;
    private int _target;
    private int _bonus;

    // A public property to access the _bonus field.
    public int Bonus
    {
        get { return _bonus; }
    }

    // Constructor to initialize a new ChecklistGoal object.
    public ChecklistGoal(string name, string description, int points, int target, int bonus)
        : base(name, description, points)
    {
        _amountCompleted = 0;
        _target = target;
        _bonus = bonus;
    }
    
    // A secondary constructor for loading a goal from a saved file.
    public ChecklistGoal(string name, string description, int points, int amountCompleted, int target, int bonus)
        : base(name, description, points)
    {
        _amountCompleted = amountCompleted;
        _target = target;
        _bonus = bonus;
    }


    // This method increments the completed count.
    public override void RecordEvent()
    {
        _amountCompleted++;
    }


    // It returns true if the amount completed meets or exceeds the target.
    public override bool IsComplete()
    {
        return _amountCompleted >= _target;
    }

   
    // including the progress of the goal.
    public override string GetDetailsString()
    {
        return $"{_shortName} ({_description}) -- Completed {_amountCompleted}/{_target} times";
    }


    // It formats the goal's data into a string for saving to a file.
    public override string GetStringRepresentation()
    {
        return $"ChecklistGoal:{_shortName},{_description},{_points},{_bonus},{_target},{_amountCompleted}";
    }
}
