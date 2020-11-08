using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DcssePortal.Data;
using DcssePortal.Model;
using DcssePortal.Web.Models;

namespace DcssePortal.Web.Controllers
{
  public class ResultsController : Controller
  {
    private ApplicationDbContext db = new ApplicationDbContext();

    // GET: Results
    public ActionResult Index()
    {
      var viewModel = new CreateResultViewModel();
      //viewModel = 
      //viewModel.Result = new Result();
      return View(db.Results.ToList());
    }

    // GET: Results/Details/5

    //[Authorize(Roles = "Admin,Student")]
    public ActionResult Details(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Result result = db.Results.Find(id);
      if (result == null)
      {
        return HttpNotFound();
      }
      return View(result);
    }

    // GET: Results/Create

    //[Authorize(Roles = "Admin")]
    public ActionResult Create()
    {
      ViewBag.Courses = db.Courses.ToList();
      ViewBag.Students = db.Students.ToList();
      return View();
    }

    // POST: Results/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    //[Authorize(Roles = "Admin")]
    public ActionResult Create(FormCollection forms)
    {
      var result = new Result();
      if (ModelState.IsValid)
      {
        var studentId = Convert.ToInt32(Request.Form["Enrollment.Student"]);
        var courseId = Convert.ToInt32(Request.Form["Enrollment.Course"]);
        if (!db.Enrollments.Any(x => x.Student.ID == studentId && x.Course.ID == courseId))
          ModelState.AddModelError("Invalid Enrollment", new Exception("Enrollment doesnot exists"));
        else
        {
          //result.Enrollment = db.Enrollments.FirstOrDefault(x => x.Student.ID == studentId && x.Course.ID == courseId);
          result.TotalMarks = Convert.ToInt16(Request.Form["TotalMarks"]);
          result.ObtainedMarks = Convert.ToInt16(Request.Form["ObtainedMarks"]);
          result.ExternalMarks = Convert.ToInt16(Request.Form["ExternalMarks"]);
          result.InternalMarks = Convert.ToInt16(Request.Form["InternalMarks"]);
          result.Grade = _GetGrade(result.ObtainedMarks);
          db.Results.Add(result);
          db.SaveChanges();
          return RedirectToAction("Index");
        }
      }

      ViewBag.Courses = db.Courses.ToList();
      ViewBag.Students = db.Students.ToList();
      return View(result);
    }

    // GET: Results/Edit/5
   // [Authorize(Roles = "Admin")]
    public ActionResult Edit(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Result result = db.Results.Find(id);
      if (result == null)
      {
        return HttpNotFound();
      }
      return View(result);
    }

    // POST: Results/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
   // [Authorize(Roles = "Admin")]
    public ActionResult Edit(Result result)
    {
      if (ModelState.IsValid)
      {
        result.Grade = _GetGrade(result.ObtainedMarks);
        db.Entry(result).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Index");
      }
      return View(result);
    }

    // GET: Results/Delete/5
    //[Authorize(Roles = "Admin")]
    public ActionResult Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Result result = db.Results.Find(id);
      if (result == null)
      {
        return HttpNotFound();
      }
      return View(result);
    }

    // POST: Results/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    //[Authorize(Roles = "Admin")]
    public ActionResult DeleteConfirmed(int id)
    {
      Result result = db.Results.Find(id);
      db.Results.Remove(result);
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

    [NonAction]
    private string _GetGrade(int obtainedMarks)
    {
      if (obtainedMarks >= 90) return "A";
      if (obtainedMarks >= 85) return "B+";
      if (obtainedMarks >= 80) return "B";
      if (obtainedMarks >= 75) return "C+";
      if (obtainedMarks >= 70) return "C";
      if (obtainedMarks >= 65) return "D+";
      if (obtainedMarks >= 60) return "D";
      return "F";

    }
  }
}
