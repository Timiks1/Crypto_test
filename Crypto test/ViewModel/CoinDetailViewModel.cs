using Crypto_test.Model.CoinMarket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_test.ViewModel
{
    public class CoinDetailViewModel
    {
        public Currency Coin { get; set; }
        public List<Market> Markets { get; set; }

        public CoinDetailViewModel(Currency coin)
        {
            Coin = coin;
            Markets = new List<Market>();
        }
    }
}
