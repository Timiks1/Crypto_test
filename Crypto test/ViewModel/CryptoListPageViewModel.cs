﻿using Crypto_test.Model.CoinMarket;
using Crypto_test.Repository;
using System.Collections.ObjectModel;

namespace Crypto_test.ViewModel
{
    public class CryptoListPageViewModel
    {
        private readonly ICurrencyRepository _repository;

        public ObservableCollection<Currency> TopCurrencies { get; private set; }
        public ObservableCollection<Currency> FilteredCurrencies { get; private set; }

        public CryptoListPageViewModel(ICurrencyRepository repository)
        {
            _repository = repository;
            TopCurrencies = new ObservableCollection<Currency>();
            FilteredCurrencies = new ObservableCollection<Currency>();
        }

        public async Task LoadCurrenciesAsync()
        {
            var currencies = await _repository.GetCurrenciesAsync();

            TopCurrencies.Clear();
            foreach (var currency in currencies)
            {
                TopCurrencies.Add(currency);
            }

            FilteredCurrencies.Clear();
            foreach (var currency in TopCurrencies.Take(20))
            {
                FilteredCurrencies.Add(currency);
            }
        }

        public void FilterCurrencies(string searchText)
        {
            FilteredCurrencies.Clear();

            if (string.IsNullOrEmpty(searchText))
            {
                foreach (var currency in TopCurrencies.Take(20))
                {
                    FilteredCurrencies.Add(currency);
                }
            }
            else
            {
                var filtered = TopCurrencies
                    .Where(c => c.name.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                                c.symbol.Contains(searchText, StringComparison.OrdinalIgnoreCase));

                foreach (var currency in filtered)
                {
                    FilteredCurrencies.Add(currency);
                }
            }
        }
    }
}
