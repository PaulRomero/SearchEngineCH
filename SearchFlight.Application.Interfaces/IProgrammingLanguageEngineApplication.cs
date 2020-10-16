using SearchFlight.Application.DTOs.Request.ProgrammingLanguageEngine;
using SearchFlight.Application.DTOs.Response;
using SearchFlight.Model;
using System;
using System.Collections.Generic;

namespace SearchFlight.Application.Interfaces
{
    public interface IProgrammingLanguageEngineApplication
    {
        List<ReportResult> SearchProgrammingLanguage(SearchProgrammingLanguageRequest request);
        List<ReportResult> SearchProgrammingLanguage(SearchProgrammingLanguageRequest request, List<ProgrammingLanguageEngine> programmingLanguageEngineList);
    }
}
