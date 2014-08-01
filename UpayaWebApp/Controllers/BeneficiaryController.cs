using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UpayaWebApp;

namespace UpayaWebApp.Controllers
{
    public class BeneficiaryController : Controller
    {
        private DataModelContainer db = new DataModelContainer();
        private static string DisabilitiesPrefix = "Disab";

        // GET: /Beneficiary/
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        public ActionResult Index()
        {
            // Only show the beneficiaries for this partner company
            Guid curCompany = AccountHelper.GetCurCompanyId(db);
            var Beneficiaries = db.Beneficiaries.Where(x => x.PCompanyId == curCompany).Include(b => b.Gender).Include(b => b.Town).Include(b => b.EducationLevel);
            return View(Beneficiaries.ToList());
        }

        // GET: /Beneficiary/Details/5
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return RedirectToAction("AppError", "Home", new { msg = "Beneficiary::Details: id == null" });
            }
            Beneficiary beneficiary = db.Beneficiaries.Find(id);
            if (beneficiary == null)
            {
                //return HttpNotFound();
                return RedirectToAction("AppError", "Home", new { msg = "Beneficiary::Details: invalid id" });
            }

            ViewBag.DisabilitiesValue = CheckBoxHelper.DisabilitiesKeysToValues(beneficiary.Disabilities, db);

