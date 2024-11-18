using Crypto_test.Model.CoinMarket;
using Crypto_test.Model.CoinCap;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Configuration;

namespace Crypto_test.Repository
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly HttpClient _httpClient;

        public CurrencyRepository()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IEnumerable<Currency>> GetCurrenciesAsync()
        {
            string apiKey = ConfigurationManager.AppSettings["CoinMarketCapApiKey"];
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException("API ключ не найден. Проверьте конфигурацию.");
            }

            string url = "https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest";
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", apiKey);
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            var response = await _httpClient.GetStringAsync(url);
            var data = JsonConvert.DeserializeObject<CoinMarketCapResponse>(response);

            return data?.Data ?? new List<Currency>();
        }


        public async Task<List<Market>> GetMarketsForCurrencyAsync(string currencyId)
        {
            string url = $"https://api.coincap.io/v2/assets/{currencyId}/markets";

            var response = await _httpClient.GetStringAsync(url);
            var data = JsonConvert.DeserializeObject<MarketResponse>(response);

            return data?.Data ?? new List<Market>();
        }

        public async Task<string> GetCurrencyIdByNameAsync(string currencyName)
        {
            string url = "https://api.coincap.io/v2/assets";

            var response = await _httpClient.GetStringAsync(url);
            var data = JsonConvert.DeserializeObject<CoinCapResponse>(response);

            return data?.Data.FirstOrDefault(c => c.Name.Equals(currencyName, StringComparison.OrdinalIgnoreCase))?.Id;
        }

        public async Task<List<CandleData>> GetHistoricalDataAsync(string currencyId)
        {
            string url = $"https://api.coincap.io/v2/assets/{currencyId}/history?interval=d1";

            var response = await _httpClient.GetStringAsync(url);
            var data = JsonConvert.DeserializeObject<HistoricalDataResponse>(response);

            return data?.Data.Select(q => new CandleData
            {
                Date = DateTime.Parse(q.Date),
                Open = q.PriceUsd,
                High = q.PriceUsd,
                Low = q.PriceUsd,
                Close = q.PriceUsd
            }).ToList();
        }
    }
}
