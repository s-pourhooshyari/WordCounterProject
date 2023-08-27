using FluentValidation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WordCounter.Application.Interface;
 
namespace WordCounter.Application.Services
{
    public class TextAnalyzer : ITextAnalyzer
    {
        private readonly IWordsCounter _wordCounter;
        private readonly IFileReader _fileReader;
        private readonly IDirectoryPathValidator _directoryPathValidator;
        private readonly IMergeService<string, int> _dictionaryMerger;

        public TextAnalyzer(IWordsCounter wordCounter, IFileReader fileReader, IDirectoryPathValidator directoryPathValidator, IMergeService<string, int> dictionaryMerger)
        {
            _wordCounter = wordCounter;
            _fileReader = fileReader;
            _directoryPathValidator = directoryPathValidator;
            _dictionaryMerger = dictionaryMerger;
        }
        public async Task<Dictionary<string, int>> AnalyzeTextFilesInDirectoryAsync(string directoryPath)
        {
            try
            {
                _directoryPathValidator.ValidateAndThrow(directoryPath);

            var files = Directory.GetFiles(directoryPath, "*.txt");
            var tasks = files.Select(async filePath =>
            {
                try
                {
                    var fileInfo = new FileInfo(filePath);
                    List<string> lines = _fileReader.ReadFileLines(filePath);

                    var documentWordCounts = await _wordCounter.CountWordsAsync(lines);
                    return documentWordCounts;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error processing file '{filePath}': {ex.Message}");
                }
            });

            var wordCounts = (await Task.WhenAll(tasks))
                .Aggregate(new Dictionary<string, int>(), (target, source) => _dictionaryMerger.MergeDictionaries(target, source));

            return wordCounts;
            }
            catch (ValidationException ex)
            {
                var errorMessages = ex.Errors.Select(error => error.ErrorMessage);
                throw new ValidationException($"Validation failed: {string.Join(", ", errorMessages)}", ex.Errors);
            }
        }
    }
}
