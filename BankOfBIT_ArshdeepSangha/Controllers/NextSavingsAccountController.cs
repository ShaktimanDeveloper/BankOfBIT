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
    public class NextSavingsAccountController : Controller
    {
        private BankOfBIT_ArshdeepSanghaContext db = new BankOfBIT_ArshdeepSanghaContext();

        //
        // GET: /NextSavingsAccount/

        public ActionResult Index()
        {
            //Returns the next savings account instance.
            return View(NextSavingsAccount.GetInstance());
        }

        //
        // GET: /NextSavingsAccount/Details/5

        public ActionResult Details(int id = 0)
        {
            NextSavingsAccount nextsavingsaccount = db.NextSavingsAccounts.Find(id);
            if (nextsavingsaccount == null)
            {
                return HttpNotFound();
            }
            return View(nextsavingsaccount);
        }

        //
        // GET: /NextSavingsAccount/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /NextSavingsAccount/Create

        [HttpPost]
        public ActionResult Create(NextSavingsAccount nextsavingsaccount)
        {
            if (ModelState.IsValid)
            {
                db.NextSavingsAccounts.Add(nextsavingsaccount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nextsavingsaccount);
        }

        //
        // GET: /NextSavingsAccount/Edit/5

        public ActionResult Edit(int id = 0)
        {
            NextSavingsAccount nextsavingsaccount = db.NextSavingsAccounts.Find(id);
            if (nextsavingsaccount == null)
            {
                return HttpNotFound();
            }
            return View(nextsavingsaccount);
        }

        //
        // POST: /NextSavingsAccount/Edit/5

        [HttpPost]
        public ActionResult Edit(NextSavingsAccount nextsavingsaccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nextsavingsaccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nextsavingsaccount);
        }

        //
        // GET: /NextSavingsAccount/Delete/5

        public ActionResult Delete(int id = 0)
        {
            NextSavingsAccount nextsavingsaccount = db.NextSavingsAccounts.Find(id);
            if (nextsavingsaccount == null)
            {
                return HttpNotFound();
            }
            return View(nextsavingsaccount);
        }

        //
        // POST: /NextSavingsAccount/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            NextSavingsAccount nextsavingsaccount = db.NextSavingsAccounts.Find(id);
            db.NextSavingsAccounts.Remove(nextsavingsaccount);
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