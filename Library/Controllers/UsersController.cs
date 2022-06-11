using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;

namespace Library.Controllers
{
    public class UsersController : LibraryController
    {
        public ActionResult Index()
        {
            Models.LibraryTableAdapters.Users1TableAdapter usersAdapter = new Models.LibraryTableAdapters.Users1TableAdapter();
            Models.Library.Users1DataTable usersResultsTable = new Models.Library.Users1DataTable();
            usersAdapter.Fill(usersResultsTable);
            ViewData["users"] = usersResultsTable.Rows;
            ViewData["title"] = "Kullanıcılar";
            return View();
        }

        public ActionResult Add(string error=null)
        {
            ViewData["title"] = "Kullanıcı Ekle";
            if (error != null)
            {
                ViewData["error"] = error;
            }
            return View();
        }
        public void ProcessNewUser(string userName, string password, string eMail)
        {
            MailAddress mailAddress;
            string hashedPassword;
            Models.LibraryTableAdapters.UsersTableAdapter usersAdapter = new Models.LibraryTableAdapters.UsersTableAdapter();

            if (userName == null)
            {
                Response.Redirect("~/users/add");
                return;
            }
            userName = userName.Trim();
            if(userName=="")
            {
                Response.Redirect("~/users/add");
                return;
            }
            if(userName.Length > 50)
            {
                Response.Redirect("~/users/add");
                return;
            }
            if (userName=="admin")
            {
                Response.Redirect("~/users/add?error=Geçersiz kullanıcı");
                return;
            }
            if (password == null)
            {
                Response.Redirect("~/users/add");
                return;
            }
            password = password.Trim();
            if (password == "")
            {
                Response.Redirect("~/users/add");
                return;
            }
            eMail = eMail.Trim();
            if (eMail == "")
            {
                Response.Redirect("~/users/add");
                return;
            }
            if (eMail.Length > 100)
            {
                Response.Redirect("~/users/add");
                return;
            }
            try
            {
                mailAddress = new MailAddress(eMail);
            }
            catch
            {
                Response.Redirect("~/users/add");
                return;
            }
            if (eMail != mailAddress.Address)
            {
                Response.Redirect("~/users/add");
                return;
            }
            hashedPassword = utility.CalculateSHA(userName, password);
            try
            {
                usersAdapter.AddUser(userName, hashedPassword, eMail);
                Response.Redirect("~/users");
            }
            catch
            {
                Response.Redirect("~/users/add");
            }
        }
        public void Delete(long id)
        {
            Models.LibraryTableAdapters.UsersTableAdapter usersAdapter = new Models.LibraryTableAdapters.UsersTableAdapter();
            usersAdapter.DeleteUser(id);
            Response.Redirect("~/users");
        }
        public ActionResult Update(long id)
        {
            Models.LibraryTableAdapters.Users1TableAdapter usersAdapter = new Models.LibraryTableAdapters.Users1TableAdapter();
            Models.Library.Users1DataTable usersTable = new Models.Library.Users1DataTable();
            Models.Library.Users1Row users1Row;

            usersAdapter.FillById(usersTable, id);
            users1Row= (Models.Library.Users1Row)usersTable.Rows[0];
            ViewData["title"] = users1Row.usersName;
            ViewData["userRow"] = users1Row;
            return View();
        }
        public void ProcessUser(long id, string userName, string password, string eMail)
        {
            MailAddress mailAddress;
            string hashedPassword;
            Models.LibraryTableAdapters.UsersTableAdapter usersAdapter = new Models.LibraryTableAdapters.UsersTableAdapter();
            bool changePassword = false;

            if (userName == null)
            {
                Response.Redirect("~/users/update/"+id.ToString());
                return;
            }
            userName = userName.Trim();
            if (userName == "")
            {
                Response.Redirect("~/users/update/" + id.ToString());
                return;
            }
            if (userName.Length > 50)
            {
                Response.Redirect("~/users/update/" + id.ToString());
                return;
            }
            if (password == null)
            {
                Response.Redirect("~/users/update/" + id.ToString());
                return;
            }
            if (password != "    ")
            {
                changePassword = true;
                password = password.Trim();
                if (password == "")
                {
                    Response.Redirect("~/users/update/" + id.ToString());
                    return;
                }
            }
            eMail = eMail.Trim();
            if (eMail.Length > 100)
            {
                Response.Redirect("~/users/update/" + id.ToString());
                return;
            }
            try
            {
                mailAddress = new MailAddress(eMail);
            }
            catch
            {
                Response.Redirect("~/users/update/" + id.ToString());
                return;
            }
            if (eMail != mailAddress.Address)
            {
                Response.Redirect("~/users/update/" + id.ToString());
                return;
            }

            try
            {
                usersAdapter = new Models.LibraryTableAdapters.UsersTableAdapter();
                if (changePassword == true)
                {
                    hashedPassword = utility.CalculateSHA(userName, password);
                    usersAdapter.UpdateUserWithPassword(userName, hashedPassword, eMail, id);
                }
                else
                {
                    usersAdapter.UpdateUserWithoutPassword(userName, eMail, id);
                }
                Response.Redirect("~/users");
            }
            catch
            {
                Response.Redirect("~/users/update/" + id.ToString());
            }
        }
    }
}