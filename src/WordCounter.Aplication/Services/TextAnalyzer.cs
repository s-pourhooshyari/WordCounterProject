using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordCounter.Application.Interface;

namespace WordCounter.Application.Services
{
    public class TextAnalyzer  : ITextAnalyzer<string, Dictionary<string, int>>
    {
        private readonly IWordsCounter _wordCounter;
        private readonly IReader _reader;

        public TextAnalyzer(IWordsCounter wordCounter, IReader reader)
        {
            _wordCounter = wordCounter;
            _reader = reader;
        }
        public async Task<Dictionary<string, int>> Analyze(string directoryPath)
        {
            try
            {
                List<string> lines = _reader.ReadLines(directoryPath);
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
