using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CyrusCorp.SEO.Analyser.Web.Helpers
{
    public static class AnalyzeHelper
    {
        private const string URL_REGEX_PATTERN = @"https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)";

        public static bool IsURLText(string text)
        {
            return Regex.IsMatch(text, URL_REGEX_PATTERN);
        }
    }
}
