using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using SearchFlight.Application;
using SearchFlight.Application.DTOs.Request;
using SearchFlight.Application.DTOs.Response;
using SearchFlight.Application.Interfaces;
using SearchFlight.Application.Services;
using SearchFlight.Application.Services.Interfaces;
using SearchFlight.Common;
using SearchFlight.CrossCutting;
using SearchFlight.Model;
using SearchFlight.Repositories;
using SearchFlight.Repositories.Interfaces;
using SearchFlight.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

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
            var service = _applicationProvider.GetService<ISearchEngineApplication>();
            var criteria = new SearchRequest();
            criteria.Criteria.Add(Constants.ProgrammingLanguage.Java);
            criteria.Criteria.Add(Constants.ProgrammingLanguage.Net);

            //Act
            var searchResult = service.SearchTerm(criteria);

            //Assert
            Assert.IsTrue(searchResult.Any(), "Search does not works");
        }

        [Test]
        public void YahooWinnerInNet()
        {
            //Arrange
            var data = GetData();

            //Act
            var searchResult = SearchResult(data);
            var isYahooWinner = searchResult.Any(q => q.TextReport.ToLower().Contains(string.Format("{0} Winner", Constants.EngineName.Yahoo).ToLower()));

            //Assert
            Assert.IsTrue(isYahooWinner, string.Format("{0} should be winner in {1}", Constants.EngineName.Yahoo, Constants.ProgrammingLanguage.Net));

        }

        [Test]
        public void JavaTotalWinner()
        {
            //Arrange
            var data = GetData();

            //Act
            var searchResult = SearchResult(data);
            var isJavaTotalWinner = searchResult.Any(q => q.TextReport.ToLower().Contains(string.Format("Total winner: {0}", Constants.ProgrammingLanguage.Java).ToLower()));

            //Assert
            Assert.IsTrue(isJavaTotalWinner, string.Format("Total Winner must be {0}", Constants.ProgrammingLanguage.Java));

        }

        [Test]
        public void CriteriaElementsNumberAreEqualsToDetailElementsNumbe()
        {
            //Arrange
            var data = GetData();

            //Act
            var searchResult = SearchResult(data);

            var criteriaItems = data.Select(q=> q.SearchText.Id).Distinct().Count();
            var detailItems = searchResult.Count(q => q.SectionResultId == (int)SectionReport_Enum.Detail);

            var sameNumberOfElements = criteriaItems == detailItems;

            //Assert
            Assert.IsTrue(sameNumberOfElements, "Criteria elements number does not match with details report elements number");

        }

        private List<SearchResult> SearchResult(List<SearchEngine> searchEngineList)
        {
            var service = _applicationProvider.GetService<ISearchEngineApplication>();
            var request = new SearchRequest();
            foreach (var text in searchEngineList.Select(q => q.SearchText.Name).Distinct())
            {
                request.Criteria.Add(text);
            }

            //Act
            return service.SearchTerm(request, searchEngineList);
        }

        private List<SearchEngine> GetData()
        {
            var searchEngineList = new List<SearchEngine>()
            {
                new SearchEngine(){
                    Engine = Constants.EngineName.Yahoo,
                    SearchText= new SearchText(1, Constants.ProgrammingLanguage.Java),
                    ResultCount = 100
                },
                new SearchEngine(){
                    Engine = Constants.EngineName.Yahoo,
                    SearchText= new SearchText(2, Constants.ProgrammingLanguage.Net),
                    ResultCount = 100
                },
                new SearchEngine(){
                    Engine = Constants.EngineName.Google,
                    SearchText= new SearchText(1, Constants.ProgrammingLanguage.Net),
                    ResultCount = 10
                },
                new SearchEngine(){
                    Engine = Constants.EngineName.Google,
                    SearchText= new SearchText(1, Constants.ProgrammingLanguage.Java),
                    ResultCount = 20
                }
            };

            return searchEngineList;
        }

        public static void RegisterServices()
        {
            var collection = new ServiceCollection();
            collection.AddScoped<ISearchEngineApplication, SearchEngineApplication>();
            collection.AddScoped<ISearchEngineRepository, SearchEngineRepository>();
            collection.AddScoped<IEngineApplication, EngineApplication>();
            collection.AddScoped<IEngineRepository, EngineRepository>();
            _applicationProvider = collection.BuildServiceProvider();
        }

    }
}