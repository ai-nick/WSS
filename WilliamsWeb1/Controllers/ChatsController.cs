using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using WilliamsWeb1.Models;
using Microsoft.AspNet.Identity;

namespace WilliamsWeb1.Controllers
{
    [Authorize]
    public class ChatsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Chats
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                return View(db.Chats.ToList());
            }
            else
            {
                var id = User.Identity.GetUserId();
                var chats = db.Chats.Where(p => p.ClientID == id);
                return View(chats.ToList());
            }
        }

        // GET: Chats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chats chats = db.Chats.Find(id);
            if (chats == null)
            {
                return HttpNotFound();
            }
            return View(chats);
        }

        // GET: Chats/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Chats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Subject")] Chats chats)
        {
            if (ModelState.IsValid)
            {
                chats.ClientID = User.Identity.GetUserId();
                chats.Client = db.Users.Find(chats.ClientID);
                db.Chats.Add(chats);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(chats);
        }

        // GET: Chats/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chats chats = db.Chats.Find(id);
            if (chats == null)
            {
                return HttpNotFound();
            }
            return View(chats);
        }

        // POST: Chats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChatId,ClientID")] Chats chats)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chats).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(chats);
        }
        public ActionResult AddMessage(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chats chats = db.Chats.Find(id);
            if (chats == null)
            {
                return HttpNotFound();
            }
            //ViewBag.messages = chats.Messages;
            return View(chats);
        }

        // POST: Chats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMessage(int id, string thisMessage, [Bind(Include = "ChatID")] Message mess)
        {
            var chats = db.Chats.Find(id);
            if (thisMessage != "")
            {
                var thisid = User.Identity.GetUserName();
                mess.ChatID = id;
                mess.Sender = thisid;
                mess.mess = thisMessage;
                chats.Messages.Add(mess);
                db.Entry(chats).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AddMessage");
            }
            return View(chats);
        }
        // GET: Chats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chats chats = db.Chats.Find(id);
            if (chats == null)
            {
                return HttpNotFound();
            }
            return View(chats);
        }

        // POST: Chats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Chats chats = db.Chats.Find(id);
            db.Chats.Remove(chats);
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
