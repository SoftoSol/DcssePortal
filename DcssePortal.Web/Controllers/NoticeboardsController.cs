using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DcssePortal.Data;
using DcssePortal.Model;

namespace DcssePortal.Web.Controllers
{
    public class NoticeboardsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Noticeboards
        public ActionResult Index()
        {
            return View(db.Noticeboards.ToList());
        }

        // GET: Noticeboards/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Noticeboard noticeboard = db.Noticeboards.Find(id);
            if (noticeboard == null)
            {
                return HttpNotFound();
            }
            return View(noticeboard);
        }

        // GET: Noticeboards/Create

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Noticeboards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "ID,Title,Body,Date")] Noticeboard noticeboard)
        {
            if (ModelState.IsValid)
            {
                db.Noticeboards.Add(noticeboard);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(noticeboard);
        }

        // GET: Noticeboards/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Noticeboard noticeboard = db.Noticeboards.Find(id);
            if (noticeboard == null)
            {
                return HttpNotFound();
            }
            return View(noticeboard);
        }

        // POST: Noticeboards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "ID,NewsTitle,NewsContent,StartDate,EndDate")] Noticeboard noticeboard)
        {
            if (ModelState.IsValid)
            {
                db.Entry(noticeboard).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(noticeboard);
        }

        // GET: Noticeboards/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Noticeboard noticeboard = db.Noticeboards.Find(id);
            if (noticeboard == null)
            {
                return HttpNotFound();
            }
            return View(noticeboard);
        }

        // POST: Noticeboards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Noticeboard noticeboard = db.Noticeboards.Find(id);
            db.Noticeboards.Remove(noticeboard);
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
