using System.Collections.Generic;

namespace CyrusCorp.SEO.Analyzer.Core
{
    public interface IAnalyzeService
    {

        Dictionary<string, int> GetWordOccurrencesFromText(string text);

        Dictionary<string, int> GetWordOccurrencesFromUrl(string url);

        Dictionary<string, int> GetMetaTagWordsFromText(string text);

        Dictionary<string, int> GetMetaTagWordsFromUrl(string url);

        List<string> GetExternalLinksFromText(string text);

        List<string> GetExternalLinksFromUrl(string url);
    }
}