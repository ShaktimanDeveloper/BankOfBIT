using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using BankOfBIT_ArshdeepSangha.Models;

namespace BankOfBIT_ArshdeepSangha.Controllers
{
    public class BankAccountController : ApiController
    {
        private BankOfBIT_ArshdeepSanghaContext db = new BankOfBIT_ArshdeepSanghaContext();

        // GET api/BankAccount
        public IEnumerable<BankAccount> GetBankAccounts()
        {
            var bankaccounts = db.BankAccounts.Include(b => b.Client).Include(b => b.AccountState);
            return bankaccounts.AsEnumerable();
        }

        // GET api/BankAccount/5
        public BankAccount GetBankAccount(int id)
        {
            BankAccount bankaccount = db.BankAccounts.Find(id);
            if (bankaccount == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return bankaccount;
        }

        // PUT api/BankAccount/5
        public HttpResponseMessage PutBankAccount(int id, BankAccount bankaccount)
        {
            if (ModelState.IsValid && id == bankaccount.BankAccountId)
            {
                db.Entry(bankaccount).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/BankAccount
        public HttpResponseMessage PostBankAccount(BankAccount bankaccount)
        {
            if (ModelState.IsValid)
            {
                db.BankAccounts.Add(bankaccount);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, bankaccount);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = bankaccount.BankAccountId }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/BankAccount/5
        public HttpResponseMessage DeleteBankAccount(int id)
        {
            BankAccount bankaccount = db.BankAccounts.Find(id);
            if (bankaccount == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.BankAccounts.Remove(bankaccount);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, bankaccount);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}