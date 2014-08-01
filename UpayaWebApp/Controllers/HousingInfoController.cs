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
    public class HousingInfoController : Controller
    {
        private DataModelContainer db = new DataModelContainer();
        private static string WaterSourcesPrefix = "WSrcs";
        private static string SavingsTypesPrefix = "Svngs";
        private static string LoanPurposeTypesPrefix = "LoanPrp";

        /* GET: /HousingInfo/
        public ActionResult Index()
        {
            var housinginfoes = db.HousingInfoes.Include(h => h.NumberOfRoomsType).Include(h => h.RoofMaterialType).Include(h => h.WaterSourceType).Include(h => h.WallMaterialType).Include(h => h.CookingEnergyType);
            return View(housinginfoes.ToList());
        }
        */

        // GET: /HousingInfo/Details/5
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return RedirectToAction("AppError", "Home", new { msg = "HousingInfo::Details: id == null" });
            }
            HousingInfo housinginfo = db.HousingInfoes.Find(id);
            if (housinginfo == null)
            {
                return RedirectToAction("Create", new { id = id.Value });
            }
            ViewBag.WaterSourceTypesValue = CheckBoxHelper.KeysToValues(housinginfo.WaterSourceTypes, CheckBoxHelper.WaterSources2ComboData(db));
            ViewBag.SavingsTypesValue = CheckBoxHelper.KeysToValues(housinginfo.SavingsTypes, CheckBoxHelper.SavingsTypes2ComboData(db));
            ViewBag.LoanPurposeTypesValue = CheckBoxHelper.KeysToValues(housinginfo.LoanPurposeTypes, CheckBoxHelper.LoanPurposeType2ComboData(db));

            return View(housinginfo);
        }

        // GET: /HousingInfo/Create
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        public ActionResult Create(Guid? id)
        {
            if (id == null)
            {
                return RedirectToAction("AppError", "Home", new { msg = "HousingInfo::Create: id == null" });
            }

            ViewBag.NumberOfRoomsTypeId = new SelectList(db.NumberOfRoomsTypes, "Id", "Title");
            ViewBag.RoofMaterialTypeId = new SelectList(db.RoofMaterialTypes, "Id", "Title");
            ViewBag.WallMaterialTypeId = new SelectList(db.WallMaterialTypes, "Id", "Title");
            ViewBag.CookingEnergyTypeId = new SelectList(db.CookingEnergyTypes, "Id", "Title");
            ViewBag.ToiletTypeId = new SelectList(db.ToiletTypes, "Id", "Title");
            //ViewBag.HouseQualityTypeId = new SelectList(db.HouseQualityTypes, "Id", "Title");
            ViewBag.ReligionId = new SelectList(db.Religions, "Id", "Title");
            ViewBag.CasteId = new SelectList(db.Castes, "Id", "Title");
            ViewBag.WaterSourceDistanceTypeId = new SelectList(db.WaterSourceDistanceTypes, "Id", "Title");
            ViewBag.HouseOwnershipTypeId = new SelectList(db.HouseOwnershipTypes, "Id", "Title");
            //
            ViewBag.Beneficiary = db.Beneficiaries.Find(id.Value);
            ViewBag.WaterSourcesCBData = CheckBoxHelper.GetCBData(CheckBoxHelper.WaterSources2ComboData(db), "", WaterSourcesPrefix);
            ViewBag.SavingsTypesCBData = CheckBoxHelper.GetCBData(CheckBoxHelper.SavingsTypes2ComboData(db), "", SavingsTypesPrefix);
            ViewBag.LoanPurposeTypesCBData = CheckBoxHelper.GetCBData(CheckBoxHelper.LoanPurposeType2ComboData(db), "", LoanPurposeTypesPrefix);

            return View();
        }

        // POST: /HousingInfo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        public ActionResult Create([Bind(Include = "Id,HasElectricity,NumberOfRoomsTypeId,RoofMaterialTypeId,WallMaterialTypeId,CookingEnergyTypeId,ToiletTypeId,WaterSourceTypes,ReligionId,CasteId,WaterSourceDistanceTypeId,HouseOwnershipTypeId")] HousingInfo housinginfo)
        {
            if (ModelState.IsValid)
            {
                housinginfo.Beneficiary = db.Beneficiaries.Find(housinginfo.Id); // ???
                housinginfo.HouseQualityTypeId = 0;
                // Get the checkbox values
                housinginfo.WaterSourceTypes = CheckBoxHelper.ExtractValues(Request.Form, CheckBoxHelper.GetIds(CheckBoxHelper.WaterSources2ComboData(db)), WaterSourcesPrefix);
                housinginfo.SavingsTypes = CheckBoxHelper.ExtractValues(Request.Form, CheckBoxHelper.GetIds(CheckBoxHelper.SavingsTypes2ComboData(db)), SavingsTypesPrefix);
                housinginfo.LoanPurposeTypes = CheckBoxHelper.ExtractValues(Request.Form, CheckBoxHelper.GetIds(CheckBoxHelper.LoanPurposeType2ComboData(db)), LoanPurposeTypesPrefix);
                //
                housinginfo.OrigEntryDate = FormatHelper.ExtractDate(Request.Form, "OrigEntryDate");

                db.HousingInfoes.Add(housinginfo);
                db.SaveChanges();
                HistoryHelper.StartHistory(housinginfo);
                return RedirectToAction("Details", new { id = housinginfo.Id });
            }

            ViewBag.Beneficiary = db.Beneficiaries.Find(housinginfo.Id);
            ViewBag.NumberOfRoomsTypeId = new SelectList(db.NumberOfRoomsTypes, "Id", "Title", housinginfo.NumberOfRoomsTypeId);
            ViewBag.RoofMaterialTypeId = new SelectList(db.RoofMaterialTypes, "Id", "Title", housinginfo.RoofMaterialTypeId);
            ViewBag.WallMaterialTypeId = new SelectList(db.WallMaterialTypes, "Id", "Title", housinginfo.WallMaterialTypeId);
            ViewBag.CookingEnergyTypeId = new SelectList(db.CookingEnergyTypes, "Id", "Title", housinginfo.CookingEnergyTypeId);
            ViewBag.ToiletTypeId = new SelectList(db.ToiletTypes, "Id", "Title", housinginfo.ToiletTypeId);
            //ViewBag.HouseQualityTypeId = new SelectList(db.HouseQualityTypes, "Id", "Title", housinginfo.HouseQualityTypeId);
            ViewBag.ReligionId = new SelectList(db.Religions, "Id", "Title", housinginfo.ReligionId);
            ViewBag.CasteId = new SelectList(db.Castes, "Id", "Title", housinginfo.CasteId);
            ViewBag.WaterSourceDistanceTypeId = new SelectList(db.WaterSourceDistanceTypes, "Id", "Title", housinginfo.WaterSourceDistanceTypeId);
            ViewBag.HouseOwnershipTypeId = new SelectList(db.HouseOwnershipTypes, "Id", "Title", housinginfo.HouseOwnershipTypeId);
            //
            ViewBag.WaterSourcesCBData = CheckBoxHelper.GetCBData(CheckBoxHelper.WaterSources2ComboData(db), housinginfo.WaterSourceTypes, WaterSourcesPrefix);
            ViewBag.SavingsTypesCBData = CheckBoxHelper.GetCBData(CheckBoxHelper.SavingsTypes2ComboData(db), housinginfo.SavingsTypes, SavingsTypesPrefix);
            ViewBag.LoanPurposeTypesCBData = CheckBoxHelper.GetCBData(CheckBoxHelper.LoanPurposeType2ComboData(db), housinginfo.LoanPurposeTypes, LoanPurposeTypesPrefix);
            return View(housinginfo);
        }

        // GET: /HousingInfo/Edit/5
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return RedirectToAction("AppError", "Home", new { msg = "HousingInfo::Edit: id == null" });
            }
            HousingInfo housinginfo = db.HousingInfoes.Find(id);
            if (housinginfo == null)
            {
                //return HttpNotFound();
                return RedirectToAction("AppError", "Home", new { msg = "HousingInfo::Edit: invalid id" });
            }
            ViewBag.NumberOfRoomsTypeId = new SelectList(db.NumberOfRoomsTypes, "Id", "Title", housinginfo.NumberOfRoomsTypeId);
            ViewBag.RoofMaterialTypeId = new SelectList(db.RoofMaterialTypes, "Id", "Title", housinginfo.RoofMaterialTypeId);
            ViewBag.WallMaterialTypeId = new SelectList(db.WallMaterialTypes, "Id", "Title", housinginfo.WallMaterialTypeId);
            ViewBag.CookingEnergyTypeId = new SelectList(db.CookingEnergyTypes, "Id", "Title", housinginfo.CookingEnergyTypeId);
            ViewBag.ToiletTypeId = new SelectList(db.ToiletTypes, "Id", "Title", housinginfo.ToiletTypeId);
            //ViewBag.HouseQualityTypeId = new SelectList(db.HouseQualityTypes, "Id", "Title", housinginfo.HouseQualityTypeId);
            ViewBag.ReligionId = new SelectList(db.Religions, "Id", "Title", housinginfo.ReligionId);
            ViewBag.CasteId = new SelectList(db.Castes, "Id", "Title", housinginfo.CasteId);
            ViewBag.WaterSourceDistanceTypeId = new SelectList(db.WaterSourceDistanceTypes, "Id", "Title", housinginfo.WaterSourceDistanceTypeId);
            ViewBag.HouseOwnershipTypeId = new SelectList(db.HouseOwnershipTypes, "Id", "Title", housinginfo.HouseOwnershipTypeId);
            //
            ViewBag.WaterSourcesCBData = CheckBoxHelper.GetCBData(CheckBoxHelper.WaterSources2ComboData(db), housinginfo.WaterSourceTypes, WaterSourcesPrefix);
            ViewBag.SavingsTypesCBData = CheckBoxHelper.GetCBData(CheckBoxHelper.SavingsTypes2ComboData(db), housinginfo.SavingsTypes, SavingsTypesPrefix);
            ViewBag.LoanPurposeTypesCBData = CheckBoxHelper.GetCBData(CheckBoxHelper.LoanPurposeType2ComboData(db), housinginfo.LoanPurposeTypes, LoanPurposeTypesPrefix);
            return View(housinginfo);
        }

        // POST: /HousingInfo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        public ActionResult Edit([Bind(Include = "Id,HasElectricity,NumberOfRoomsTypeId,RoofMaterialTypeId,WallMaterialTypeId,CookingEnergyTypeId,ToiletTypeId,WaterSourceTypes,ReligionId,CasteId,WaterSourceDistanceTypeId,HouseOwnershipTypeId")] HousingInfo housinginfo)
        {
            if (ModelState.IsValid)
            {
                // Get the checkbox values
                housinginfo.WaterSourceTypes = CheckBoxHelper.ExtractValues(Request.Form, CheckBoxHelper.GetIds(CheckBoxHelper.WaterSources2ComboData(db)), WaterSourcesPrefix);
                housinginfo.SavingsTypes = CheckBoxHelper.ExtractValues(Request.Form, CheckBoxHelper.GetIds(CheckBoxHelper.SavingsTypes2ComboData(db)), SavingsTypesPrefix);
                housinginfo.LoanPurposeTypes = CheckBoxHelper.ExtractValues(Request.Form, CheckBoxHelper.GetIds(CheckBoxHelper.LoanPurposeType2ComboData(db)), LoanPurposeTypesPrefix);
                //
                housinginfo.OrigEntryDate = FormatHelper.ExtractDate(Request.Form, "OrigEntryDate");
                db.Entry(housinginfo).State = EntityState.Modified;
                db.SaveChanges();
                HistoryHelper.RecordHistory(housinginfo);
                return RedirectToAction("Details", new { id = housinginfo.Id });
            }
            ViewBag.NumberOfRoomsTypeId = new SelectList(db.NumberOfRoomsTypes, "Id", "Title", housinginfo.NumberOfRoomsTypeId);
            ViewBag.RoofMaterialTypeId = new SelectList(db.RoofMaterialTypes, "Id", "Title", housinginfo.RoofMaterialTypeId);
            ViewBag.WallMaterialTypeId = new SelectList(db.WallMaterialTypes, "Id", "Title", housinginfo.WallMaterialTypeId);
            ViewBag.CookingEnergyTypeId = new SelectList(db.CookingEnergyTypes, "Id", "Title", housinginfo.CookingEnergyTypeId);
            ViewBag.HouseQualityTypeId = new SelectList(db.HouseQualityTypes, "Id", "Title", housinginfo.HouseQualityTypeId);
            ViewBag.ReligionId = new SelectList(db.Religions, "Id", "Title", housinginfo.ReligionId);
            ViewBag.CasteId = new SelectList(db.Castes, "Id", "Title", housinginfo.CasteId);
            ViewBag.WaterSourceDistanceTypeId = new SelectList(db.WaterSourceDistanceTypes, "Id", "Title", housinginfo.WaterSourceDistanceTypeId);
            ViewBag.HouseOwnershipTypeId = new SelectList(db.HouseOwnershipTypes, "Id", "Title", housinginfo.HouseOwnershipTypeId);
            //
            ViewBag.WaterSourcesCBData = CheckBoxHelper.GetCBData(CheckBoxHelper.WaterSources2ComboData(db), housinginfo.WaterSourceTypes, WaterSourcesPrefix);
            ViewBag.SavingsTypesCBData = CheckBoxHelper.GetCBData(CheckBoxHelper.SavingsTypes2ComboData(db), housinginfo.SavingsTypes, SavingsTypesPrefix);
            ViewBag.LoanPurposeTypesCBData = CheckBoxHelper.GetCBData(CheckBoxHelper.LoanPurposeType2ComboData(db), housinginfo.LoanPurposeTypes, LoanPurposeTypesPrefix);
            return View(housinginfo);
        }

        /* GET: /HousingInfo/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HousingInfo housinginfo = db.HousingInfoes.Find(id);
            if (housinginfo == null)
            {
                return HttpNotFound();
            }
            return View(housinginfo);
        }

        // POST: /HousingInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            HousingInfo housinginfo = db.HousingInfoes.Find(id);
            db.HousingInfoes.Remove(housinginfo);
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
