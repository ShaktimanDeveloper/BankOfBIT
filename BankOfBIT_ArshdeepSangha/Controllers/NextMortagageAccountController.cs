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
    public class NextMortagageAccountController : Controller
    {
        private BankOfBIT_ArshdeepSanghaContext db = new BankOfBIT_ArshdeepSanghaContext();

        //
        // GET: /NextMortagageAccount/

        public ActionResult Index()
        {
            //Returns the next mortgage account instance.
            return View(NextMortagageAccount.GetInstance());
        }

        //
        // GET: /NextMortagageAccount/Details/5

        public ActionResult Details(int id = 0)
        {
            NextMortagageAccount nextmortagageaccount = db.NextMortagageAccounts.Find(id);
            if (nextmortagageaccount == null)
            {
                return HttpNotFound();
            }
            return View(nextmortagageaccount);
        }

        //
        // GET: /NextMortagageAccount/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /NextMortagageAccount/Create

        [HttpPost]
        public ActionResult Create(NextMortagageAccount nextmortagageaccount)
        {
            if (ModelState.IsValid)
            {
                db.NextMortagageAccounts.Add(nextmortagageaccount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nextmortagageaccount);
        }

        //
        // GET: /NextMortagageAccount/Edit/5

        public ActionResult Edit(int id = 0)
        {
            NextMortagageAccount nextmortagageaccount = db.NextMortagageAccounts.Find(id);
            if (nextmortagageaccount == null)
            {
                return HttpNotFound();
            }
            return View(nextmortagageaccount);
        }

        //
        // POST: /NextMortagageAccount/Edit/5

        [HttpPost]
        public ActionResult Edit(NextMortagageAccount nextmortagageaccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nextmortagageaccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nextmortagageaccount);
        }

        //
        // GET: /NextMortagageAccount/Delete/5

        public ActionResult Delete(int id = 0)
        {
            NextMortagageAccount nextmortagageaccount = db.NextMortagageAccounts.Find(id);
            if (nextmortagageaccount == null)
            {
                return HttpNotFound();
            }
            return View(nextmortagageaccount);
        }

        //
        // POST: /NextMortagageAccount/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            NextMortagageAccount nextmortagageaccount = db.NextMortagageAccounts.Find(id);
            db.NextMortagageAccounts.Remove(nextmortagageaccount);
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