using InfoTrackTest.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoTrackTest.Helpers
{
    public class BaseSearchHelper : ISearchHelper
    {
        public string url { get; protected set; }
        public string countQuery { get; protected set; }
        public string resultDivStarter { get; protected set; }

        public BaseSearchHelper()
        {
            this.url = "www.url.com/";
            this.countQuery = "count=";
            this.resultDivStarter = "<div>";
        }

        public List<String> GetHtmlResults(int resultCount, string keywords)
        {
            var searchTerms = keywords.Replace(' ', '+');

            string siteUrl = url + countQuery + resultCount.ToString() + "&q=";

            // setup client and load results page into a string
            System.Net.WebClient webClient = new System.Net.WebClient();
            byte[] rawPage = webClient.DownloadData(siteUrl + searchTerms);
            string pageHtml = System.Text.Encoding.UTF8.GetString(rawPage);

            // get the index of the first search result div then trim preceeding garbage
            var firstResultIndex = pageHtml.IndexOf(resultDivStarter);
            pageHtml = pageHtml.Substring(firstResultIndex);

            // create results list
            var resultsHtml = new List<String>();

            // iterate through page splittig out each result
            for (int i = 0; i < resultCount; i++)
            {
                // find start of second result div by offsetting from the start of the string
                var workingIndex = pageHtml.IndexOf(resultDivStarter, 1);

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

            return resultsHtml;
        }
    }
}
