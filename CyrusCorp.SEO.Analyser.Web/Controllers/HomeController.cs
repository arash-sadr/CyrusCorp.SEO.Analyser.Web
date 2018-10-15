
using CyrusCorp.SEO.Analyser.Web.Models;
using CyrusCorp.SEO.Analyser.Web.Services;
using CyrusCorp.SEO.Analyser.Web.Helpers;
using CyrusCorp.SEO.Analyzer.Core;
using System.Web.Mvc;

namespace CyrusCorp.SEO.Analyser.Web.Controllers
{
    public class HomeController : Controller
    {
        private IAnalyzeService _analyzeService;

        public HomeController(): this(new AnalyzeService(new ExceptionService()))
        {

        }

        public HomeController(IAnalyzeService analyzeService)
        {            
            _analyzeService = analyzeService;            
        }

        //
        // GET: /Account/Login
        [HttpGet]
        public ActionResult Analyze(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        public ActionResult Analyze(AnalyzeCaseViewModel model)
        {

            AnalysisResultModel resultModel = new AnalysisResultModel();
            if (ModelState.IsValid)
            {

                
                if (model.IsURL)
                {
                    if (!AnalyzeHelper.IsURLText(model.Text))
                    {
                        ModelState.AddModelError("Text", "Please type the URL in correct patten: http://www.example.com");
                        return View(model);
                    }
                    
                    if (model.IsCheckingNumberOfWords) resultModel.OccuredWords = _analyzeService.GetWordOccurancesFromUrl(model.Text);
                    if (model.IsListingMetaTags) resultModel.MetaTagWords = _analyzeService.GetMetaTagWordsFromUrl(model.Text);
                    if (model.IsListingExternalLinks) resultModel.ExternalLinksList = _analyzeService.GetExternalLinksFromText (model.Text);
                   
                }
                else
                {
                    if (model.IsCheckingNumberOfWords) resultModel.OccuredWords = _analyzeService.GetWordOccurancesFromText(model.Text);
                    if (model.IsListingMetaTags) resultModel.MetaTagWords = _analyzeService.GetMetaTagWordsFromText(model.Text);
                    if (model.IsListingExternalLinks) resultModel.ExternalLinksList = _analyzeService.GetExternalLinksFromText(model.Text);

                }
            }

            return PartialView("AnalysisSummary" , resultModel);
        }

      

    }
}
