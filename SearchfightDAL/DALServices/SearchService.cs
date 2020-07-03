using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SearchfightDAL;
using SearchfightDAL.DALServices;
using SearchfightDAL.Models;

namespace SearchfightEngine.Interfaces
{
    public class SearchService : ISearchService
    {
        public async Task<List<SearchResultDto>> Search(SearchRequestModel request)
        {
            var searchResult = new List<SearchResultDto>();

            foreach (var requestEntity in request.SearchRequests)
            {
                foreach (var searchEngineEntity in request.SearchEngineEntities)
                {
                    double amountOfResults = double.MinValue;

                    if (searchEngineEntity.SearchEngine == SearchEngine.Google)
                    {
                        amountOfResults = GoogleSearchResultCalculator.ExtractGoogleAmountOfResultsUsingManualCall(searchEngineEntity.SearchEngineQuery, requestEntity);
                    }
                    else if (searchEngineEntity.SearchEngine == SearchEngine.Bing)
                    {
                        amountOfResults = await BingSearchResultCalculator.ExtractBingAmountOfResults(searchEngineEntity.SearchEngineQuery, requestEntity);
                    }

                    var requestId = Guid.NewGuid();

                    searchResult.Add(new SearchResultDto
                    {
                        RequestId = requestId,
                        RequestValue = requestEntity,
                        ResultCount = amountOfResults,
                        SearchEngineName = searchEngineEntity.SearchEngine
                    });
                }
            }

            return searchResult;
        }
    }
}
