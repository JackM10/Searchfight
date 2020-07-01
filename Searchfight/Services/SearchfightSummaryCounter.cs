using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SearchfightDAL;
using SearchfightEngine.Interfaces;

namespace SearchfightEngine.Services
{
    public class SearchfightSummaryCounter : ISearchfightSummaryCounter
    {
        public IEnumerable<string> ProcessSummary(List<SearchResultDto> searchResult)
        {
            //ToDo: to implement real logic:
            return new List<string>{$"Google winner: ............"};
        }
    }
}
