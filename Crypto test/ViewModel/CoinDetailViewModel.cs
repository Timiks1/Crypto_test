using Crypto_test.Model.CoinMarket;
using Crypto_test.Repository;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.ComponentModel;


namespace Crypto_test.ViewModel
{
    public class CoinDetailViewModel : INotifyPropertyChanged
    {
        private readonly ICurrencyRepository _repository;

        private List<Market> _markets;
        private PlotModel _candleStickModel;

        public Currency Coin { get; set; }

        public List<Market> Markets
        {
            get => _markets;
            set
            {
                _markets = value;
                OnPropertyChanged(nameof(Markets));
            }
        }

        public PlotModel CandleStickModel
        {
            get => _candleStickModel;
            set
            {
                _candleStickModel = value;
                OnPropertyChanged(nameof(CandleStickModel));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public CoinDetailViewModel(Currency coin, ICurrencyRepository repository)
        {
            Coin = coin;
            _repository = repository;
            Markets = new List<Market>();
        }

        public async Task InitializeAsync()
        {
            string coinId = await _repository.GetCurrencyIdByNameAsync(Coin.name);
            if (string.IsNullOrEmpty(coinId))
            {
                Console.WriteLine("Ошибка: ID монеты не найден.");
                return;
            }

            Markets = await _repository.GetMarketsForCurrencyAsync(coinId);
            if (Markets == null || Markets.Count == 0)
            {
                Console.WriteLine("Ошибка: Рынки не найдены.");
            }

            var historicalData = await _repository.GetHistoricalDataAsync(coinId);
            if (historicalData == null || historicalData.Count == 0)
            {
                Console.WriteLine("Ошибка: Исторические данные не найдены.");
                return;
            }

            var model = new PlotModel { Title = $"{Coin.name} Price Chart" };
            model.Axes.Add(new DateTimeAxis { Position = AxisPosition.Bottom, StringFormat = "dd/MM", Title = "Date" });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Price (USD)" });

            var series = new LineSeries { Title = "Price", StrokeThickness = 2 };
            foreach (var data in historicalData)
            {
                series.Points.Add(new DataPoint(DateTimeAxis.ToDouble(data.Date), data.Close));
            }

            model.Series.Add(series);
            CandleStickModel = model;
        }

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
