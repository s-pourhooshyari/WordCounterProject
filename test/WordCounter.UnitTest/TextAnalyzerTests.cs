using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using WordCounter.Application.Interface;
using WordCounter.Application.Services;
using Xunit;

namespace WordCounter.Tests
{
    public class TextAnalyzerTests
    {
        [Fact]
        public async Task Analyze_ValidDirectoryPath_ReturnsWordCounts()
        {
            var wordCounterMock = new Mock<IWordsCounter>();
            var readerMock = new Mock<IReader>();

            string directoryPath = "valid/directory/path";
            var lines = new List<string> { "word1 word2", "word3 word4 word1" };
            var wordCounts = new Dictionary<string, int>
            {
                { "word1", 2 },
                { "word2", 1 },
                { "word3", 1 },
                { "word4", 1 }
            };

            readerMock.Setup(fr => fr.ReadLines(directoryPath)).Returns(lines);
            wordCounterMock.Setup(wc => wc.CountWordsAsync(lines)).ReturnsAsync(wordCounts);

            var textAnalyzer = new TextAnalyzer(wordCounterMock.Object, readerMock.Object);

            var result = await textAnalyzer.Analyze(directoryPath);

            wordCounterMock.Verify();
            readerMock.Verify();
            Assert.Equal(wordCounts, result);
        }
        [Fact]
        public async Task Analyze_ValidationFails_ThrowsValidationException()
        {
            var wordCounterMock = new Mock<IWordsCounter>();
            var readerMock = new Mock<IReader>();

            string directoryPath = "invalid/directory/path";
            var validationFailure = new ValidationFailure("directoryPath", "Invalid path.");
            var validationException = new ValidationException(new[] { validationFailure });

            readerMock.Setup(fr => fr.ReadLines(directoryPath)).Throws(validationException);

            var textAnalyzer = new TextAnalyzer(wordCounterMock.Object, readerMock.Object);
 
            await Assert.ThrowsAsync<ValidationException>(async () => await textAnalyzer.Analyze(directoryPath));
        }
    }
}
