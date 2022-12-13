using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSearch.App.Models
{
    class SearchParams
    {
        public string Word { get; set; }
        public string CityState { get; set; }
        public int NumberOfPage { get; set; }
    }
}
