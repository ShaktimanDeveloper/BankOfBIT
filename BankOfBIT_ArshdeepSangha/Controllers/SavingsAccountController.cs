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
    public class SavingsAccountController : Controller
    {
        private BankOfBIT_ArshdeepSanghaContext db = new BankOfBIT_ArshdeepSanghaContext();

        //
        // GET: /SavingsAccount/

        public ActionResult Index()
        {
            //Replaced the Bankaccount to Savings account in order to the index for it.
            var bankaccounts = db.SavingsAccounts.Include(s => s.Client).Include(s => s.AccountState);
            return View(bankaccounts.ToList());
        }

        //
        // GET: /SavingsAccount/Details/5

        public ActionResult Details(int id = 0)
        {
            SavingsAccount savingsaccount = db.SavingsAccounts.Find(id);
            if (savingsaccount == null)
            {
                return HttpNotFound();
            }
            return View(savingsaccount);
        }

        //
        // GET: /SavingsAccount/Create

        public ActionResult Create()
        {
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FullName");
            ViewBag.AccountStateId = new SelectList(db.AccountStates, "AccountStateId", "Description");
            return View();
        }

        //
        // POST: /SavingsAccount/Create

        [HttpPost]
        public ActionResult Create(SavingsAccount savingsaccount)
        {
            //Calling method for auto increment.
            savingsaccount.SetNextAccountNumber();

            if (ModelState.IsValid)
            {
                db.BankAccounts.Add(savingsaccount);
                db.SaveChanges();
                
                //Swicthes the state if the account requirements are fullfilled.
                savingsaccount.ChangeState();
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }

            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FullName", savingsaccount.ClientId);
            ViewBag.AccountStateId = new SelectList(db.AccountStates, "AccountStateId", "Description", savingsaccount.AccountStateId);
            return View(savingsaccount);
        }

        //
        // GET: /SavingsAccount/Edit/5

        public ActionResult Edit(int id = 0)
        {
            SavingsAccount savingsaccount = db.SavingsAccounts.Find(id);
            if (savingsaccount == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FullName", savingsaccount.ClientId);
            ViewBag.AccountStateId = new SelectList(db.AccountStates, "AccountStateId", "Description", savingsaccount.AccountStateId);
            return View(savingsaccount);
        }

        //
        // POST: /SavingsAccount/Edit/5

        [HttpPost]
        public ActionResult Edit(SavingsAccount savingsaccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(savingsaccount).State = EntityState.Modified;
                db.SaveChanges();
                //code for database
                savingsaccount.ChangeState();
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FullName", savingsaccount.ClientId);
            ViewBag.AccountStateId = new SelectList(db.AccountStates, "AccountStateId", "Description", savingsaccount.AccountStateId);
            return View(savingsaccount);
        }

        //
        // GET: /SavingsAccount/Delete/5

        public ActionResult Delete(int id = 0)
        {
            SavingsAccount savingsaccount = db.SavingsAccounts.Find(id);
            if (savingsaccount == null)
            {
                return HttpNotFound();
            }
            return View(savingsaccount);
        }

        //
        // POST: /SavingsAccount/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            SavingsAccount savingsaccount = db.SavingsAccounts.Find(id);
            db.BankAccounts.Remove(savingsaccount);
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