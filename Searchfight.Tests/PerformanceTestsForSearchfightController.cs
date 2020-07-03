using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Collections.Generic;
using Moq;
using Xunit;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Searchfight.Controllers;
using SearchfightDAL;
using SearchfightDAL.Models;
using SearchfightEngine.Interfaces;

namespace Searchfight.Tests
{
    public class PerformanceTestsForSearchfightController
    {
        [Fact]
        public void ListReturnAllValuesFromRepoLessThan100ms()
        {
            //Arrange & Act
            var benchSummary = BenchmarkRunner.Run<SearchfightControllersBenchmarks>();

            //Assert
            Assert.True(benchSummary.Reports[0].ResultStatistics.Mean < 1_000_000);
        }
    }

    public class SearchfightControllersBenchmarks
    {
        [Benchmark]
        public async Task ReturnAllValuesFromRepo()
        {
            var requestedData = new List<string> { "test1", "test2" };
            var expectedResult = new List<string> { "test: Google: 1" };
            var searchResponse = new List<SearchResultDto>
            {
                new SearchResultDto
                {
                    RequestValue = "test",
                    ResultCount = 1,
                    SearchEngineName = SearchEngine.Google,
                    RequestId = Guid.Empty
                }
            };
            Mock<ISearchfightSummaryCounter> mockSearchfightSummaryCounter = new Mock<ISearchfightSummaryCounter>();
            Mock<ISearchService> mockSearchService = new Mock<ISearchService>();
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();

            mockSearchService.CallBase = true;
            mockSearchService.Setup(x => x.Search(It.IsAny<SearchRequestModel>())).Returns(Task.FromResult(searchResponse));
            mockConfiguration.Setup(x => x.GetSection("Searchfight:SearchEngines:Google").Value).Returns("SearchEngnieQueryString");
            mockConfiguration.Setup(x => x.GetSection("Searchfight:SearchEngines:Bing").Value).Returns("SearchEngnieQueryString");
            SearchfightController controller = new SearchfightController(
                null,
                mockSearchfightSummaryCounter.Object,
                mockSearchService.Object,
                mockConfiguration.Object);

            var result = await controller.Get(requestedData.ToArray());

            Assert.IsType<List<string>>(result);
            Assert.Equal(expectedResult, result);
        }
    }
}