using Crypto_test.Model;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Crypto_test.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly HttpClient _httpClient = new HttpClient();
        public ObservableCollection<Currency> TopCurrencies { get; set; }

        public MainViewModel()
        {
            TopCurrencies = new ObservableCollection<Currency>();
            LoadTopCurrencies();


        }

        private async Task LoadTopCurrencies()
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Add("User-Agent", "CryptoApp/1.0");
                var response = await _httpClient.GetStringAsync("https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&order=market_cap_desc&per_page=10&page=1&sparkline=false");

                var data = JsonConvert.DeserializeObject<List<Currency>>(response);

                TopCurrencies.Clear();
                foreach (var currency in data)
                {
                    TopCurrencies.Add(currency);
                }
            }
            catch (Exception ex) { }
        }

    }
}
