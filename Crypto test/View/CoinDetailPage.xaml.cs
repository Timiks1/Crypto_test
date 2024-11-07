using Crypto_test.Model.CoinMarket;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot;
using OxyPlot.Series;
using Crypto_test.Model.CoinCap;

namespace Crypto_test.View
{
    /// <summary>
    /// Логика взаимодействия для CoinDetailPage.xaml
    /// </summary>
    public partial class CoinDetailPage : Page
    {
        public PlotModel CandleStickModel { get; private set; }

        public CoinDetailPage(Currency currency)
        {
            InitializeComponent();
            DataContext = this;
            DataContext = currency; // Устанавливаем объект Currency как DataContext
            InitializeCoinData(currency.name);

        }
        private async void InitializeCoinData(string coinName)
        {
            // Получаем ID монеты по её имени
            string coinId = await GetCoinIdByName(coinName);

            if (!string.IsNullOrEmpty(coinId))
            {
                // Загружаем и отображаем данные графика
                LoadChartData(coinId);
            }
            else
            {
                MessageBox.Show($"Монета с именем {coinName} не найдена.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
        public async Task<string> GetCoinIdByName(string coinName)
        {
            using var client = new HttpClient();
            string url = "https://api.coincap.io/v2/assets";

            var response = await client.GetStringAsync(url);
            var data = JsonConvert.DeserializeObject<CoinCapResponse>(response);

            var coin = data.Data.FirstOrDefault(c => c.Name.Equals(coinName, StringComparison.OrdinalIgnoreCase));
            return coin?.Id;  // Вернет id монеты или null, если не найдено
        }
        public async Task<List<CandleData>> FetchHistoricalData(string coinId)
        {
            using var client = new HttpClient();
            string url = $"https://api.coincap.io/v2/assets/{coinId}/history?interval=d1";

            var response = await client.GetStringAsync(url);
            var data = JsonConvert.DeserializeObject<HistoricalDataResponse>(response);

            return data?.Data.Select(q => new CandleData
            {
                Date = DateTime.Parse(q.Date),
                Open = q.PriceUsd,  // CoinCap API не возвращает ohlc напрямую
                High = q.PriceUsd,
                Low = q.PriceUsd,
                Close = q.PriceUsd
            }).ToList();
        }
        private async void LoadChartData(string coinId)
        {
            var historicalData = await FetchHistoricalData(coinId);

            if (historicalData != null)
            {
                CandleStickModel = new PlotModel { Title = "Price Chart" };

                // Настройка осей
                CandleStickModel.Axes.Add(new DateTimeAxis
                {
                    Position = AxisPosition.Bottom,
                    StringFormat = "dd/MM",
                    Title = "Date",
                    IntervalType = DateTimeIntervalType.Days
                });

                CandleStickModel.Axes.Add(new LinearAxis
                {
                    Position = AxisPosition.Left,
                    Title = "Price (USD)"
                });

                // Используем LineSeries для создания непрерывной линии
                var lineSeries = new LineSeries
                {
                    Color = OxyColors.Red, // Цвет линии
                    StrokeThickness = 2    // Толщина линии
                };

                foreach (var data in historicalData)
                {
                    lineSeries.Points.Add(new DataPoint(DateTimeAxis.ToDouble(data.Date), data.Close));
                }

                CandleStickModel.Series.Add(lineSeries);
                CandleStickChart.Model = CandleStickModel;
            }
        }

    }
}
