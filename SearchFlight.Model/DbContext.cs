using SearchFlight.Application.DTOs.Request;
using SearchFlight.CrossCutting;
using System;
using System.Collections.Generic;

namespace SearchFlight.Model
{
    public static class DbContext
    {
        //This class emulate the entity framework Context

        public static List<SearchEngine> GetEngineDummyResult(SearchRequest request)
        {
            var id = 1;
            var searchEngineList = new List<SearchEngine>();

            request.Criteria.ForEach(text => {
                var searchText = new SearchText(id++, text);
                foreach (var engine in GetEngineList())
                {
                    var progLangEngine = new SearchEngine() { Engine = engine.Name, SearchText = searchText, ResultCount = new Random().Next(1000, 50000) };
                    searchEngineList.Add(progLangEngine);
                }
            });

            return searchEngineList;

        }

        public static List<Engine> GetEngineList()
        {
            List<Engine> engineList = new List<Engine>();
            engineList.Add(new Engine() { Id = 1, Name = Constants.EngineName.Google });
            engineList.Add(new Engine() { Id = 2, Name = Constants.EngineName.Bing });
            engineList.Add(new Engine() { Id = 3, Name = Constants.EngineName.Yahoo });

            return engineList;

        }


    }

}
