using SearchFlight.Application.DTOs.Request.ProgrammingLanguageEngine;
using SearchFlight.Model;
using SearchFlight.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SearchFlight.Repository
{
    public class ProgrammingLanguageEngineRepository: IProgrammingLanguageEngineRepository
    {
        public List<ProgrammingLanguageEngine> GetAll(SearchProgrammingLanguageRequest request)
        {
            return DbContext.GetDummyData(request);
        }

    }
}
