using InfoTrackTest.Helpers;
using InfoTrackTest.Helpers.Interfaces;
using InfoTrackTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InfoTrackTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGoogleSearchHelper _googleHelper;
        private readonly IBingSearchHelper _bingHelper;

        public HomeController(
            ILogger<HomeController> logger,
            IGoogleSearchHelper googleSearchHelper,
            IBingSearchHelper bingSearchHelper
            )
        {
            _logger = logger;
            _googleHelper = googleSearchHelper;
            _bingHelper = bingSearchHelper;
        }

        [HttpPost]
        public IActionResult ExecuteSeach(SearchFormModel search)
        {
            int desiredResultCount = 100;

            var googleResultsHtml = _googleHelper.GetHtmlResults(desiredResultCount, search.SearchTerms);

            // Leave Bing out for now, client not evaluating javascript or something?
            //var bingResultsHtml = _bingHelper.GetHtmlResults(desiredResultCount, search.SearchTerms);

            ViewBag.SearchResult = ParseResult(search.TargetURL, googleResultsHtml);

            return View("Index");
        }

        private static String ParseResult(string searchTargetURL, List<string> googleResultsHtml)
        {
            var output = new StringBuilder();

            // iterate through each result div. If it mention our target URL, then add the
            // result rank to our reult for the user
            for (int i = 0; i < googleResultsHtml.Count; i++)
            {
                if (googleResultsHtml[i].Contains(searchTargetURL))
                {
                    var numberToPrint = i + 1; // +1 to make 0-index human readable
                    output.Append(numberToPrint.ToString() + " ");
                }
            }

            // if there's no matching results, return a 0
            if (output.ToString() == "")
            {
                output.Append("0");
            }

            return output.ToString();
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
