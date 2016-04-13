using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryData;
using Library.Models;

namespace Library.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/
        private string connectionString = @"Data Source=YS\SQLEXPRESS1;Initial Catalog=Library;Integrated Security=True";
        public ActionResult Main()
        {
            DataBase Db = new DataBase(connectionString);
            HomeModel homeModel = new HomeModel();
            homeModel.Books = Db.GetBooks();
            return View(homeModel);
        }
        public ActionResult SignIn()
        {
            return View();
        }
        public ActionResult SignUp()
        {
            return View();
        }
    }
}
