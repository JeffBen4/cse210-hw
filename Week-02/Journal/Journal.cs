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

    public void ExportCsv(string filename)
    {
        using var sw = new StreamWriter(filename);
        sw.WriteLine("Date,Prompt,Response");
        foreach (var e in _entries)
            sw.WriteLine(e.ToCsvLine());
    }

    public void ImportCsv(string filename)
    {
        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found.");
            return;
        }
        _entries.Clear();
        var lines = File.ReadAllLines(filename);
        int start = 0;
        if (lines.Length > 0 && lines[0].StartsWith("Date,Prompt,Response")) start = 1;
        for (int i = start; i < lines.Length; i++)
        {
            var e = Entry.FromCsvLine(lines[i]);
            if (e != null) _entries.Add(e);
        }
    }

    public List<Entry> Search(string keyword)
    {
        var results = new List<Entry>();
        if (string.IsNullOrWhiteSpace(keyword)) return results;
        var k = keyword.Trim();
        foreach (var e in _entries)
        {
            if ((e.Prompt?.IndexOf(k, StringComparison.OrdinalIgnoreCase) ?? -1) >= 0 ||
                (e.Response?.IndexOf(k, StringComparison.OrdinalIgnoreCase) ?? -1) >= 0 ||
                (e.Date?.IndexOf(k, StringComparison.OrdinalIgnoreCase) ?? -1) >= 0)
            {
                results.Add(e);
            }
        }
        return results;
    }
}
