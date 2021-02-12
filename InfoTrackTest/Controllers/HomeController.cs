using InfoTrackTest.Helpers;
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
            int desiredResultCount = 100;

            var resultsHtml = new GoogleSearchHelper().GetHtmlResults(desiredResultCount, search.SearchTerms);

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
