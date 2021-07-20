using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONParser
{
    class Transaction
    {
        public string TransactionNumber { set; get; }
        public string Location { set; get; }
        public string Date { set; get; }
        public string Method { set; get; }
        public string Name { set; get; }
        public string Brand { set; get; }
        public string LastFour { set; get; }
        public string Amount { set; get; }
        public string AuthCode { set; get; }
        public string Status { set; get; }
    }
}
