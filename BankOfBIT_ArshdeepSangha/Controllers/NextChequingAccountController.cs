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
    public class NextChequingAccountController : Controller
    {
        private BankOfBIT_ArshdeepSanghaContext db = new BankOfBIT_ArshdeepSanghaContext();

        //
        // GET: /NextChequingAccount/

        public ActionResult Index()
        {
            //return View(db.NextChequingAccounts.ToList());
            return View(NextChequingAccount.GetInstance());
        }

        //
        // GET: /NextChequingAccount/Details/5

        public ActionResult Details(int id = 0)
        {
            NextChequingAccount nextchequingaccount = db.NextChequingAccounts.Find(id);
            if (nextchequingaccount == null)
            {
                return HttpNotFound();
            }
            return View(nextchequingaccount);
        }

        //
        // GET: /NextChequingAccount/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /NextChequingAccount/Create

        [HttpPost]
        public ActionResult Create(NextChequingAccount nextchequingaccount)
        {
            if (ModelState.IsValid)
            {
                db.NextChequingAccounts.Add(nextchequingaccount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nextchequingaccount);
        }

        //
        // GET: /NextChequingAccount/Edit/5

        public ActionResult Edit(int id = 0)
        {
            NextChequingAccount nextchequingaccount = db.NextChequingAccounts.Find(id);
            if (nextchequingaccount == null)
            {
                return HttpNotFound();
            }
            return View(nextchequingaccount);
        }

        //
        // POST: /NextChequingAccount/Edit/5

        [HttpPost]
        public ActionResult Edit(NextChequingAccount nextchequingaccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nextchequingaccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nextchequingaccount);
        }

        //
        // GET: /NextChequingAccount/Delete/5

        public ActionResult Delete(int id = 0)
        {
            NextChequingAccount nextchequingaccount = db.NextChequingAccounts.Find(id);
            if (nextchequingaccount == null)
            {
                return HttpNotFound();
            }
            return View(nextchequingaccount);
        }

        //
        // POST: /NextChequingAccount/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            NextChequingAccount nextchequingaccount = db.NextChequingAccounts.Find(id);
            db.NextChequingAccounts.Remove(nextchequingaccount);
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