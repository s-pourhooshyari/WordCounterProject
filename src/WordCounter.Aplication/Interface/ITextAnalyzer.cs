using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordCounter.Domain.Context.Entities;

namespace WordCounter.Application.Interface
{
    public interface ITextAnalyzer
    {
        Dictionary<string, int> AnalyzeTextFilesInDirectory(string directoryPath);
    }
}
