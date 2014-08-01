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
    public class ChildController : Controller
    {
        private DataModelContainer db = new DataModelContainer();
        private static string DisabilitiesPrefix = "Disab";
        private static string WhyNotInSchoolPrefix = "WNIS";

        // GET: /Child/
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        public ActionResult Index(Guid? bid)
        {
            // Children are per Beneficiary
            if (bid == null)
            {
                //return RedirectToAction("Index", "Home");
                return RedirectToAction("AppError", "Home", new { msg = "Child::Index: bid == null" });
            }

            ViewBag.Beneficiary = db.Beneficiaries.Find(bid);

            var children = db.Children.Where(c => c.BeneficiaryId == bid).Include(c => c.Gender).Include(c => c.Beneficiary).Include(c => c.SchoolType);
            return View(children.ToList());
        }

        // GET: /Child/Details/5
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return RedirectToAction("AppError", "Home", new { msg = "Child::Details: id == null" });
            }
            Child child = db.Children.Find(id);
            if (child == null)
            {
                //return HttpNotFound();
                return RedirectToAction("AppError", "Home", new { msg = "Child::Details: invalid id" });
            }

            ViewBag.Beneficiary = db.Beneficiaries.Find(child.BeneficiaryId);
            ViewBag.DisabilitiesValue = CheckBoxHelper.DisabilitiesKeysToValues(child.Disabilities, db);
            ViewBag.WhyNotInSchoolValue = CheckBoxHelper.KeysToValues(child.WhyNotInSchool, CheckBoxHelper.WhyNotInSchoolTypes2ComboData(db));

            return View(child);
        }

        // GET: /Child/Create
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        public ActionResult Create(Guid? bid)
        {
            if (bid == null)
            {
                //return RedirectToAction("Index", "Home");
                return RedirectToAction("AppError", "Home", new { msg = "Child::Create: bid == null" });
            }

            ViewBag.Beneficiary = db.Beneficiaries.Find(bid);
            if (ViewBag.Beneficiary == null)
            {
                return RedirectToAction("AppError", "Home", new { msg = "Child::Create: invalid bid" });
            }

            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Title");
            ViewBag.DisabilityId = new SelectList(db.Disabilities, "Id", "Title");
            //ViewBag.BeneficiaryId = new SelectList(db.Beneficiaries, "Id", "Name");
            ViewBag.SchoolTypeId = new SelectList(db.SchoolTypes, "Id", "Title");
            //ViewBag.ClassLevelId = new SelectList(db.ClassLevels, "Id", "Title");
            //ViewBag.ChildRelationshipId = new SelectList(db.ChildRelationships, "Id", "Title");
            //ViewBag.SchoolDistanceId = new SelectList(db.SchoolDistances, "Id", "Title");
            ViewBag.SchoolAttendance = new SelectList(db.SchoolDaysPerWeek, "Id", "Title");

            // Checkbox sets
            ViewBag.DisabilitiesCBData = CheckBoxHelper.GetDisabilities(db, String.Empty, DisabilitiesPrefix);
            ViewBag.WhyNotInSchoolCBData = CheckBoxHelper.GetCBData(CheckBoxHelper.WhyNotInSchoolTypes2ComboData(db), String.Empty, WhyNotInSchoolPrefix);

            return View();
        }

        // POST: /Child/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,GenderId,Disability,BeneficiaryId,EnrolledInSchool,WhyNotInSchool,SchoolTypeId,ClassLevelId,SchoolDistanceId,MonthlyEduExpenses,SchoolAttendance,BirthDay,BirthMonth,BirthYear")] Child child)
        {
            if (ModelState.IsValid)
            {
                child.Id = Guid.NewGuid();
                child.ChildRelationshipId = db.ChildRelationships.OrderBy(x => x.Id).FirstOrDefault().Id; // First is "Unknown", 0 is not acceptable
                // Get the checkbox values
                if (child.Disability)
                    child.Disabilities = CheckBoxHelper.ExtractValues(Request.Form, CheckBoxHelper.GetDisabilityIds(db), DisabilitiesPrefix);
                if(!child.EnrolledInSchool)
                    child.WhyNotInSchool = CheckBoxHelper.ExtractValues(Request.Form, CheckBoxHelper.GetIds(CheckBoxHelper.WhyNotInSchoolTypes2ComboData(db)), WhyNotInSchoolPrefix);
                // 
                child.OrigEntryDate = FormatHelper.ExtractDate(Request.Form, "OrigEntryDate");

                db.Children.Add(child);
                db.SaveChanges();
                HistoryHelper.StartHistory(child);
                return RedirectToAction("Index", new { bid = child.BeneficiaryId });
            }

            ViewBag.Beneficiary = db.Beneficiaries.Find(child.BeneficiaryId);
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Title", child.GenderId);
            //ViewBag.DisabilityId = new SelectList(db.Disabilities, "Id", "Title", child.DisabilityId);
            //ViewBag.BeneficiaryId = new SelectList(db.Beneficiaries, "Id", "Name", child.BeneficiaryId);
            ViewBag.SchoolTypeId = new SelectList(db.SchoolTypes, "Id", "Title", child.SchoolTypeId);
            //ViewBag.ClassLevelId = new SelectList(db.ClassLevels, "Id", "Title", child.ClassLevelId);
            //ViewBag.ChildRelationshipId = new SelectList(db.ChildRelationships, "Id", "Title", child.ChildRelationshipId);
            //ViewBag.SchoolDistanceId = new SelectList(db.SchoolDistances, "Id", "Title", child.SchoolDistanceId);
            ViewBag.SchoolAttendance = new SelectList(db.SchoolDaysPerWeek, "Id", "Title");

            // Checkbox sets
            ViewBag.DisabilitiesCBData = CheckBoxHelper.GetDisabilities(db, child.Disabilities, DisabilitiesPrefix);
            ViewBag.WhyNotInSchoolCBData = CheckBoxHelper.GetCBData(CheckBoxHelper.WhyNotInSchoolTypes2ComboData(db), child.WhyNotInSchool, WhyNotInSchoolPrefix);

            return View(child);
        }

        // GET: /Child/Edit/5
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return RedirectToAction("AppError", "Home", new { msg = "Child::Edit: id == null" });
            }
            Child child = db.Children.Find(id);
            if (child == null)
            {
                //return HttpNotFound();
                return RedirectToAction("AppError", "Home", new { msg = "Child::Edit: invalid id" });
            }

            ViewBag.Beneficiary = db.Beneficiaries.Find(child.BeneficiaryId);
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Title", child.GenderId);
            //ViewBag.DisabilityId = new SelectList(db.Disabilities, "Id", "Title", child.DisabilityId);
            //ViewBag.BeneficiaryId = new SelectList(db.Beneficiaries, "Id", "Name", child.BeneficiaryId);
            ViewBag.SchoolTypeId = new SelectList(db.SchoolTypes, "Id", "Title", child.SchoolTypeId);
            //ViewBag.ClassLevelId = new SelectList(db.ClassLevels, "Id", "Title", child.ClassLevelId);
            //ViewBag.ChildRelationshipId = new SelectList(db.ChildRelationships, "Id", "Title", child.ChildRelationshipId);
            //ViewBag.SchoolDistanceId = new SelectList(db.SchoolDistances, "Id", "Title", child.SchoolDistanceId);
            ViewBag.SchoolAttendance = new SelectList(db.SchoolDaysPerWeek, "Id", "Title");

            // Checkbox sets
            ViewBag.DisabilitiesCBData = CheckBoxHelper.GetDisabilities(db, child.Disabilities, DisabilitiesPrefix);
            ViewBag.WhyNotInSchoolCBData = CheckBoxHelper.GetCBData(CheckBoxHelper.WhyNotInSchoolTypes2ComboData(db), child.WhyNotInSchool, WhyNotInSchoolPrefix);

            return View(child);
        }

        // POST: /Child/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,GenderId,Disability,BeneficiaryId,EnrolledInSchool,WhyNotInSchool,SchoolTypeId,ClassLevelId,SchoolDistanceId,MonthlyEduExpenses,SchoolAttendance,BirthDay,BirthMonth,BirthYear,ChildRelationshipId")] Child child)
        {
            if (ModelState.IsValid)
            {
                db.Entry(child).State = EntityState.Modified;
                // Get the checkbox values
                if (child.Disability)
                    child.Disabilities = CheckBoxHelper.ExtractValues(Request.Form, CheckBoxHelper.GetDisabilityIds(db), DisabilitiesPrefix);
                else
                    child.Disabilities = String.Empty;
                if (!child.EnrolledInSchool)
                    child.WhyNotInSchool = CheckBoxHelper.ExtractValues(Request.Form, CheckBoxHelper.GetIds(CheckBoxHelper.WhyNotInSchoolTypes2ComboData(db)), WhyNotInSchoolPrefix);
                else
                    child.WhyNotInSchool = String.Empty;
                // 
                child.OrigEntryDate = FormatHelper.ExtractDate(Request.Form, "OrigEntryDate");

                db.SaveChanges();
                HistoryHelper.RecordHistory(child);
                return RedirectToAction("Index", new { bid = child.BeneficiaryId });
            }

            ViewBag.Beneficiary = db.Beneficiaries.Find(child.BeneficiaryId);
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Title", child.GenderId);
            //ViewBag.DisabilityId = new SelectList(db.Disabilities, "Id", "Title", child.DisabilityId);
            //ViewBag.BeneficiaryId = new SelectList(db.Beneficiaries, "Id", "Name", child.BeneficiaryId);
            ViewBag.SchoolTypeId = new SelectList(db.SchoolTypes, "Id", "Title", child.SchoolTypeId);
            //ViewBag.ClassLevelId = new SelectList(db.ClassLevels, "Id", "Title", child.ClassLevelId);
            //ViewBag.ChildRelationshipId = new SelectList(db.ChildRelationships, "Id", "Title");
            //ViewBag.SchoolDistanceId = new SelectList(db.SchoolDistances, "Id", "Title");
            ViewBag.SchoolAttendance = new SelectList(db.SchoolDaysPerWeek, "Id", "Title");
            // Checkbox sets
            ViewBag.DisabilitiesCBData = CheckBoxHelper.GetDisabilities(db, child.Disabilities, DisabilitiesPrefix);
            ViewBag.WhyNotInSchoolCBData = CheckBoxHelper.GetCBData(CheckBoxHelper.WhyNotInSchoolTypes2ComboData(db), child.WhyNotInSchool, WhyNotInSchoolPrefix);

            return View(child);
        }

        /* GET: /Child/Delete/5
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Child child = db.Children.Find(id);
            if (child == null)
            {
                return HttpNotFound();
            }
            return View(child);
        }

        // POST: /Child/Delete/5
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Child child = db.Children.Find(id);
            db.Children.Remove(child);
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
