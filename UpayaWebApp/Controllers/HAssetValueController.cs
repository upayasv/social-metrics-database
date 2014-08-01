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
    public class HAssetValueController : Controller
    {
        private DataModelContainer db = new DataModelContainer();

        // GET: /HAssetValue/
        [Authorize(Roles = "PartnerAdmin")]
        public ActionResult Index()
        {
            Guid curUserId = AccountHelper.GetCurUserId();
            Guid companyId = db.PartnerAdmins.Find(curUserId).PartnerCompanyId;

            // Create the missing records
            //IEnumerable<HAssetValue> valueRecs = db.HAssetValues.Where(h => h.PartnerCompanyId == companyId);
            IEnumerable<AssetType> assetRecs = db.AssetTypes.ToList();

            foreach(AssetType at in assetRecs)
            {
                HAssetValue rec = db.HAssetValues.SingleOrDefault(av => av.PartnerCompanyId == companyId && av.AssetTypeId == at.Id);
                if(rec == null)
                {
                    rec = new HAssetValue();
                    rec.PartnerCompanyId = companyId;
                    rec.AssetTypeId = at.Id;
                    db.HAssetValues.Add(rec);
                    db.SaveChanges();
                }
            }

            //
            var hassetvalues = db.HAssetValues.Where(h => h.PartnerCompanyId == companyId).Include(h => h.AssetType);
            return View(hassetvalues.ToList());
        }

        // GET: /HAssetValue/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HAssetValue hassetvalue = db.HAssetValues.Find(id);
            if (hassetvalue == null)
            {
                return HttpNotFound();
            }
            return View(hassetvalue);
        }

        /* GET: /HAssetValue/Create
        public ActionResult Create()
        {
            ViewBag.AssetTypeId = new SelectList(db.AssetTypes, "Id", "Title");
            return View();
        }

        // POST: /HAssetValue/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,PartnerCompanyId,AssetTypeId,Value")] HAssetValue hassetvalue)
        {
            if (ModelState.IsValid)
            {
                db.HAssetValues.Add(hassetvalue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AssetTypeId = new SelectList(db.AssetTypes, "Id", "Title", hassetvalue.AssetTypeId);
            return View(hassetvalue);
        }
        */

        // GET: /HAssetValue/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return RedirectToAction("AppError", "Home", new { msg = "HAssetValue::Edit: id == null" });
            }
            HAssetValue hassetvalue = db.HAssetValues.Find(id);
            if (hassetvalue == null)
            {
                //return HttpNotFound();
                return RedirectToAction("AppError", "Home", new { msg = "HAssetValue::Edit: invalid id" });
            }

            ViewBag.PartnerCompanyId = hassetvalue.PartnerCompanyId;
            ViewBag.AssetTypeId = hassetvalue.AssetTypeId;
            //ViewBag.AssetTypeId = new SelectList(db.AssetTypes, "Id", "Title", hassetvalue.AssetTypeId);
            return View(hassetvalue);
        }

        // POST: /HAssetValue/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,PartnerCompanyId,AssetTypeId,Value")] HAssetValue hassetvalue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hassetvalue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PartnerCompanyId = hassetvalue.PartnerCompanyId;
            ViewBag.AssetTypeId = hassetvalue.AssetTypeId;
            //ViewBag.AssetTypeId = new SelectList(db.AssetTypes, "Id", "Title", hassetvalue.AssetTypeId);

            return View(hassetvalue);
        }

        /* GET: /HAssetValue/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HAssetValue hassetvalue = db.HAssetValues.Find(id);
            if (hassetvalue == null)
            {
                return HttpNotFound();
            }
            return View(hassetvalue);
        }

        // POST: /HAssetValue/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HAssetValue hassetvalue = db.HAssetValues.Find(id);
            db.HAssetValues.Remov(hassetvalue);
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
