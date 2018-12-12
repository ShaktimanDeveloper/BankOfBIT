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
    public class PayeeController : Controller
    {
        private BankOfBIT_ArshdeepSanghaContext db = new BankOfBIT_ArshdeepSanghaContext();

        //
        // GET: /Payee/

        public ActionResult Index()
        {
            return View(db.Payees.ToList());
        }

        //
        // GET: /Payee/Details/5

        public ActionResult Details(int id = 0)
        {
            Payee payee = db.Payees.Find(id);
            if (payee == null)
            {
                return HttpNotFound();
            }
            return View(payee);
        }

        //
        // GET: /Payee/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Payee/Create

        [HttpPost]
        public ActionResult Create(Payee payee)
        {
            if (ModelState.IsValid)
            {
                db.Payees.Add(payee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(payee);
        }

        //
        // GET: /Payee/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Payee payee = db.Payees.Find(id);
            if (payee == null)
            {
                return HttpNotFound();
            }
            return View(payee);
        }

        //
        // POST: /Payee/Edit/5

        [HttpPost]
        public ActionResult Edit(Payee payee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(payee);
        }

        //
        // GET: /Payee/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Payee payee = db.Payees.Find(id);
            if (payee == null)
            {
                return HttpNotFound();
            }
            return View(payee);
        }

        //
        // POST: /Payee/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Payee payee = db.Payees.Find(id);
            db.Payees.Remove(payee);
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