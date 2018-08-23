using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PLL.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Main()
        {

            return View();
        }

        public ActionResult Reg()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult ForgotPass()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Kononov()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Imanov()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Kovtunenko()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Position()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Resume()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}