using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class BooksController : LibraryController
    {
        public ActionResult Index()
        {
            Models.LibraryTableAdapters.booksTableAdapter booksAdapter = new Models.LibraryTableAdapters.booksTableAdapter();
            Models.Library.booksDataTable booksTable = new Models.Library.booksDataTable();
            booksAdapter.Fill(booksTable);
            ViewData["books"] = booksTable.Rows;
            ViewData["title"] = "Kitaplar";
            return View();
        }
        public ActionResult Add(string error = null)
        {
            Models.LibraryTableAdapters.WritesTableAdapter writersAdapter = new Models.LibraryTableAdapters.WritesTableAdapter();
            Models.Library.WritesDataTable writersTable = new Models.Library.WritesDataTable();
            writersAdapter.Fill(writersTable);
            ViewData["writers"] = writersTable.Rows;

            ViewData["title"] = "Kitap Ekle";
            if (error != null)
            {
                ViewData["error"] = error;
            }
            return View();
        }
        public void ProcessNewbook(string bookName, string keywords, long writer)
        {
            Models.LibraryTableAdapters.booksTableAdapter booksAdapter; 
            long id;
            DateTime addedOn;
            Models.LibraryTableAdapters.QueriesTableAdapter queriesTableAdapter = new Models.LibraryTableAdapters.QueriesTableAdapter();

            if (bookName == null)
            {
                Response.Redirect("~/books/add");
                return;
            }
            bookName = bookName.Trim();
            if (bookName == "")
            {
                Response.Redirect("~/books/add");
                return;
            }
            if (bookName.Length > 100)
            {
                Response.Redirect("~/books/add");
                return;
            }
            if (keywords != null)
            {
                keywords = keywords.Trim();
                if (keywords == "")
                {
                    keywords = null;
                }
                else
                {
                    if (keywords.Length > 100)
                    {
                        Response.Redirect("~/books/add");
                        return;
                    }
                }
            }
            try
            {
                booksAdapter= new Models.LibraryTableAdapters.booksTableAdapter();
                addedOn = DateTime.Now;
                booksAdapter.AddBook(bookName, keywords, addedOn);
                id = booksAdapter.BookId(bookName, addedOn).Value;
                queriesTableAdapter.AddWriterBooks(writer, id);
                Response.Redirect("~/books");
            }
            catch
            {
                Response.Redirect("~/books/add");
            }
        }
        public void Delete(long id)
        {
            Models.LibraryTableAdapters.booksTableAdapter booksAdapter = new Models.LibraryTableAdapters.booksTableAdapter();
            booksAdapter.DeleteBook(id);
            Response.Redirect("~/books");
        }
        public ActionResult Update(long id , string error = null)
        {
            Models.LibraryTableAdapters.DataTable2TableAdapter booksAdapter = new Models.LibraryTableAdapters.DataTable2TableAdapter();
            Models.Library.DataTable2DataTable booksTable = new Models.Library.DataTable2DataTable();
            Models.Library.DataTable2Row booksRow;
            Models.LibraryTableAdapters.WritesTableAdapter writersAdapter = new Models.LibraryTableAdapters.WritesTableAdapter();
            Models.Library.WritesDataTable writersTable = new Models.Library.WritesDataTable();

            booksAdapter.FillById(booksTable, id);
            booksRow = (Models.Library.DataTable2Row)booksTable.Rows[0];
            ViewData["title"] = booksRow.BookName;
            ViewData["bookRow"] = booksRow;

            if (error != null)
            {
                ViewData["error"] = error;
            }
            writersAdapter.Fill(writersTable);
            ViewData["writers"] = writersTable.Rows;
            return View();
        }
        public void ProcessBook(long id, string bookName, string keywords, long writer)
        {
            Models.LibraryTableAdapters.booksTableAdapter booksAdapter = new Models.LibraryTableAdapters.booksTableAdapter();
            Models.LibraryTableAdapters.QueriesTableAdapter queriesTableAdapter = new Models.LibraryTableAdapters.QueriesTableAdapter();

            if (bookName == null)
            {
                Response.Redirect("~/books/update/" + id.ToString());
                return;
            }
            bookName = bookName.Trim();
            if (bookName == "")
            {
                Response.Redirect("~/books/update/" + id.ToString());
                return;
            }
            if (bookName.Length > 100)
            {
                Response.Redirect("~/books/update/" + id.ToString());
                return;
            }

            if (keywords != null)
            {
                keywords = keywords.Trim();
                if (keywords == "")
                {
                    keywords = null;
                }
                else
                {
                    if (keywords.Length > 100)
                    {
                        Response.Redirect("~/books/update");
                        return;
                    }
                }
            }
            try
            {
                booksAdapter = new Models.LibraryTableAdapters.booksTableAdapter();
                booksAdapter.UpdateBook(bookName, keywords, id);
                queriesTableAdapter.DeleteWriterBook(id);
                queriesTableAdapter.AddWriterBooks(writer, id);
                Response.Redirect("~/books");
            }
            catch
            {
                Response.Redirect("~/books/update");
            }
        }
    }
}