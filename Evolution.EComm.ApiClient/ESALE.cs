using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolution.EComm.ApiClient
{
    public partial class ESALE
    {
        public int Id { get; set; }
        public string location { get; set; }
        public Nullable<int> pos_no { get; set; }
        public Nullable<int> docket_no { get; set; }
        public System.DateTime trans_date { get; set; }
        public string sperson { get; set; }
        public string shipaddress { get; set; }
        public Nullable<bool> addchg { get; set; }
        public string ret_ssp { get; set; }
        public string comment { get; set; }
        public string dcomment { get; set; }
        public Nullable<int> line_no { get; set; }
        public long isc_code { get; set; }
        public decimal qty { get; set; }
        public Nullable<decimal> qty_rec { get; set; }
        public decimal sale_value { get; set; }
        public Nullable<decimal> rew_points { get; set; }
        public decimal tax_value { get; set; }
        public decimal discount { get; set; }
        public decimal cost { get; set; }
        public Nullable<int> cust_no { get; set; }
        public long club { get; set; }
        public Nullable<decimal> reversal { get; set; }
        public Nullable<bool> reversed { get; set; }
        public Nullable<bool> proc_flag { get; set; }
        public System.DateTime upd_date { get; set; }
        public string upd_file { get; set; }
        public string flybuy_no { get; set; }
        public string gift_no { get; set; }
        public bool taxfree { get; set; }
        public string taxf_no { get; set; }
        public Nullable<bool> excl_perf { get; set; }
        public Nullable<bool> procflag { get; set; }
        public Nullable<bool> club_ext { get; set; }
        public Nullable<bool> clubgv { get; set; }
        public Nullable<bool> couponsale { get; set; }
        public Nullable<bool> promo { get; set; }
        public string sno { get; set; }
        public string secssp { get; set; }
        public Nullable<bool> club_rew { get; set; }
        public Nullable<bool> backedup { get; set; }
        public string prcode { get; set; }
        public System.DateTime completed { get; set; }
        public string order_ref { get; set; }
        public string status { get; set; }
        public string pcode { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public int EORDERID { get; set; }

        public virtual EORDER EORDER { get; set; }
        public virtual ESALE ESALE1 { get; set; }
        public virtual ESALE ESALE2 { get; set; }
    }
}
