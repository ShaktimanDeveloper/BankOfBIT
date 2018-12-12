using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankOfBIT_ArshdeepSangha.Controllers
{
    /// <summary>
    /// Modified index page message.
    /// </summary>
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Online Banking for the Bank of BIT Clients";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Bank of BIT Application";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Find our contact Information here.";

            return View();
        }
    }
}
