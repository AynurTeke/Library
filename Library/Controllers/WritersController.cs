using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class WritersController : LibraryController
    {
        public ActionResult Index()
        {
            Models.LibraryTableAdapters.WritesTableAdapter writersAdapter = new Models.LibraryTableAdapters.WritesTableAdapter();
            Models.Library.WritesDataTable writersTable = new Models.Library.WritesDataTable();
            writersAdapter.Fill(writersTable);
            ViewData["writers"] = writersTable.Rows;
            ViewData["title"] = "Yazarlar";
            return View();
        }
        public ActionResult Add(string error = null)
        {
            ViewData["title"] = "Yazar Ekle";
            if (error != null)
            {
                ViewData["error"] = error;
            }
            return View();
        }
        public void ProcessNewWriter(long id, string writerName, string surname, string AKA)
        {
            Models.LibraryTableAdapters.WritesTableAdapter writersAdapter = new Models.LibraryTableAdapters.WritesTableAdapter();

            if (writerName == null)
            {
                Response.Redirect("~/writers/add");
                return;
            }
            writerName = writerName.Trim();
            if (writerName == "")
            {
                Response.Redirect("~/writers/add");
                return;
            }
            if (writerName.Length > 100)
            {
                Response.Redirect("~/writers/add");
                return;
            }
            if (surname != null)
            {
                surname = surname.Trim();
                if (surname == "")
                {
                    surname = null;
                }
                else
                {
                    if (surname.Length > 100)
                    {
                        Response.Redirect("~/writers/add");
                        return;
                    }
                }
            }
            if (AKA != null)
            {
                AKA = AKA.Trim();
                if (AKA == "")
                {
                    AKA = null;
                }
                else
                {
                    if (AKA.Length > 50)
                    {
                        Response.Redirect("~/writers/add");
                        return;
                    }
                }
            }
            try
            {
                writersAdapter.AddWriter(writerName, surname, AKA);
                Response.Redirect("~/writers");
            }
            catch
            {
                Response.Redirect("~/writers/add");
            }
        }
        public void Delete(long id)
        {
            Models.LibraryTableAdapters.WritesTableAdapter writersAdapter = new Models.LibraryTableAdapters.WritesTableAdapter();
            writersAdapter.DeleteWriters(id);
            Response.Redirect("~/writers");
        }
        public ActionResult Update(long id)
        {
            Models.LibraryTableAdapters.WritesTableAdapter writersAdapter = new Models.LibraryTableAdapters.WritesTableAdapter();
            Models.Library.WritesDataTable writersTable = new Models.Library.WritesDataTable();
            Models.Library.WritesRow writersRow;

            writersAdapter.FillById(writersTable, id);
            writersRow = (Models.Library.WritesRow)writersTable.Rows[0];
            ViewData["title"] = writersRow.writersName;
            ViewData["writerRow"] = writersRow;
            return View();
        }
        public void ProcessWriter(long id, string writerName, string writerSurname, string AKA)
        {
            Models.LibraryTableAdapters.WritesTableAdapter writersAdapter = new Models.LibraryTableAdapters.WritesTableAdapter();

            if (writerName == null)
            {
                Response.Redirect("~/writers/update/" + id.ToString());
                return;
            }
            writerName = writerName.Trim();
            if (writerName == "")
            {
                Response.Redirect("~/writers/update/" + id.ToString());
                return;
            }
            if (writerName.Length > 100)
            {
                Response.Redirect("~/writers/update/" + id.ToString());
                return;
            }

            if (writerSurname != null)
            {
                writerSurname = writerSurname.Trim();
                if (writerSurname == "")
                {
                    writerSurname = null;
                }
                else
                {
                    if (writerSurname.Length > 100)
                    {
                        Response.Redirect("~/writers/update");
                        return;
                    }
                }
            }
            if (AKA != null)
            {
                AKA = AKA.Trim();
                if (AKA == "")
                {
                    AKA = null;
                }
                else
                {
                    if (AKA.Length > 50)
                    {
                        Response.Redirect("~/writers/update");
                        return;
                    }
                }
            }
            try
            {
                writersAdapter.UpdateWriter(writerName, writerSurname, AKA, id);
                Response.Redirect("~/writers");
            }
            catch
            {
                Response.Redirect("~/writers/update");
            }
        }
    }
}