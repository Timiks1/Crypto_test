using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Crypto_test.Model.CoinGecko
{
    public class CoinDetail
    {
        public string Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string HashingAlgorithm { get; set; }
        public int? BlockTimeInMinutes { get; set; }
        public string GenesisDate { get; set; }
        public string CountryOrigin { get; set; }
        public double SentimentVotesUpPercentage { get; set; }
        public double SentimentVotesDownPercentage { get; set; }
        public int MarketCapRank { get; set; }
        public MarketData MarketData { get; set; }
        public CommunityData CommunityData { get; set; }
        public DeveloperData DeveloperData { get; set; }
        public Links Links { get; set; }
        public Image Image { get; set; }
    }
}
