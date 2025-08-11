// Goal.cs
//
// This file defines the abstract Goal class.
// It serves as the base class for all goal types
// in the Digihealth App.
//
// The class provides a common interface for all goals,
// including a short name, a description, and points value.
// Derived classes will implement the specific behaviors
// for recording events and determining completion.

public abstract class Goal
{
    // Member variables for the common attributes of all goals.
    protected string _shortName;
    protected string _description;
    protected int _points;

    // A public property to allow other classes to access the _points value.
    public int Points
    {
        get { return _points; }
    }

    // Constructor to initialize the common attributes.
    public Goal(string name, string description, int points)
    {
        _shortName = name;
        _description = description;
        _points = points;
    }

    // This is an abstract method that must be overridden
    // in all derived classes to define the behavior
    // when an event for the goal is recorded.
    public abstract void RecordEvent();

    // This is an abstract method that must be overridden
    // in all derived classes to determine
    // if the goal has been completed.
    public abstract bool IsComplete();

    // This is a virtual method that provides a default string
    // representation of the goal details.
    // It can be overridden by derived classes if
    // a different format is needed.
    public virtual string GetDetailsString()
    {
        return $"{_shortName} ({_description})";
    }

    // This is an abstract method that must be overridden
    // to provide a string representation of the goal's data.
    // This is used for saving the goal to a file.
    public abstract string GetStringRepresentation();
}
