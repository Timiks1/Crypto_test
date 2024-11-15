using Crypto_test.Model.CoinCap;
using Crypto_test.Model.CoinMarket;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Crypto_test.Repository
{
    public interface ICurrencyRepository
    {
        Task<IEnumerable<Currency>> GetCurrenciesAsync();
        Task<List<Market>> GetMarketsForCurrencyAsync(string currencyId);
        Task<string> GetCurrencyIdByNameAsync(string currencyName);
        Task<List<CandleData>> GetHistoricalDataAsync(string currencyId);
    }
}
