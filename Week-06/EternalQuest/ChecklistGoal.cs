public class ChecklistGoal : Goal
{
    public int TargetCount { get; private set; }
    public int Bonus { get; private set; }
    public int CurrentCount { get; private set; }

    public ChecklistGoal(string name, string description, int points, int targetCount, int bonus, int currentCount = 0)
        : base(name, description, points)
    {
        TargetCount = targetCount;
        Bonus = bonus;
        CurrentCount = currentCount;
    }

    public override bool IsComplete => CurrentCount >= TargetCount;

    public override int RecordEvent()
    {
        if (IsComplete) return 0;
        CurrentCount++;
        if (IsComplete) return Points + Bonus;
        return Points;
    }

    public override string GetStatus()
    {
        var box = IsComplete ? "[X]" : "[ ]";
        return $"{box} {Name} ({Description}) — Completed {CurrentCount}/{TargetCount}";
    }

    public override string Serialize()
    {
        return $"ChecklistGoal|{Name}|{Description}|{Points}|{TargetCount}|{Bonus}|{CurrentCount}";
    }
}
