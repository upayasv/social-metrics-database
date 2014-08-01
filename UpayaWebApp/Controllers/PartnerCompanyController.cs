using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UpayaWebApp;

namespace UpayaWebApp.Controllers
{
    public class PartnerCompanyController : Controller
    {
        private DataModelContainer db = new DataModelContainer();

        // GET: /PartnerCompany/
        [Authorize(Roles = "UpayaAdmin")]
        public ActionResult Index()
        {
            return View(db.PartnerCompanies.ToList());
        }

        // GET: /PartnerCompany/Details/5
        [Authorize(Roles = "UpayaAdmin")]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartnerCompany partnercompany = db.PartnerCompanies.Find(id);
            if (partnercompany == null)
            {
                return HttpNotFound();
            }
            return View(partnercompany);
        }

        // GET: /PartnerCompany/Create
        [Authorize(Roles = "UpayaAdmin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /PartnerCompany/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name,Phone,Fax,Email,StartDateDay,StartDateMonth,StartDateYear")] PartnerCompany partnercompany)
        {
            if (ModelState.IsValid)
            {
                partnercompany.Id = Guid.NewGuid();
                db.PartnerCompanies.Add(partnercompany);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(partnercompany);
        }

        // GET: /PartnerCompany/Edit/5
        [Authorize(Roles = "UpayaAdmin")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartnerCompany partnercompany = db.PartnerCompanies.Find(id);
            if (partnercompany == null)
            {
                return HttpNotFound();
            }
            return View(partnercompany);
        }

        // POST: /PartnerCompany/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "UpayaAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Phone,Fax,Email,StartDateDay,StartDateMonth,StartDateYear")] PartnerCompany partnercompany)
        {
            if (ModelState.IsValid)
            {
                db.Entry(partnercompany).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(partnercompany);
        }

        /* GET: /PartnerCompany/Delete/5
        [Authorize(Roles = "UpayaAdmin")]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartnerCompany partnercompany = db.PartnerCompanies.Find(id);
            if (partnercompany == null)
            {
                return HttpNotFound();
            }
            return View(partnercompany);
        }

        // POST: /PartnerCompany/Delete/5
        [Authorize(Roles = "UpayaAdmin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            PartnerCompany partnercompany = db.PartnerCompanies.Find(id);
            db.PartnerCompanies.Remove(partnercompany);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        */

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
