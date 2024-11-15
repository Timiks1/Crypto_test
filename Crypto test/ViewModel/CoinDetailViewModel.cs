using Crypto_test.Model.CoinCap;
using Crypto_test.Model.CoinMarket;
using Newtonsoft.Json;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Crypto_test.ViewModel
{
    public class CoinDetailViewModel : INotifyPropertyChanged
    {
        private readonly HttpClient _httpClient = new();

        public Currency Coin { get; set; }
        public List<Market> Markets { get; private set; }
        public PlotModel CandleStickModel { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public CoinDetailViewModel(Currency coin)
        {
            Coin = coin;
            Markets = new List<Market>();
        }

        public async Task InitializeCoinDataAsync()
        {
            var coinId = await GetCoinIdByName(Coin.name);

            if (!string.IsNullOrEmpty(coinId))
            {
                await LoadChartDataAsync(coinId);
                Markets = await GetMarketsForCoinAsync(coinId);
                OnPropertyChanged(nameof(Markets));
            }
        }

        private async Task<string> GetCoinIdByName(string coinName)
        {
            string url = "https://api.coincap.io/v2/assets";

            var response = await _httpClient.GetStringAsync(url);
            var data = JsonConvert.DeserializeObject<CoinCapResponse>(response);

            var coin = data.Data.FirstOrDefault(c => c.Name.Equals(coinName, StringComparison.OrdinalIgnoreCase));
            return coin?.Id;
        }

        private async Task<List<Market>> GetMarketsForCoinAsync(string coinId)
        {
            string url = $"https://api.coincap.io/v2/assets/{coinId}/markets";

            var response = await _httpClient.GetStringAsync(url);
            var data = JsonConvert.DeserializeObject<MarketResponse>(response);

            return data?.Data ?? new List<Market>();
        }

        private async Task<List<CandleData>> FetchHistoricalDataAsync(string coinId)
        {
            string url = $"https://api.coincap.io/v2/assets/{coinId}/history?interval=d1";

            var response = await _httpClient.GetStringAsync(url);
            var data = JsonConvert.DeserializeObject<HistoricalDataResponse>(response);

            return data?.Data.Select(q => new CandleData
            {
                Date = DateTime.Parse(q.Date),
                Open = q.PriceUsd,
                High = q.PriceUsd,
                Low = q.PriceUsd,
                Close = q.PriceUsd
            }).ToList();
        }

        private async Task LoadChartDataAsync(string coinId)
        {
            var historicalData = await FetchHistoricalDataAsync(coinId);

            if (historicalData != null)
            {
                var model = new PlotModel { Title = $"{Coin.name} Price Chart" };

                // Настройка осей
                model.Axes.Add(new DateTimeAxis
                {
                    Position = AxisPosition.Bottom,
                    StringFormat = "dd/MM",
                    Title = "Date",
                    IntervalType = DateTimeIntervalType.Days
                });

                model.Axes.Add(new LinearAxis
                {
                    Position = AxisPosition.Left,
                    Title = "Price (USD)"
                });

                // Добавление данных графика
                var lineSeries = new LineSeries
                {
                    Color = OxyColors.Red,
                    StrokeThickness = 2
                };

                foreach (var data in historicalData)
                {
                    lineSeries.Points.Add(new DataPoint(DateTimeAxis.ToDouble(data.Date), data.Close));
                }

                model.Series.Add(lineSeries);
                CandleStickModel = model;

                OnPropertyChanged(nameof(CandleStickModel));
            }
        }

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
