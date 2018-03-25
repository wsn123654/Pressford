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
    public class LikesController : Controller
    {
        // Likes Controller for Likes Management

        private Model1 db = new Model1();

        // GET: Likes
        public ActionResult Index()
        {
            return View(db.Likes.ToList());
        }

        // GET: Likes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Likes likes = db.Likes.Find(id);
            if (likes == null)
            {
                return HttpNotFound();
            }
            return View(likes);
        }

        // GET: Likes/Create
        [SessionAuthorize]
        public ActionResult Create()
        {
            Likes likes = new Likes();
            likes.NewsID = Convert.ToInt32(Request["NewsID"]);
            likes.UserID = Convert.ToInt32(HttpContext.Session["UserID"]);
            likes.timestamp = DateTime.Now;

            int l = db.Likes.Where(a => (a.NewsID == likes.NewsID) && (a.UserID == likes.UserID)).Count();
            if (l <= 0)
            {
                db.Likes.Add(likes);
                db.SaveChanges();
            }
            
            return RedirectToAction("Index", "News", null);
        }

        // POST: Likes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionAuthorize]
        public ActionResult Create([Bind(Include = "ID,NewsID,UserID,timestamp")] Likes likes)
        {
            if (ModelState.IsValid)
            {
                db.Likes.Add(likes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(likes);
        }

        // GET: Likes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Likes likes = db.Likes.Find(id);
            if (likes == null)
            {
                return HttpNotFound();
            }
            return View(likes);
        }

        // POST: Likes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NewsID,UserID,timestamp")] Likes likes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(likes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(likes);
        }

        // GET: Likes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Likes likes = db.Likes.Find(id);
            if (likes == null)
            {
                return HttpNotFound();
            }
            return View(likes);
        }

        // POST: Likes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Likes likes = db.Likes.Find(id);
            db.Likes.Remove(likes);
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
