using System;
using System.Collections.Generic;
using System.Text;

namespace SearchFlight.Model
{
    public class SearchEngine
    {
        public string Engine { get; set; }
        public int ResultCount { get; set; }
        public SearchText SearchText { get; set; }
    }
}
