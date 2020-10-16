using SearchFlight.Application.DTOs.Request.ProgrammingLanguageEngine;
using SearchFlight.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SearchFlight.Repositories.Interfaces
{
    public interface IProgrammingLanguageEngineRepository
    {
        List<ProgrammingLanguageEngine> GetAll(SearchProgrammingLanguageRequest request);

    }
}
