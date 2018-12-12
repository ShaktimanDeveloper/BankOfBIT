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
    public class GoldStateController : Controller
    {
        private BankOfBIT_ArshdeepSanghaContext db = new BankOfBIT_ArshdeepSanghaContext();

        //
        // GET: /GoldState/

        public ActionResult Index()
        {
            //Returns the goldstate account state instance
            return View(GoldState.GetInstance());
        }

        //
        // GET: /GoldState/Details/5

        public ActionResult Details(int id = 0)
        {
            GoldState goldstate = db.GoldStates.Find(id);
            if (goldstate == null)
            {
                return HttpNotFound();
            }
            return View(goldstate);
        }

        //
        // GET: /GoldState/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /GoldState/Create

        [HttpPost]
        public ActionResult Create(GoldState goldstate)
        {
            if (ModelState.IsValid)
            {
                //creates the goldstate state
                db.AccountStates.Add(goldstate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(goldstate);
        }

        //
        // GET: /GoldState/Edit/5

        public ActionResult Edit(int id = 0)
        {
            GoldState goldstate = db.GoldStates.Find(id);
            if (goldstate == null)
            {
                return HttpNotFound();
            }
            return View(goldstate);
        }

        //
        // POST: /GoldState/Edit/5

        [HttpPost]
        public ActionResult Edit(GoldState goldstate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(goldstate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(goldstate);
        }

        //
        // GET: /GoldState/Delete/5

        public ActionResult Delete(int id = 0)
        {
            GoldState goldstate = db.GoldStates.Find(id);
            if (goldstate == null)
            {
                return HttpNotFound();
            }
            return View(goldstate);
        }

        //
        // POST: /GoldState/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            GoldState goldstate = db.GoldStates.Find(id);
            db.AccountStates.Remove(goldstate);
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