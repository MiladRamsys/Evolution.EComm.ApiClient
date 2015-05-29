using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolution.EComm.ApiClient
{
    public class PRODUCT
    {
        public int isc_code { get; set; }
        public string Description { get; set; }
        public string LongDescription { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool IsOnPromotion { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
