using System.Collections.Generic;

namespace WordCounter.Application.Interface
{
    public interface IWordsCounter
    {
        Dictionary<string, int> CountWords(IEnumerable<string> lines);
    }

}