            return View(beneficiary);
        }

        // GET: /Beneficiary/Create
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        public ActionResult Create()
        {
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Title");
            //ViewBag.ReligionId = new SelectList(db.Religions, "Id", "Title");
            //ViewBag.CasteId = new SelectList(db.Castes, "Id", "Title");
            ViewBag.TownId = new SelectList(db.Towns, "Id", "Name");
            //ViewBag.DisabilityId = new SelectList(db.Disabilities, "Id", "Title");
            ViewBag.EducationLevelId = new SelectList(db.EducationLevels, "Id", "Title");
            //ViewBag.LanguageId = new SelectList(db.Languages, "Id", "Title");
            ViewBag.PrimaryOccupationId = new SelectList(db.Occupations, "Id", "Title");
            ViewBag.SecondaryOccupationId = new SelectList(db.Occupations, "Id", "Title");
            // Checkbox sets
            ViewBag.DisabilitiesCBData = CheckBoxHelper.GetDisabilities(db, String.Empty, DisabilitiesPrefix); 
            
            return View();
        }

        // POST: /Beneficiary/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,GenderId,TownId,Address,Phone,Disability,EducationLevelId,BirthDay,BirthMonth,BirthYear,UniqueId,PrimaryOccupationId,SecondaryOccupationId,Block,PrimaryWorkDaysM,SecondaryWorkDaysM,PrimaryDailyWage,SecondaryDailyWage,StartDateDay,StartDateMonth,StartDateYear")] Beneficiary beneficiary)
        {
            if (beneficiary.Address == null)
                beneficiary.Address = string.Empty;

            if (ModelState.IsValid)
            {
                beneficiary.Id = Guid.NewGuid();
                beneficiary.ReligionId = beneficiary.CasteId = beneficiary.LanguageId = 1; //
                beneficiary.OrigEntryDate = FormatHelper.ExtractDate(Request.Form, "OrigEntryDate");
                // Assign to the active company
                beneficiary.PCompanyId = AccountHelper.GetCurCompanyId(db);

                db.Beneficiaries.Add(beneficiary);
                try
                {
                    db.SaveChanges();
                }
                catch(DbEntityValidationException dbe)
                {
                    //string ermsg = "";
                    //foreach (DbEntityValidationResult res in dbe.EntityValidationErrors)
                    //    ermsg += res + "; ";
                    return RedirectToAction("AppError", "Home", new { msg = "Beneficiary::Create: DbEntityValidationException. Please contact admin." });
                }
                HistoryHelper.StartHistory(beneficiary);
                return RedirectToAction("Index");
            }

            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Title", beneficiary.GenderId);
            //ViewBag.ReligionId = new SelectList(db.Religions, "Id", "Title", beneficiary.ReligionId);
            //ViewBag.CasteId = new SelectList(db.Castes, "Id", "Title", beneficiary.CasteId);
            ViewBag.TownId = new SelectList(db.Towns, "Id", "Name", beneficiary.TownId);
            //ViewBag.DisabilityId = new SelectList(db.Disabilities, "Id", "Title", beneficiary.DisabilityId);
            ViewBag.EducationLevelId = new SelectList(db.EducationLevels, "Id", "Title", beneficiary.EducationLevelId);
            //ViewBag.LanguageId = new SelectList(db.Languages, "Id", "Title", beneficiary.LanguageId);
            ViewBag.PrimaryOccupationId = new SelectList(db.Occupations, "Id", "Title");
            ViewBag.SecondaryOccupationId = new SelectList(db.Occupations, "Id", "Title");
            // Checkbox sets
            ViewBag.DisabilitiesCBData = CheckBoxHelper.GetDisabilities(db, beneficiary.Disabilities, DisabilitiesPrefix);
            //
            beneficiary.OrigEntryDate = FormatHelper.ExtractDate(Request.Form, "OrigEntryDate");

            return View(beneficiary);
        }

        // GET: /Beneficiary/Edit/5
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return RedirectToAction("AppError", "Home", new { msg = "Beneficiary::Edit: id == null" });
            }
            Beneficiary beneficiary = db.Beneficiaries.Find(id);
            if (beneficiary == null)
            {
                //return HttpNotFound();
                return RedirectToAction("AppError", "Home", new { msg = "Beneficiary::Edit: invalid id" });
            }
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Title", beneficiary.GenderId);
            //ViewBag.ReligionId = new SelectList(db.Religions, "Id", "Title", beneficiary.ReligionId);
            //ViewBag.CasteId = new SelectList(db.Castes, "Id", "Title", beneficiary.CasteId);
            ViewBag.TownId = new SelectList(db.Towns, "Id", "Name", beneficiary.TownId);
            //ViewBag.DisabilityId = new SelectList(db.Disabilities, "Id", "Title", Beneficiary.DisabilityId);
            ViewBag.EducationLevelId = new SelectList(db.EducationLevels, "Id", "Title", beneficiary.EducationLevelId);
            //ViewBag.LanguageId = new SelectList(db.Languages, "Id", "Title", beneficiary.LanguageId);
            ViewBag.PrimaryOccupationId = new SelectList(db.Occupations, "Id", "Title", beneficiary.PrimaryOccupationId);
            ViewBag.SecondaryOccupationId = new SelectList(db.Occupations, "Id", "Title", beneficiary.SecondaryOccupationId);
            // Checkbox sets
            ViewBag.DisabilitiesCBData = CheckBoxHelper.GetDisabilities(db, beneficiary.Disabilities, DisabilitiesPrefix);

            return View(beneficiary);
        }

        // POST: /Beneficiary/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,GenderId,TownId,Address,Phone,Disability,EducationLevelId,BirthDay,BirthMonth,BirthYear,UniqueId,PrimaryOccupationId,SecondaryOccupationId,Block,PrimaryWorkDaysM,SecondaryWorkDaysM,PrimaryDailyWage,SecondaryDailyWage,StartDateDay,StartDateMonth,StartDateYear,ReligionId,CasteId,LanguageId,PCompanyId")] Beneficiary beneficiary)
        {
            if (beneficiary.Address == null)
                beneficiary.Address = string.Empty;

            if (ModelState.IsValid)
            {
                db.Entry(beneficiary).State = EntityState.Modified;
                // Get the checkbox values
                if (beneficiary.Disability)
                    beneficiary.Disabilities = CheckBoxHelper.ExtractValues(Request.Form, CheckBoxHelper.GetDisabilityIds(db), DisabilitiesPrefix);
                else
                    beneficiary.Disabilities = String.Empty;
                //
                beneficiary.OrigEntryDate = FormatHelper.ExtractDate(Request.Form, "OrigEntryDate");
                try
                {
                    db.SaveChanges();
                }
                catch(Exception ex)
                {
                    return RedirectToAction("AppError", "Home", new { msg = "Beneficiary::Edit: Exception: " + ex.Message });
                }
                HistoryHelper.RecordHistory(beneficiary);
                return RedirectToAction("Index");
            }
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Title", beneficiary.GenderId);
            //ViewBag.ReligionId = new SelectList(db.Religions, "Id", "Title", beneficiary.ReligionId);
            //ViewBag.CasteId = new SelectList(db.Castes, "Id", "Title", beneficiary.CasteId);
            ViewBag.TownId = new SelectList(db.Towns, "Id", "Name", beneficiary.TownId);
            //ViewBag.DisabilityId = new SelectList(db.Disabilities, "Id", "Title", Beneficiary.DisabilityId);
            ViewBag.EducationLevelId = new SelectList(db.EducationLevels, "Id", "Title", beneficiary.EducationLevelId);
            //ViewBag.LanguageId = new SelectList(db.Languages, "Id", "Title", beneficiary.LanguageId);
            ViewBag.PrimaryOccupationId = new SelectList(db.Occupations, "Id", "Title", beneficiary.PrimaryOccupationId);
            ViewBag.SecondaryOccupationId = new SelectList(db.Occupations, "Id", "Title", beneficiary.SecondaryOccupationId);
            // Checkbox sets
            ViewBag.DisabilitiesCBData = CheckBoxHelper.GetDisabilities(db, beneficiary.Disabilities, DisabilitiesPrefix);

            return View(beneficiary);
        }

        /* GET: /Beneficiary/Delete/5
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beneficiary Beneficiary = db.Beneficiaries.Find(id);
            if (Beneficiary == null)
            {
                return HttpNotFound();
            }
            return View(Beneficiary);
        }

        // POST: /Beneficiary/Delete/5
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Beneficiary Beneficiary = db.Beneficiaries.Find(id);
            db.Beneficiaries.Remove(Beneficiary);
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
