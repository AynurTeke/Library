using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class MembersController : Controller
    {
        // GET: Members
        public ActionResult Index()
        {
            Models.LibraryTableAdapters.MembersTableAdapter membersAdapter = new Models.LibraryTableAdapters.MembersTableAdapter();
            Models.Library.MembersDataTable membersTable = new Models.Library.MembersDataTable();

            membersAdapter.Fill(membersTable);
            ViewData["members"] = membersTable.Rows;
            ViewData["title"] = "Üyeler";
            return View();
        }
    }
}