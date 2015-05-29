using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolution.EComm.ApiClient
{
    public partial class EPAY
    {
        public int Id { get; set; }
        public string location { get; set; }
        public Nullable<int> pos_no { get; set; }
        public Nullable<int> docket_no { get; set; }
        public System.DateTime trans_date { get; set; }
        public int line_no { get; set; }
        public string paytype { get; set; }
        public string card_no { get; set; }
        public Nullable<int> month_exp { get; set; }
        public Nullable<int> year_exp { get; set; }
        public string billtoken { get; set; }
        public decimal amount { get; set; }
        public Nullable<decimal> shipping_amount { get; set; }
        public Nullable<decimal> amnt_used { get; set; }
        public string @ref { get; set; }
        public Nullable<System.DateTime> upd_date { get; set; }
        public Nullable<int> cust_no { get; set; }
        public string order_ref { get; set; }
        public string gift_no { get; set; }
        public Nullable<bool> procflag { get; set; }
        public string cardname { get; set; }
        public Nullable<bool> backedup { get; set; }
        public Nullable<bool> completed { get; set; }
        public string trn_type { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public int EORDERID { get; set; }

        public virtual EORDER EORDER { get; set; }
    }
}
