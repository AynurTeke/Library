using System.Web.Mvc;


namespace Library.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string keywords = null) //ben bu indexi içinde keywords var da yok da çağırabilirim. Null ise eski hali ile çalışır.
        {
            Models.LibraryTableAdapters.DataTable1TableAdapter searchAdapter = new Models.LibraryTableAdapters.DataTable1TableAdapter();
            Models.Library.DataTable1DataTable searchResultsTable = new Models.Library.DataTable1DataTable();
            if (keywords != null)
            {
                searchAdapter.Fill(searchResultsTable, keywords.Trim()); //keywords'ü kullanarak veritabanında çalıştır bulunanları searchResultsTable içine koy.
                ViewData["results"] = searchResultsTable.Rows; //viewdata mvc controller ile view arasında bağlantı sağlayan yapıdır. İçine searchResultsTable satırlarını atıyor.
            }
            ViewData["keywords"] = keywords;
            return View();
        }

        public ActionResult Login(string error= null)
        {
            if (error != null)
            {
                ViewData["error"] = error;
            }
            return View();
        }

        public void CheckLogin(string userName, string password)
        {
            Models.LibraryTableAdapters.UsersTableAdapter usersAdapter = new Models.LibraryTableAdapters.UsersTableAdapter();
            Models.Library.UsersDataTable usersTable = new Models.Library.UsersDataTable();
            Models.Library.UsersRow userRow;
            string hashedStr;

            usersAdapter.Fill(usersTable, userName);
            if (usersTable.Rows.Count != 1)
            {
                Response.Redirect("~/home/login?error=Bilinmeyen kullanıcı");
                return;
            }
            userRow = (Models.Library.UsersRow)usersTable.Rows[0];
            hashedStr=utility.CalculateSHA(userName, password);

            if (hashedStr != userRow.Password)
            {
                Response.Redirect("~/home/login?error=Hatalı Şifre");
                return;
            }
            Session["userId"] = userRow.id; //Mevcut tarayıcıda oturum açar, başka bir domaine gidene kadar oturum açık kalır, Login olmak gibi bir oturum açmak değil
            Response.Redirect("~/main",false);
        }

    }
}