using System;
using System.Collections.Generic;
using WordCounter.Application.Interface;
using WordCounter.Application.Services;

namespace WordCounter.UI.ConsoleUI
{
    public class ConsoleUI
    {
        private readonly ITextAnalyzer _textAnalyzer;

        public ConsoleUI(ITextAnalyzer textAnalyzer)
        {
            _textAnalyzer = textAnalyzer;
        }

        public void Run()
        {
            Console.WriteLine("Welcome to the Word Counting Application!");
            Console.Write("Enter the directory path: ");
            string directoryPath = Console.ReadLine();

            IWordsCounter wordCounter = new WordsCounter();
            ITextAnalyzer textAnalyzer = new TextAnalyzer(wordCounter);

            Dictionary<string, int> wordCounts = textAnalyzer.AnalyzeTextFilesInDirectory(directoryPath);

            Console.WriteLine("Word Counts:");
            foreach (var kvp in wordCounts)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }

        }
    }
}
