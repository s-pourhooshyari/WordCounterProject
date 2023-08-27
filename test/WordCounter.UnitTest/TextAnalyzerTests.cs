using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using WordCounter.Application.Interface;
using WordCounter.Application.Services;
using Xunit;

namespace WordCounter.Tests
{
    public class TextAnalyzerTests
    {
        [Fact]
        public async Task AnalyzeTextFilesInDirectoryAsync_Should_Throw_ValidationException_On_Invalid_Directory()
        {
            var directoryPath = "test";
            var directoryPathValidatorMock = new Mock<IDirectoryPathValidator>();

            var services = new ServiceCollection();
            services.AddTransient<IDirectoryPathValidator, DirectoryPathValidator>();
            var serviceProvider = services.BuildServiceProvider();

            var directoryPathValidator = serviceProvider.GetRequiredService<IDirectoryPathValidator>();

            var textAnalyzer = new TextAnalyzer(
                Mock.Of<IWordsCounter>(),
                Mock.Of<IFileReader>(),
                directoryPathValidator,
                Mock.Of<IMergeService<string, int>>()
            );

            await Assert.ThrowsAsync<ValidationException>(async () => await textAnalyzer.AnalyzeTextFilesInDirectoryAsync(directoryPath));
        }
    }
}
