using System.Collections.Generic;

namespace CyrusCorp.SEO.Analyzer.Core
{
    public interface IAnalyzeService
    {

        Dictionary<string, int> GetWordOccurancesFromText(string text);

        Dictionary<string, int> GetWordOccurancesFromUrl(string url);

        Dictionary<string, int> GetMetaTagWordsFromText(string text);

        Dictionary<string, int> GetMetaTagWordsFromUrl(string url);

        List<string> GetExternalLinksFromText(string text);

        List<string> GetExternalLinksFromUrl(string url);
    }
}