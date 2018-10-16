using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.IO;
using Newtonsoft.Json;

namespace CyrusCorp.SEO.Analyzer.Core
{
    public class AnalyzeService : IAnalyzeService
    {
        const string WORD_REGEX_PATTERN = @"\w+";
        const string STOPWORDS_PATH = "Assets/stopWords.json";
        IExceptionService _exceptionService;
        List<string> _stopWords = new List<string>();

        public AnalyzeService(IExceptionService exceptionService)
        {
            _exceptionService = exceptionService;
        }

        public AnalyzeService() : this(new ExceptionService())
        {

        }

        public List<string> StopWords
        {
            get
            {
                if (_stopWords.Count <= 0)
                    _stopWords =  GetStopWords(STOPWORDS_PATH); 
                return _stopWords;
            }
        }


        public bool IsURLText(string text)
        {
            return true;
        }

        public Dictionary<string, int> GetWordOccurrencesFromText(string text)
        {
            _exceptionService.ThrowIfNull(text, "url");

            text = GetTextFromHTML(text);

            var result = new Dictionary<string, int>();
            var matches = Regex.Matches(text, WORD_REGEX_PATTERN);
            foreach (Match word in matches)
            {
                var key = word.Value.ToLower();
                if (!StopWords.Contains(key, StringComparer.OrdinalIgnoreCase))
                    if (result.ContainsKey(key))
                        result[key]++;
                    else
                        result.Add(key, 1);
            }
            return result;
        }
        public Dictionary<string, int> GetWordOccurrencesFromUrl(string url)
        {
            _exceptionService.ThrowIfNull(url, "url");

            return GetWordOccurrencesFromText(GetHTMLFromURL(url));
        }

        public string GetTextFromHTML(string html)
        {
            _exceptionService.ThrowIfNull(html, "url");


            var htmlDoc = new HtmlDocument();

            htmlDoc.LoadHtml(html);
            var nodes = htmlDoc.DocumentNode.SelectNodes("//text()[not(parent::script or parent::style)]");
            if (nodes == null)
                return null;

            return HttpUtility.HtmlDecode(string.Join(" ", nodes.Select(x => x.InnerText)));


            // TODO   IMPROVENT of the web crawler:
            
            //try
            //{
            //    using (WebClient client = new WebClient())
            //    {
            //        text = client.DownloadString(url);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    var exceptionItem = _exceptionService.HandleError("RegisterSearchText", ex);


            //    //return NotFound(exceptionItem);
            //}
            //return text;
        }

        public Dictionary<string, int> GetMetaTagWordsFromText(string text)
        {
            _exceptionService.ThrowIfNull(text, "text");

            var result = new Dictionary<string, int>();
            var keywords = GetMetaKeyWordsFromHtml(text).Select(s => s.ToLower())
                .Distinct().ToList();

            if (keywords.Count() == 0)
                return result;

            text = GetTextFromHTML(text).ToLower();

            var pattern = string.Join("|", keywords.Select(x => $"(\\b{x}\\b)"));
            var matches = Regex.Matches(text, pattern);

            foreach (var keyword in keywords)
                result.Add(keyword, 0);

            foreach (Match keyword in matches)
            {
                var key = keyword.Value;
                if (result.ContainsKey(key))
                    result[key]++;
                else
                    result.Add(key, 1);
            }

            return result;
        }
        public Dictionary<string, int> GetMetaTagWordsFromUrl(string url)
        {
            _exceptionService.ThrowIfNull(url, "url");

            return GetMetaTagWordsFromText(GetHTMLFromURL(url));
        }

        public List<string> GetWordsFromText(string checkText)
        {
            _exceptionService.ThrowIfNull(checkText, "checkText");

            var words = SplitSentenceIntoWords(checkText.ToLower(), 1);

            List<string> modifiedWords = new List<string>();

            foreach (var word in words)
            {
                var stripedWords = word;

                if (!string.IsNullOrWhiteSpace(stripedWords) &&
                    Regex.IsMatch(stripedWords, "^[a-z\u00c0-\u00f6]+$", RegexOptions.IgnoreCase) &&
                    stripedWords.Length > 1)
                {
                    modifiedWords.Add(stripedWords);
                }

            }
            return modifiedWords.ToList<string>();
        }

        public List<string> GetExternalLinksFromText(string text)
        {
            _exceptionService.ThrowIfNull(text, "text");

            return GetATagUrlsFromHtml(text).Where(x => IsAbsoluteUrl(x)).ToList();
        }

       

        public List<string> GetExternalLinksFromUrl(string url)
        {
            _exceptionService.ThrowIfNull(url, "url");

            return GetExternalLinksFromText(GetHTMLFromURL(url));
        }

       

        #region Private Methods
        private static IEnumerable<string> SplitSentenceIntoWords(string sentence, int wordMinLength)
        {
            var word = new StringBuilder();
            foreach (var chr in sentence)
            {
                if (Char.IsPunctuation(chr) || Char.IsSeparator(chr) || Char.IsWhiteSpace(chr))
                {
                    if (word.Length > wordMinLength)
                    {
                        yield return word.ToString();
                        word.Clear();
                    }
                }
                else
                {
                    word.Append(chr);
                }
            }

            if (word.Length > wordMinLength)
            {
                yield return word.ToString();
            }
        }

        private string GetHTMLFromURL(string url)
        {
            _exceptionService.ThrowIfNull(url, "url");

            return new HtmlWeb().Load(url).Text;
        }

        private static List<string> GetStopWords(string stopWordsPath)
        {
            var words = new List<string>();
            try
            {
                var jsonText = File.ReadAllText(stopWordsPath);
                return JsonConvert.DeserializeObject<IList<string>>(jsonText).ToList();
            }
            catch (Exception)
            {
            }
            return words;
        }

        private List<string> GetMetaKeyWordsFromHtml(string html)
        {
            _exceptionService.ThrowIfNull(html, "html");

            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var nodes = doc.DocumentNode.SelectNodes("//meta[translate(@name,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')='keywords' and @content]");
            if (nodes == null)
                return new List<string>();

            var result = nodes.Select(n => n.Attributes["content"].Value)
                              .Where(s => !string.IsNullOrWhiteSpace(s))
                              .SelectMany(s => s.Split(','))
                              .Select(s => HttpUtility.HtmlDecode(s.Trim()));

            return result.Distinct().ToList();
        }

        private List<string> GetATagUrlsFromHtml(string html)
        {
            _exceptionService.ThrowIfNull(html, "html");

            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var nodes = doc.DocumentNode.SelectNodes("//a[@href]");
            if (nodes == null)
                return new List<string>();

            var result = nodes.Select(n => n.Attributes["href"].Value)
                  .Where(s => !string.IsNullOrWhiteSpace(s))
                  .Select(x => x.ToLower());

            return result.Distinct().ToList();
        }

        private bool IsAbsoluteUrl(string input)
        {
            Uri result;
            if (Uri.TryCreate(input, UriKind.RelativeOrAbsolute, out result))
                return result.IsAbsoluteUri;
            return false;
        }

        #endregion
    }
}

