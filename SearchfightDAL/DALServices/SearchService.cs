using System.Collections.Generic;
using SearchfightDAL;
using SearchfightDAL.Models;

namespace SearchfightEngine.Interfaces
{
    public class SearchService : ISearchService
    {
        public List<SearchResultDto> Search(SearchRequestModel request)
        {
            //ToDO: replace with call to search engine:
            return new List<SearchResultDto> {
                new SearchResultDto
                {
                    RequestValue = ".net",
                    ResultCount = 100,
                    SearchEngineName = SearchEngine.Google
                },
                new SearchResultDto
                {
                    ResultCount = 50,
                    RequestValue = "java",
                    SearchEngineName = SearchEngine.Google
                },
                new SearchResultDto
                {
                    SearchEngineName = SearchEngine.Bing,
                    RequestValue = ".net",
                    ResultCount = 200
                },
                new SearchResultDto
                {
                    ResultCount = 100,
                    SearchEngineName = SearchEngine.Bing,
                    RequestValue = "java"
                }
            };
        }
    }
}
