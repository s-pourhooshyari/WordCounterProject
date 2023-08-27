using System.Collections.Generic;

namespace WordCounter.Application.Interface
{
    public interface ITextAnalyzer
    {
        Dictionary<string, int> AnalyzeTextFilesInDirectory(string directoryPath);
    }
}
