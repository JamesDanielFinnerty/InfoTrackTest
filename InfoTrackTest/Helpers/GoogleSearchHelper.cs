using InfoTrackTest.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoTrackTest.Helpers
{
    public class GoogleSearchHelper : BaseSearchHelper
    {
        public GoogleSearchHelper()
        {
            this.url = "https://www.google.co.uk/search?";
            this.countQuery = "num=";
            this.resultDivStarter = "<div class=\"ZINbbc xpd O9g5cc uUPGi\"><div class=\"kCrYT\">";
        }
    }
}
