using System;

public abstract class Goal
{
    public string Name { get; protected set; }
    public string Description { get; protected set; }
    public int Points { get; protected set; }

    protected Goal(string name, string description, int points)
    {
        Name = name;
        Description = description;
        Points = points;
    }

    public abstract bool IsComplete { get; }
    public abstract int RecordEvent();
    public abstract string GetStatus();
    public abstract string Serialize();

    public static Goal? Deserialize(string data)
    {
        var parts = data.Split('|');
        if (parts.Length < 1) return null;
        var type = parts[0];
        if (type == "SimpleGoal")
        {
            if (parts.Length < 5) return null;
            var name = parts[1];
            var desc = parts[2];
            var pts = int.Parse(parts[3]);
            var done = bool.Parse(parts[4]);
            return new SimpleGoal(name, desc, pts, done);
        }
        if (type == "EternalGoal")
        {
            if (parts.Length < 4) return null;
            var name = parts[1];
            var desc = parts[2];
            var pts = int.Parse(parts[3]);
            return new EternalGoal(name, desc, pts);
        }
        if (type == "ChecklistGoal")
        {
            if (parts.Length < 7) return null;
            var name = parts[1];
            var desc = parts[2];
            var pts = int.Parse(parts[3]);
            var target = int.Parse(parts[4]);
            var bonus = int.Parse(parts[5]);
            var count = int.Parse(parts[6]);
            return new ChecklistGoal(name, desc, pts, target, bonus, count);
        }
        return null;
    }
}
