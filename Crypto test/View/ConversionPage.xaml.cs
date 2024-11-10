using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Crypto_test.Model;
using Crypto_test.Model.CoinMarket;

namespace Crypto_test.View
{
    public partial class ConversionPage : Page
    {
        public ObservableCollection<Currency> Currencies { get; set; }

        public ConversionPage(ObservableCollection<Currency> currencies)
        {
            InitializeComponent();
            Currencies = currencies;
            DataContext = this;
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
            // Проверка на корректность ввода
            if (FromCurrencyComboBox.SelectedItem == null || ToCurrencyComboBox.SelectedItem == null || string.IsNullOrEmpty(AmountTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, выберите валюты и введите сумму для конвертации.");
                return;
            }

            if (!decimal.TryParse(AmountTextBox.Text, out decimal amount))
            {
                MessageBox.Show("Введите корректное числовое значение для суммы.");
                return;
            }

            // Получаем выбранные валюты
            var fromCurrency = FromCurrencyComboBox.SelectedItem as Currency;
            var toCurrency = ToCurrencyComboBox.SelectedItem as Currency;

            if (fromCurrency == null || toCurrency == null)
                return;

            // Выполняем конвертацию на основе текущего курса
            decimal fromRate = (decimal)fromCurrency.quote.USD.price;
            decimal toRate = (decimal)toCurrency.quote.USD.price;
            decimal convertedAmount = (amount * fromRate) / toRate;

            // Отображаем результат
            ResultTextBlock.Text = $"{amount} {fromCurrency.symbol} = {Math.Round(convertedAmount, 2)} {toCurrency.symbol}";
        }

    }
}
