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
    public class MajorExpensesController : Controller
    {
        private DataModelContainer db = new DataModelContainer();

        /* GET: /MajorExpenses/
        public ActionResult Index()
        {
            return View(db.MajorExpenses.ToList());
        }
        */

        // GET: /MajorExpenses/Details/5
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return RedirectToAction("AppError", "Home", new { msg = "MajorExpenses::Details: id == null" });
            }
            MajorExpensesInfo majorexpensesinfo = db.MajorExpenses.Find(id);
            if (majorexpensesinfo == null)
            {
                // Create a new record
                return RedirectToAction("Create", new { id = id.Value });
                /*
                MajorExpensesInfo ma = new MajorExpensesInfo();
                ma.Id = id.Value;
                ma.Beneficiary = db.Beneficiaries.Find(ma.Id); // ???
                db.MajorExpenses.Add(ma);
                db.SaveChanges();
                */ 
            }
            majorexpensesinfo = db.MajorExpenses.Find(id);
            return View(majorexpensesinfo);
        }

        // GET: /MajorExpenses/Create
        public ActionResult Create(Guid? id)
        {
            if (id == null)
            {
                return RedirectToAction("AppError", "Home", new { msg = "MajorExpenses::Create: id == null" });
            }

            ViewBag.Beneficiary = db.Beneficiaries.Find(id);
            return View();
        }

        // POST: /MajorExpenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        public ActionResult Create([Bind(Include = "Id,FoodM,RentM,SchoolFeesM,WaterAndElecM,CableTvDishM,LoanRepaymentsM,AlcoholM,CinemaFestivFunctA,LoomRelA,OtherExpM,OtherExpDescr")] MajorExpensesInfo majorexpensesinfo)
        {
            if (ModelState.IsValid)
            {
                majorexpensesinfo.Beneficiary = db.Beneficiaries.Find(majorexpensesinfo.Id); //
                majorexpensesinfo.OrigEntryDate = FormatHelper.ExtractDate(Request.Form, "OrigEntryDate");
                db.MajorExpenses.Add(majorexpensesinfo);
                db.SaveChanges();
                HistoryHelper.StartHistory(majorexpensesinfo);
                return RedirectToAction("Details", new { id = majorexpensesinfo.Id });
            }

            ViewBag.Beneficiary = db.Beneficiaries.Find(majorexpensesinfo.Id);
            return View(majorexpensesinfo);
        }

        // GET: /MajorExpenses/Edit/5
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return RedirectToAction("AppError", "Home", new { msg = "MajorExpenses::Edit: id == null" });
            }
            MajorExpensesInfo majorexpensesinfo = db.MajorExpenses.Find(id);
            if (majorexpensesinfo == null)
            {
                //return HttpNotFound();
                return RedirectToAction("AppError", "Home", new { msg = "MajorExpenses::Edit: invalid id" });
            }
            ViewBag.Beneficiary = db.Beneficiaries.Find(id);
            return View(majorexpensesinfo);
        }

        // POST: /MajorExpenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        public ActionResult Edit([Bind(Include = "Id,FoodM,RentM,SchoolFeesM,WaterAndElecM,CableTvDishM,LoanRepaymentsM,AlcoholM,CinemaFestivFunctA,LoomRelA,OtherExpM,OtherExpDescr")] MajorExpensesInfo mei)
        {
            if (ModelState.IsValid)
            {
                mei.OrigEntryDate = FormatHelper.ExtractDate(Request.Form, "OrigEntryDate");
                db.Entry(mei).State = EntityState.Modified;
                db.SaveChanges();
                HistoryHelper.RecordHistory(mei);
                return RedirectToAction("Details", new { id = mei.Id });
            }
            ViewBag.Beneficiary = db.Beneficiaries.Find(mei.Id);
            return View(mei);
        }

        /* GET: /MajorExpenses/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MajorExpensesInfo majorexpensesinfo = db.MajorExpenses.Find(id);
            if (majorexpensesinfo == null)
            {
                return HttpNotFound();
            }
            return View(majorexpensesinfo);
        }

        // POST: /MajorExpenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            MajorExpensesInfo majorexpensesinfo = db.MajorExpenses.Find(id);
            db.MajorExpenses.Remove(majorexpensesinfo);
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
