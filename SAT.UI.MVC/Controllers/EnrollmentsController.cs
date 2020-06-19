using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClassLibrary1;

namespace SAT.UI.MVC.Controllers
{
    public class EnrollmentsController : Controller
    {
        private SATEntities db = new SATEntities();
        [Authorize(Roles = "Admin, Scheduling")]
        // GET: Enrollments
        public ActionResult Index()
        {
            var enrollments = db.Enrollments.Include(e => e.ScheduledClass).Include(e => e.Student);
            return View(enrollments.ToList());
        }
        [Authorize(Roles = "Admin, Scheduling")]
        // GET: Enrollments/Details/5
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
        [Authorize(Roles = "Admin, Scheduling")]
        // GET: Enrollments/Create
        public ActionResult Create()
        {
            ViewBag.ScheduledClassID = new SelectList(db.ScheduledClasses, "ScheduledClassID", "InstructorName");
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName");
            return View();
        }

        // POST: Enrollments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EnrollmentID,StudentID,ScheduledClassID,EnrollmentDate")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Enrollments.Add(enrollment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ScheduledClassID = new SelectList(db.ScheduledClasses, "ScheduledClassID", "InstructorName", enrollment.ScheduledClassID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", enrollment.StudentID);
            return View(enrollment);
        }
        [Authorize(Roles = "Admin, Scheduling")]
        // GET: Enrollments/Edit/5
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
            ViewBag.ScheduledClassID = new SelectList(db.ScheduledClasses, "ScheduledClassID", "InstructorName", enrollment.ScheduledClassID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", enrollment.StudentID);
            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EnrollmentID,StudentID,ScheduledClassID,EnrollmentDate")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enrollment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ScheduledClassID = new SelectList(db.ScheduledClasses, "ScheduledClassID", "InstructorName", enrollment.ScheduledClassID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", enrollment.StudentID);
            return View(enrollment);
        }
        [Authorize(Roles = "Admin, Scheduling")]
        // GET: Enrollments/Delete/5
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
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Enrollment enrollment = db.Enrollments.Find(id);
            if (User.IsInRole("Admin"))
            {
                db.Enrollments.Remove(enrollment);
            }
            else
            {
                //enrollment.isActive = false;
                db.Entry(enrollment).State = EntityState.Modified;
            }
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
