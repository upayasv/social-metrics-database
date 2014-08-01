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
    public class HouseholdAssetController : Controller
    {
        private DataModelContainer db = new DataModelContainer();

        // GET: /HouseholdAsset/
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        public ActionResult Index(Guid? bid)
        {
            if (bid == null)
            {
                //return RedirectToAction("Index", "Home");
                return RedirectToAction("AppError", "Home", new { msg = "HouseholdAsset::Index: bid == null" });
            }

            ViewBag.Beneficiary = db.Beneficiaries.Find(bid);

            var householdassets = db.HouseholdAssets.Where(a => a.BeneficiaryId == bid).Include(h => h.AssetType).Include(h => h.Beneficiary);
            return View(householdassets.ToList());
        }

        // GET: /HouseholdAsset/Details/5
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return RedirectToAction("AppError", "Home", new { msg = "Asset::Details: id == null" });
            }
            HouseholdAsset hhAsset = db.HouseholdAssets.Find(id);
            if (hhAsset == null)
            {
                //return HttpNotFound();
                return RedirectToAction("AppError", "Home", new { msg = "Asset::Details: invalid id" });
            }
            ViewBag.Beneficiary = db.Beneficiaries.Find(hhAsset.BeneficiaryId);
            return View(hhAsset);
        }

        // GET: /HouseholdAsset/Create
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        public ActionResult Create(Guid? bid)
        {
            if (bid == null)
            {
                //return RedirectToAction("Index", "Home");
                return RedirectToAction("AppError", "Home", new { msg = "HouseholdAsset::Create: bid == null" });
            }

            ViewBag.Beneficiary = db.Beneficiaries.Find(bid);
            ViewBag.AssetTypeId = new SelectList(db.AssetTypes, "Id", "Title");
            //ViewBag.BeneficiaryId = new SelectList(db.Beneficiaries, "Id", "Name");
            return View();
        }

        // POST: /HouseholdAsset/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,AssetTypeId,BeneficiaryId,Count")] HouseholdAsset householdasset)
        {
            if (ModelState.IsValid)
            {
                householdasset.TotalValue = 0; // ??
                householdasset.OrigEntryDate = FormatHelper.ExtractDate(Request.Form, "OrigEntryDate");
                db.HouseholdAssets.Add(householdasset);
                db.SaveChanges();
                HistoryHelper.StartHistory(householdasset);
                return RedirectToAction("Index", new { bid = householdasset.BeneficiaryId });
            }

            ViewBag.Beneficiary = db.Beneficiaries.Find(householdasset.BeneficiaryId);
            ViewBag.AssetTypeId = new SelectList(db.AssetTypes, "Id", "Title", householdasset.AssetTypeId);
            //ViewBag.BeneficiaryId = new SelectList(db.Beneficiaries, "Id", "Name", householdasset.BeneficiaryId);
            return View(householdasset);
        }

        // GET: /HouseholdAsset/Edit/5
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return RedirectToAction("AppError", "Home", new { msg = "Asset::Edit: id == null" });
            }
            HouseholdAsset householdasset = db.HouseholdAssets.Find(id);
            if (householdasset == null)
            {
                //return HttpNotFound();
                return RedirectToAction("AppError", "Home", new { msg = "Asset::Edit: invalid id" });
            }

            ViewBag.Beneficiary = db.Beneficiaries.Find(householdasset.BeneficiaryId);
            ViewBag.AssetTypeId = new SelectList(db.AssetTypes, "Id", "Title", householdasset.AssetTypeId);
            //ViewBag.BeneficiaryId = new SelectList(db.Beneficiaries, "Id", "Name", householdasset.BeneficiaryId);

            return View(householdasset);
        }

        // POST: /HouseholdAsset/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,AssetTypeId,BeneficiaryId,Count")] HouseholdAsset householdasset)
        {
            if (ModelState.IsValid)
            {
                householdasset.OrigEntryDate = FormatHelper.ExtractDate(Request.Form, "OrigEntryDate");
                db.Entry(householdasset).State = EntityState.Modified;
                db.SaveChanges();
                HistoryHelper.RecordHistory(householdasset);
                return RedirectToAction("Index", new { bid = householdasset.BeneficiaryId });
            }

            ViewBag.Beneficiary = db.Beneficiaries.Find(householdasset.BeneficiaryId);
            ViewBag.AssetTypeId = new SelectList(db.AssetTypes, "Id", "Title", householdasset.AssetTypeId);
            //ViewBag.BeneficiaryId = new SelectList(db.Beneficiaries, "Id", "Name", householdasset.BeneficiaryId);

            return View(householdasset);
        }

        /* GET: /HouseholdAsset/Delete/5
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HouseholdAsset householdasset = db.HouseholdAssets.Find(id);
            if (householdasset == null)
            {
                return HttpNotFound();
            }
            return View(householdasset);
        }

        // POST: /HouseholdAsset/Delete/5
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            HouseholdAsset householdasset = db.HouseholdAssets.Find(id);
            db.HouseholdAssets.Remove(householdasset);
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
