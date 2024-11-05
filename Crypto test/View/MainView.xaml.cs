using Crypto_test.Model;
using Crypto_test.Model.CoinGecko;
using Crypto_test.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Crypto_test.View
{
    /// <summary>
    /// Логика взаимодействия для MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {

            InitializeComponent();

            CryptoListView.MouseDoubleClick += CryptoListView_MouseDoubleClick;

        }
        private void ToggleTheme_Click(object sender, RoutedEventArgs e)
        {
            var theme = Application.Current.Resources.MergedDictionaries[0].Source.ToString();
            if (theme.Contains("LightTheme"))
            {
                Application.Current.Resources.MergedDictionaries[0].Source = new Uri("View/Themes/DarkTheme.xaml", UriKind.Relative);
            }
            else
            {
                Application.Current.Resources.MergedDictionaries[0].Source = new Uri("View/Themes/LightTheme.xaml", UriKind.Relative);
            }
        }

        private async void CryptoListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (CryptoListView.SelectedItem is Currency selectedCurrency)
            {
                // Открываем новое окно и передаем ID монеты для загрузки данных
                var detailWindow = new CoinDetailView();
                var coinDetails = await LoadCoinDetailsAsync(selectedCurrency.Id);
                if (coinDetails != null)
                {
                    detailWindow.DataContext = coinDetails;
                    detailWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Не удалось загрузить данные для выбранной монеты.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Метод для загрузки данных с API CoinGecko
        private async Task<CoinDetail> LoadCoinDetailsAsync(string coinId)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = $"https://api.coingecko.com/api/v3/coins/{coinId}";
                    var response = await client.GetStringAsync(url);
                    return JsonConvert.DeserializeObject<CoinDetail>(response);
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
