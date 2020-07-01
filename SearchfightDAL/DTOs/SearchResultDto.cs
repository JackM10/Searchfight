using System;

namespace SearchfightDAL
{
    public class SearchResultDto
    {
        public string RequestValue { get; set; }

        public int ResultCount { get; set; }

        public SearchEngine SearchEngineName { get; set; }
    }
}
