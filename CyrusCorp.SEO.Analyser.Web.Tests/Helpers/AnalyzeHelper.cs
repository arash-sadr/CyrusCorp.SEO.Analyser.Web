using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CyrusCorp.SEO.Analyser.Web.Helpers;
using System.Collections.Generic;

namespace CyrusCorp.SEO.Analyser.Web.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void IsURLText_Should_Return_False_When_URLPattern_False()
        {
            var dic = new Dictionary<string, bool>() {
                { "this", false },
                { "http://www.google.com", true },
                { "/page.aspx", false },
                { "//page.aspx", false },
                { "/httf/something", false },
                { "httf://something.cover.com", false },
               
            };
            foreach (var item in dic)
            {
                var actual = AnalyzeHelper.IsURLText(item.Key);
                var expected = item.Value;
                Assert.AreEqual(expected, actual);
            }
        }
    }
}
