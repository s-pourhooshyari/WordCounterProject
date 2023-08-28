using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using WordCounter.Application.Interface;
using WordCounter.UI.ConsoleUI;

namespace WordCounter.ConsoleApp
{
    class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                IServiceSetup serviceSetup = new ServiceSetup();
                IServiceProvider serviceProvider = serviceSetup.SetupServices();

                var textAnalyzer = serviceProvider.GetRequiredService<ITextAnalyzer<string, Dictionary<string, int>>>();

                var consoleUI = new ConsoleUI(textAnalyzer);

                consoleUI.Run();

            }
            catch (ValidationException ex)
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
