using System;
using System.Collections.Generic;
using System.Text;

namespace SearchFlight.Application.DTOs.Request.ProgrammingLanguageEngine
{
    public class SearchProgrammingLanguageRequest
    {
        public SearchProgrammingLanguageRequest()
        {
            Criteria = new List<string>();
        }
        public List<string> Criteria { get; set; }
    }
}
