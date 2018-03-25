using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KPMGNews.Models;

namespace KPMGNews.Controllers
{
    public class AccountsController : Controller
    {
        // Accounts Controller for Login Management


        private Model1 db = new Model1();

        // GET: Accounts
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Accounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Accounts/Create
        public ActionResult Create()
        {
            return View();
        }


        // GET: Accounts/Create
        public ActionResult LogOut()
        {
            KPMGNews.Models.LoginData logindata = new KPMGNews.Models.LoginData(this.HttpContext);
            logindata.LogOut();
            return RedirectToAction("Login", "Accounts", null);
        }

        // GET: Accounts/Create
        public ActionResult Login()
        {
            return View();
        }

        


        [HttpPost]
        public ActionResult Login(User user)
        {

            int u = (from users in db.Users
                     where users.Login.ToString() == user.Login.ToString() && (users.Password.ToString() == user.Password.ToString() )
                     select users).Count();

            if (u > 0)
            {
                Session["Username"] = user.Login;
                Session["Password"] = user.Password;

                return RedirectToAction("Index", "Home", null);
            }
            else
            {
                return new RedirectResult("~/Accounts/Login?message=Session expired or Incorrect Login, Please login again.", false);
            }


        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,Login,Password,UserTypeID")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Accounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,Login,Password,UserTypeID")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Accounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
