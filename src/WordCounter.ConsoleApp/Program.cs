using System;
using System.IO;
using WordCounter.Application.Services;
using WordCounter.UI.ConsoleUI;


namespace WordCounter.ConsoleApp
{
    class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var wordCounter = new WordsCounter();
                var textAnalysisService = new TextAnalyzer(wordCounter);
                var consoleUI = new ConsoleUI(textAnalysisService);

                consoleUI.Run();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Error: " + ex.Message);

            }

            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine("Error: " + ex.Message);

            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Error: " + ex.Message);

            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);

            }


        }
    }
}
