using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using DcssePortal.Data;
using DcssePortal.Model;

namespace DcssePortal.Web.Controllers
{
    public class FeedbacksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Feedbacks
        public ActionResult Index()
        {
            return View(db.Feedbacks.ToList());
        }

        // GET: Feedbacks/Details/5

        //[Authorize(Roles = "Student")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedbacks.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }

        // GET: Feedbacks/Create
        //[Authorize(Roles = "Faculty")]
        public ActionResult Create()
        {
      ViewBag.Courses = db.Courses.ToList();
      ViewBag.Students = db.Students.ToList();
            return View();
        }

        // POST: Feedbacks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Faculty")]
        public ActionResult Create(Feedback feedback)
        {
      if (ModelState.IsValid)
      {
        var studentId = Convert.ToInt32(Request.Form["Student"]);
        var courseId = Convert.ToInt32(Request.Form["Course"]);
        if (!db.Enrollments.Any(x => x.Course.ID == courseId && x.Student.ID == studentId))
          ModelState.AddModelError("Invalid Enrollment", new Exception("Invalid Enrollment"));
        else
        {
          feedback.Enrollment = db.Enrollments.FirstOrDefault(x => x.Course.ID == courseId && x.Student.ID == studentId);
          db.Feedbacks.Add(feedback);
          db.SaveChanges();
          return RedirectToAction("Index");
        }
      }

      ViewBag.Courses = db.Courses.ToList();
      ViewBag.Students = db.Students.ToList();
      return View(feedback);
        }

        // GET: Feedbacks/Edit/5
        //[Authorize(Roles = "Faculty")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedbacks.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }

      ViewBag.Courses = db.Courses.ToList();
      ViewBag.Students = db.Students.ToList();
      return View(feedback);
        }

        // POST: Feedbacks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Faculty")]
        public ActionResult Edit(Feedback feedback)
        {
      if (ModelState.IsValid)
      {

        var studentId = Convert.ToInt32(Request.Form["Student"]);
        var courseId = Convert.ToInt32(Request.Form["Course"]);
        if (!db.Enrollments.Any(x => x.Course.ID == courseId && x.Student.ID == studentId))
          ModelState.AddModelError("Invalid Enrollment", new Exception("Invalid Enrollment"));
        else
        {
          feedback.Enrollment = db.Enrollments.FirstOrDefault(x => x.Course.ID == courseId && x.Student.ID == studentId);
          db.Entry(feedback).State = EntityState.Modified;
          db.SaveChanges();
          return RedirectToAction("Index");
        }
      }

      ViewBag.Courses = db.Courses.ToList();
      ViewBag.Students = db.Students.ToList();
      return View(feedback);
        }

        // GET: Feedbacks/Delete/5
        //[Authorize(Roles = "Faculty")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedbacks.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }

        // POST: Feedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Faculty")]
        public ActionResult DeleteConfirmed(int id)
        {
            Feedback feedback = db.Feedbacks.Find(id);
            db.Feedbacks.Remove(feedback);
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
