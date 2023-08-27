using System.Collections.Generic;
using System.Threading.Tasks;

namespace WordCounter.Application.Interface
{
    public interface IWordsCounter
    {
        Task<Dictionary<string, int>> CountWordsAsync(IEnumerable<string> lines);
    }
}
