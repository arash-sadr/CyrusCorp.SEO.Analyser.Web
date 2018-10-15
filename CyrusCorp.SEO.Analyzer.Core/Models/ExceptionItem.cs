using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyrusCorp.SEO.Analyzer.Core.Models
{
    public class ExceptionItem
    {
        public string Source { get; set; }
        public string ErrorMethod { get; set; }
        public string Message { get; set; }
        public ExceptionItem InnerException { get; set; }

    }
}