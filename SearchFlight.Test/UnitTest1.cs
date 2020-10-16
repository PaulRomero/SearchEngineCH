using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using SearchFlight.Application;
using SearchFlight.Application.DTOs.Request.ProgrammingLanguageEngine;
using SearchFlight.Application.Interfaces;
using SearchFlight.Model;
using SearchFlight.Repositories.Interfaces;
using SearchFlight.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using SearchFlight.CrossCutting;
using SearchFlight.Application.DTOs.Response;
using SearchFlight.Application.Services.Interfaces;
using SearchFlight.Application.Services;
using SearchFlight.Repositories;
using SearchFlight.Common;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        private static IServiceProvider _applicationProvider;

        public Tests()
        {
            RegisterServices();
        }

        [Test]
        public void SearchDoesNotWork()
        {
            //Arrange
            var service = _applicationProvider.GetService<IProgrammingLanguageEngineApplication>();
            var criteria = new SearchProgrammingLanguageRequest();
            criteria.Criteria.Add(Constants.ProgrammingLanguage.Java);
            criteria.Criteria.Add(Constants.ProgrammingLanguage.Net);

            //Act
            var searchProgrammingLanguaResult = service.SearchProgrammingLanguage(criteria);

            //Assert
            Assert.IsTrue(searchProgrammingLanguaResult.Any(), "Search does not works");
        }

        [Test]
        public void YahooWinnerInNet()
        {
            //Arrange
            var programmingLanguageEngineList = GetData();

            //Act
            var searchProgrammingLanguaResult = SearchCriteria(programmingLanguageEngineList);
            var isYahooWinner = searchProgrammingLanguaResult.Any(q => q.TextReport.ToLower().Contains(string.Format("{0} Winner", Constants.EngineName.Yahoo).ToLower()));

            //Assert
            Assert.IsTrue(isYahooWinner, string.Format("{0} should be winner in {1}", Constants.EngineName.Yahoo, Constants.ProgrammingLanguage.Net));

        }

        [Test]
        public void JavaTotalWinner()
        {
            //Arrange
            var programmingLanguageEngineList = GetData();

            //Act
            var searchProgrammingLanguaResult = SearchCriteria(programmingLanguageEngineList);
            var isJavaTotalWinner = searchProgrammingLanguaResult.Any(q => q.TextReport.ToLower().Contains(string.Format("Total winner: {0}", Constants.ProgrammingLanguage.Java).ToLower()));

            //Assert
            Assert.IsTrue(isJavaTotalWinner, string.Format("Total Winner must be {0}", Constants.ProgrammingLanguage.Java));

        }

        [Test]
        public void CriteriaElementsIsEqualsDetailElements()
        {
            //Arrange
            var programmingLanguageEngineList = GetData();

            //Act
            var searchProgrammingLanguaResult = SearchCriteria(programmingLanguageEngineList);

            var criteriaItems = programmingLanguageEngineList.Select(q=> q.ProgrammingLanguage.Id).Distinct().Count();
            var detailItems = searchProgrammingLanguaResult.Count(q => q.SectionReportId == (int)SectionReport_Enum.Detail);

            var sameNumberOfElements = criteriaItems == detailItems;

            //Assert
            Assert.sTrue(sameNumberOfElements, "Criteria elements does not match with details Report elements");

        }

        private List<ReportResult> SearchCriteria(List<ProgrammingLanguageEngine> programmingLanguageEngineList)
        {
            var service = _applicationProvider.GetService<IProgrammingLanguageEngineApplication>();
            var request = new SearchProgrammingLanguageRequest();
            foreach (var programmingLanguage in programmingLanguageEngineList.Select(q => q.ProgrammingLanguage.Name).Distinct())
            {
                request.Criteria.Add(programmingLanguage);
            }

            //Act
            return service.SearchProgrammingLanguage(request, programmingLanguageEngineList);
        }

        private List<ProgrammingLanguageEngine> GetData()
        {
            var programmingLanguageEngineList = new List<ProgrammingLanguageEngine>()
            {
                new ProgrammingLanguageEngine(){
                    Engine = Constants.EngineName.Yahoo,
                    ProgrammingLanguage= new ProgrammingLanguage(1, Constants.ProgrammingLanguage.Java),
                    ResultCount = 100
                },
                new ProgrammingLanguageEngine(){
                    Engine = Constants.EngineName.Yahoo,
                    ProgrammingLanguage= new ProgrammingLanguage(2, Constants.ProgrammingLanguage.Net),
                    ResultCount = 100
                },
                new ProgrammingLanguageEngine(){
                    Engine = Constants.EngineName.Google,
                    ProgrammingLanguage= new ProgrammingLanguage(1, Constants.ProgrammingLanguage.Net),
                    ResultCount = 10
                },
                new ProgrammingLanguageEngine(){
                    Engine = Constants.EngineName.Google,
                    ProgrammingLanguage= new ProgrammingLanguage(1, Constants.ProgrammingLanguage.Java),
                    ResultCount = 20
                }
            };

            return programmingLanguageEngineList;
        }

        public static void RegisterServices()
        {
            var collection = new ServiceCollection();
            collection.AddScoped<IProgrammingLanguageEngineApplication, ProgrammingLanguageEngineApplication>();
            collection.AddScoped<IProgrammingLanguageEngineRepository, ProgrammingLanguageEngineRepository>();
            collection.AddScoped<IEngineApplication, EngineApplication>();
            collection.AddScoped<IEngineRepository, EngineRepository>();
            _applicationProvider = collection.BuildServiceProvider();
        }

    }
}