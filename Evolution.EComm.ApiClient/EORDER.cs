using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolution.EComm.ApiClient
{
    public partial class EORDER
    {
        public EORDER()
        {
            this.EPAYs = new HashSet<EPAY>();
            this.ESALEs = new HashSet<ESALE>();
        }

        public int Id { get; set; }
        public string order_ref { get; set; }


        public virtual ICollection<EPAY> EPAYs { get; set; }
        public virtual ICollection<ESALE> ESALEs { get; set; }
    }
}
