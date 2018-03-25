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
    public class NewsController : Controller
    {
        // News Controller for News Management

        private Model1 db = new Model1();

        // GET: News
        [SessionAuthorize]
        public ActionResult Index()
        {
            return View(db.NewsArticle.Where(a => a.deleted == false).OrderByDescending(a => a.timestamp).ToList());
        }

        // GET: News/Details/5
        [SessionAuthorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.NewsArticle.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: News/Create
        [SessionAuthorize]
        public ActionResult Create()
        {
            KPMGNews.Models.LoginData logindata = new KPMGNews.Models.LoginData(HttpContext);
            if (!logindata.usertype().Equals("Publisher")) { return RedirectToAction("Index","News",null); }

            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionAuthorize]
        public ActionResult Create([Bind(Include = "NewsID,Text,UserID,timestamp,deleted")] News news)
        {
            KPMGNews.Models.LoginData logindata = new KPMGNews.Models.LoginData(HttpContext);
            if (!logindata.usertype().Equals("Publisher")) { return RedirectToAction("Index", "News", null); }

            if (ModelState.IsValid)
            {
                news.timestamp = DateTime.Now;
                news.UserID = Convert.ToInt32(HttpContext.Session["UserID"]);
                news.deleted = false;
                db.NewsArticle.Add(news);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(news);
        }

        // GET: News/Edit/5
        [SessionAuthorize]
        public ActionResult Edit(int? id)
        {
            KPMGNews.Models.LoginData logindata = new KPMGNews.Models.LoginData(HttpContext);
            if (!logindata.usertype().Equals("Publisher")) { return RedirectToAction("Index", "News", null); }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.NewsArticle.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionAuthorize]
        public ActionResult Edit([Bind(Include = "NewsID,Text,UserID,timestamp,deleted")] News news)
        {
            KPMGNews.Models.LoginData logindata = new KPMGNews.Models.LoginData(HttpContext);
            if (!logindata.usertype().Equals("Publisher")) { return RedirectToAction("Index", "News", null); }

            if (ModelState.IsValid)
            {
                news.timestamp = DateTime.Now;
                news.UserID = Convert.ToInt32(HttpContext.Session["UserID"]);
                news.deleted = false;
                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(news);
        }

        // GET: News/Delete/5
        [SessionAuthorize]
        public ActionResult Delete(int? id)
        {
            KPMGNews.Models.LoginData logindata = new KPMGNews.Models.LoginData(HttpContext);
            if (!logindata.usertype().Equals("Publisher")) { return RedirectToAction("Index", "News", null); }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.NewsArticle.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [SessionAuthorize]
        public ActionResult DeleteConfirmed(int id)
        {
            KPMGNews.Models.LoginData logindata = new KPMGNews.Models.LoginData(HttpContext);
            if (!logindata.usertype().Equals("Publisher")) { return RedirectToAction("Index", "News", null); }

            News news = db.NewsArticle.Find(id);
            news.deleted = true;
            //db.NewsArticle.Remove(news);
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
