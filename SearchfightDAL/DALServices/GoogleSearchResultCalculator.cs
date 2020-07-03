using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace SearchfightDAL.DALServices
{
    public static class GoogleSearchResultCalculator
    {
        public static double ExtractGoogleAmountOfResultsUsingManualCall(string searchEngineQuery, string requestEntity)
        {
            var doc = new HtmlWeb().Load(searchEngineQuery + requestEntity);
            HtmlNode div = new HtmlNode(HtmlNodeType.Document, new HtmlDocument(), 0);
            div = doc.DocumentNode.SelectSingleNode("//div[@id='result-stats']");

            string responseBody = div.InnerText;

            var matches = Regex.Matches(responseBody, @"[0-9].+?(?=\()");
            var total = matches[0].Value;
            var parsedstr = Regex.Replace(total, @"\s+", "");
            var resultAmount = double.Parse(parsedstr);

            return resultAmount;
        }
    }
}
