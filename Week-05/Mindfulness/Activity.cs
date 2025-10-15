using System;
using System.Threading;

public abstract class Activity
{
    private string _name;
    private string _description;
    private int _durationSeconds;

    protected Activity(string name, string description)
    {
        _name = name;
        _description = description;
        _durationSeconds = 0;
    }

    protected string Name => _name;
    protected string Description => _description;
    protected int DurationSeconds => _durationSeconds;

    public void Run()
    {
        ShowStart();
        Execute();
        ShowEnd();
    }

    protected abstract void Execute();

    private void ShowStart()
    {
        Console.WriteLine($"Welcome to the {_name}.");
        Console.WriteLine(Description);
        Console.Write("How many seconds would you like for this session? ");
        var input = Console.ReadLine();
        if (!int.TryParse(input, out int secs) || secs <= 0) secs = 30;
        _durationSeconds = secs;
        Console.WriteLine("Get ready to begin...");
        Spinner(3);
        Console.WriteLine();
    }

    private void ShowEnd()
    {
        Console.WriteLine("Well done!");
        Spinner(2);
        Console.WriteLine($"You have completed the {_name} for {DurationSeconds} seconds.");
        Spinner(3);
        Console.WriteLine();
    }

    protected void Spinner(int seconds)
    {
        var frames = new[] { "|", "/", "-", "\\" };
        var end = DateTime.UtcNow.AddSeconds(seconds);
        int i = 0;
        while (DateTime.UtcNow < end)
        {
            Console.Write(frames[i % frames.Length]);
            Thread.Sleep(150);
            Console.Write("\b");
            i++;
        }
    }

    protected void Countdown(int seconds)
    {
        for (int s = seconds; s > 0; s--)
        {
            Console.Write($"{s}");
            Thread.Sleep(1000);
            Console.Write("\b \b");
        }
    }
}
