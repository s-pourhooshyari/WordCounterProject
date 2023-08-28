using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using WordCounter.Application.Interface;
using WordCounter.Application.Services;
using Xunit;
using System.Diagnostics;
using System.IO;


public class PerformanceTest
{
    [Fact]
    public void Method_ShouldExecuteWithinTimeRange()
    {
        var fullPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\")) + @"TestFiles\TextFile.txt"; 

        TimeSpan expectedExecutionTime = TimeSpan.FromMilliseconds(10000);
        var stopwatch = new Stopwatch();

        stopwatch.Start();
        var wordCounterMock = new Mock<IWordsCounter>();
        var readerMock = new Mock<IReader>();

        readerMock.Setup(fr => fr.ReadLines(fullPath));

        var textAnalyzer = new TextAnalyzer(wordCounterMock.Object, readerMock.Object);

        stopwatch.Stop();

        // Assert
        Assert.True(stopwatch.Elapsed <= expectedExecutionTime,
            $"Execution time exceeded. Expected: {expectedExecutionTime}, Actual: {stopwatch.Elapsed}");
    }
}

