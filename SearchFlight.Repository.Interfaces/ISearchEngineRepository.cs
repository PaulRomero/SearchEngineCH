using SearchFlight.Application.DTOs.Request;
using SearchFlight.Model;
using System.Collections.Generic;

namespace SearchFlight.Repositories.Interfaces
{
    public interface ISearchEngineRepository
    {
        List<SearchEngine> GetAll(SearchRequest request);

    }
}
