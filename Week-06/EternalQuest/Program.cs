using System;
using System.Collections.Generic;
using System.IO;

/*
Exceeds requirements:
- Level system: Level = 1 + (Score / 500). Level-up message shown when the level increases.
- Badges: unlocks simple textual badges for first record, first checklist completion, and reaching levels 2/3/4+. Badges are saved and loaded.

Core features:
- Simple, Eternal, Checklist goals with shared base class and overrides
- Create goals, list, record events, scoring
- Save and load goals with score and badges
*/

class Program
{
    static int _totalScore = 0;
    static int _level = 1;
    static readonly List<Goal> _goals = new();
    static readonly HashSet<string> _badges = new();

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Eternal Quest");
            Console.WriteLine($"Score: {_totalScore}  |  Level: {_level}");
            Console.WriteLine("1. Create a new goal");
            Console.WriteLine("2. List goals");
            Console.WriteLine("3. Record an event");
            Console.WriteLine("4. Save");
            Console.WriteLine("5. Load");
            Console.WriteLine("6. Show badges");
            Console.WriteLine("7. Quit");
            Console.Write("Choose an option (1-7): ");
            var choice = Console.ReadLine();
            Console.WriteLine();

            if (choice == "1") CreateGoal();
            else if (choice == "2") ListGoals();
            else if (choice == "3") RecordEvent();
            else if (choice == "4") Save();
            else if (choice == "5") Load();
            else if (choice == "6") ShowBadges();
            else if (choice == "7") break;
            else Console.WriteLine("Invalid option\n");
        }
    }

    static void CreateGoal()
    {
        Console.WriteLine("Select goal type");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.Write("Type: ");
        var t = Console.ReadLine();
        Console.Write("Name: ");
        var name = Console.ReadLine() ?? "";
        Console.Write("Description: ");
        var desc = Console.ReadLine() ?? "";
        Console.Write("Points: ");
        if (!int.TryParse(Console.ReadLine(), out var pts)) { Console.WriteLine("Invalid points\n"); return; }

        if (t == "1")
        {
            _goals.Add(new SimpleGoal(name, desc, pts));
            Console.WriteLine("Simple goal created\n");
        }
        else if (t == "2")
        {
            _goals.Add(new EternalGoal(name, desc, pts));
            Console.WriteLine("Eternal goal created\n");
        }
        else if (t == "3")
        {
            Console.Write("Target count: ");
            if (!int.TryParse(Console.ReadLine(), out var target)) { Console.WriteLine("Invalid target\n"); return; }
            Console.Write("Bonus on completion: ");
            if (!int.TryParse(Console.ReadLine(), out var bonus)) { Console.WriteLine("Invalid bonus\n"); return; }
            _goals.Add(new ChecklistGoal(name, desc, pts, target, bonus));
            Console.WriteLine("Checklist goal created\n");
        }
        else
        {
            Console.WriteLine("Invalid type\n");
        }
    }

    static void ListGoals()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("(No goals)\n");
            return;
        }
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetStatus()}");
        }
        Console.WriteLine();
    }

    static void RecordEvent()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("(No goals)\n");
            return;
        }
        ListGoals();
        Console.Write("Select goal number: ");
        if (!int.TryParse(Console.ReadLine(), out var idx)) { Console.WriteLine("Invalid selection\n"); return; }
        idx -= 1;
        if (idx < 0 || idx >= _goals.Count) { Console.WriteLine("Invalid selection\n"); return; }

        var earned = _goals[idx].RecordEvent();
        if (earned <= 0) { Console.WriteLine("No points earned\n"); return; }

        var prevLevel = _level;
        _totalScore += earned;
        UpdateLevel();
        Console.WriteLine($"Points earned: {earned}");
        Console.WriteLine($"Total score: {_totalScore}");
        if (_level > prevLevel) UnlockBadge($"Reached Level {_level}", $"LevelUp_{_level}");
        UnlockBadge("First Record", "FirstRecord");
        if (_goals[idx] is ChecklistGoal cg && cg.IsComplete) UnlockBadge("Checklist Completed", "ChecklistCompleted");
        Console.WriteLine();
    }

    static void Save()
    {
        Console.Write("Filename: ");
        var file = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(file)) { Console.WriteLine("Invalid filename\n"); return; }

        using var sw = new StreamWriter(file);
        sw.WriteLine($"{_totalScore}|{_level}");
        if (_badges.Count > 0) sw.WriteLine(string.Join(";", _badges));
        else sw.WriteLine("-");
        foreach (var g in _goals) sw.WriteLine(g.Serialize());
        Console.WriteLine("Saved\n");
    }

    static void Load()
    {
        Console.Write("Filename: ");
        var file = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(file) || !File.Exists(file)) { Console.WriteLine("File not found\n"); return; }

        var lines = File.ReadAllLines(file);
        if (lines.Length < 2) { Console.WriteLine("File is invalid\n"); return; }

        var head = lines[0].Split('|');
        if (!int.TryParse(head[0], out _totalScore)) _totalScore = 0;
        if (!int.TryParse(head.Length > 1 ? head[1] : "1", out _level)) _level = 1;

        _badges.Clear();
        if (lines[1] != "-" && lines[1].Length > 0)
        {
            foreach (var b in lines[1].Split(';'))
                if (!string.IsNullOrWhiteSpace(b)) _badges.Add(b);
        }

        _goals.Clear();
        for (int i = 2; i < lines.Length; i++)
        {
            var g = Goal.Deserialize(lines[i]);
            if (g != null) _goals.Add(g);
        }
        Console.WriteLine("Loaded\n");
    }

    static void ShowBadges()
    {
        if (_badges.Count == 0) { Console.WriteLine("(No badges)\n"); return; }
        Console.WriteLine("Badges:");
        foreach (var b in _badges) Console.WriteLine($"- {b}");
        Console.WriteLine();
    }

    static void UpdateLevel()
    {
        var newLevel = 1 + (_totalScore / 500);
        if (newLevel > _level)
        {
            _level = newLevel;
            Console.WriteLine($"Level up! You are now Level {_level}");
        }
    }

    static void UnlockBadge(string humanName, string key)
    {
        if (_badges.Add(key))
            Console.WriteLine($"Badge unlocked: {humanName}");
    }
}
