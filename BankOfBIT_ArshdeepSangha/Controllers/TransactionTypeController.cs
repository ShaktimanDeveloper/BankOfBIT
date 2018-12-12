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
    public class TransactionTypeController : Controller
    {
        private BankOfBIT_ArshdeepSanghaContext db = new BankOfBIT_ArshdeepSanghaContext();

        //
        // GET: /TransactionType/

        public ActionResult Index()
        {
            return View(db.TransactionTypes.ToList());
        }

        //
        // GET: /TransactionType/Details/5

        public ActionResult Details(int id = 0)
        {
            TransactionType transactiontype = db.TransactionTypes.Find(id);
            if (transactiontype == null)
            {
                return HttpNotFound();
            }
            return View(transactiontype);
        }

        //
        // GET: /TransactionType/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /TransactionType/Create

        [HttpPost]
        public ActionResult Create(TransactionType transactiontype)
        {
            if (ModelState.IsValid)
            {
                db.TransactionTypes.Add(transactiontype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(transactiontype);
        }

        //
        // GET: /TransactionType/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TransactionType transactiontype = db.TransactionTypes.Find(id);
            if (transactiontype == null)
            {
                return HttpNotFound();
            }
            return View(transactiontype);
        }

        //
        // POST: /TransactionType/Edit/5

        [HttpPost]
        public ActionResult Edit(TransactionType transactiontype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transactiontype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transactiontype);
        }

        //
        // GET: /TransactionType/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TransactionType transactiontype = db.TransactionTypes.Find(id);
            if (transactiontype == null)
            {
                return HttpNotFound();
            }
            return View(transactiontype);
        }

        //
        // POST: /TransactionType/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            TransactionType transactiontype = db.TransactionTypes.Find(id);
            db.TransactionTypes.Remove(transactiontype);
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