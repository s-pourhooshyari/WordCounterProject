 
using Moq;
using System;
using System.Collections.Generic;
using WordCounter.Application.Interface;
using WordCounter.Application.Services;
using Xunit;

namespace WordCounter.UnitTest
{
    public class WordCountUnitTest
    {
        private readonly WordsCounter _wordsCounter;

        public WordCountUnitTest()
        {
            _wordsCounter = new WordsCounter();
        }

        [Fact]
        public void AnalyzeTextFilesInDirectory_SmallFiles_ReturnsWordCounts()
        {
            // Arrange
            var wordCounterMock = new Mock<IWordsCounter>();
            wordCounterMock.Setup(wc => wc.CountWords(It.IsAny<IEnumerable<string>>()))
                           .Returns(new Dictionary<string, int>
                           {
                           {"word1", 2},
                           {"word2", 3}
                           });

            var textAnalyzer = new TextAnalyzer(wordCounterMock.Object);

            // Act
            Dictionary<string, int> result = textAnalyzer.AnalyzeTextFilesInDirectory("testDirectory");

            // Assert
            Assert.Equal(2, result.Count);
            Assert.True(result.ContainsKey("word1"));
            Assert.True(result.ContainsKey("word2"));
            Assert.Equal(2, result["word1"]);
            Assert.Equal(3, result["word2"]);
        }

    }
}
