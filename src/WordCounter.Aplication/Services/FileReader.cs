using FluentValidation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WordCounter.Application.Interface;

namespace WordCounter.Application.Services
{
    public class FileReader : IReader
    {
        private readonly IDirectoryPathValidator _directoryPathValidator;

        public FileReader(IDirectoryPathValidator directoryPathValidator)
        {
            _directoryPathValidator = directoryPathValidator;
        }
        public List<string> ReadLines(string directoryPath)
        {
            _directoryPathValidator.ValidateAndThrow(directoryPath);

            var files = Directory.GetFiles(directoryPath, "*.txt");
            var lines = new List<string>();
            lines = files.SelectMany(file => File.ReadLines(file)).ToList();
            return lines;
        }
    }
}