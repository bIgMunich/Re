using Re.Models;
using System.Web;
using System.Web.Mvc;

namespace Re
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new UserAuthAttribute());
        }
    }
}