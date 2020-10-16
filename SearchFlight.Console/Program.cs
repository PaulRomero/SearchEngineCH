using Microsoft.Extensions.DependencyInjection;
using SearchFlight.Application;
using SearchFlight.Application.DTOs.Request.ProgrammingLanguageEngine;
using SearchFlight.Application.Interfaces;
using SearchFlight.Application.Services;
using SearchFlight.Application.Services.Interfaces;
using SearchFlight.Console.ExceptionHandler;
using SearchFlight.Repositories;
using SearchFlight.Repositories.Interfaces;
using SearchFlight.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SearchFlight.Console
{
    public class Program
    {
        private static IServiceProvider _applicationProvider;

        public static void Main(string[] args)
        {
            try
            {
                RegisterServices();
                SearchWords(args);
            }
            catch (SearchFlightException ex)
            {
                System.Console.WriteLine(string.Format("BUSINESS ERROR: {0}", ex.Message));
                
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(string.Format("SYSTEM ERROR: {0}{1}{2}", ex.Message, Environment.NewLine, ex.StackTrace.ToString()));
                System.Console.ReadKey();
            }
            finally
            {
                DisposeServices();
            }
            System.Console.ReadLine();
        }

        private static void SearchWords(string[] args)
        {
            System.Console.WriteLine("=============");
            System.Console.WriteLine("Search Flight ");
            System.Console.WriteLine("=============");
            System.Console.WriteLine("Enter search criteria separated by empty space : ");
            var read = System.Console.ReadLine();

            if (string.IsNullOrEmpty(read))
            {
                System.Console.WriteLine("Please enter criteria...");
                SearchWords(args);
                return;
            }

            var request = CreateRequest(read);

            SearchCriteria(request);
        }

        private static void SearchCriteria(SearchProgrammingLanguageRequest request)
        {
            var service = _applicationProvider.GetService<IProgrammingLanguageEngineApplication>();
            var searchProgrammingLanguaResult = service.SearchProgrammingLanguage(request);

            if (searchProgrammingLanguaResult.Any())
            {
                searchProgrammingLanguaResult.ForEach(q =>
                {
                    System.Console.WriteLine(string.Format("{0}", q.TextReport));
                });
            }
            else
            {
                throw new SearchFlightException("Search result does not work");
            }
        }

        private static SearchProgrammingLanguageRequest CreateRequest(string read)
        {
            var changeRead = read.Replace('"', '|');
            var split = changeRead.Split(' ');
            var criteriaList = CreateCriteriaList(split);
            var request = new SearchProgrammingLanguageRequest()
            {
                Criteria = criteriaList
            };

            return request;
        }

        private static List<string> CreateCriteriaList(string[] searchCriteria)
        {
            var criteriaElements = new List<string>();
            var completeText = string.Empty;
            var flagContinueFindingText = false;
            foreach (var criteria in searchCriteria)
            {
                if (flagContinueFindingText)
                {
                    bool existsQuotationLastCharacter = criteria.TrimEnd().Substring(criteria.TrimEnd().Length - 1, 1).Equals("|");
                    completeText = string.Format("{0} {1}", completeText, criteria.Replace("|", ""));
                    if (existsQuotationLastCharacter)
                    {
                        flagContinueFindingText = false;
                        criteriaElements.Add(completeText);
                        completeText = "";
                    }
                }
                else
                {
                    bool existsQuotationFirstCharacter = criteria.Substring(0, 1).Equals("|");
                    if (existsQuotationFirstCharacter)
                    {
                        completeText = string.Format("{0}{1}", completeText, criteria.Replace("|", ""));
                        flagContinueFindingText = true;
                    }
                    else
                    {
                        criteriaElements.Add(criteria.Replace("|", " "));
                    }
                }

            }
            if (completeText != "")
            {
                criteriaElements.Add(completeText);
            }

            return criteriaElements.Select(q => q.ToLower()).ToList();
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

        public static void DisposeServices()
        {
            if (_applicationProvider == null)
            {
                return;
            }
            if (_applicationProvider is IDisposable)
            {
                ((IDisposable)_applicationProvider).Dispose();
            }
        }
    }
}
