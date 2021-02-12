using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoTrackTest.Helpers.Interfaces
{
    public interface ISearchHelper
    {
        public List<String> GetHtmlResults(int resultCount, string keywords);
    }
}
