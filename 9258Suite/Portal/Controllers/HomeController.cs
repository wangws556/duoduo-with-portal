using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mime;

namespace Portal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NavigateTo(string route)
        {
            if (string.IsNullOrEmpty(route))
                return View("Index");
            return View(route);
        }

        public ActionResult About()
        {
            return View("About");
        }

        public FilePathResult DownLoadClient()
        {
            var path = Server.MapPath("~/Downloads/9258Setup.exe");
            return File(path, MediaTypeNames.Application.Octet, "9258Setup.exe");
        }
    }
}
