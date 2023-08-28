using System.Threading.Tasks;

namespace WordCounter.Application.Interface
{
    public interface ITextAnalyzer<Tin, Tout>
    {
        Task<Tout> Analyze(Tin directoryPath);
    }
}
