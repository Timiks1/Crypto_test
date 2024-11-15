using Crypto_test.Model.CoinMarket;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Crypto_test.ViewModel
{
    public class ConversionPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Currency> _currencies;
        private Currency _fromCurrency;
        private Currency _toCurrency;
        private string _amountText;
        private string _conversionResult;
        public ICommand ConvertCommand { get; }

        public ObservableCollection<Currency> Currencies
        {
            get => _currencies;
            set { _currencies = value; OnPropertyChanged(nameof(Currencies)); }
        }

        public Currency FromCurrency
        {
            get => _fromCurrency;
            set { _fromCurrency = value; OnPropertyChanged(nameof(FromCurrency)); }
        }

        public Currency ToCurrency
        {
            get => _toCurrency;
            set { _toCurrency = value; OnPropertyChanged(nameof(ToCurrency)); }
        }

        public string AmountText
        {
            get => _amountText;
            set { _amountText = value; OnPropertyChanged(nameof(AmountText)); }
        }

        public string ConversionResult
        {
            get => _conversionResult;
            set { _conversionResult = value; OnPropertyChanged(nameof(ConversionResult)); }
        }

        public ConversionPageViewModel(ObservableCollection<Currency> currencies)
        {
            Currencies = currencies;
            ConvertCommand = new RelayCommand(Convert);
        }

        public void Convert()
        {
            if (FromCurrency == null || ToCurrency == null || string.IsNullOrEmpty(AmountText))
            {
                ConversionResult = "Пожалуйста, выберите валюты и введите сумму для конвертации.";
                return;
            }

            if (!decimal.TryParse(AmountText, out decimal amount))
            {
                ConversionResult = "Введите корректное числовое значение для суммы.";
                return;
            }

            decimal fromRate = (decimal)FromCurrency.quote.USD.price;
            decimal toRate = (decimal)ToCurrency.quote.USD.price;
            decimal convertedAmount = (amount * fromRate) / toRate;

            ConversionResult = $"{amount} {FromCurrency.symbol} = {Math.Round(convertedAmount, 2)} {ToCurrency.symbol}";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
