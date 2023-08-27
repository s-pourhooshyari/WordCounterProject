using System.Collections.Generic;
using System.Threading.Tasks;

namespace WordCounter.Application.Interface
{
    public interface ITextAnalyzer
    {
        Task<Dictionary<string, int>> AnalyzeTextFilesInDirectoryAsync(string directoryPath);
    }
}
