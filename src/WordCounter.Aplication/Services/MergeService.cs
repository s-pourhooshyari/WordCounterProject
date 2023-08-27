using System.Collections.Generic;
using WordCounter.Application.Interface;

namespace WordCounter.Application.Services
{
    public class MergeService<Key, Value> : IMergeService<string, int>
    {
        public Dictionary<string, int> MergeDictionaries(Dictionary<string, int> target, Dictionary<string, int> source)
        {
            foreach (var kvp in source)
            {
                if (target.ContainsKey(kvp.Key))
                {
                    target[kvp.Key] += kvp.Value;
                }
                else
                {
                    target[kvp.Key] = kvp.Value;
                }
            }
            return target;
        }
    }
}
