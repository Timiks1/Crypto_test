using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_test.Model.CoinGecko
{
    public class MarketData
    {
        public Price CurrentPrice { get; set; }
        public Price Ath { get; set; }
        public Price Atl { get; set; }
        public double PriceChangePercentage24H { get; set; }
    }
}
