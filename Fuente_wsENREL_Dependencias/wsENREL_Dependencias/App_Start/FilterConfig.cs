using System.Web;
using System.Web.Mvc;

namespace wsENREL_Dependencias
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}