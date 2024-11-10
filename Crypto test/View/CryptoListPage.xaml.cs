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
    public partial class CryptoListPage : Page
    {
        private readonly HttpClient _httpClient;
        public ObservableCollection<Currency> TopCurrencies { get; set; }
        public ObservableCollection<Currency> FilteredCurrencies { get; set; } // Отфильтрованный список
        private string ApiKey;

        public CryptoListPage()
        {
            InitializeComponent();
            TopCurrencies = new ObservableCollection<Currency>();
            FilteredCurrencies = new ObservableCollection<Currency>(); // Инициализация отфильтрованной коллекции
            _httpClient = new HttpClient();
            CryptoItemsControl.ItemsSource = FilteredCurrencies;
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
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", ApiKey);
                _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

                // Убираем лимит, чтобы получить все валюты
                string url = "https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest";
                var response = await _httpClient.GetStringAsync(url);

                var data = JsonConvert.DeserializeObject<CoinMarketCapResponse>(response);

                TopCurrencies.Clear();
                FilteredCurrencies.Clear();

                // Сохраняем все валюты в TopCurrencies
                foreach (var currency in data.Data)
                {
                    TopCurrencies.Add(currency);
                }

                // Отображаем только первые 20 валют в FilteredCurrencies
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


        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filterText = SearchTextBox.Text.ToLower();
            FilteredCurrencies.Clear();

            if (string.IsNullOrEmpty(filterText))
            {
                // Если поле ввода пустое, показываем первые 20 монет
                foreach (var currency in TopCurrencies.Take(20))
                {
                    FilteredCurrencies.Add(currency);
                }
            }
            else
            {
                // Фильтруем список по введённому тексту
                foreach (var currency in TopCurrencies)
                {
                    if (currency.name.ToLower().Contains(filterText) || currency.symbol.ToLower().Contains(filterText))
                    {
                        FilteredCurrencies.Add(currency);
                    }
                }
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
        private void OpenConversionPage_Click(object sender, RoutedEventArgs e)
        {
            // Переход на страницу конвертации и передача списка валют
            var conversionPage = new ConversionPage(TopCurrencies);
            NavigationService.Navigate(conversionPage);
        }

    }
}
