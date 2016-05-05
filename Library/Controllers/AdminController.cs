using LibraryData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Library.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        private string connectionString = @"Data Source=YS\SQLEXPRESS1;Initial Catalog=Library;Integrated Security=True";
       [Authorize]
        public ActionResult Main()
        {
            return View();
        }
        public ActionResult Signin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminSignin(string firstName, string lastName, string password)
        {
            DataBase Db = new DataBase(connectionString);
            Admin admin = Db.GetAdmin(firstName, lastName, password);
            FormsAuthentication.SetAuthCookie(admin.FirstName, true);         
            return RedirectToAction("Main");
        }

    }
}
