using FluentValidation;
using System;
using System.IO;
using System.Linq;
using WordCounter.Application.Interface;

namespace WordCounter.Application.Services
{
    public class DirectoryPathValidator : AbstractValidator<string>, IDirectoryPathValidator
    {
        public DirectoryPathValidator()
        {
            RuleFor(directoryPath => directoryPath).NotEmpty()
               .WithMessage("Directory path must not be empty.")
               .Custom((directoryPath, context) =>
               {
                   try
                   {
                       if (!Directory.Exists(directoryPath))
                       {
                           context.AddFailure($"Directory '{directoryPath}' does not exist.");
                       }
                       else if (!Directory.GetFiles(directoryPath, "*.txt").Any())
                       {
                           context.AddFailure("No text files found in the directory.");
                       }
                   }
                   catch (Exception ex)
                   {
                       context.AddFailure($"Error validating directory: {ex.Message}");
                   }
               });
        }
    }
}
