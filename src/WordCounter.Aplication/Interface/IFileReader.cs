using System.Collections.Generic;

namespace WordCounter.Application.Interface
{
    public interface IFileReader
    {
        List<string> ReadFileLines(string filePath);
    }
}