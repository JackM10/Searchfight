using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SearchfightDAL.DALServices
{
    public class BingSearchResultCalculator
    {
        public static async Task<double> ExtractBingAmountOfResults(string searchEngineQuery, string requestEntity)
        {
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(searchEngineQuery + requestEntity);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            int firstIndexOfBingResultNumber = responseBody.IndexOf(@"span class=""sb_count") + 22;
            int lastIndexOfBingResultNumber = firstIndexOfBingResultNumber + 15;
            string textWithResultNumber = responseBody.Substring(firstIndexOfBingResultNumber, lastIndexOfBingResultNumber - firstIndexOfBingResultNumber);
            var textWithResultNumberWithoutCommas = textWithResultNumber.Replace(",", "");
            textWithResultNumberWithoutCommas = textWithResultNumberWithoutCommas.Replace(" ", "");
            var onlySearchResultNumber = Regex.Matches(textWithResultNumberWithoutCommas, @"[0-9]+")[0].Value;

            var resultAmount = double.Parse(onlySearchResultNumber);

            return resultAmount;
        }
    }
}
