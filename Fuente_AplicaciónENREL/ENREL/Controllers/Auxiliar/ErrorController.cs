using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace ENREL.Controllers.Auxiliar
{
    [HandleError]
    [OutputCache(Location=OutputCacheLocation.None)]
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HttpError403()
        {
            return View();
        }

        public ActionResult HttpError404()
        {
            return View();
        }

        public ActionResult HttpError500(string error)
        {
            return View();
        }

        public ActionResult HttpError502(string error)
        {
            return View();
        }

        public ActionResult HttpError503(string error)
        {
            return View();
        }
	}
}