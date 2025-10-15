using System;
using System.Collections.Generic;

public class PromptBank
{
    private readonly Random _rng = new();
    private readonly Queue<string> _reflectionQueue;
    private readonly Queue<string> _listingQueue;

    public PromptBank()
    {
        _reflectionQueue = new Queue<string>(Shuffle(new[]
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        }));

        _listingQueue = new Queue<string>(Shuffle(new[]
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        }));
    }

    public string NextReflectionPrompt()
    {
        if (_reflectionQueue.Count == 0) RefillReflection();
        return _reflectionQueue.Dequeue();
    }

    public string NextListingPrompt()
    {
        if (_listingQueue.Count == 0) RefillListing();
        return _listingQueue.Dequeue();
    }

    private void RefillReflection()
    {
        foreach (var p in Shuffle(new[]
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        })) _reflectionQueue.Enqueue(p);
    }

    private void RefillListing()
    {
        foreach (var p in Shuffle(new[]
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        })) _listingQueue.Enqueue(p);
    }

    private IEnumerable<string> Shuffle(IEnumerable<string> items)
    {
        var list = new List<string>(items);
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = _rng.Next(i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
        return list;
    }
}
