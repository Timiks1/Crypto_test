using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_test.Model.CoinMarket
{
    public class Market
    {
        public string exchangeId { get; set; }
        public string baseId { get; set; }
        public string quoteId { get; set; }
        public string baseSymbol { get; set; }
        public string quoteSymbol { get; set; }
        public string volumeUsd24Hr { get; set; }
        public string priceUsd { get; set; }
        public string volumePercent { get; set; }
    }
}
