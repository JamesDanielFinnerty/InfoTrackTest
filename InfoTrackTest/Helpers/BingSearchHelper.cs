using InfoTrackTest.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoTrackTest.Helpers
{
    public class BingSearchHelper : BaseSearchHelper, IBingSearchHelper
    {
        public BingSearchHelper()
        {
            this.url = "https://www.bing.com/search?";
            this.countQuery = "count=";
            this.resultDivStarter = "<li class=\"b_algo\" data-bm=\"";
        }
    }
}
