using Crypto_test.Model.CoinMarket;
using Crypto_test.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Crypto_test.View
{
    public partial class ConversionPage : Page
    {
        private readonly ConversionPageViewModel _viewModel;

        public ConversionPage(ObservableCollection<Currency> currencies)
        {
            InitializeComponent();
            _viewModel = new ConversionPageViewModel(currencies);
            DataContext = _viewModel;
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

        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Convert();
        }
    }
}
