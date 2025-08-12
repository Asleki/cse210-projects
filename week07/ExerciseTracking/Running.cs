using System;

public class Running : Activity
{
    private double _distance; // In kilometers

    public Running(DateTime date, int minutes, double distance)
        : base(date, minutes)
    {
        _distance = distance;
    }

    public override double GetDistance()
    {
        return _distance;
    }

    public override double GetSpeed()
    {
        // Speed = (distance / minutes) * 60
        return (_distance / GetMinutes()) * 60;
    }

    public override double GetPace()
    {
        // Pace = minutes / distance
        return GetMinutes() / _distance;
    }
}