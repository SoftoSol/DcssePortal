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
    public class DateSheetsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DateSheets
        public ActionResult Index()
        {

            return View(db.DateSheets.ToList());
        }
        public ActionResult Search()
        {

            return View();
        }

        // GET: DateSheets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DateSheet dateSheet = db.DateSheets.Find(id);
            if (dateSheet == null)
            {
                return HttpNotFound();
            }
            return View(dateSheet);
        }

        // GET: DateSheets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DateSheets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Content,Department")] DateSheet dateSheet)
        {
            if (ModelState.IsValid)
            {
                db.DateSheets.Add(dateSheet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dateSheet);
        }

        // GET: DateSheets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DateSheet dateSheet = db.DateSheets.Find(id);
            if (dateSheet == null)
            {
                return HttpNotFound();
            }
            return View(dateSheet);
        }

        // POST: DateSheets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Content,Department")] DateSheet dateSheet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dateSheet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dateSheet);
        }

        // GET: DateSheets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DateSheet dateSheet = db.DateSheets.Find(id);
            if (dateSheet == null)
            {
                return HttpNotFound();
            }
            return View(dateSheet);
        }

        // POST: DateSheets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DateSheet dateSheet = db.DateSheets.Find(id);
            db.DateSheets.Remove(dateSheet);
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
