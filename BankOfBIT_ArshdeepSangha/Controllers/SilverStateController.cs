using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankOfBIT_ArshdeepSangha.Models;

namespace BankOfBIT_ArshdeepSangha.Controllers
{
    public class SilverStateController : Controller
    {
        private BankOfBIT_ArshdeepSanghaContext db = new BankOfBIT_ArshdeepSanghaContext();

        //
        // GET: /SilverState/

        public ActionResult Index()
        {
            //Returns the instance of the Silver state.
            return View(SilverState.GetInstance());
        }

        //
        // GET: /SilverState/Details/5

        public ActionResult Details(int id = 0)
        {
            SilverState silverstate = db.SilverStates.Find(id);
            if (silverstate == null)
            {
                return HttpNotFound();
            }
            return View(silverstate);
        }

        //
        // GET: /SilverState/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /SilverState/Create

        [HttpPost]
        public ActionResult Create(SilverState silverstate)
        {
            if (ModelState.IsValid)
            {
                db.AccountStates.Add(silverstate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(silverstate);
        }

        //
        // GET: /SilverState/Edit/5

        public ActionResult Edit(int id = 0)
        {
            SilverState silverstate = db.SilverStates.Find(id);
            if (silverstate == null)
            {
                return HttpNotFound();
            }
            return View(silverstate);
        }

        //
        // POST: /SilverState/Edit/5

        [HttpPost]
        public ActionResult Edit(SilverState silverstate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(silverstate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(silverstate);
        }

        //
        // GET: /SilverState/Delete/5

        public ActionResult Delete(int id = 0)
        {
            SilverState silverstate = db.SilverStates.Find(id);
            if (silverstate == null)
            {
                return HttpNotFound();
            }
            return View(silverstate);
        }

        //
        // POST: /SilverState/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            SilverState silverstate = db.SilverStates.Find(id);
            db.AccountStates.Remove(silverstate);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}