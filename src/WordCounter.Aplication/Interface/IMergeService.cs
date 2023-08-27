using System.Collections.Generic;

namespace WordCounter.Application.Interface
{
    public interface IMergeService<TKey, TValue>
    {
        Dictionary<TKey, TValue> MergeDictionaries(Dictionary<TKey, TValue> target, Dictionary<TKey, TValue> source);
    }
}
