using Newtonsoft.Json;
using System;

namespace Crypto_test.Model
{
    public class Currency
    {
        public string Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }

        [JsonProperty("current_price")]
        public decimal CurrentPrice { get; set; }

        [JsonProperty("market_cap")]
        public long MarketCap { get; set; }

        [JsonProperty("market_cap_rank")]
        public int MarketCapRank { get; set; }

        [JsonProperty("price_change_percentage_24h")]
        public decimal? PriceChangePercentage24h { get; set; }

        // Дополнительные свойства для более полного соответствия ответу API
        [JsonProperty("total_volume")]
        public long TotalVolume { get; set; }

        [JsonProperty("high_24h")]
        public decimal High24h { get; set; }

        [JsonProperty("low_24h")]
        public decimal Low24h { get; set; }

        [JsonProperty("price_change_24h")]
        public decimal? PriceChange24h { get; set; }

        [JsonProperty("market_cap_change_24h")]
        public long? MarketCapChange24h { get; set; }

        [JsonProperty("market_cap_change_percentage_24h")]
        public decimal? MarketCapChangePercentage24h { get; set; }

        [JsonProperty("circulating_supply")]
        public decimal? CirculatingSupply { get; set; }

        [JsonProperty("total_supply")]
        public decimal? TotalSupply { get; set; }

        [JsonProperty("max_supply")]
        public decimal? MaxSupply { get; set; }

        [JsonProperty("ath")]
        public decimal? Ath { get; set; }

        [JsonProperty("ath_change_percentage")]
        public decimal? AthChangePercentage { get; set; }

        [JsonProperty("ath_date")]
        public DateTime? AthDate { get; set; }

        [JsonProperty("atl")]
        public decimal? Atl { get; set; }

        [JsonProperty("atl_change_percentage")]
        public decimal? AtlChangePercentage { get; set; }

        [JsonProperty("atl_date")]
        public DateTime? AtlDate { get; set; }

        [JsonProperty("roi")]
        public object Roi { get; set; } // Если значение всегда null, можно оставить object или nullable тип

        [JsonProperty("last_updated")]
        public DateTime? LastUpdated { get; set; }
    }
}
