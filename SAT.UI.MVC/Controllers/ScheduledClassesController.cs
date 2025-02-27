﻿using System;
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
    public class ScheduledClassesController : Controller
    {
        private SATEntities db = new SATEntities();
        [Authorize(Roles = "Admin, Scheduling")]
        // GET: ScheduledClasses
        public ActionResult Index()
        {
            var scheduledClasses = db.ScheduledClasses.Include(s => s.Cours).Include(s => s.ScheduledClassStatus);
            return View(scheduledClasses.ToList());
        }
        [Authorize(Roles = "Admin, Scheduling")]
        // GET: ScheduledClasses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScheduledClass scheduledClass = db.ScheduledClasses.Find(id);
            if (scheduledClass == null)
            {
                return HttpNotFound();
            }
            return View(scheduledClass);
        }
        [Authorize(Roles = "Admin, Scheduling")]
        // GET: ScheduledClasses/Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName");
            ViewBag.SCSID = new SelectList(db.ScheduledClassStatuses, "SCSID", "SCSName");
            return View();
        }

        // POST: ScheduledClasses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ScheduledClassID,CourseID,StartDate,EndDate,InstructorName,Location,SCSID")] ScheduledClass scheduledClass)
        {
            if (ModelState.IsValid)
            {
                db.ScheduledClasses.Add(scheduledClass);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", scheduledClass.CourseID);
            ViewBag.SCSID = new SelectList(db.ScheduledClassStatuses, "SCSID", "SCSName", scheduledClass.SCSID);
            return View(scheduledClass);
        }
        [Authorize(Roles = "Admin, Scheduling")]
        // GET: ScheduledClasses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScheduledClass scheduledClass = db.ScheduledClasses.Find(id);
            if (scheduledClass == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", scheduledClass.CourseID);
            ViewBag.SCSID = new SelectList(db.ScheduledClassStatuses, "SCSID", "SCSName", scheduledClass.SCSID);
            return View(scheduledClass);
        }

        // POST: ScheduledClasses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ScheduledClassID,CourseID,StartDate,EndDate,InstructorName,Location,SCSID")] ScheduledClass scheduledClass)
        {
            if (ModelState.IsValid)
            {
                db.Entry(scheduledClass).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", scheduledClass.CourseID);
            ViewBag.SCSID = new SelectList(db.ScheduledClassStatuses, "SCSID", "SCSName", scheduledClass.SCSID);
            return View(scheduledClass);
        }
        [Authorize(Roles = "Admin")]
        // GET: ScheduledClasses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScheduledClass scheduledClass = db.ScheduledClasses.Find(id);
            if (scheduledClass == null)
            {
                return HttpNotFound();
            }
            return View(scheduledClass);
        }

        // POST: ScheduledClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ScheduledClass scheduledClass = db.ScheduledClasses.Find(id);

            if (User.IsInRole("Admin"))
            {
                db.ScheduledClasses.Remove(scheduledClass);
            }
            else
            {
                //scheduledClass.isActive = false;
                db.Entry(scheduledClass).State = EntityState.Modified;
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
