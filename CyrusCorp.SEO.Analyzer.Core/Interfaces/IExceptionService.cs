using CyrusCorp.SEO.Analyzer.Core.Models;
using System;

namespace CyrusCorp.SEO.Analyzer.Core
{
    public interface IExceptionService
    {
        ExceptionItem HandleError(string identifier, Exception exception);
        void ThrowIfNull(object argumentValue, string argumentName);
      
    }
}