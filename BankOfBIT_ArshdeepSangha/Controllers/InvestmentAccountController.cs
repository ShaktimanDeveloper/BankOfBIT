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
    public class InvestmentAccountController : Controller
    {
        private BankOfBIT_ArshdeepSanghaContext db = new BankOfBIT_ArshdeepSanghaContext();

        //
        // GET: /InvestmentAccount/

        public ActionResult Index()
        {
            //Returns the Investment account index for bankaccounts and replaced bankaccount and added Investment account instead
            var bankaccounts = db.InvestmentAccounts.Include(i => i.Client).Include(i => i.AccountState);
            return View(bankaccounts.ToList());
        }

        //
        // GET: /InvestmentAccount/Details/5

        public ActionResult Details(int id = 0)
        {
            InvestmentAccount investmentaccount = db.InvestmentAccounts.Find(id);
            if (investmentaccount == null)
            {
                return HttpNotFound();
            }
            return View(investmentaccount);
        }

        //
        // GET: /InvestmentAccount/Create

        public ActionResult Create()
        {
            //Displays the fullname for the client
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FullName");
            ViewBag.AccountStateId = new SelectList(db.AccountStates, "AccountStateId", "Description");
            return View();
        }

        //
        // POST: /InvestmentAccount/Create

        [HttpPost]
        public ActionResult Create(InvestmentAccount investmentaccount)
        {
            //Calling method for auto increment.
            investmentaccount.SetNextAccountNumber();

            if (ModelState.IsValid)
            {
                db.BankAccounts.Add(investmentaccount);
                
                db.SaveChanges();
                //Changes the accountstate if the account requirements are not fullfilled
                investmentaccount.ChangeState();
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //Displays the fullname for the client
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FullName", investmentaccount.ClientId);
            ViewBag.AccountStateId = new SelectList(db.AccountStates, "AccountStateId", "Description", investmentaccount.AccountStateId);
            return View(investmentaccount);
        }

        //
        // GET: /InvestmentAccount/Edit/5

        public ActionResult Edit(int id = 0)
        {
            InvestmentAccount investmentaccount = db.InvestmentAccounts.Find(id);
            if (investmentaccount == null)
            {
                return HttpNotFound();
            }

            //Displays the fullname for the client
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FullName", investmentaccount.ClientId);
            ViewBag.AccountStateId = new SelectList(db.AccountStates, "AccountStateId", "Description", investmentaccount.AccountStateId);
            return View(investmentaccount);
        }

        //
        // POST: /InvestmentAccount/Edit/5

        [HttpPost]
        public ActionResult Edit(InvestmentAccount investmentaccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(investmentaccount).State = EntityState.Modified;
                db.SaveChanges();
                //code for database
                investmentaccount.ChangeState();
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //Displays the fullname for the client
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FullName", investmentaccount.ClientId);
            ViewBag.AccountStateId = new SelectList(db.AccountStates, "AccountStateId", "Description", investmentaccount.AccountStateId);
            return View(investmentaccount);
        }

        //
        // GET: /InvestmentAccount/Delete/5

        public ActionResult Delete(int id = 0)
        {
            InvestmentAccount investmentaccount = db.InvestmentAccounts.Find(id);
            if (investmentaccount == null)
            {
                return HttpNotFound();
            }
            return View(investmentaccount);
        }

        //
        // POST: /InvestmentAccount/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            InvestmentAccount investmentaccount = db.InvestmentAccounts.Find(id);
            db.BankAccounts.Remove(investmentaccount);
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