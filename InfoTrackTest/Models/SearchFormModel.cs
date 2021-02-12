using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoTrackTest.Models
{
    public class SearchFormModel
    {
        public string SearchTerms { get; set; }
        public string TargetURL { get; set; }

        public SearchFormModel()
        {
            TargetURL = "www.infotrack.co.uk";
            SearchTerms = "land registry search";
        }
    }
}
