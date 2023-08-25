using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WordCounter.Application.Interface;

namespace WordCounter.Application.Services
{
    public class TextAnalyzer : ITextAnalyzer
    {
        private readonly IWordsCounter _wordCounter;

        public TextAnalyzer(IWordsCounter wordCounter)
        {
            _wordCounter = wordCounter;
        }

        public Dictionary<string, int> AnalyzeTextFilesInDirectory(string directoryPath)
        {
            var wordCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            try
            {

                if (string.IsNullOrWhiteSpace(directoryPath))
                {
                    throw new ArgumentException("Directory path is empty or null.", nameof(directoryPath));
                }

                if (!Directory.Exists(directoryPath))
                {
                    throw new DirectoryNotFoundException($"Directory '{directoryPath}' not found.");
                }

                List<string> files = Directory.GetFiles(directoryPath, "*.txt").ToList();

                if (files.Count == 0)
                {
                    throw new InvalidOperationException("No text files found in the directory.");
                }

              

                foreach (string filePath in files)
                {
                    try
                    {
                        FileInfo fileInfo = new FileInfo(filePath);
                        if (fileInfo.Length <= 1024 * 1024) // Adjust the threshold as needed
                        {
                            List<string> lines = File.ReadLines(filePath).ToList();
                            var documentWordCounts = _wordCounter.CountWords(lines);
                            MergeWordCounts(wordCounts, documentWordCounts);
                        }
                        else
                        {
                            string content = File.ReadAllText(filePath);
                            List<string> lines = content.Split('\n').ToList();
                            var documentWordCounts = _wordCounter.CountWords(lines);
                            MergeWordCounts(wordCounts, documentWordCounts);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                    }
                }
                return wordCounts;

            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                Environment.Exit(1);
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                Environment.Exit(1);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                Environment.Exit(1);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                Environment.Exit(1);
            }

            return wordCounts;
        }

        private void MergeWordCounts(Dictionary<string, int> target, Dictionary<string, int> source)
        {
            foreach (var kvp in source)
            {
                if (target.ContainsKey(kvp.Key))
                {
                    target[kvp.Key] += kvp.Value;
                }
                else
                {
                    target[kvp.Key] = kvp.Value;
                }
            }
        }
    }

}
