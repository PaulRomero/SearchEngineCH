using SearchFlight.Application.DTOs.Request;
using SearchFlight.Application.DTOs.Response;
using SearchFlight.Application.Interfaces;
using SearchFlight.Common;
using SearchFlight.Model;
using SearchFlight.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SearchFlight.Application
{
    public class SearchEngineApplication : ISearchEngineApplication
    {
        private readonly ISearchEngineRepository _searchEngineRepository;

        public SearchEngineApplication(ISearchEngineRepository searchEngineRepository)
        {
            this._searchEngineRepository = searchEngineRepository;
        }


        #region [public methods]
        public List<SearchResult> SearchTerm(SearchRequest request)
        {
            var data = _searchEngineRepository.GetAll(request);
            return CreateResults(data, request);
        }

        public List<SearchResult> SearchTerm(SearchRequest request, List<SearchEngine> searchEngineList)
        {
            return CreateResults(searchEngineList, request);
        }

        #endregion

        #region [private methods]

        private List<SearchResult> CreateResults(List<SearchEngine> searchEngineList, SearchRequest request)
        {
            List<SearchResult> searchResultList = new List<SearchResult>();

            GetDetailReport(searchEngineList, searchResultList);

            GetWinnerReport(searchEngineList, searchResultList);

            GetTotalReport(searchEngineList, searchResultList);

            return searchResultList;
        }

        private void GetDetailReport(List<SearchEngine> searchEngineList, List<SearchResult> searchResultList)
        {
            if (!searchEngineList.Any())
                return;

            var searchTextList = searchEngineList
                        .GroupBy(q => q.SearchText.Name)
                        .ToList();

            searchTextList.ForEach(term =>
            {
                var engines = searchEngineList
                        .Where(r => r.SearchText.Name == term.Key)
                        .ToList();

                var text = string.Format("{0} :", term.Key);

                engines.ForEach(q =>
                {
                    text = text + string.Format("{0} : {1} ", q.Engine, q.ResultCount);
                });

                InsertResult((int)SectionReport_Enum.Detail, text, searchResultList);

            });
        }
        private void GetWinnerReport(List<SearchEngine> searchEngineList, List<SearchResult> searchResultList)
        {
            if (!searchEngineList.Any())
                return;

            var winners = searchEngineList
                    .GroupBy(l => new { l.SearchText.Name })
                    .Select(cl => new
                    {
                        Term = cl.First().SearchText.Name,
                        MaxResultCount = cl.Max(c => c.ResultCount)
                    })
                    .ToList();

            winners.ForEach(q =>
            {
                var winner = searchEngineList.First(r => r.SearchText.Name == q.Term && r.ResultCount == q.MaxResultCount);
                var text = string.Format("{0} winner: {1}", winner.Engine, winner.SearchText.Name);
                InsertResult((int)SectionReport_Enum.EngineWinner, text, searchResultList);
            });
        }
        private void GetTotalReport(List<SearchEngine> searchEngineList, List<SearchResult> searchResultList)
        {
            if (!searchEngineList.Any())
                return;

            var winners = searchEngineList
                    .GroupBy(l => new { l.SearchText.Name })
                    .Select(cl => new
                    {
                        Term = cl.First().SearchText.Name,
                        MaxResultCount = cl.Sum(c => c.ResultCount)
                    })
                    .ToList();

            var maxResultCount = winners
                    .Max(l => l.MaxResultCount);

            var totalWinner = winners.First(r => r.MaxResultCount == maxResultCount);
            var text = string.Format("Total winner: {0}", totalWinner.Term);
            InsertResult((int)SectionReport_Enum.TotaWinner, text, searchResultList);
        }

        private void InsertResult(int sectionResultId, string text, List<SearchResult> resultList)
        {
            var result = new SearchResult()
            {
                SectionResultId = sectionResultId,
                TextReport = text
            };
            resultList.Add(result);
        }

        #endregion

    }
}
