using DcssePortal.Data;
using DcssePortal.Model;
using DcssePortal.Web.Models;

using System;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DcssePortal.Web.Controllers
{
  [Authorize(Roles ="Faculty")]
    public class FeedbacksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

    // GET: Feedbacks
    [Authorize(Roles = "Faculty,Student")]
    public ActionResult Index()
        {
            return View(db.Feedbacks.ToList());
        }

    // GET: Feedbacks/Details/5

    //[Authorize(Roles = "Student")]
    [Authorize(Roles = "Faculty,Student")]
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
            return View();
        }

        // POST: Feedbacks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Faculty")]
        public ActionResult Create(FeedbackViewModel viewModel)
        {
      Feedback feedback = new Feedback();
      if (ModelState.IsValid)
      {
        feedback.Date = DateTime.Now;
        feedback.Title = viewModel.Title;
        feedback.Course = db.Courses.FirstOrDefault(x => x.ID == viewModel.Course);
        if (feedback.Course == null) throw new Exception("Could not find course");
        var facultyId = db.Faculties.FirstOrDefault(x => x.Email == db.Users.FirstOrDefault(y => y.UserName == User.Identity.Name).Email).ID;
        var courseId = Convert.ToInt32(Request.Form["Course"]);
        if (!db.Courses.Any(x => x.ID == courseId && x.Faculty.ID == facultyId))
         throw new Exception("Invalid Enrollment");
        else
        {
          //feedback.Enrollment = db.Enrollments.FirstOrDefault(x => x.Course.ID == courseId && x.Student.ID == studentId);
          db.Feedbacks.Add(feedback);
          db.SaveChanges();
          return RedirectToAction("Index");
        }
      }

      ViewBag.Courses = db.Courses.ToList();
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
      FeedbackViewModel feedbackViewModel = new FeedbackViewModel()
      {
        ID = feedback.ID,
        Title = feedback.Title,
        Course = feedback.Course.ID
      };
      return View(feedbackViewModel);
        }

        // POST: Feedbacks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Faculty")]
        public ActionResult Edit(FeedbackViewModel viewModel)
    {
      Feedback feedback;// = new Feedback();
      if (ModelState.IsValid)
      {
        feedback = db.Feedbacks.Find(viewModel.ID);
        if (feedback == null) throw new ObjectNotFoundException("Feedback not found.");
        feedback.Date = DateTime.Now;
        feedback.Title = viewModel.Title;
        feedback.Course = db.Courses.FirstOrDefault(x => x.ID == viewModel.Course);
        if (feedback.Course == null) throw new Exception("Could not find course");
        var facultyId = db.Faculties.FirstOrDefault(x => x.Email == db.Users.FirstOrDefault(y => y.UserName == User.Identity.Name).Email).ID;
        var courseId = Convert.ToInt32(Request.Form["Course"]);
        if (!db.Courses.Any(x => x.ID == courseId && x.Faculty.ID == facultyId))
          throw new Exception("Invalid Enrollment");
        else
        {
          //feedback.Enrollment = db.Enrollments.FirstOrDefault(x => x.Course.ID == courseId && x.Student.ID == studentId);
          db.Entry(feedback).State = EntityState.Modified;
          db.SaveChanges();
          return RedirectToAction("Index");
        }
      }

      ViewBag.Courses = db.Courses.ToList();
      ViewBag.Students = db.Students.ToList();
      return View(viewModel);
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
