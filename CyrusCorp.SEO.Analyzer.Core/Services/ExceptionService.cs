using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CyrusCorp.SEO.Analyzer.Core.Models;

namespace CyrusCorp.SEO.Analyzer.Core
{
    public class ExceptionService : IExceptionService
    {
        public ExceptionItem HandleError(string identifier, Exception ex)
        {
            var exceptionItem = new ExceptionItem();

            try
            {
                HandleErrorManagement(identifier, exceptionItem, ex);
                // TODO : LOG PROVIDER TO FILE WRITE THE LOG
            }
            catch (Exception)
            {
            }

            return exceptionItem;
        }


        private void HandleErrorManagement(string identifier, ExceptionItem exceptionItem, Exception ex)
        {
            exceptionItem.Source = ex.Source;
            exceptionItem.ErrorMethod = identifier;
            exceptionItem.Message = ex.Message;

            if (ex.InnerException != null)
            {
                HandleErrorManagement("inner_" + identifier, exceptionItem, ex.InnerException);
            }
        }

        public void ThrowIfNull(object argumentValue, string argumentName)
        {
            if (argumentValue == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

    }
}
