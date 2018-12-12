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
    public class AccountStateController : ApiController
    {
        private BankOfBIT_ArshdeepSanghaContext db = new BankOfBIT_ArshdeepSanghaContext();

        // GET api/AccountState
        public IEnumerable<AccountState> GetAccountStates()
        {
            return db.AccountStates.AsEnumerable();
        }

        // GET api/AccountState/5
        public AccountState GetAccountState(int id)
        {
            AccountState accountstate = db.AccountStates.Find(id);
            if (accountstate == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return accountstate;
        }

        // PUT api/AccountState/5
        public HttpResponseMessage PutAccountState(int id, AccountState accountstate)
        {
            if (ModelState.IsValid && id == accountstate.AccountStateId)
            {
                db.Entry(accountstate).State = EntityState.Modified;

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

        // POST api/AccountState
        public HttpResponseMessage PostAccountState(AccountState accountstate)
        {
            if (ModelState.IsValid)
            {
                db.AccountStates.Add(accountstate);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, accountstate);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = accountstate.AccountStateId }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/AccountState/5
        public HttpResponseMessage DeleteAccountState(int id)
        {
            AccountState accountstate = db.AccountStates.Find(id);
            if (accountstate == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.AccountStates.Remove(accountstate);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, accountstate);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}