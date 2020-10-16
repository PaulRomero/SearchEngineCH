using SearchFlight.Application.DTOs.Request.ProgrammingLanguageEngine;
using SearchFlight.CrossCutting;
using System;
using System.Collections.Generic;

namespace SearchFlight.Model
{
    public static class DbContext
    {
        //This class emulate the entity framework Context

        public static List<ProgrammingLanguageEngine> GetDummyData(SearchProgrammingLanguageRequest request)
        {
            var id = 1;
            var ProgrammingLanguageList = new List<ProgrammingLanguageEngine>();

            request.Criteria.ForEach(language => {
                var languageProgramming = new ProgrammingLanguage(id++, language);
                foreach (var engine in GetEngineList())
                {
                    var progLangEngine = new ProgrammingLanguageEngine() { Engine = engine.Name, ProgrammingLanguage = languageProgramming, ResultCount = new Random().Next(1000, 50000) };
                    ProgrammingLanguageList.Add(progLangEngine);
                }
            });

            return ProgrammingLanguageList;

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
