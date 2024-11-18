using Crypto_test.Model.CoinMarket;
using Crypto_test.Repository;
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

            var repository = new CurrencyRepository();
            _viewModel = new CryptoListPageViewModel(repository);

            DataContext = _viewModel;

            Loaded += async (s, e) => await _viewModel.LoadCurrenciesAsync();
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
        private void SetLightTheme_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current as App)?.ChangeTheme("Light");
        }

        private void SetDarkTheme_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current as App)?.ChangeTheme("Dark");
        }

    }
}
