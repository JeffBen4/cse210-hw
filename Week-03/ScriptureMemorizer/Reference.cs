using System;
using System.Text.RegularExpressions;

namespace ScriptureMemorizer
{
    public class Reference
    {
        public string Book { get; }
        public int Chapter { get; }
        public int VerseStart { get; }
        public int? VerseEnd { get; }

        // Single verse
        public Reference(string book, int chapter, int verse)
        {
            Book = book;
            Chapter = chapter;
            VerseStart = verse;
            VerseEnd = null;
        }

        // Verse range
        public Reference(string book, int chapter, int verseStart, int verseEnd)
        {
            Book = book;
            Chapter = chapter;
            VerseStart = verseStart;
            VerseEnd = verseEnd;
        }

        public override string ToString()
        {
            return VerseEnd.HasValue
                ? $"{Book} {Chapter}:{VerseStart}-{VerseEnd.Value}"
                : $"{Book} {Chapter}:{VerseStart}";
        }

        // Parse "John 3:16" or "Proverbs 3:5-6"
        public static bool TryParse(string input, out Reference? reference)
        {
            reference = null;
            var match = Regex.Match(input.Trim(),
                @"^(?<book>.+?)\s+(?<chap>\d+):(?<v1>\d+)(-(?<v2>\d+))?$");
            if (!match.Success) return false;

            string book = match.Groups["book"].Value.Trim();
            int chap = int.Parse(match.Groups["chap"].Value);
            int v1 = int.Parse(match.Groups["v1"].Value);

            if (match.Groups["v2"].Success)
            {
                int v2 = int.Parse(match.Groups["v2"].Value);
                reference = new Reference(book, chap, v1, v2);
            }
            else
            {
                reference = new Reference(book, chap, v1);
            }
            return true;
        }
    }
}
