using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WordCounter.Application.Interface;

namespace WordCounter.Application.Services
{
    public class FileReader : IFileReader
    {
        public List<string> ReadFileLines(string directoryPath)
        {
            var files = Directory.GetFiles(directoryPath, "*.txt");
            List<string> lines = new List<string>();
            lines = files.SelectMany(file => File.ReadLines(file)).ToList();
            return lines;
        }
    }
}