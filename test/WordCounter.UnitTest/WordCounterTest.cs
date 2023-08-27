using System.Collections.Generic;
using System.Threading.Tasks;
using WordCounter.Application.Services;
using Xunit;

namespace WordCounter.Tests
{
    public class WordsCounterTests
    {
        [Fact]
        public async Task CountWordsAsync_Should_Count_Words_Correctly()
        {
            var lines = new List<string> { "This is a test.", "Another test sentence." };
            var wordsCounter = new WordsCounter();

            var wordCounts = await wordsCounter.CountWordsAsync(lines);

            Assert.NotNull(wordCounts);
            Assert.Equal(6, wordCounts.Count);
            Assert.Equal(2, wordCounts["test"]);
            Assert.Equal(1, wordCounts["this"]);
            Assert.Equal(1, wordCounts["is"]);
            Assert.Equal(1, wordCounts["a"]);
            Assert.Equal(1, wordCounts["another"]);
        }
    }
}
