using System.Collections.Generic;
using SearchfightDAL;
using SearchfightDAL.Models;

namespace SearchfightEngine.Interfaces
{
    public interface ISearchService
    {
        List<SearchResultDto> Search(SearchRequestModel request);
    }
}
