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
    public class TransactionController : Controller
    {
        private BankOfBIT_ArshdeepSanghaContext db = new BankOfBIT_ArshdeepSanghaContext();

        //
        // GET: /Transaction/

        public ActionResult Index()
        {
            var transactions = db.Transactions.Include(t => t.BankAccount).Include(t => t.TransactionType);
            return View(transactions.ToList());
        }

        //
        // GET: /Transaction/Details/5

        public ActionResult Details(int id = 0)
        {
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        //
        // GET: /Transaction/Create

        public ActionResult Create()
        {
            //Changed one of the select list to account number
            ViewBag.BankAccountId = new SelectList(db.BankAccounts, "BankAccountId", "AccountNumber");
            ViewBag.TransactionTypeId = new SelectList(db.TransactionTypes, "TransactionTypeId", "Description");
            return View();
        }

        //
        // POST: /Transaction/Create

        [HttpPost]
        public ActionResult Create(Transaction transaction)
        {
            //Calling method for auto increment.
            transaction.SetNextTransactionNumber();

            if (ModelState.IsValid)
            {
                db.Transactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //Changed one of the select list to account number
            ViewBag.BankAccountId = new SelectList(db.BankAccounts, "BankAccountId", "AccountNumber", transaction.BankAccountId);
            ViewBag.TransactionTypeId = new SelectList(db.TransactionTypes, "TransactionTypeId", "Description", transaction.TransactionTypeId);
            return View(transaction);
        }

        //
        // GET: /Transaction/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }

            //Changed one of the select list to account number
            ViewBag.BankAccountId = new SelectList(db.BankAccounts, "BankAccountId", "AccountNumber", transaction.BankAccountId);
            ViewBag.TransactionTypeId = new SelectList(db.TransactionTypes, "TransactionTypeId", "Description", transaction.TransactionTypeId);
            return View(transaction);
        }

        //
        // POST: /Transaction/Edit/5

        [HttpPost]
        public ActionResult Edit(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //Changed one of the select list to account number
            ViewBag.BankAccountId = new SelectList(db.BankAccounts, "BankAccountId", "AccountNumber", transaction.BankAccountId);
            ViewBag.TransactionTypeId = new SelectList(db.TransactionTypes, "TransactionTypeId", "Description", transaction.TransactionTypeId);
            return View(transaction);
        }

        //
        // GET: /Transaction/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        //
        // POST: /Transaction/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);
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