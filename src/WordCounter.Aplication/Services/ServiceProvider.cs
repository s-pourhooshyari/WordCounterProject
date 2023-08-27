using Microsoft.Extensions.DependencyInjection;
using System;
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
            .AddSingleton<IMergeService<string, int>, MergeService<string, int>>()
            .AddSingleton<ITextAnalyzer, TextAnalyzer>()
            .BuildServiceProvider();

        return serviceProvider;
    }
}

