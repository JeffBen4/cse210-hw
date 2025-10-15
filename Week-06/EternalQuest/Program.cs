using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static int totalScore = 0;
    static List<Goal> goals = new();

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Eternal Quest");
            Console.WriteLine($"Score: {totalScore}");
            Console.WriteLine("1. Create a new goal");
            Console.WriteLine("2. List goals");
            Console.WriteLine("3. Record an event");
            Console.WriteLine("4. Save");
            Console.WriteLine("5. Load");
            Console.WriteLine("6. Quit");
            Console.Write("Choose an option (1-6): ");
            var choice = Console.ReadLine();
            Console.WriteLine();

            if (choice == "1") CreateGoal();
            else if (choice == "2") ListGoals();
            else if (choice == "3") RecordEvent();
            else if (choice == "4") Save();
            else if (choice == "5") Load();
            else if (choice == "6") break;
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
            goals.Add(new SimpleGoal(name, desc, pts));
            Console.WriteLine("Simple goal created\n");
        }
        else if (t == "2")
        {
            goals.Add(new EternalGoal(name, desc, pts));
            Console.WriteLine("Eternal goal created\n");
        }
        else if (t == "3")
        {
            Console.Write("Target count: ");
            if (!int.TryParse(Console.ReadLine(), out var target)) { Console.WriteLine("Invalid target\n"); return; }
            Console.Write("Bonus on completion: ");
            if (!int.TryParse(Console.ReadLine(), out var bonus)) { Console.WriteLine("Invalid bonus\n"); return; }
            goals.Add(new ChecklistGoal(name, desc, pts, target, bonus));
            Console.WriteLine("Checklist goal created\n");
        }
        else
        {
            Console.WriteLine("Invalid type\n");
        }
    }

    static void ListGoals()
    {
        if (goals.Count == 0)
        {
            Console.WriteLine("(No goals)\n");
            return;
        }
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].GetStatus()}");
        }
        Console.WriteLine();
    }

    static void RecordEvent()
    {
        if (goals.Count == 0)
        {
            Console.WriteLine("(No goals)\n");
            return;
        }
        ListGoals();
        Console.Write("Select goal number: ");
        if (!int.TryParse(Console.ReadLine(), out var idx)) { Console.WriteLine("Invalid selection\n"); return; }
        idx -= 1;
        if (idx < 0 || idx >= goals.Count) { Console.WriteLine("Invalid selection\n"); return; }

        var earned = goals[idx].RecordEvent();
        if (earned <= 0) Console.WriteLine("No points earned\n");
        else
        {
            totalScore += earned;
            Console.WriteLine($"Points earned: {earned}");
            Console.WriteLine($"Total score: {totalScore}\n");
        }
    }

    static void Save()
    {
        Console.Write("Filename: ");
        var file = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(file)) { Console.WriteLine("Invalid filename\n"); return; }

        using var sw = new StreamWriter(file);
        sw.WriteLine(totalScore);
        foreach (var g in goals) sw.WriteLine(g.Serialize());
        Console.WriteLine("Saved\n");
    }

    static void Load()
    {
        Console.Write("Filename: ");
        var file = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(file) || !File.Exists(file)) { Console.WriteLine("File not found\n"); return; }

        var lines = File.ReadAllLines(file);
        if (lines.Length == 0) { Console.WriteLine("File is empty\n"); return; }

        if (!int.TryParse(lines[0], out totalScore)) totalScore = 0;
        goals.Clear();
        for (int i = 1; i < lines.Length; i++)
        {
            var g = Goal.Deserialize(lines[i]);
            if (g != null) goals.Add(g);
        }
        Console.WriteLine("Loaded\n");
    }
}
