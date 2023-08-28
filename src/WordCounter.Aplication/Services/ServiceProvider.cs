using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using WordCounter.Application.Interface;
using WordCounter.Application.Services;

public class ServiceSetup : IServiceSetup
{
    public IServiceProvider SetupServices()
    {
        var serviceProvider = new ServiceCollection()
            .AddSingleton<IWordsCounter, WordsCounter>()
            .AddSingleton<IFileReader, FileReader>()
            .AddSingleton<IDirectoryPathValidator, DirectoryPathValidator>()
            .AddSingleton<ITextAnalyzer<string, Dictionary<string, int>>, TextAnalyzer<string, Dictionary<string, int>>>()
            .BuildServiceProvider();

        return serviceProvider;
    }
}

