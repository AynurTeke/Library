using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class LibraryController : Controller
    {
        public LibraryController()
        {
            if (System.Web.HttpContext.Current.Session["userId"] == null)
            {
                System.Web.HttpContext.Current.Response.Redirect("~/Home/Login");
            }
        }
    }
}