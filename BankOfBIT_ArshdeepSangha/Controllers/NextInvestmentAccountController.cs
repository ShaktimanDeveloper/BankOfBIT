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
    public class NextInvestmentAccountController : Controller
    {
        private BankOfBIT_ArshdeepSanghaContext db = new BankOfBIT_ArshdeepSanghaContext();

        //
        // GET: /NextInvestmentAccount/

        public ActionResult Index()
        {
            //Returns the nextInvestmentAccount instance.
            return View(NextInvestmentAccount.GetInstance());
        }

        //
        // GET: /NextInvestmentAccount/Details/5

        public ActionResult Details(int id = 0)
        {
            NextInvestmentAccount nextinvestmentaccount = db.NextInvestmentAccounts.Find(id);
            if (nextinvestmentaccount == null)
            {
                return HttpNotFound();
            }
            return View(nextinvestmentaccount);
        }

        //
        // GET: /NextInvestmentAccount/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /NextInvestmentAccount/Create

        [HttpPost]
        public ActionResult Create(NextInvestmentAccount nextinvestmentaccount)
        {
            if (ModelState.IsValid)
            {
                db.NextInvestmentAccounts.Add(nextinvestmentaccount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nextinvestmentaccount);
        }

        //
        // GET: /NextInvestmentAccount/Edit/5

        public ActionResult Edit(int id = 0)
        {
            NextInvestmentAccount nextinvestmentaccount = db.NextInvestmentAccounts.Find(id);
            if (nextinvestmentaccount == null)
            {
                return HttpNotFound();
            }
            return View(nextinvestmentaccount);
        }

        //
        // POST: /NextInvestmentAccount/Edit/5

        [HttpPost]
        public ActionResult Edit(NextInvestmentAccount nextinvestmentaccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nextinvestmentaccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nextinvestmentaccount);
        }

        //
        // GET: /NextInvestmentAccount/Delete/5

        public ActionResult Delete(int id = 0)
        {
            NextInvestmentAccount nextinvestmentaccount = db.NextInvestmentAccounts.Find(id);
            if (nextinvestmentaccount == null)
            {
                return HttpNotFound();
            }
            return View(nextinvestmentaccount);
        }

        //
        // POST: /NextInvestmentAccount/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            NextInvestmentAccount nextinvestmentaccount = db.NextInvestmentAccounts.Find(id);
            db.NextInvestmentAccounts.Remove(nextinvestmentaccount);
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