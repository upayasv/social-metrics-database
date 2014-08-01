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
    public class GovServicesController : Controller
    {
        private DataModelContainer db = new DataModelContainer();
        private static string CardsPrefix = "Cs";
        private static string ServicesPrefix = "Ss";

        /* GET: /GovServices/
        public ActionResult Index()
        {
            return View(db.GovernmentServices.ToList());
        }
        */

        // GET: /GovServices/Details/5
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return RedirectToAction("AppError", "Home", new { msg = "GovServices::Details: id == null" });
            }
            GovernmentServicesInfo govServices = db.GovernmentServices.Find(id);
            if (govServices == null)
            {
                return RedirectToAction("Create", new { id = id.Value });
                /*
                GovernmentServicesInfo gs = new GovernmentServicesInfo();
                gs.Id = id.Value;
                gs.Beneficiary = db.Beneficiaries.Find(gs.Id); // ???
                gs.GovCards = gs.GovServices = String.Empty;
                gs.RcvGovPension = gs.RcvPrivatePension = false;
                
                db.GovernmentServices.Add(gs);
                db.SaveChanges();
                */ 
            }
            govServices = db.GovernmentServices.Find(id);
            govServices.Beneficiary = db.Beneficiaries.Find(id);
            ViewBag.CardsValue = CheckBoxHelper.GovCardsKeysToValues(govServices.GovCards, db);
            ViewBag.ServicesValue = CheckBoxHelper.GovServicesKeysToValues(govServices.GovServices, db);

            return View(govServices);
        }

        // GET: /GovServices/Create
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        public ActionResult Create(Guid? id)
        {
            if (id == null)
            {
                return RedirectToAction("AppError", "Home", new { msg = "HousingInfo::Create: id == null" });
            }

            ViewBag.Beneficiary = db.Beneficiaries.Find(id);
            // Checkbox sets
            ViewBag.CardsCBData = CheckBoxHelper.GetGovCards(db, "", CardsPrefix);
            ViewBag.ServicesCBData = CheckBoxHelper.GetGovServices(db, "", ServicesPrefix);

            return View();
        }

        // POST: /GovServices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        public ActionResult Create([Bind(Include = "Id,GovCards,OtherCardDescr,GovServices")] GovernmentServicesInfo governmentservicesinfo)
        {
            if (ModelState.IsValid)
            {
                governmentservicesinfo.Beneficiary = db.Beneficiaries.Find(governmentservicesinfo.Id); // ???
                // Get the checkbox values
                governmentservicesinfo.GovCards = CheckBoxHelper.ExtractValues(Request.Form, CheckBoxHelper.GetGovCardIds(db), CardsPrefix);
                governmentservicesinfo.GovServices = CheckBoxHelper.ExtractValues(Request.Form, CheckBoxHelper.GetGovServiceIds(db), ServicesPrefix);
                //
                governmentservicesinfo.OrigEntryDate = FormatHelper.ExtractDate(Request.Form, "OrigEntryDate");

                db.GovernmentServices.Add(governmentservicesinfo);
                db.SaveChanges();
                HistoryHelper.StartHistory(governmentservicesinfo);
                return RedirectToAction("Details", new { id = governmentservicesinfo.Id });
            }

            ViewBag.Beneficiary = db.Beneficiaries.Find(governmentservicesinfo.Id);
            // Checkbox sets
            ViewBag.CardsCBData = CheckBoxHelper.GetGovCards(db, "", CardsPrefix);
            ViewBag.ServicesCBData = CheckBoxHelper.GetGovServices(db, "", ServicesPrefix);
            return View(governmentservicesinfo);
        }

        // GET: /GovServices/Edit/5
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return RedirectToAction("AppError", "Home", new { msg = "GovServices::Edit: id == null" });
            }
            GovernmentServicesInfo governmentservicesinfo = db.GovernmentServices.Find(id);
            if (governmentservicesinfo == null)
            {
                //return HttpNotFound();
                return RedirectToAction("AppError", "Home", new { msg = "GovServices::Edit: invalid id" });
            }

            ViewBag.Beneficiary = db.Beneficiaries.Find(id);
            // Checkbox sets
            ViewBag.CardsCBData = CheckBoxHelper.GetGovCards(db, governmentservicesinfo.GovCards, CardsPrefix);
            ViewBag.ServicesCBData = CheckBoxHelper.GetGovServices(db, governmentservicesinfo.GovServices, ServicesPrefix);

            return View(governmentservicesinfo);
        }

        // POST: /GovServices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        public ActionResult Edit([Bind(Include = "Id,GovCards,OtherCardDescr,GovServices")] GovernmentServicesInfo governmentservicesinfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(governmentservicesinfo).State = EntityState.Modified;
                // Get the checkbox values
                governmentservicesinfo.GovCards = CheckBoxHelper.ExtractValues(Request.Form, CheckBoxHelper.GetGovCardIds(db), CardsPrefix);
                governmentservicesinfo.GovServices = CheckBoxHelper.ExtractValues(Request.Form, CheckBoxHelper.GetGovServiceIds(db), ServicesPrefix);
                //
                governmentservicesinfo.OrigEntryDate = FormatHelper.ExtractDate(Request.Form, "OrigEntryDate");

                db.SaveChanges();
                HistoryHelper.RecordHistory(governmentservicesinfo);
                return RedirectToAction("Details", new { id = governmentservicesinfo.Id });
            }
            ViewBag.Beneficiary = db.Beneficiaries.Find(governmentservicesinfo.Id);
            // Checkbox sets
            ViewBag.CardsCBData = CheckBoxHelper.GetGovCards(db, governmentservicesinfo.GovCards, CardsPrefix);
            ViewBag.ServicesCBData = CheckBoxHelper.GetGovServices(db, governmentservicesinfo.GovServices, ServicesPrefix);
            return View(governmentservicesinfo);
        }

        /* GET: /GovServices/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GovernmentServicesInfo governmentservicesinfo = db.GovernmentServices.Find(id);
            if (governmentservicesinfo == null)
            {
                return HttpNotFound();
            }
            return View(governmentservicesinfo);
        }

        // POST: /GovServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            GovernmentServicesInfo governmentservicesinfo = db.GovernmentServices.Find(id);
            db.GovernmentServices.Remove(governmentservicesinfo);
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
