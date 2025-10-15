using System;

public abstract class Activity
{
    private DateTime _date;
    private int _minutes;

    protected Activity(DateTime date, int minutes)
    {
        _date = date;
        _minutes = minutes;
    }

    protected DateTime Date => _date;
    protected int Minutes => _minutes;

    public abstract double GetDistanceMiles();
    public abstract double GetSpeedMph();
    public abstract double GetPaceMinPerMile();

    public virtual string GetSummary()
    {
        string d = _date.ToString("dd MMM yyyy");
        string type = GetType().Name.Replace("Activity", "");
        double dist = GetDistanceMiles();
        double speed = GetSpeedMph();
        double pace = GetPaceMinPerMile();
        return $"{d} {type} ({_minutes} min): Distance {dist:0.0} miles, Speed {speed:0.0} mph, Pace {pace:0.00} min per mile";
    }
}
