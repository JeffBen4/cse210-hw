public class SimpleGoal : Goal
{
    private bool _done;

    public SimpleGoal(string name, string description, int points, bool done = false)
        : base(name, description, points)
    {
        _done = done;
    }

    public override bool IsComplete => _done;

    public override int RecordEvent()
    {
        if (_done) return 0;
        _done = true;
        return Points;
    }

    public override string GetStatus()
    {
        var box = _done ? "[X]" : "[ ]";
        return $"{box} {Name} ({Description})";
    }

    public override string Serialize()
    {
        return $"SimpleGoal|{Name}|{Description}|{Points}|{_done}";
    }
}
