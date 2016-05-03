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
        [HttpPost]
        public ActionResult MemberSignin(string firstName, string lastName, string password)
        {
            DataBase Db = new DataBase(connectionString);
            Member member = Db.GetMember(firstName, lastName, password);
            HomeModel homeModel = new HomeModel();
            homeModel.User = member;
            homeModel.Books = Db.GetBooks();
            return RedirectToAction("Main", homeModel);
        }
          [HttpPost]
        public ActionResult CheckUser(string firstName, string lastName)
        {
            DataBase Db = new DataBase(connectionString);
           bool valid= Db.CheckUser(firstName, lastName);
           return Json(valid);
        }
        [HttpPost]
        public ActionResult NewUser(string firstName, string lastName, string password)
        {
            DataBase Db = new DataBase(connectionString);
            Member member = Db.AddMember(firstName, lastName, password);       
            HomeModel homeModel = new HomeModel();
            homeModel.User = member;
            homeModel.Books = Db.GetBooks();
            return RedirectToAction("Main", homeModel);
        }
    }
}
