using ConsoleApp1.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp1.CrossCuting
{

    public class SecuritiesDataProviderService : ISecuritiesDataProviderService
    {
        private readonly HttpClient _dataProviderClient;

        public SecuritiesDataProviderService(HttpClient dataProviderClient)
        {
            _dataProviderClient = dataProviderClient;
        }

        public async Task<DataProviderIsinResponse?> GetIsinAsync(string? isin)
        {

            //https://securities.dataprovider.com
            var result = await _dataProviderClient.GetAsync($"/securityprice/{isin}");


            var dataProviderResponse = await result.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<DataProviderIsinResponse>(dataProviderResponse);


        }
    }
}
