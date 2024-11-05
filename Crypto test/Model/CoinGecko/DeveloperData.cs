using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_test.Model.CoinGecko
{
    public class DeveloperData
    {
        public int Forks { get; set; }
        public int Stars { get; set; }
        public int Subscribers { get; set; }
        public int TotalIssues { get; set; }
        public int ClosedIssues { get; set; }
        public int PullRequestsMerged { get; set; }
        public int PullRequestContributors { get; set; }
    }

}
