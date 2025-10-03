using System;

namespace ScriptureMemorizer
{
    public class Word
    {
        private readonly string _raw;
        private bool _hidden;

        public Word(string token)
        {
            _raw = token;
            _hidden = false;
        }

        public bool IsHidden => _hidden;

        public string Display()
        {
            if (!_hidden) return _raw;

            char[] chars = _raw.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (char.IsLetter(chars[i]))
                {
                    chars[i] = '_';
                }
            }
            return new string(chars);
        }

        public void Hide()
        {
            _hidden = true;
        }
    }
}
