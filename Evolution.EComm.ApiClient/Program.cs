using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Evolution.EComm.ApiClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (var cryptoProvider = new RNGCryptoServiceProvider())
            //{
            //    byte[] secretKeyByteArray = new byte[32]; //256 bit
            //    cryptoProvider.GetBytes(secretKeyByteArray);
            //    var APIKey = Convert.ToBase64String(secretKeyByteArray);
            //    var ID = Guid.NewGuid().ToString();
            //}
            RunAsync().Wait();
        }

        static async Task RunAsync()
        {

            Console.WriteLine("Calling the back-end API");
            //base URL
            //string apiBaseAddress = "http://VSNET/";
            string apiBaseAddress = "http://ramsys.cloudapp.net:1001/";

            CustomDelegatingHandler customDelegatingHandler = new CustomDelegatingHandler();

            HttpClient client = HttpClientFactory.Create(customDelegatingHandler);
            //ORDER HEAD
            EORDER order = new EORDER();
            order.order_ref = "test";
            //SALE LINES
            order.ESALEs = new List<ESALE>();
            order.EPAYs = new List<EPAY>();
            var sale = new ESALE();
            sale.location = "WHS";
            sale.pos_no = 0;
            sale.docket_no = 0;
            sale.trans_date = DateTime.Now;
            sale.sperson = "#RMS";
            sale.shipaddress = "";
            sale.addchg = false;
            sale.ret_ssp = "";
            sale.comment = "";
            sale.dcomment = "";
            sale.line_no = 0;
            sale.isc_code = 123;
            sale.qty = 2;
            sale.qty_rec = 2;
            sale.sale_value = 86.09M;
            sale.rew_points = 0.0M;
            sale.tax_value = 12.91M;
            sale.discount = 55.95M;
            sale.cost = 0;
            sale.cust_no = 0;
            sale.club = 65967;
            sale.reversal = 0M;
            sale.reversed = false;
            sale.proc_flag = false;
            sale.upd_date = DateTime.Now;
            sale.upd_file = "";
            sale.flybuy_no = "";
            sale.gift_no = "";
            sale.taxfree = false;
            sale.taxf_no = "";
            sale.excl_perf = false;
            sale.procflag = false;
            sale.club_ext = false;
            sale.clubgv = false;
            sale.couponsale = false;
            sale.promo = false;
            sale.sno = "";
            sale.secssp = "";
            sale.club_rew = false;
            sale.backedup = false;
            sale.prcode = "";
            sale.completed = DateTime.Now;
            sale.order_ref = "ORD0073295";
            //PAYMENT LINES
            var pay = new EPAY
            {
                amount = 1.0M,
                amnt_used = 1.1M,
                backedup = false,
                billtoken = "",
                card_no = "TEST",
                cardname = "TEST2",
                completed = true,
                CreatedOn = DateTime.Now,
                cust_no = 0,
                docket_no = 0,
                gift_no = "",
                line_no = 0,
                location = "",
                month_exp = 1,
                order_ref = "ORDE1",
                paytype = "EMC",
                pos_no = 0,
                procflag = false,
                @ref = "TEST",
                shipping_amount = 10M,
                trans_date = DateTime.Now,
                trn_type = "TEST",
                upd_date = DateTime.Now,
                year_exp = 1
            };
            order.EPAYs.Add(pay);
            order.ESALEs.Add(sale);
            //Post ORDER
            //HttpResponseMessage response = await client.PostAsJsonAsync(apiBaseAddress + "api/orders", order);
            string lastUpdate = DateTime.Now.AddDays(-10).ToString("dd-MM-yyyy hh:mm:ss");
            //GET an ORDER 
            HttpResponseMessage response = await client.GetAsync(apiBaseAddress + String.Format("api/orders/10"));
            //GET a product
            //HttpResponseMessage response = await client.GetAsync(apiBaseAddress + String.Format("api/Products/101621"));
            //GET a list of Products 
            // HttpResponseMessage response = await client.GetAsync(apiBaseAddress + String.Format("api/Products/GetProducts?last_update={0}", lastUpdate));
            // GET a list of stock levels sinc last update
            //HttpResponseMessage response = await client.GetAsync(apiBaseAddress + String.Format("api/Products/GetStockLevel?last_update={0}", lastUpdate));
            // GET a stock level for a products
            //HttpResponseMessage response = await client.GetAsync(apiBaseAddress + String.Format("api/Products/GetStockLevelForSKU/101621"));

            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseString);
                Console.WriteLine("HTTP Status: {0}, Reason {1}. Press ENTER to exit", response.StatusCode, response.ReasonPhrase);
            }
            else
            {
                Console.WriteLine("Failed to call the API. HTTP Status: {0}, Reason {1}", response.StatusCode, response.ReasonPhrase);
            }

            Console.ReadLine();
        }

        public class CustomDelegatingHandler : DelegatingHandler
        {
            //Obtained from the server earlier, APIKey MUST be stored securly and in App.Config
            private string APPId = "deb35c8a-67cf-460c-9eba-70980d3f6c7c";
            private string APIKey = "p8Pp6ksnF8LPlpgp/xZm2I/z4m30G72SpsG2eekKXaU=";

            protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {

                HttpResponseMessage response = null;
                string requestContentBase64String = string.Empty;

                string requestUri = System.Web.HttpUtility.UrlEncode(request.RequestUri.AbsoluteUri.ToLower());

                string requestHttpMethod = request.Method.Method;

                //Calculate UNIX time
                DateTime epochStart = new DateTime(1970, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc);
                TimeSpan timeSpan = DateTime.UtcNow - epochStart;
                string requestTimeStamp = Convert.ToUInt64(timeSpan.TotalSeconds).ToString();

                //create random nonce for each request
                string nonce = Guid.NewGuid().ToString("N");

                //Checking if the request contains body, usually will be null wiht HTTP GET and DELETE
                if (request.Content != null)
                {
                    byte[] content = await request.Content.ReadAsByteArrayAsync();
                    MD5 md5 = MD5.Create();
                    //Hashing the request body, any change in request body will result in different hash, we'll incure message integrity
                    byte[] requestContentHash = md5.ComputeHash(content);
                    requestContentBase64String = Convert.ToBase64String(requestContentHash);
                }

                //Creating the raw signature string
                string signatureRawData = String.Format("{0}{1}{2}{3}{4}{5}", APPId, requestHttpMethod, requestUri, requestTimeStamp, nonce, requestContentBase64String);

                var secretKeyByteArray = Convert.FromBase64String(APIKey);

                byte[] signature = Encoding.UTF8.GetBytes(signatureRawData);

                using (HMACSHA256 hmac = new HMACSHA256(secretKeyByteArray))
                {
                    byte[] signatureBytes = hmac.ComputeHash(signature);
                    string requestSignatureBase64String = Convert.ToBase64String(signatureBytes);
                    //Setting the values in the Authorization header using custom scheme (amx)
                    request.Headers.Authorization = new AuthenticationHeaderValue("amx", string.Format("{0}:{1}:{2}:{3}", APPId, requestSignatureBase64String, nonce, requestTimeStamp));
                }

                response = await base.SendAsync(request, cancellationToken);

                return response;
            }
        }

        private void GenerateAPPKey()
        {
            using (var cryptoProvider = new RNGCryptoServiceProvider())
            {
                byte[] secretKeyByteArray = new byte[32]; //256 bit
                cryptoProvider.GetBytes(secretKeyByteArray);
                var APIKey = Convert.ToBase64String(secretKeyByteArray);
            }
        }
    }
}
