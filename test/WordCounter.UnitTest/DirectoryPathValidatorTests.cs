using FluentValidation;
using WordCounter.Application.Services;
using Xunit;

namespace WordCounter.Tests
{
    public class DirectoryPathValidatorTests
    {
        [Theory]
        [InlineData("")]
        [InlineData("test")]
        public void Validate_Should_Have_Error_When_DirectoryPath_Is_Empty_Or_Null(string directoryPath)
        {
            var validator = new DirectoryPathValidator();

            var result = validator.Validate(new ValidationContext<string>(directoryPath));

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, error => error.ErrorMessage == "Directory path must not be empty."
            || error.ErrorMessage == "Directory 'test' does not exist.");
        }

        [Fact]
        public void Validate_Should_Not_Have_Error_When_DirectoryPath_Is_Valid()
        {
            var validator = new DirectoryPathValidator();

            var result = validator.Validate(new ValidationContext<string>(@"E:\FileProcessing\Files"));

            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }
    }
}
