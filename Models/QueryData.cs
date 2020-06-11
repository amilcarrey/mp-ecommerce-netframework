using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mp_ecommerce_netframework.Models
{
    public class QueryData
    {
        public string collection_id { get; set; }
        public string collection_status { get; set; }
        public string external_reference { get; set; }
        public string payment_type { get; set; }
        public string preference_id { get; set; }
        public string site_id { get; set; }
        public string processing_mode { get; set; }
        public string merchant_account_id { get; set; }
    }
}
