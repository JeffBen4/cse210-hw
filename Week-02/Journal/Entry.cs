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

    public string ToCsvLine()
    {
        static string Q(string s) => $"\"{(s ?? string.Empty).Replace("\"", "\"\"")}\"";
        return $"{Q(Date)},{Q(Prompt)},{Q(Response)}";
    }

    public static Entry? FromCsvLine(string line)
    {
        var parts = SplitCsvLine(line);
        if (parts.Count < 3) return null;
        return new Entry(parts[0], parts[1], parts[2]);
    }

    private static System.Collections.Generic.List<string> SplitCsvLine(string line)
    {
        var list = new System.Collections.Generic.List<string>();
        if (line == null) return list;

        bool inQuotes = false;
        var current = new System.Text.StringBuilder();
        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];
            if (inQuotes)
            {
                if (c == '"')
                {
                    bool hasNext = i + 1 < line.Length;
                    if (hasNext && line[i + 1] == '"') { current.Append('"'); i++; }
                    else { inQuotes = false; }
                }
                else current.Append(c);
            }
            else
            {
                if (c == '"') inQuotes = true;
                else if (c == ',') { list.Add(current.ToString()); current.Clear(); }
                else current.Append(c);
            }
        }
        list.Add(current.ToString());
        return list;
    }
}
