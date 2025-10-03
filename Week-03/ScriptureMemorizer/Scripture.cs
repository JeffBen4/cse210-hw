using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ScriptureMemorizer
{
    public class Scripture
    {
        public Reference Reference { get; }
        private readonly List<Word> _words;
        private readonly Random _rng = new Random();

        public Scripture(Reference reference, string text)
        {
            Reference = reference;
            var tokens = Regex.Split(text.Trim(), @"\s+");
            _words = tokens.Select(t => new Word(t)).ToList();
        }

        public string Display()
        {
            string text = string.Join(" ", _words.Select(w => w.Display()));
            return $"{Reference}\n{text}";
        }

        public bool AllHidden() => _words.All(w => w.IsHidden || IsWordNonAlphabetic(w));

        public void HideRandomWords(int count)
        {
            var candidates = _words
                .Where(w => !w.IsHidden && !IsWordNonAlphabetic(w))
                .ToList();

            if (candidates.Count == 0) return;

            int toHide = Math.Min(count, candidates.Count);
            Shuffle(candidates);
            foreach (var w in candidates.Take(toHide))
            {
                w.Hide();
            }
        }

        private void Shuffle<T>(IList<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = _rng.Next(i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }

        private bool IsWordNonAlphabetic(Word w)
        {
            string shown = w.Display();
            return !Regex.IsMatch(shown, @"[A-Za-zÁÉÍÓÚáéíóúÑñ]");
        }
    }
}
