using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
 

namespace WordCounter.Infrastructure
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
