using SearchFlight.Application.DTOs.Request;
using SearchFlight.Model;
using SearchFlight.Repositories.Interfaces;
using System.Collections.Generic;

namespace SearchFlight.Repository
{
    public class SearchEngineRepository: ISearchEngineRepository
    {
        public List<SearchEngine> GetAll(SearchRequest request)
        {
            return DbContext.GetEngineDummyResult(request);
        }

    }
}
