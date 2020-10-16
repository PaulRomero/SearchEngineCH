using SearchFlight.Application.DTOs.Request;
using SearchFlight.Application.DTOs.Response;
using SearchFlight.Model;
using System.Collections.Generic;

namespace SearchFlight.Application.Interfaces
{
    public interface ISearchEngineApplication
    {
        List<SearchResult> SearchTerm(SearchRequest request);
        List<SearchResult> SearchTerm(SearchRequest request, List<SearchEngine> searchEngineList);
    }
}
