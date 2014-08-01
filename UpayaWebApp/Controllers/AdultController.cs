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
    public class AdultController : Controller
    {
        private DataModelContainer db = new DataModelContainer();
        private static string DisabilitiesPrefix = "Disab";

        // GET: /Adult/
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        public ActionResult Index(Guid? bid)
        {
            // Adults are per Beneficiary
            if (bid == null)
            {
                //return RedirectToAction("Index", "Home");
                return RedirectToAction("AppError", "Home", new { msg = "Adult::Details: bid == null" });
            }

            ViewBag.Beneficiary = db.Beneficiaries.Find(bid);
            var adults = db.Adults.Where(a => a.BeneficiaryId == bid).Include(a => a.Gender).Include(a => a.EducationLevel).Include(a => a.Beneficiary).Include(a => a.AdultRelationship);
            return View(adults.ToList());
        }

        // GET: /Adult/Details/5
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return RedirectToAction("AppError", "Home", new { msg = "Adult::Details: id == null" });
            }
            Adult adult = db.Adults.Find(id);
            if (adult == null)
            {
                //return HttpNotFound();
                return RedirectToAction("AppError", "Home", new { msg = "Adult::Details: invalid id" });
            }

            ViewBag.DisabilitiesValue = CheckBoxHelper.DisabilitiesKeysToValues(adult.Disabilities, db);

            return View(adult);
        }

        // GET: /Adult/Create
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        public ActionResult Create(Guid? bid)
        {
            if (bid == null)
            {
                //return RedirectToAction("Index", "Home");
                return RedirectToAction("AppError", "Home", new { msg = "Adult::Create: bid == null" });
            }

            ViewBag.Beneficiary = db.Beneficiaries.Find(bid);
            if (ViewBag.Beneficiary == null)
            {
                return RedirectToAction("AppError", "Home", new { msg = "Adult::Create: invalid bid" });
            }

            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Title");
            //ViewBag.DisabilityId = new SelectList(db.Disabilities, "Id", "Title");
            ViewBag.EducationLevelId = new SelectList(db.EducationLevels, "Id", "Title");
            //ViewBag.BeneficiaryId = new SelectList(db.Beneficiaries, "Id", "Name");
            ViewBag.AdultRelationshipId = new SelectList(db.AdultRelationships, "Id", "Title");
            ViewBag.PrimaryOccupationId = new SelectList(db.Occupations, "Id", "Title");
            ViewBag.SecondaryOccupationId = new SelectList(db.Occupations, "Id", "Title");
            // Checkbox sets
            ViewBag.DisabilitiesCBData = CheckBoxHelper.GetDisabilities(db, String.Empty, DisabilitiesPrefix);

            return View();
        }

        // POST: /Adult/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,GenderId,Disability,EducationLevelId,BeneficiaryId,AdultRelationshipId,AdultOccupationId,BirthDay,BirthMonth,BirthYear,EmplSameCompany, PrimaryOccupationId,SecondaryOccupationId,Block,PrimaryWorkDaysM,SecondaryWorkDaysM,PrimaryDailyWage,SecondaryDailyWage")] Adult adult)
        {
            if (ModelState.IsValid)
            {
                adult.Id = Guid.NewGuid();
                // Get the checkbox values
                if (adult.Disability)
                    adult.Disabilities = CheckBoxHelper.ExtractValues(Request.Form, CheckBoxHelper.GetDisabilityIds(db), DisabilitiesPrefix);
                // 
                adult.OrigEntryDate = FormatHelper.ExtractDate(Request.Form, "OrigEntryDate");

                db.Adults.Add(adult);
                db.SaveChanges();
                HistoryHelper.StartHistory(adult);
                return RedirectToAction("Index", new { bid = adult.BeneficiaryId });
            }

            ViewBag.Beneficiary = db.Beneficiaries.Find(adult.BeneficiaryId);
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Title", adult.GenderId);
            //ViewBag.DisabilityId = new SelectList(db.Disabilities, "Id", "Title", adult.DisabilityId);
            ViewBag.EducationLevelId = new SelectList(db.EducationLevels, "Id", "Title", adult.EducationLevelId);
            //ViewBag.BeneficiaryId = new SelectList(db.Beneficiaries, "Id", "Name", adult.BeneficiaryId);
            ViewBag.AdultRelationshipId = new SelectList(db.AdultRelationships, "Id", "Title", adult.AdultRelationshipId);
            ViewBag.PrimaryOccupationId = new SelectList(db.Occupations, "Id", "Title");
            ViewBag.SecondaryOccupationId = new SelectList(db.Occupations, "Id", "Title");
            // Checkbox sets
            ViewBag.DisabilitiesCBData = CheckBoxHelper.GetDisabilities(db, String.Empty, DisabilitiesPrefix);

            return View(adult);
        }

        // GET: /Adult/Edit/5
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return RedirectToAction("AppError", "Home", new { msg = "Adult::Edit: id == null" });
            }
            Adult adult = db.Adults.Find(id);
            if (adult == null)
            {
                //return HttpNotFound();
                return RedirectToAction("AppError", "Home", new { msg = "Adult::Edit: invalid id" });
            }

            ViewBag.Beneficiary = db.Beneficiaries.Find(adult.BeneficiaryId);
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Title", adult.GenderId);
            //ViewBag.DisabilityId = new SelectList(db.Disabilities, "Id", "Title", adult.DisabilityId);
            ViewBag.EducationLevelId = new SelectList(db.EducationLevels, "Id", "Title", adult.EducationLevelId);
            //ViewBag.BeneficiaryId = new SelectList(db.Beneficiaries, "Id", "Name", adult.BeneficiaryId);
            ViewBag.AdultRelationshipId = new SelectList(db.AdultRelationships, "Id", "Title", adult.AdultRelationshipId);
            ViewBag.PrimaryOccupationId = new SelectList(db.Occupations, "Id", "Title", adult.PrimaryOccupationId);
            ViewBag.SecondaryOccupationId = new SelectList(db.Occupations, "Id", "Title", adult.SecondaryOccupationId);
            // Checkbox sets
            ViewBag.DisabilitiesCBData = CheckBoxHelper.GetDisabilities(db, adult.Disabilities, DisabilitiesPrefix);

            return View(adult);
        }

        // POST: /Adult/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,GenderId,Disability,EducationLevelId,BeneficiaryId,AdultRelationshipId,BirthDay,BirthMonth,BirthYear,EmplSameCompany, PrimaryOccupationId,SecondaryOccupationId,Block,PrimaryWorkDaysM,SecondaryWorkDaysM,PrimaryDailyWage,SecondaryDailyWage")] Adult adult)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adult).State = EntityState.Modified;
                // Get the checkbox values
                if (adult.Disability)
                    adult.Disabilities = CheckBoxHelper.ExtractValues(Request.Form, CheckBoxHelper.GetDisabilityIds(db), DisabilitiesPrefix);
                else
                    adult.Disabilities = String.Empty;
                // 
                adult.OrigEntryDate = FormatHelper.ExtractDate(Request.Form, "OrigEntryDate");

                db.SaveChanges();
                HistoryHelper.RecordHistory(adult);
                return RedirectToAction("Index", new { bid = adult.BeneficiaryId });
            }

            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Title", adult.GenderId);
            //ViewBag.DisabilityId = new SelectList(db.Disabilities, "Id", "Title", adult.DisabilityId);
            ViewBag.EducationLevelId = new SelectList(db.EducationLevels, "Id", "Title", adult.EducationLevelId);
            //ViewBag.BeneficiaryId = new SelectList(db.Beneficiaries, "Id", "Name", adult.BeneficiaryId);
            ViewBag.AdultRelationshipId = new SelectList(db.AdultRelationships, "Id", "Title", adult.AdultRelationshipId);
            ViewBag.PrimaryOccupationId = new SelectList(db.Occupations, "Id", "Title", adult.PrimaryOccupationId);
            ViewBag.SecondaryOccupationId = new SelectList(db.Occupations, "Id", "Title", adult.SecondaryOccupationId);
            // Checkbox sets
            ViewBag.DisabilitiesCBData = CheckBoxHelper.GetDisabilities(db, adult.Disabilities, DisabilitiesPrefix);

            return View(adult);
        }

        /* GET: /Adult/Delete/5
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adult adult = db.Adults.Find(id);
            if (adult == null)
            {
                return HttpNotFound();
            }
            return View(adult);
        }

        / POST: /Adult/Delete/5
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Adult adult = db.Adults.Find(id);
            db.Adults.Remove(adult);
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
