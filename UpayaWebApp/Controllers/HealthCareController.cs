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
    public class HealthCareController : Controller
    {
        private DataModelContainer db = new DataModelContainer();
        private static string HcProvidersPrefix = "HCPs";
        //private static string HealthIssuesPrefix = "HIPs";
        //private static string MedSourcesPrefix = "MSPs";

        /* GET: /HealthCare/
        public ActionResult Index()
        {
            return View(db.HealthCareInfos.ToList());
        }
        */
        // GET: /HealthCare/Details/5
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return RedirectToAction("AppError", "Home", new { msg = "MajorExpenses::Details: id == null" });
            }
            HealthCareInfo healthcareinfo = db.HealthCareInfos.Find(id);
            if (healthcareinfo == null)
            {
                return RedirectToAction("Create", new { id = id.Value });
                /*
                HealthCareInfo hci = new HealthCareInfo();
                hci.Id = id.Value;
                hci.Beneficiary = db.Beneficiaries.Find(hci.Id); // ???
                hci.HcProviders = String.Empty;
                db.HealthCareInfos.Add(hci);
                db.SaveChanges();
                */ 
            }
            healthcareinfo = db.HealthCareInfos.Find(id);
            ViewBag.Beneficiary = db.Beneficiaries.Find(id);
            ViewBag.HcProvidersValue = CheckBoxHelper.HcProviderKeysToValues(healthcareinfo.HcProviders, db);
            //ViewBag.HealthIssuesValue = CheckBoxHelper.HealthIssueKeysToValues(healthcareinfo.HealthIssues, db);
            //ViewBag.MedSourcesValue = CheckBoxHelper.MedSourceKeysToValues(healthcareinfo.MedSources, db);

            return View(healthcareinfo);
        }

        // GET: /HealthCare/Create
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        public ActionResult Create(Guid? id)
        {
            if (id == null)
            {
                return RedirectToAction("AppError", "Home", new { msg = "HealthCare::Create: id == null" });
            }

            ViewBag.Beneficiary = db.Beneficiaries.Find(id);
            // Checkbox sets
            ViewBag.HcProvidersCBData = CheckBoxHelper.GetHcProviders(db, "", HcProvidersPrefix);
            return View();
        }

        // POST: /HealthCare/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        public ActionResult Create([Bind(Include = "Id,HcProviders")] HealthCareInfo healthcareinfo)
        {
            if (ModelState.IsValid)
            {
                healthcareinfo.Beneficiary = db.Beneficiaries.Find(healthcareinfo.Id); // ???
                // Get the checkbox values
                healthcareinfo.HcProviders = CheckBoxHelper.ExtractValues(Request.Form, CheckBoxHelper.GetHcProviderIds(db), HcProvidersPrefix);
                healthcareinfo.OrigEntryDate = FormatHelper.ExtractDate(Request.Form, "OrigEntryDate");
                db.HealthCareInfos.Add(healthcareinfo);
                db.SaveChanges();
                HistoryHelper.StartHistory(healthcareinfo);
                return RedirectToAction("Details", new { id = healthcareinfo.Id });
            }

            ViewBag.Beneficiary = db.Beneficiaries.Find(healthcareinfo.Id);
            // Checkbox sets
            ViewBag.HcProvidersCBData = CheckBoxHelper.GetHcProviders(db, healthcareinfo.HcProviders, HcProvidersPrefix);
            return View(healthcareinfo);
        }

        // GET: /HealthCare/Edit/5
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return RedirectToAction("AppError", "Home", new { msg = "HealthCare::Edit: id == null" });
            }
            HealthCareInfo healthcareinfo = db.HealthCareInfos.Find(id);
            if (healthcareinfo == null)
            {
                //return HttpNotFound();
                return RedirectToAction("AppError", "Home", new { msg = "HealthCare::Edit: invalid id" });
            }
            // Checkbox sets
            ViewBag.HcProvidersCBData = CheckBoxHelper.GetHcProviders(db, healthcareinfo.HcProviders, HcProvidersPrefix);
            //ViewBag.HealthIssuesCBData = CheckBoxHelper.GetHealthIssues(db, healthcareinfo.HealthIssues, HealthIssuesPrefix);
            //ViewBag.MedSourcesCBData = CheckBoxHelper.GetMedSources(db, healthcareinfo.MedSources, MedSourcesPrefix);

            return View(healthcareinfo);
        }

        // POST: /HealthCare/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        public ActionResult Edit([Bind(Include = "Id,HcProviders")] HealthCareInfo hci)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hci).State = EntityState.Modified;
                // Get the checkbox values
                hci.HcProviders = CheckBoxHelper.ExtractValues(Request.Form, CheckBoxHelper.GetHcProviderIds(db), HcProvidersPrefix);
                //hci.HealthIssues = CheckBoxHelper.ExtractValues(Request.Form, CheckBoxHelper.GetHealthIssueIds(db), HealthIssuesPrefix);
                //hci.MedSources = CheckBoxHelper.ExtractValues(Request.Form, CheckBoxHelper.GetMedSourceIds(db), MedSourcesPrefix);
                hci.OrigEntryDate = FormatHelper.ExtractDate(Request.Form, "OrigEntryDate");
                //
                db.SaveChanges();
                HistoryHelper.RecordHistory(hci);
                return RedirectToAction("Details", new { id = hci.Id });
            }
            return View(hci);
        }

        /* GET: /HealthCare/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HealthCareInfo healthcareinfo = db.HealthCareInfos.Find(id);
            if (healthcareinfo == null)
            {
                return HttpNotFound();
            }
            return View(healthcareinfo);
        }

        // POST: /HealthCare/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            HealthCareInfo healthcareinfo = db.HealthCareInfos.Find(id);
            db.HealthCareInfos.Remove(healthcareinfo);
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
