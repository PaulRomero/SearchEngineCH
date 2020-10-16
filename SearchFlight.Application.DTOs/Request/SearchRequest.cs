using System;
using System.Collections.Generic;
using System.Text;

namespace SearchFlight.Application.DTOs.Request
{
    public class SearchRequest
    {
        public SearchRequest()
        {
            Criteria = new List<string>();
        }
        public List<string> Criteria { get; set; }
    }
}
