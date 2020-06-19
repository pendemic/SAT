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
    public class StudentsController : Controller
    {
        private SATEntities db = new SATEntities();
        [Authorize(Roles = "Admin, Scheduling, Employee")]
        // GET: Students
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.StudentStatus);
            return View(students.ToList());
        }
        [Authorize(Roles = "Admin, Scheduling")]
        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }
        [Authorize(Roles = "Admin")]
        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentID,FirstName,LastName,StreetAddress,City,State,ZipCode,Major,Phone,Email,PhotoUrl,SSID")] Student student, HttpPostedFileBase studentImage)
        {
            if (ModelState.IsValid)
            {
                #region File Upload Students
                string imageName = "blank person.png";

                if (studentImage != null)
                {
                    //file name
                    imageName = studentImage.FileName;

                    string ext = imageName.Substring(imageName.LastIndexOf('.'));
                    string[] gExt = { "jpg", ".jpeg", ".png", ".gif" };

                    if (gExt.Contains(ext.ToLower()) && studentImage.ContentLength <=4194304)
                    {
                        imageName = Guid.NewGuid() + ext;
                        studentImage.SaveAs(Server.MapPath("~/Content/assets/images/SAT_Students/" + imageName));
                    }//end if
                    else //extension is bad
                    {
                        imageName = "blank person.png";
                    }//end else
                }//end If studentImage != null

                student.PhotoUrl = imageName;

                #endregion
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName", student.SSID);
            return View(student);
        }
        [Authorize(Roles = "Admin, Employee")]
        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName", student.SSID);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentID,FirstName,LastName,StreetAddress,City,State,ZipCode,Major,Phone,Email,PhotoUrl,SSID")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName", student.SSID);
            return View(student);
        }
        [Authorize(Roles = "Admin, Employee")]
        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);

            
            if (User.IsInRole("Admin"))
            {
                db.Students.Remove(student);
            }
            
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        //public ActionResult withdrawn(int id)
        //{
        //    Student student = db.Students.Find(id);
        //    if (User.IsInRole("Employee"))
        //    {
        //        student.SSID.Equals(3);
        //        db.Entry(student).State = EntityState.Modified;


        //    }

        //    db.SaveChanges();
        //    return RedirectToAction("Index");

        //}

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
