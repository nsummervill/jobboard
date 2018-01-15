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
using System.Data.Entity.Validation;

namespace JobBoard.UI.MVC.Controllers
{
    [Authorize(Roles="Employee, Manager, Admin")]
    public class OpenPositionsController : Controller
    {
        private JobBoardEntities db = new JobBoardEntities();

        // GET: OpenPositions
        public ActionResult Index()
        {
            var openPositions = db.OpenPositions.Include(o => o.Location).Include(o => o.Position);
            return View(openPositions.ToList());
        }
        [Authorize(Roles = "Employee, Manager, Admin")]
        // GET: OpenPositions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OpenPosition openPosition = db.OpenPositions.Find(id);
            if (openPosition == null)
            {
                return HttpNotFound();
            }
            return View(openPosition);
        }
        [Authorize(Roles ="Manager, Admin")]
        // GET: OpenPositions/Create
        
        public ActionResult Create()
        {
            if (User.IsInRole("Manager"))
            {
                var currentUserId = User.Identity.GetUserId();
                ViewBag.LocationID = 
                new SelectList(db.Locations.Where(x => x.ManagerID == currentUserId), "LocationID", "City");
            }
            else
            {
                ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "City");
            }
            
            ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "Title");
            return View();
        }

        // POST: OpenPositions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OpenPositionID,PositionID,LocationID")] OpenPosition openPosition)
        {
            if (ModelState.IsValid)
            {
                db.OpenPositions.Add(openPosition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //if you landed here, it's because there was an error with your new object
            if (User.IsInRole("Manager"))
            {
                //if you're a manager, only see your locations
                var currentUserId = User.Identity.GetUserId();
                ViewBag.LocationID = 
                    new SelectList(
                        db.Locations.Where(x => x.ManagerID == currentUserId), 
                    "LocationID", "StoreNumber", openPosition.LocationID);
            }
            else
            {
                //Admin sees all locations
                ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "StoreNumber", openPosition.LocationID);
            }
            
            ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "Title", openPosition.PositionID);
            return View(openPosition);
        }
        [Authorize(Roles = "Manager, Admin")]
        // GET: OpenPositions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OpenPosition openPosition = db.OpenPositions.Find(id);
            if (openPosition == null)
            {
                return HttpNotFound();
            }
            if (User.IsInRole("Manager"))
            {
                //if you're a manager, only see your locations
                var currentUserId = User.Identity.GetUserId();
                ViewBag.LocationID =
                    new SelectList(
                        db.Locations.Where(x => x.ManagerID == currentUserId),
                    "LocationID", "StoreNumber", openPosition.LocationID);
            }
            else
            {
                //Admin sees all locations
                ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "StoreNumber", openPosition.LocationID);
            }
            ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "Title", openPosition.PositionID);
            return View(openPosition);
        }

        // POST: OpenPositions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OpenPositionID,PositionID,LocationID")] OpenPosition openPosition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(openPosition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (User.IsInRole("Manager"))
            {
                //if you're a manager, only see your locations
                var currentUserId = User.Identity.GetUserId();
                ViewBag.LocationID =
                    new SelectList(
                        db.Locations.Where(x => x.ManagerID == currentUserId),
                    "LocationID", "StoreNumber", openPosition.LocationID);
            }
            else
            {
                //Admin sees all locations
                ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "StoreNumber", openPosition.LocationID);
            }
            ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "Title", openPosition.PositionID);
            return View(openPosition);
        }
        [Authorize(Roles = "Manager, Admin")]
        // GET: OpenPositions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OpenPosition openPosition = db.OpenPositions.Find(id);
            if (openPosition == null)
            {
                return HttpNotFound();
            }
            return View(openPosition);
        }

        // POST: OpenPositions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OpenPosition openPosition = db.OpenPositions.Find(id);
            db.OpenPositions.Remove(openPosition);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Apply(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OpenPosition openPosition = db.OpenPositions.Find(id);
            if (openPosition == null)
            {
                return HttpNotFound();
            }
            var currentUserID = User.Identity.GetUserId();
            Application a = new Application();
            a.OpenPositionID = id.Value;
            a.UserID = currentUserID;
            a.ApplicationDate = DateTime.Now;
            a.IsDeclined = false;
            a.ResumeFile = db.AspNetUsers
                .Where(x => x.Id == currentUserID).SingleOrDefault().ResumeFile;
            db.Applications.Add(a);
            //db.SaveChanges();
            

            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
            }
            return RedirectToAction("Index", "OpenPositions");
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
