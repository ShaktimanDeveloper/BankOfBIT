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
    public class InstitutionController : Controller
    {
        private BankOfBIT_ArshdeepSanghaContext db = new BankOfBIT_ArshdeepSanghaContext();

        //
        // GET: /Institution/

        public ActionResult Index()
        {
            return View(db.Institutions.ToList());
        }

        //
        // GET: /Institution/Details/5

        public ActionResult Details(int id = 0)
        {
            Institution institution = db.Institutions.Find(id);
            if (institution == null)
            {
                return HttpNotFound();
            }
            return View(institution);
        }

        //
        // GET: /Institution/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Institution/Create

        [HttpPost]
        public ActionResult Create(Institution institution)
        {
            if (ModelState.IsValid)
            {
                db.Institutions.Add(institution);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(institution);
        }

        //
        // GET: /Institution/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Institution institution = db.Institutions.Find(id);
            if (institution == null)
            {
                return HttpNotFound();
            }
            return View(institution);
        }

        //
        // POST: /Institution/Edit/5

        [HttpPost]
        public ActionResult Edit(Institution institution)
        {
            if (ModelState.IsValid)
            {
                db.Entry(institution).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(institution);
        }

        //
        // GET: /Institution/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Institution institution = db.Institutions.Find(id);
            if (institution == null)
            {
                return HttpNotFound();
            }
            return View(institution);
        }

        //
        // POST: /Institution/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Institution institution = db.Institutions.Find(id);
            db.Institutions.Remove(institution);
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