using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WilliamsWeb1.Models;

namespace WilliamsWeb1.Controllers
{
    [Authorize]
    public class MilestonesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: Milestones
        public ActionResult Index(Guid? id)
        {
            if (User.IsInRole("Admin"))
            {
                return View(db.Milestones.ToList());
            }
            if (id == null)
            {
                if (User.IsInRole("Admin"))
                {
                    return View(db.Milestones.ToList());
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            var milestones = db.Milestones.Where(p => p.ParentID == id).ToList();
            return View(milestones);
        }

        // GET: Milestones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Milestone milestone = db.Milestones.Find(id);
            if (milestone == null)
            {
                return HttpNotFound();
            }
            return View(milestone);
        }

        // GET: Milestones/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.prjctID = id;
            return View();
        }

        // POST: Milestones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Guid id, [Bind(Include = "milestoneID,estimatedHours,hoursWorked,hoursPaid,AmountDue,AmountPaid,Name,Description,ParentID")] Milestone milestone)
        {
            var prjct = db.Projects.Find(id);
            if (ModelState.IsValid)
            {
                milestone.ParentID = id;
                milestone.Parent = prjct;
                prjct.addMilestone(milestone);
                db.Milestones.Add(milestone);
                db.SaveChanges();
                return RedirectToAction("Index",new {id = id});
            }
            return View(milestone);
        }

        // GET: Milestones/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Milestone milestone = db.Milestones.Find(id);
            if (milestone == null)
            {
                return HttpNotFound();
            }
            return View(milestone);
        }

        // POST: Milestones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "milestoneID,hoursWorked,hoursPaid,Name,Description,ParentID")] Milestone milestone)
        {
            if (ModelState.IsValid)
            {
                var prnt = db.Projects.Find(milestone.ParentID);
                milestone.addHours(prnt.HourlyRate);
                db.Entry(milestone).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = milestone.ParentID });
            }
            return View(milestone);
        }

        // GET: Milestones/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Milestone milestone = db.Milestones.Find(id);
            if (milestone == null)
            {
                return HttpNotFound();
            }
            return View(milestone);
        }

        // POST: Milestones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Milestone milestone = db.Milestones.Find(id);
            var idizzle = milestone.ParentID;
            var prjct = db.Projects.Find(idizzle);
            prjct.removeMilestone(milestone);
            db.Milestones.Remove(milestone);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = idizzle });
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
