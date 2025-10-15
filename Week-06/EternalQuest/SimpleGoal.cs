public class SimpleGoal : Goal
{
    public bool Done { get; private set; }

    public SimpleGoal(string name, string description, int points, bool done = false)
        : base(name, description, points)
    {
        Done = done;
    }

    public override bool IsComplete => Done;

    public override int RecordEvent()
    {
        if (Done) return 0;
        Done = true;
        return Points;
    }

    public override string GetStatus()
    {
        var box = Done ? "[X]" : "[ ]";
        return $"{box} {Name} ({Description})";
    }

    public override string Serialize()
    {
        return $"SimpleGoal|{Name}|{Description}|{Points}|{Done}";
    }
}
