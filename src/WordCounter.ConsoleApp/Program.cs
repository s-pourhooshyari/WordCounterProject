using System;
using WordCounter.Application.Services;
using WordCounter.UI.ConsoleUI;
 

namespace WordCounter.ConsoleApp
{
    class Program
    {
        public static void Main(string[] args)
        {
            var wordCounter = new WordsCounter();
            var textAnalysisService = new TextAnalyzer(wordCounter);
            var consoleUI = new ConsoleUI(textAnalysisService);

            consoleUI.Run();
        }
    }
}
