using System.Web;
using System.Web.Mvc;

namespace mp_ecommerce_netframework
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
