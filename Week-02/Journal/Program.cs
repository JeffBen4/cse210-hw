using System;

/*
Exceeds core requirements:
1) CSV export/import with correct handling of commas and quotes (Entry.ToCsvLine / FromCsvLine).
2) Search feature to find entries by keyword (date/prompt/response).

Core features included:
- Random prompts, write entry with date
- Display all entries
- Save/Load to text with custom '|' separator
*/

class Program
{
    static void Main(string[] args)
    {
        var journal = new Journal();
        var prompts = new PromptGenerator();

        while (true)
        {
            Console.WriteLine("Journal Menu");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file (text)");
            Console.WriteLine("4. Load the journal from a file (text)");
            Console.WriteLine("5. Export CSV");
            Console.WriteLine("6. Import CSV");
            Console.WriteLine("7. Search entries");
            Console.WriteLine("8. Quit");
            Console.Write("Choose an option (1-8): ");
            string choice = Console.ReadLine();
            Console.WriteLine();

            if (choice == "1")
            {
                string prompt = prompts.NextPrompt();
                Console.WriteLine(prompt);
                Console.Write("> ");
                string response = Console.ReadLine();
                string date = DateTime.Now.ToShortDateString();
                journal.Add(new Entry(date, prompt, response));
                Console.WriteLine("Entry added.\n");
            }
            else if (choice == "2")
            {
                journal.Display();
            }
            else if (choice == "3")
            {
                Console.Write("Enter filename to save (e.g., journal.txt): ");
                string filename = Console.ReadLine();
                journal.SaveToFile(filename);
                Console.WriteLine("Journal saved.\n");
            }
            else if (choice == "4")
            {
                Console.Write("Enter filename to load (e.g., journal.txt): ");
                string filename = Console.ReadLine();
                journal.LoadFromFile(filename);
                Console.WriteLine("Journal loaded.\n");
            }
            else if (choice == "5")
            {
                Console.Write("Enter CSV filename to export (e.g., journal.csv): ");
                string filename = Console.ReadLine();
                journal.ExportCsv(filename);
                Console.WriteLine("CSV exported.\n");
            }
            else if (choice == "6")
            {
                Console.Write("Enter CSV filename to import (e.g., journal.csv): ");
                string filename = Console.ReadLine();
                journal.ImportCsv(filename);
                Console.WriteLine("CSV imported.\n");
            }
            else if (choice == "7")
            {
                Console.Write("Enter keyword to search: ");
                string keyword = Console.ReadLine();
                var results = journal.Search(keyword);
                if (results.Count == 0) Console.WriteLine("No matches.\n");
                else
                {
                    foreach (var e in results)
                    {
                        Console.WriteLine(e.ToString());
                        Console.WriteLine();
                    }
                }
            }
            else if (choice == "8")
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid option.\n");
            }
        }
    }
}
