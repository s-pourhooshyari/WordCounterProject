using System.Collections.Generic;

namespace WordCounter.Application.Interface
{
    public interface IReader
    {
        List<string> ReadLines(string filePath);
    }
}