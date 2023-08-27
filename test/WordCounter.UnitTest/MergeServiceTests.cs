using System.Collections.Generic;
using WordCounter.Application.Services;
using Xunit;

namespace WordCounter.Tests
{
    public class MergeServiceTests
    {
        [Fact]
        public void MergeDictionaries_Should_Merge_Dictionaries_Correctly()
        {
            var mergeService = new MergeService<string, int>();
            var targetDictionary = new Dictionary<string, int>
            {
                ["a"] = 1,
                ["b"] = 2
            };
            var sourceDictionary = new Dictionary<string, int>
            {
                ["b"] = 3,
                ["c"] = 4
            };

            var mergedDictionary = mergeService.MergeDictionaries(targetDictionary, sourceDictionary);

            Assert.NotNull(mergedDictionary);
            Assert.Equal(3, mergedDictionary.Count);
            Assert.Equal(1, mergedDictionary["a"]);
            Assert.Equal(5, mergedDictionary["b"]);
            Assert.Equal(4, mergedDictionary["c"]);
        }
    }
}
