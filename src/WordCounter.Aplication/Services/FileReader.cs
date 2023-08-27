using System.Collections.Generic;
using System.IO;
using System.Linq;
using WordCounter.Application.Interface;

namespace WordCounter.Application.Services
{
    public class FileReader : IFileReader
    {
        public List<string> ReadFileLines(string filePath)
        {
            return File.ReadLines(filePath).ToList();
        }
    }
}