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
    public class ChequingAccountController : Controller
    {
        private BankOfBIT_ArshdeepSanghaContext db = new BankOfBIT_ArshdeepSanghaContext();

        //
        // GET: /ChequingAccount/

        public ActionResult Index()
        {
            //Gets the chequing account index.
            var bankaccounts = db.ChequingAccounts.Include(c => c.Client).Include(c => c.AccountState);
            return View(bankaccounts.ToList());
        }

        //
        // GET: /ChequingAccount/Details/5

        public ActionResult Details(int id = 0)
        {
            ChequingAccount chequingaccount = db.ChequingAccounts.Find(id);
            if (chequingaccount == null)
            {
                return HttpNotFound();
            }
            return View(chequingaccount);
        }

        //
        // GET: /ChequingAccount/Create

        public ActionResult Create()
        {   
            //Displays the fullname for the Client
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FullName");
            //Displays the Account state description
            ViewBag.AccountStateId = new SelectList(db.AccountStates, "AccountStateId", "Description");
            return View();
        }

        //
        // POST: /ChequingAccount/Create

        [HttpPost]
        public ActionResult Create(ChequingAccount chequingaccount)
        {
            //Calling method for auto increment.
            chequingaccount.SetNextAccountNumber();

            if (ModelState.IsValid)
            {
                db.BankAccounts.Add(chequingaccount);
                db.SaveChanges();

                //code for database
                chequingaccount.ChangeState();
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FullName", chequingaccount.ClientId);
            ViewBag.AccountStateId = new SelectList(db.AccountStates, "AccountStateId", "Description", chequingaccount.AccountStateId);
            return View(chequingaccount);
        }

        //
        // GET: /ChequingAccount/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ChequingAccount chequingaccount = db.ChequingAccounts.Find(id);
            if (chequingaccount == null)
            {
                return HttpNotFound();
            }
            
            //Displays the fullname of the client
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FullName", chequingaccount.ClientId);
            ViewBag.AccountStateId = new SelectList(db.AccountStates, "AccountStateId", "Description", chequingaccount.AccountStateId);
            return View(chequingaccount);
        }

        //
        // POST: /ChequingAccount/Edit/5

        [HttpPost]
        public ActionResult Edit(ChequingAccount chequingaccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chequingaccount).State = EntityState.Modified;

                db.SaveChanges();

                //If the conditions for this account's state is not fullfilled,change the state for the account.
                chequingaccount.ChangeState();
                //Save the changes right after the change.
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FullName", chequingaccount.ClientId);
            ViewBag.AccountStateId = new SelectList(db.AccountStates, "AccountStateId", "Description", chequingaccount.AccountStateId);
            return View(chequingaccount);
        }

        //
        // GET: /ChequingAccount/Delete/5

        public ActionResult Delete(int id = 0)
        {
            //Assigned chequing account to this ,it was previously just account state.
            ChequingAccount chequingaccount = db.ChequingAccounts.Find(id);
            if (chequingaccount == null)
            {
                return HttpNotFound();
            }
            return View(chequingaccount);
        }

        //
        // POST: /ChequingAccount/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            //The delete method,find the id for the chequing account.
            ChequingAccount chequingaccount = db.ChequingAccounts.Find(id);
            db.BankAccounts.Remove(chequingaccount);
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