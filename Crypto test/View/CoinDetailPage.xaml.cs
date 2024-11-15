using Crypto_test.Model.CoinMarket;
using Crypto_test.Repository;
using Crypto_test.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Crypto_test.View
{
    public partial class CoinDetailPage : Page
    {
        private readonly CoinDetailViewModel _viewModel;

        public CoinDetailPage(Currency currency)
        {
            InitializeComponent();

            // Создаём экземпляр репозитория
            var repository = new CurrencyRepository();

            // Передаём валюту и репозиторий в ViewModel
            _viewModel = new CoinDetailViewModel(currency, repository);
            DataContext = _viewModel;

            // Вызываем асинхронную инициализацию данных
            Loaded += async (s, e) => await _viewModel.InitializeAsync();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
            else
            {
                MessageBox.Show("Нет предыдущей страницы для возврата.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
