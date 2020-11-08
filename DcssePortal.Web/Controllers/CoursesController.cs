using DcssePortal.Data;
using DcssePortal.Model;

using System;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DcssePortal.Web.Controllers
{
  public class CoursesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

    // GET: Courses
    [Authorize(Roles = "Student, Faculty")]
    public ActionResult Index()
        {
            return View(db.Courses.ToList());
        }

        // GET: Courses/Details/5
        [Authorize(Roles ="Student, Faculty")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Courses/Create
        [Authorize(Roles ="Faculty")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Faculty")]
        public ActionResult Create(Course course)
        {
            if (ModelState.IsValid)
            {
        var faculties = from faculty in db.Faculties
                              join user in db.Users on faculty.Email equals user.Email
                              select faculty;
        if (faculties.Count() ==0)
        {
          throw new ObjectNotFoundException("faculty not found");
        }
        course.Faculty = faculties.First();
        db.Courses.Add(course);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(course);
        }

        // GET: Courses/Edit/5
        [Authorize(Roles ="Faculty")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Faculty")]
        public ActionResult Edit(Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        [Authorize(Roles ="Faculty")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Faculty")]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    [HttpGet]
    [Authorize(Roles = "Student")]
    public ActionResult Join()
    {
      return View();
    }


    [HttpPost]
    [Authorize(Roles ="Student")]
    public ActionResult Join(FormCollection forms)
    {
      if (ModelState.IsValid)
      {
        var instructureId = Convert.ToInt32(forms["InstructureId"]);
        var courseCode = forms["courseCode"];
        var secretCode = forms["secretCode"].Trim();
        var course = db.Courses.FirstOrDefault(x => x.CourseCode == courseCode && x.Faculty.ID == instructureId);
        var currentStudents = from std in db.Students
                             join user in db.Users on std.Email equals user.Email
                             select std;
        if (currentStudents.Count()==0)
        {
          throw new ObjectNotFoundException("could not find student");
          ModelState.AddModelError("identificationProblem", "Cannot identify student");
          return View();
        }
        var currentStudent = currentStudents.FirstOrDefault();
        if (course == null)
        {
          throw new ObjectNotFoundException("could not find course");
        }
        if (course.SecretCode == secretCode)
        {
          course.Enrollments.Add(new Enrollment { Course = course, Student = currentStudent });
          db.SaveChanges();
        }
        else
        {
          ModelState.AddModelError("invalid key", "invalid secret key");
        }
      }
      return View();
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
