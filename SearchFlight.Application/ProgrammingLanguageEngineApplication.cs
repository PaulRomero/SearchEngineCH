using SearchFlight.Application.DTOs.Request.ProgrammingLanguageEngine;
using SearchFlight.Application.DTOs.Response;
using SearchFlight.Application.Interfaces;
using SearchFlight.Common;
using SearchFlight.Model;
using SearchFlight.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SearchFlight.Application
{
    public class ProgrammingLanguageEngineApplication : IProgrammingLanguageEngineApplication
    {
        private readonly IProgrammingLanguageEngineRepository _programmingLanguageEngineRepository;

        public ProgrammingLanguageEngineApplication(IProgrammingLanguageEngineRepository programmingLanguageEngineRepository)
        {
            this._programmingLanguageEngineRepository = programmingLanguageEngineRepository;
        }


        #region [public methods]
        public List<ReportResult> SearchProgrammingLanguage(SearchProgrammingLanguageRequest request)
        {
            var data = _programmingLanguageEngineRepository.GetAll(request);
            return CreateResults(data, request);
        }

        public List<ReportResult> SearchProgrammingLanguage(SearchProgrammingLanguageRequest request, List<ProgrammingLanguageEngine> programmingLanguageEngineList)
        {
            return CreateResults(programmingLanguageEngineList, request);
        }



        #endregion

        #region [private methods]

        private List<ReportResult> CreateResults(List<ProgrammingLanguageEngine> programmingLanguageEngineList, SearchProgrammingLanguageRequest request)
        {
            List<ReportResult> searchProgrammingLanguageResultList = new List<ReportResult>();

            GetDetailReport(programmingLanguageEngineList, searchProgrammingLanguageResultList);

            GetWinnerReport(programmingLanguageEngineList, searchProgrammingLanguageResultList);

            GetTotalReport(programmingLanguageEngineList, searchProgrammingLanguageResultList);

            return searchProgrammingLanguageResultList;
        }

        private void GetDetailReport(List<ProgrammingLanguageEngine> programmingLanguageEngineList, List<ReportResult> reportResultList)
        {
            if (!programmingLanguageEngineList.Any())
                return;

            var programmingLanguages = programmingLanguageEngineList
                        .GroupBy(q => q.ProgrammingLanguage.Name)
                        .ToList();

            programmingLanguages.ForEach(progLang =>
            {
                var engines = programmingLanguageEngineList
                        .Where(r => r.ProgrammingLanguage.Name == progLang.Key)
                        .ToList();

                var text = string.Format("{0} :", progLang.Key);

                engines.ForEach(q =>
                {
                    text = text + string.Format("{0} : {1} ", q.Engine, q.ResultCount);
                });

                InsertResult((int)SectionReport_Enum.Detail, text, reportResultList);

            });
        }
        private void GetWinnerReport(List<ProgrammingLanguageEngine> programmingLanguageEngineList, List<ReportResult> reportResultList)
        {
            if (!programmingLanguageEngineList.Any())
                return;

            var winners = programmingLanguageEngineList
                    .GroupBy(l => new { l.ProgrammingLanguage.Name })
                    .Select(cl => new
                    {
                        ProgramingLanguageName = cl.First().ProgrammingLanguage.Name,
                        MaxResultCount = cl.Max(c => c.ResultCount)
                    })
                    .ToList();

            winners.ForEach(q =>
            {
                var langProgWinner = programmingLanguageEngineList.First(r => r.ProgrammingLanguage.Name == q.ProgramingLanguageName && r.ResultCount == q.MaxResultCount);
                var text = string.Format("{0} winner: {1}", langProgWinner.Engine, langProgWinner.ProgrammingLanguage.Name);
                InsertResult((int)SectionReport_Enum.EngineWinner, text, reportResultList);
            });
        }
        private void GetTotalReport(List<ProgrammingLanguageEngine> programmingLanguageEngineList, List<ReportResult> reportResultList)
        {
            if (!programmingLanguageEngineList.Any())
                return;

            var winners = programmingLanguageEngineList
                    .GroupBy(l => new { l.ProgrammingLanguage.Name })
                    .Select(cl => new
                    {
                        ProgramingLanguageName = cl.First().ProgrammingLanguage.Name,
                        MaxResultCount = cl.Sum(c => c.ResultCount)
                    })
                    .ToList();

            var maxResultCount = winners
                    .Max(l => l.MaxResultCount);

            var langProgWinner = winners.First(r => r.MaxResultCount == maxResultCount);
            var text = string.Format("Total winner: {0}", langProgWinner.ProgramingLanguageName);
            InsertResult((int)SectionReport_Enum.TotaWinner, text, reportResultList);
        }

        private void InsertResult(int sectionReportId, string text, List<ReportResult> resultList)
        {
            var result = new ReportResult()
            {
                SectionReportId = sectionReportId,
                TextReport = text
            };
            resultList.Add(result);
        }

        #endregion

    }
}
