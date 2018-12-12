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
    public class NextTransactionNumberController : Controller
    {
        private BankOfBIT_ArshdeepSanghaContext db = new BankOfBIT_ArshdeepSanghaContext();

        //
        // GET: /NextTransactionNumber/

        public ActionResult Index()
        {
            //Returns the next transaction number instance.
            return View(NextTransactionNumber.GetInstance());
        }

        //
        // GET: /NextTransactionNumber/Details/5

        public ActionResult Details(int id = 0)
        {
            NextTransactionNumber nexttransactionnumber = db.NextTransactionNumbers.Find(id);
            if (nexttransactionnumber == null)
            {
                return HttpNotFound();
            }
            return View(nexttransactionnumber);
        }

        //
        // GET: /NextTransactionNumber/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /NextTransactionNumber/Create

        [HttpPost]
        public ActionResult Create(NextTransactionNumber nexttransactionnumber)
        {
            if (ModelState.IsValid)
            {
                db.NextTransactionNumbers.Add(nexttransactionnumber);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nexttransactionnumber);
        }

        //
        // GET: /NextTransactionNumber/Edit/5

        public ActionResult Edit(int id = 0)
        {
            NextTransactionNumber nexttransactionnumber = db.NextTransactionNumbers.Find(id);
            if (nexttransactionnumber == null)
            {
                return HttpNotFound();
            }
            return View(nexttransactionnumber);
        }

        //
        // POST: /NextTransactionNumber/Edit/5

        [HttpPost]
        public ActionResult Edit(NextTransactionNumber nexttransactionnumber)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nexttransactionnumber).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nexttransactionnumber);
        }

        //
        // GET: /NextTransactionNumber/Delete/5

        public ActionResult Delete(int id = 0)
        {
            NextTransactionNumber nexttransactionnumber = db.NextTransactionNumbers.Find(id);
            if (nexttransactionnumber == null)
            {
                return HttpNotFound();
            }
            return View(nexttransactionnumber);
        }

        //
        // POST: /NextTransactionNumber/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            NextTransactionNumber nexttransactionnumber = db.NextTransactionNumbers.Find(id);
            db.NextTransactionNumbers.Remove(nexttransactionnumber);
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