using System;
using System.Collections.Generic;
using System.IO;

// EXTRAS (for 100%):
// 1) Scripture library: the program chooses one at random from an internal list.
// 2) Optional file loading: if "scriptures.txt" exists in the executable folder,
//    scriptures are added from the file (format: Reference|Text, e.g., "John 3:16|For God so loved...")
// 3) Smart selection: when hiding, it only chooses words that are not already hidden.
//    (completes the stretch challenge)

namespace ScriptureMemorizer
{
    public class Program
    {
        static void Main()
        {
            // Build scripture library
            var scriptures = BuildLibrary();

            // Pick one at random
            var rng = new Random();
            var chosen = scriptures[rng.Next(scriptures.Count)];

            // Main loop
            while (true)
            {
                Console.Clear();
                Console.WriteLine(chosen.Display());
                Console.WriteLine();
                Console.Write("Press ENTER to hide more words, or type 'quit' to end: ");
                string? input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input) &&
                    input.Trim().Equals("quit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                // Hide 3–5 words each time
                int toHide = rng.Next(3, 6);
                chosen.HideRandomWords(toHide);

                if (chosen.AllHidden())
                {
                    Console.Clear();
                    Console.WriteLine(chosen.Display());
                    Console.WriteLine("\n(All words are hidden. Program will end.)");
                    break;
                }
            }
        }

        private static List<Scripture> BuildLibrary()
        {
            var list = new List<Scripture>();

            // Built-in examples
            list.Add(new Scripture(
                new Reference("John", 3, 16),
                "For God so loved the world, that he gave his only begotten Son, " +
                "that whosoever believeth in him should not perish, but have everlasting life."));

            list.Add(new Scripture(
                new Reference("Proverbs", 3, 5, 6),
                "Trust in the Lord with all thine heart; and lean not unto thine own understanding. " +
                "In all thy ways acknowledge him, and he shall direct thy paths."));

            list.Add(new Scripture(
                new Reference("Psalm", 23, 1),
                "The Lord is my shepherd; I shall not want."));

            // Optional file loading
            string path = Path.Combine(AppContext.BaseDirectory, "scriptures.txt");
            if (File.Exists(path))
            {
                var lines = File.ReadAllLines(path);
                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line) || !line.Contains("|")) continue;
                    var parts = line.Split('|');
                    if (parts.Length < 2) continue;

                    string refStr = parts[0].Trim();
                    string text = string.Join("|", parts[1..]).Trim();

                    if (Reference.TryParse(refStr, out var refObj) && refObj != null)
                    {
                        list.Add(new Scripture(refObj, text));
                    }
                }
            }

            return list;
        }
    }
}
