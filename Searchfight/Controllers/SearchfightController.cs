using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SearchfightDAL.Models;
using SearchfightEngine.Interfaces;

namespace Searchfight.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchfightController : ControllerBase
    {
        private readonly ILogger<SearchfightController> _logger;
        private readonly ISearchfightSummaryCounter _summaryCounter;
        private readonly ISearchService _searchService;

        public SearchfightController(
            ILogger<SearchfightController> logger,
            ISearchfightSummaryCounter summaryCounter,
            ISearchService searchService)
        {
            _logger = logger;
            _summaryCounter = summaryCounter;
            _searchService = searchService;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            var result = new List<string>();

            string[] args; //this one to remove after resolving issue
            if(false)// (args.Length == 0)
            {
                _logger.LogInformation($"Empty request comes in {DateTime.Now}");

                return new List<string> {"Request can't be empty"};
            }

            var searchRequest = new SearchRequestModel
            {
                //ToDo
            };

            var searchResult = _searchService.Search(searchRequest);
            
            foreach (var str in searchResult)
            {
                result.Add($"{str.RequestValue}: {str.SearchEngineName}: {str.ResultCount}");
            }
            
            var summaryResults = _summaryCounter.ProcessSummary(searchResult);

            foreach (var summaryResult in summaryResults)
            {
                result.Add(summaryResult);
            }

            return result;
        }
    }
}
