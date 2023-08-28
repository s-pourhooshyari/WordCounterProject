using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WordCounter.Application.Interface;

namespace WordCounter.UI.ConsoleUI
{
    public class ConsoleUI
    {
        private readonly ITextAnalyzer<string, Dictionary<string, int>> _textAnalyzer;

        public ConsoleUI(ITextAnalyzer<string, Dictionary<string, int>> textAnalyzer)
        {
            _textAnalyzer = textAnalyzer;
        }

        public async void Run()
        {
            Console.WriteLine("Welcome to the Word Counting Application!");
            Console.Write("Enter the directory path: ");
            string directoryPath = Console.ReadLine();
            try
            {
                IServiceSetup serviceSetup = new ServiceSetup();
                IServiceProvider serviceProvider = serviceSetup.SetupServices();

                var textAnalyzer = serviceProvider.GetRequiredService<ITextAnalyzer<string, Dictionary<string, int>>>();

                var wordCounts = await textAnalyzer.Analyze(directoryPath);
                Console.WriteLine("Word Counts:");
                foreach (var kvp in wordCounts)
                {
                    Console.WriteLine($"{kvp.Key}: {kvp.Value}");
                }
            }
            catch (ValidationException ex)
            {
                Console.WriteLine("Validation errors occurred:");
                foreach (var error in ex.Message)
                {
                    Console.WriteLine(error);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}
