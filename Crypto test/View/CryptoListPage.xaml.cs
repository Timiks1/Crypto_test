using Crypto_test.Model;
using Crypto_test.Model.CoinMarket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Crypto_test.View
{
    /// <summary>
    /// Логика взаимодействия для CryptoListPage.xaml
    /// </summary>
    public partial class CryptoListPage : Page
    {
        private readonly HttpClient _httpClient;
        public ObservableCollection<Currency> TopCurrencies { get; set; }
        private string ApiKey; // Ваш ключ CoinMarketCap

        public CryptoListPage()
        {
            InitializeComponent();
            TopCurrencies = new ObservableCollection<Currency>();
            _httpClient = new HttpClient();
            CryptoItemsControl.ItemsSource = TopCurrencies;
            ApiKey = ConfigurationManager.AppSettings["CoinMarketCapApiKey"];
            if (string.IsNullOrEmpty(ApiKey))
            {
                MessageBox.Show("API ключ не найден в конфигурации.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            LoadTopCurrencies();
        }


        private async Task LoadTopCurrencies()
        {
            try
            {
                // Настраиваем заголовки для CoinMarketCap API
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", ApiKey);
                _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

                // Выполняем запрос к API CoinMarketCap
                string url = "https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest?limit=20";
                var response = await _httpClient.GetStringAsync(url);

                // Десериализуем ответ
                var data = JsonConvert.DeserializeObject<CoinMarketCapResponse>(response);

                // Очищаем и добавляем полученные данные в коллекцию
                TopCurrencies.Clear();
                foreach (var currency in data.Data)
                {
                    TopCurrencies.Add(currency);
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибки
                Console.WriteLine($"Error loading top currencies: {ex.Message}");
            }
        }

        private void OnCurrencyCardClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.DataContext is Currency selectedCurrency)
            {
                var coinDetailsPage = new CoinDetailPage(selectedCurrency);
                NavigationService.Navigate(coinDetailsPage);
            }
        }
    }
}

