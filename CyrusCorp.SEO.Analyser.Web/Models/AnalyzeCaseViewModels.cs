using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CyrusCorp.SEO.Analyser.Web.Models
{

    public class AnalyzeCaseViewModel
    {
        [AllowHtml]
        [Required(AllowEmptyStrings = false, ErrorMessage = "You can't leave this empty.")]
        public string Text { get; set; }

        [Display(Name = "I'm analyzing a URL")]
        public bool IsURL { get; set; }

        [Display(Name = "Words | Number of Occurence")]
        public bool IsCheckingNumberOfWords { get; set; }

        [Display(Name = "Meta Tags | Number of Occurence")]
        public bool IsListingMetaTags { get; set; }

        [Display(Name = "Links | Number of Occurence")]
        public bool IsListingExternalLinks { get; set; }

        [Display(Name = "Stop-worlds | Number of Occurence")]
        public bool IsFilteringStopWords { get; set; }

    }

    public class AnalysisResultModel
    {
        public Dictionary<string, int> OccuredWords { get; set; }

        public Dictionary<string, int> MetaTagWords { get; set; }

        public List<string> ExternalLinksList { get; set; }
    }
}
