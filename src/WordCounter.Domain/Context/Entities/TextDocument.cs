using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordCounter.Domain.Context.Entities
{
    public class TextDocument
    {
        public string FilePath { get; }
        public string Content { get; }

        public TextDocument(string filePath, string content)
        {
            FilePath = filePath;
            Content = content;
        }
    }
}
