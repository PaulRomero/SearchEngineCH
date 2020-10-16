using NUnit.Framework;
using SearchFlight.Application.DTOs.Request.ProgrammingLanguageEngine;
using SearchFlight.Application.Interfaces;
using System;

namespace SearchFligth.NTest
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
        public void IsNotSearching()
        {
            //Arrange
            var service = _applicationProvider.GetService<IProgrammingLanguageEngineApplication>();
            var criteria = new SearchProgrammingLanguageRequest();
            criteria.Criteria.Add(Constants.ProgrammingLanguage.Java);
            criteria.Criteria.Add(Constants.ProgrammingLanguage.Net);

            //Act
            var searchProgrammingLanguaResult = service.SearchProgrammingLanguage(criteria);

            //Assert
            Assert.IsNotEmpty(searchProgrammingLanguaResult, "Search is not working for correct criteria java or net");
        }

        [Test]
        public void YahooWinnerInNetWord()
        {
            //Arrange
            var service = _applicationProvider.GetService<IProgrammingLanguageEngineApplication>();
            var programmingLanguageEngine = new List<ProgrammingLanguageEngine>()
            {
                new ProgrammingLanguageEngine(){
                    Engine = Constants.Engine.Yahoo,
                    ProgrammingLanguageId = 2,
                    ResultCount = 9999999 //the largest amount
                }
            };

            var criteria = new SearchProgrammingLanguageRequest();
            criteria.Criteria.Add(Constants.ProgrammingLanguage.Java);
            criteria.Criteria.Add(Constants.ProgrammingLanguage.Net);

            //Act
            var searchProgrammingLanguaResult = service.SearchProgrammingLanguageForTest(criteria, programmingLanguageEngine);
            var isYahooWinner = searchProgrammingLanguaResult.Any(q => q.Text.ToLower().Contains("yahoo winner"));

            //Assert
            Assert.IsTrue(isYahooWinner, "Yahoo should be winner in .Net");
        }


        public static void RegisterServices()
        {
            var collection = new ServiceCollection();
            collection.AddScoped<IProgrammingLanguageEngineApplication, ProgrammingLanguageEngineApplication>();
            _applicationProvider = collection.BuildServiceProvider();
        }
    }