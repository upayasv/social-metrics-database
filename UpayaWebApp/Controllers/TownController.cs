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
    public class TownController : Controller
    {
        private DataModelContainer db = new DataModelContainer();

        // GET: /Town/
        public ActionResult Index()
        {
            var towns = db.Towns.Include(t => t.Country);
            return View(towns.ToList());
        }

        // GET: /Town/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Town town = db.Towns.Find(id);
            if (town == null)
            {
                return HttpNotFound();
            }
            return View(town);
        }

        // GET: /Town/Create
        [Authorize(Roles = "PartnerAdmin, UpayaAdmin")]
        public ActionResult Create()
        {
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name");

            // States
            ViewBag.StateId = new SelectList(db.States.Where(x => x.CountryId == "in"), "Id", "Name"); // !!! 

            // Districts
            ViewBag.DistrictId = new SelectList(db.Districts.Where(x => x.StateId == 0), "Id", "Name"); // Empty
            return View();
        }

        // POST: /Town/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "PartnerAdmin, UpayaAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CountryId,Name,StateId,DistrictId,PostalCode")] Town town)
        {
            if (ModelState.IsValid)
            {
                db.Towns.Add(town);
                db.SaveChanges();
                HistoryHelper.StartHistory(town);
                return RedirectToAction("Index");
            }

            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name");

            // States
            ViewBag.StateId = new SelectList(db.States.Where(x => x.CountryId == town.CountryId), "Id", "Name");
            if (db.States.Any(x => x.Id == town.StateId))
                ViewBag.SID = db.States.Where(x => x.Id == town.StateId).First().Name;
            else
                ViewBag.SID = "--- Please select ---";

            // Districts
            ViewBag.DistrictId = new SelectList(db.Districts.Where(x => x.StateId == town.StateId), "Id", "Name");

            return View(town);
        }

        // GET: /Town/Edit/5
        [Authorize(Roles = "PartnerAdmin, UpayaAdmin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return RedirectToAction("AppError", "Town", new { msg = "Town::Edit: id == null" });
            }
            Town town = db.Towns.Find(id);
            if (town == null)
            {
                //return HttpNotFound();
                return RedirectToAction("AppError", "Town", new { msg = "Town::Edit: invalid id" });
            }
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", town.CountryId);
            ViewBag.StateId = new SelectList(db.States.Where(x => x.CountryId == town.CountryId).OrderBy(xx => xx.Name), "Id", "Name", town.StateId);
            ViewBag.DistrictId = new SelectList(db.Districts.Where(x => x.StateId == town.StateId).OrderBy(xx => xx.Name), "Id", "Name", town.DistrictId);
            return View(town);
        }

        // POST: /Town/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "PartnerAdmin, UpayaAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CountryId,StateId,DistrictId,PostalCode")] Town town)
        {
            if (ModelState.IsValid)
            {
                db.Entry(town).State = EntityState.Modified;
                db.SaveChanges();
                HistoryHelper.RecordHistory(town);
                return RedirectToAction("Index");
            }
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", town.CountryId);
            ViewBag.StateId = new SelectList(db.States.Where(x => x.CountryId == town.CountryId).OrderBy(xx => xx.Name), "Id", "Name", town.StateId);
            ViewBag.DistrictId = new SelectList(db.Districts.Where(x => x.StateId == town.StateId).OrderBy(xx => xx.Name), "Id", "Name", town.DistrictId);
            return View(town);
        }

        /* GET: /Town/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Town town = db.Towns.Find(id);
            if (town == null)
            {
                return HttpNotFound();
            }
            return View(town);
        }

        // POST: /Town/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Town town = db.Towns.Find(id);
            db.Towns.Remove(town);
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

        //----- JSON helper functions -----
        public JsonResult GetDistricts(short id)
        {
            List<SelectListItem> li = new List<SelectListItem>();
            foreach (District d in db.Districts.Where(x => x.StateId == id))
            {
                li.Add(new SelectListItem { Text = d.Name, Value = d.Id.ToString() });   
            }
            return Json(li);
        }
    }
}
