using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using WordCounter.Application.Interface;

namespace WordCounter.Application.Services
{
    public class WordsCounter : IWordsCounter
    {
        private readonly Regex _wordRegex = new Regex(@"\b\w+\b");

        public Dictionary<string, int> CountWords(IEnumerable<string> lines)
        {
            var wordCount = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            foreach (var line in lines)
            {
                MatchCollection matches = _wordRegex.Matches(line);
                foreach (Match match in matches)
                {
                    string word = match.Value;
                    if (wordCount.ContainsKey(word))
                    {
                        wordCount[word]++;
                    }
                    else
                    {
                        wordCount[word] = 1;
                    }
                }
            }

            return wordCount;
        }
    }
}
