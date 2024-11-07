using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_test.Model.CoinCap
{
    public class HistoricalQuote
    {
        [JsonProperty("priceUsd")]
        public double PriceUsd { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }
    }
}
