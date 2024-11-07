using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_test.Model.CoinMarket
{
    public class USD
    {
        public double price { get; set; }
        public double volume_24h { get; set; }
        public double volume_change_24h { get; set; }
        public double percent_change_1h { get; set; }
        public double percent_change_24h { get; set; }
        public double percent_change_7d { get; set; }
        public double percent_change_30d { get; set; }
        public double percent_change_60d { get; set; }
        public double percent_change_90d { get; set; }
        public double market_cap { get; set; }
        public double market_cap_dominance { get; set; }
        public double fully_diluted_market_cap { get; set; }
        public object tvl { get; set; }
        public DateTime last_updated { get; set; }
    }
}
