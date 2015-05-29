using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolution.EComm.ApiClient
{
    public class STOCKLEVEL
    {
        public int ISC_CODE { get; set; }
        public decimal Qty { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
