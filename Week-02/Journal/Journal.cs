using System;
using System.Collections.Generic;
using System.IO;

public class Journal
{
    private readonly List<Entry> _entries = new();

    public void Add(Entry entry) => _entries.Add(entry);

    public void Display()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("(Journal is empty)");
            return;
        }

        foreach (var e in _entries)
        {
            Console.WriteLine(e.ToString());
            Console.WriteLine();
        }
    }

    public void SaveToFile(string filename)
    {
        using var sw = new StreamWriter(filename);
        foreach (var e in _entries)
            sw.WriteLine(e.ToStorageLine());
    }

    public void LoadFromFile(string filename)
    {
        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found.");
            return;
        }

        _entries.Clear();
        var lines = File.ReadAllLines(filename);
        foreach (var line in lines)
        {
            var e = Entry.FromStorageLine(line);
            if (e != null) _entries.Add(e);
        }
    }
}
