using Crypto_test.Model.CoinMarket;
using Crypto_test.ViewModel;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Crypto_test.View
{
    public partial class CryptoListPage : Page
    {
        private readonly CryptoListPageViewModel _viewModel;

        public CryptoListPage()
        {
            InitializeComponent();

            // Инициализация ViewModel
            _viewModel = new CryptoListPageViewModel();
            DataContext = _viewModel;

            // Загрузка данных
            var apiKey = ConfigurationManager.AppSettings["CoinMarketCapApiKey"];
            if (string.IsNullOrEmpty(apiKey))
            {
                MessageBox.Show("API ключ не найден в конфигурации.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _ = _viewModel.LoadCurrenciesAsync(apiKey);
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.FilterCurrencies(SearchTextBox.Text);
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
            var conversionPage = new ConversionPage(_viewModel.TopCurrencies);
            NavigationService.Navigate(conversionPage);
        }
    }
}
