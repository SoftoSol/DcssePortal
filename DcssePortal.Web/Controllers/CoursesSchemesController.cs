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
    public class CoursesSchemesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CoursesSchemes
        public ActionResult Index()
        {
            return View(db.CoursesSchemes.ToList());
        }

        // GET: CoursesSchemes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoursesScheme coursesScheme = db.CoursesSchemes.Find(id);
            if (coursesScheme == null)
            {
                return HttpNotFound();
            }
            return View(coursesScheme);
        }

        // GET: CoursesSchemes/Create
        [Authorize(Roles ="Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CoursesSchemes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Admin")]
        public ActionResult Create(CoursesScheme coursesScheme)
        {
            if (ModelState.IsValid)
            {
        coursesScheme.Date = DateTime.Now;
                db.CoursesSchemes.Add(coursesScheme);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(coursesScheme);
        }

        // GET: CoursesSchemes/Edit/5
        [Authorize(Roles ="Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoursesScheme coursesScheme = db.CoursesSchemes.Find(id);
            if (coursesScheme == null)
            {
                return HttpNotFound();
            }
            return View(coursesScheme);
        }

        // POST: CoursesSchemes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Admin")]
        public ActionResult Edit(CoursesScheme coursesScheme)
        {
            if (ModelState.IsValid)
            {
        coursesScheme.Date = DateTime.Now;
                db.Entry(coursesScheme).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(coursesScheme);
        }

        // GET: CoursesSchemes/Delete/5
        //[Authorize(Roles ="Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoursesScheme coursesScheme = db.CoursesSchemes.Find(id);
            if (coursesScheme == null)
            {
                return HttpNotFound();
            }
            return View(coursesScheme);
        }

        // POST: CoursesSchemes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            CoursesScheme coursesScheme = db.CoursesSchemes.Find(id);
            db.CoursesSchemes.Remove(coursesScheme);
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
