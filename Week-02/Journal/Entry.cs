using System;

public class Entry
{
    public string Date { get; set; }
    public string Prompt { get; set; }
    public string Response { get; set; }

    public Entry(string date, string prompt, string response)
    {
        Date = date;
        Prompt = prompt;
        Response = response;
    }

    public override string ToString()
    {
        return $"[{Date}] {Prompt}\n{Response}";
    }

    public string ToStorageLine()
    {
        return $"{Date}|{Prompt}|{Response}";
    }

    public static Entry? FromStorageLine(string line)
    {
        var parts = line.Split('|');
        if (parts.Length < 3) return null;
        return new Entry(parts[0], parts[1], string.Join("|", parts, 2, parts.Length - 2));
    }
}
