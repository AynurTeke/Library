using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class MainController : LibraryController
    {
        // GET: Main
        public ActionResult Index()
        {
            ViewData["title"] = "Ana Sayfa";
            return View();
        }
    }
}