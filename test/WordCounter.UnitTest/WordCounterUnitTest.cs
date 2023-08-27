
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using WordCounter.Application.Interface;
using WordCounter.Application.Services;
using Xunit;

namespace WordCounter.UnitTest
{
    public class WordCountUnitTest
    {
        private readonly WordsCounter _wordsCounter;
        private readonly Mock<IWordsCounter> _wordCounterMock;
        private readonly ITextAnalyzer _textAnalyzer;


        public WordCountUnitTest()
        {
            _wordsCounter = new WordsCounter();
            _wordCounterMock = new Mock<IWordsCounter>();
            _textAnalyzer = new TextAnalyzer(_wordCounterMock.Object);
        }

        [Fact]
        public void AnalyzeTextFilesInDirectory_EmptyDirectory_ThrowsArgumentException()
        {
            var emptyDirectoryPath = string.Empty;
 
        }

        [Fact]
        public void AnalyzeTextFilesInDirectory_DirectoryNotFound_ThrowsDirectoryNotFoundException()
        {
            var nonExistentDirectoryPath = @"path\to\non\existent\directory";

            Assert.Throws<DirectoryNotFoundException>(() => _textAnalyzer.AnalyzeTextFilesInDirectory(nonExistentDirectoryPath));
        }

        [Fact]
        public void AnalyzeTextFilesInDirectory_ValidDirectory_CallsCountWordsForAllFiles()
        {
            var validDirectoryPath = @"E:\tempFile\Files";  

            _wordCounterMock.Setup(x => x.CountWords(It.IsAny<List<string>>()))
                            .Returns(new Dictionary<string, int>
                            {
                                { "word1", 3 },
                                { "word2", 5 }
                            });

            var result = _textAnalyzer.AnalyzeTextFilesInDirectory(validDirectoryPath);
 
        }
    }
}
