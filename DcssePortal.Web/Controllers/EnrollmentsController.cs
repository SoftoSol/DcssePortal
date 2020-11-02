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
  public class EnrollmentsController : Controller
  {
    private ApplicationDbContext db = new ApplicationDbContext();

    // GET: Enrollments
    public ActionResult Index()
    {
      return View(db.Enrollments.ToList());
    }

    // GET: Enrollments/Details/5
    [AllowAnonymous]
    public ActionResult Details(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Enrollment enrollment = db.Enrollments.Find(id);
      if (enrollment == null)
      {
        return HttpNotFound();
      }
      return View(enrollment);
    }

    // GET: Enrollments/Create

    //[Authorize(Roles = "Student,Faculty")]
    public ActionResult Create()
    {
      ViewBag.Courses = db.Courses.ToList();
      ViewBag.Students = db.Students.ToList();
      return View();
    }

    // POST: Enrollments/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    //[Authorize(Roles = "Admin,Faculty")]
    public ActionResult Create(FormCollection forms)
    {
      var enrollment = new Enrollment();
      if (ModelState.IsValid)
      {
        var courseId = Convert.ToInt32(Request.Form["Course"]);
        var studentId = Convert.ToInt32(Request.Form["Student"]);
        enrollment.Student = db.Students.FirstOrDefault(x => x.ID == studentId);
        enrollment.Course = db.Courses.FirstOrDefault(x => x.ID == courseId);
        if (db.Enrollments.Any(x => x.Course.ID == courseId && x.Student.ID == studentId))
          ModelState.AddModelError("Duplicate Enrollment", new Exception("Duplicate Enrollment"));
        else
        {
          db.Enrollments.Add(enrollment);
          db.SaveChanges();
          return RedirectToAction("Index");
        }
      }

      ViewBag.Courses = db.Courses.ToList();
      ViewBag.Students = db.Students.ToList();
      return View(enrollment);
    }

    // GET: Enrollments/Edit/5

    [Authorize(Roles = "Faculty")]
    public ActionResult Edit(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Enrollment enrollment = db.Enrollments.Find(id);
      if (enrollment == null)
      {
        return HttpNotFound();
      }
      return View(enrollment);
    }

    // POST: Enrollments/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Faculty")]
    public ActionResult Edit([Bind(Include = "ID")] Enrollment enrollment)
    {
      if (ModelState.IsValid)
      {
        db.Entry(enrollment).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Index");
      }
      return View(enrollment);
    }

    // GET: Enrollments/Delete/5

    [Authorize(Roles = "Faculty")]
    public ActionResult Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Enrollment enrollment = db.Enrollments.Find(id);
      if (enrollment == null)
      {
        return HttpNotFound();
      }
      return View(enrollment);
    }

    // POST: Enrollments/Delete/5
    [HttpPost, ActionName("Delete")]

    [Authorize(Roles = "Faculty")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      Enrollment enrollment = db.Enrollments.Find(id);
      db.Enrollments.Remove(enrollment);
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
