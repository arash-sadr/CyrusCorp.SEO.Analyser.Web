using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CyrusCorp.SEO.Analyser.Web.Helpers
{
    public static class AnalyzeHelper
    {
        private const string URL_REGEX_PATTERN = @"^(http:\/\/www\.|https:\/\/www\.|http:\/\/|https:\/\/)?[a-z0-9]+([\-\.]{1}[a-z0-9]+)*\.[a-z]{2,5}(:[0-9]{1,5})?(\/.*)?$";

        public static bool IsURLText(string text)
        {
            return Regex.IsMatch(text, URL_REGEX_PATTERN);
        }
    }
}
