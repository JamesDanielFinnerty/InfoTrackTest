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

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

        }

        [HttpPost]
        public IActionResult ExecuteSeach(SearchFormModel search)
        {

            string siteUrl = "https://www.google.co.uk/search?num=100&q=";
            string searchTerms = search.SearchTerms;

            // format terms for google query
            searchTerms = searchTerms.Replace(' ', '+');

            string divStarter = "<div class=\"ZINbbc xpd O9g5cc uUPGi\"><div class=\"kCrYT\">";
            int resultCount = 100;

            System.Net.WebClient webClient = new System.Net.WebClient();
            byte[] rawPage = webClient.DownloadData(siteUrl + searchTerms);
            string pageHtml = System.Text.Encoding.UTF8.GetString(rawPage);

            // get the index of the first occurance of the start of Google result HTML
            var resultsHtml = new List<String>();

            // find index for start of first organic result
            var firstResultIndex = pageHtml.IndexOf(divStarter);

            // trim preceeding data
            pageHtml = pageHtml.Substring(firstResultIndex);

            for (int i = 0; i < resultCount; i++)
            {
                // find start of second result div by offsetting from the start of the string
                var workingIndex = pageHtml.IndexOf(divStarter, 1);

                // if no further results
                if (workingIndex == -1)
                {
                    // handle final result case by appending all remainging html
                    resultsHtml.Add(pageHtml);
                }
                else
                {
                    // handle usual case by removing html relating to result and appending to result list
                    var targetDiv = pageHtml.Substring(0, workingIndex);
                    resultsHtml.Add(targetDiv);

                    // trim current div off of the start of the file so that we cna continue the process
                    pageHtml = pageHtml.Substring(workingIndex);
                }
            }

            var output = new StringBuilder();

            for(int i = 0; i < resultsHtml.Count; i++)
            {
                if(resultsHtml[i].Contains(search.TargetURL))
                {
                    var numberToPrint = i + 1; // handle 0 offsetting
                    output.Append(numberToPrint.ToString() + " ");
                }
            }

            if(output.ToString() == "")
            {
                output.Append("0");
            }

            ViewBag.SearchResult = output.ToString();
            return View("Index");
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
