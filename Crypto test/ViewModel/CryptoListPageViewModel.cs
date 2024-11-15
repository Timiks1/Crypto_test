using Crypto_test.Model.CoinMarket;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Crypto_test.ViewModel
{
    public class CryptoListPageViewModel : INotifyPropertyChanged
    {
        private readonly HttpClient _httpClient;

        public ObservableCollection<Currency> TopCurrencies { get; set; }
        public ObservableCollection<Currency> FilteredCurrencies { get; set; }

        public CryptoListPageViewModel()
        {
            TopCurrencies = new ObservableCollection<Currency>();
            FilteredCurrencies = new ObservableCollection<Currency>();
            _httpClient = new HttpClient();
        }

        public async Task LoadCurrenciesAsync(string apiKey)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", apiKey);
                _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

                string url = "https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest";
                var response = await _httpClient.GetStringAsync(url);

                var data = JsonConvert.DeserializeObject<CoinMarketCapResponse>(response);

                TopCurrencies.Clear();
                FilteredCurrencies.Clear();

                foreach (var currency in data.Data)
                {
                    TopCurrencies.Add(currency);
                }

                // Показать первые 20 валют
                foreach (var currency in TopCurrencies.Take(20))
                {
                    FilteredCurrencies.Add(currency);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading top currencies: {ex.Message}");
            }
        }

        public void FilterCurrencies(string searchText)
        {
            FilteredCurrencies.Clear();

            if (string.IsNullOrEmpty(searchText))
            {
                foreach (var currency in TopCurrencies.Take(20))
                {
                    FilteredCurrencies.Add(currency);
                }
            }
            else
            {
                foreach (var currency in TopCurrencies)
                {
                    if (currency.name.ToLower().Contains(searchText.ToLower()) || currency.symbol.ToLower().Contains(searchText.ToLower()))
                    {
                        FilteredCurrencies.Add(currency);
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
