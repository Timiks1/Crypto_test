using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_test.Model
{
    public class ApiResponse<T>
    {
        public List<T> Data { get; set; }
        public object Meta { get; set; }
    }
}
