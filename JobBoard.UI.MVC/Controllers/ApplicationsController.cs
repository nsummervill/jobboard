using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JobBoard.Data.EF;
using Microsoft.AspNet.Identity;

namespace JobBoard.UI.MVC.Controllers
{
    [Authorize(Roles = "Employee, Manager, Admin")]
    public class ApplicationsController : Controller
    {
        private JobBoardEntities db = new JobBoardEntities();
        [Authorize(Roles = "Employee, Manager, Admin")]
        // GET: Applications
        public ActionResult Index()
        {
            var currentUserId = User.Identity.GetUserId();
            if (User.IsInRole("Employee"))
            {

                var applications = db.Applications.Where(x => x.UserID == currentUserId).Include(a => a.OpenPosition);
                return View(applications.ToList());
            }
            else if (User.IsInRole("Manager"))
            {
                var applications = db.Applications.Where(x => x.OpenPosition.Location.ManagerID == currentUserId).Include(a => a.OpenPosition);
                return View(applications.ToList());
            }
            else if (User.IsInRole("Admin"))
            {
                var applications = db.Applications.Include(a => a.OpenPosition);
                return View(applications.ToList());
            }
            else
            {
                return null;
            }
        }
        [Authorize(Roles = "Manager, Admin")]
        // GET: Applications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }
        [Authorize(Roles = "Manager, Admin")]
        // GET: Applications/Create
        public ActionResult Create()
        {
            ViewBag.OpenPositionID = new SelectList(db.OpenPositions, "OpenPositionID", "OpenPositionID");
            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ApplicationID,OpenPositionID,UserID,ApplicationDate,ManagerNotes,IsDeclined,ResumeFile")] Application application)
        {
            if (ModelState.IsValid)
            {
                db.Applications.Add(application);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OpenPositionID = new SelectList(db.OpenPositions, "OpenPositionID", "OpenPositionID", application.OpenPositionID);
            return View(application);
        }
        [Authorize(Roles = "Manager, Admin")]
        // GET: Applications/Edit/5
        public ActionResult Edit(int? id)
        {
           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            ViewBag.OpenPositionID = new SelectList(db.OpenPositions, "OpenPositionID", "OpenPositionID", application.OpenPositionID);
            return View(application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ApplicationID,OpenPositionID,UserID,ApplicationDate,ManagerNotes,IsDeclined,ResumeFile")] Application application)
        {
            if (ModelState.IsValid)
            {
                db.Entry(application).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OpenPositionID = new SelectList(db.OpenPositions, "OpenPositionID", "OpenPositionID", application.OpenPositionID);
            return View(application);
        }
        [Authorize(Roles = "Employee, Manager, Admin")]
        // GET: Applications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Application application = db.Applications.Find(id);
            db.Applications.Remove(application);
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
