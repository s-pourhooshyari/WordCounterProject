using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordCounter.Application.Interface;

namespace WordCounter.Application.Services
{
    public class TextAnalyzer<Tin, Tout> : ITextAnalyzer<string, Dictionary<string, int>>
    {
        private readonly IWordsCounter _wordCounter;
        private readonly IFileReader _fileReader;
        private readonly IDirectoryPathValidator _directoryPathValidator;

        public TextAnalyzer(IWordsCounter wordCounter, IFileReader fileReader, IDirectoryPathValidator directoryPathValidator)
        {
            _wordCounter = wordCounter;
            _fileReader = fileReader;
            _directoryPathValidator = directoryPathValidator;
        }
        public async Task<Dictionary<string, int>> Analyze(string directoryPath)
        {
            try
            {
                _directoryPathValidator.ValidateAndThrow(directoryPath);

                List<string> lines = _fileReader.ReadFileLines(directoryPath);
                Dictionary<string, int> documentWordCounts = new Dictionary<string, int>();
                Task tasks = Task.Run(async () =>
                {
                    documentWordCounts = await _wordCounter.CountWordsAsync(lines);
                });
                tasks.Wait();
                return documentWordCounts;
            }
            catch (ValidationException ex)
            {
                var errorMessages = ex.Errors.Select(error => error.ErrorMessage);
                throw new ValidationException($"Validation failed: {string.Join(", ", errorMessages)}", ex.Errors);
            }
        }
    }
}
