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
    public class MealsController : Controller
    {
        private DataModelContainer db = new DataModelContainer();
        //private static string MealTypesPrefix = "MTs";
        private static string MealSourcesPrefix = "MSs";
        private static string CEGrainsPrefix = "Grns";
        private static string CEPulsePrefix = "Plss";
        private static string CEVegetablesPrefix = "Vgts";
        

        /* GET: /Meals/
        public ActionResult Index()
        {
            var meals = db.Meals;
            return View(meals.ToList());
        }
        */

        // GET: /Meals/Details/5
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return RedirectToAction("AppError", "Home", new { msg = "Meals::Details: id == null"});
            }
            Meals meals = db.Meals.Find(id);
            if (meals == null)
            {
                return RedirectToAction("Create", new { id = id.Value });
                /*
                Meals mrec = new Meals();
                mrec.Id = id.Value;
                mrec.Beneficiary = db.Beneficiaries.Find(mrec.Id); // ???
                mrec.MealTypes = String.Empty;
                mrec.FoodSourceTypes = String.Empty;
                db.Meals.Add(mrec);
                db.SaveChanges();
                */ 
            }
            //meals = db.Meals.Find(id);
            ViewBag.Beneficiary = db.Beneficiaries.Find(id);
            //ViewBag.MealTypesValue = CheckBoxHelper.MealTypesKeysToValues(meals.MealTypes, db);
            ViewBag.FoodSourceTypesValue = CheckBoxHelper.FoodSourceTypesKeysToValues(meals.FoodSourceTypes, db);
            ViewBag.CEGrainsValue = CheckBoxHelper.KeysToValues(meals.CEGrains, CheckBoxHelper.GrainTypes2ComboData(db));
            ViewBag.CEPulseValue = CheckBoxHelper.KeysToValues(meals.CEPulse, CheckBoxHelper.PulseTypes2ComboData(db));
            ViewBag.CEVegetablesValue = CheckBoxHelper.KeysToValues(meals.CEVegetables, CheckBoxHelper.VegetableTypes2ComboData(db));

            return View(meals);
        }

        // GET: /Meals/Create
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        public ActionResult Create(Guid? id)
        {
            if (id == null)
            {
                return RedirectToAction("AppError", "Home", new { msg = "Meals::Create: id == null" });
            }

            // Checkbox sets
            ViewBag.Beneficiary = db.Beneficiaries.Find(id);
            //ViewBag.MealTypesCBData = CheckBoxHelper.GetMealTypes(db, "", MealTypesPrefix);
            ViewBag.FoodSourceTypesCBData = CheckBoxHelper.GetMealSources(db, "", MealSourcesPrefix);
            ViewBag.CEGrainsCBData = CheckBoxHelper.GetCBData(CheckBoxHelper.GrainTypes2ComboData(db), "", CEGrainsPrefix);
            ViewBag.CEPulseCBData = CheckBoxHelper.GetCBData(CheckBoxHelper.PulseTypes2ComboData(db), "", CEPulsePrefix);
            ViewBag.CEVegetablesCBData = CheckBoxHelper.GetCBData(CheckBoxHelper.VegetableTypes2ComboData(db), "", CEVegetablesPrefix);
            //ViewBag.MealTypeId = new SelectList(db.MealTypes, "Id", "Title");
            //ViewBag.FoodSourceTypesCBData = new SelectList(db.FoodSourceTypes, "Id", "Title");
            ViewBag.AvgNonVegPerMTypeId = new SelectList(db.AvgNonVegPerMTypes, "Id", "Title");
            ViewBag.PKGPerMTypeId = new SelectList(db.PKGPerMTypes, "Id", "Title");
            return View();
        }

        // POST: /Meals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        public ActionResult Create([Bind(Include = "Id,AvgMealsPerDay,AnyNonVegetarian,AvgNonVegPerMonth,FoodSourceTypes,MilkAsideAmount,CEGrains,CEPulse,CEVegetables,AvgNonVegPerMTypeId,PKGPerMTypeId")] Meals meals)
        {
            if (ModelState.IsValid)
            {
                meals.Beneficiary = db.Beneficiaries.Find(meals.Id); // ???
                // Get the checkbox values
                //meals.MealTypes = CheckBoxHelper.ExtractValues(Request.Form, CheckBoxHelper.GetMealTypeIds(db), MealTypesPrefix);
                meals.FoodSourceTypes = CheckBoxHelper.ExtractValues(Request.Form, CheckBoxHelper.GetFoodSourceIds(db), MealSourcesPrefix);
                meals.CEGrains = CheckBoxHelper.ExtractValues(Request.Form, CheckBoxHelper.GetIds(CheckBoxHelper.GrainTypes2ComboData(db)), CEGrainsPrefix);
                meals.CEPulse = CheckBoxHelper.ExtractValues(Request.Form, CheckBoxHelper.GetIds(CheckBoxHelper.PulseTypes2ComboData(db)), CEPulsePrefix);
                meals.CEVegetables = CheckBoxHelper.ExtractValues(Request.Form, CheckBoxHelper.GetIds(CheckBoxHelper.VegetableTypes2ComboData(db)), CEVegetablesPrefix);
                // 
                meals.OrigEntryDate = FormatHelper.ExtractDate(Request.Form, "OrigEntryDate");

                db.Meals.Add(meals);
                db.SaveChanges();
                HistoryHelper.StartHistory(meals);
                return RedirectToAction("Details", new { id = meals.Id });
            }

            ViewBag.Beneficiary = db.Beneficiaries.Find(meals.Id); // ???
            //ViewBag.MealTypeId = new SelectList(db.MealTypes, "Id", "Title", meals.MealTypeId);
            //ViewBag.FoodSourceTypesId = new SelectList(db.FoodSourceTypes, "Id", "Title", meals.FoodSourceTypesId);
            // Checkbox sets
            //ViewBag.MealTypesCBData = CheckBoxHelper.GetMealTypes(db, meals.MealTypes, MealTypesPrefix);
            ViewBag.FoodSourceTypesCBData = CheckBoxHelper.GetMealSources(db, meals.FoodSourceTypes, MealSourcesPrefix);
            ViewBag.CEGrainsCBData = CheckBoxHelper.GetCBData(CheckBoxHelper.GrainTypes2ComboData(db), meals.CEGrains, CEGrainsPrefix);
            ViewBag.CEPulseCBData = CheckBoxHelper.GetCBData(CheckBoxHelper.PulseTypes2ComboData(db), meals.CEPulse, CEPulsePrefix);
            ViewBag.CEVegetablesCBData = CheckBoxHelper.GetCBData(CheckBoxHelper.VegetableTypes2ComboData(db), meals.CEVegetables, CEVegetablesPrefix);
            ViewBag.AvgNonVegPerMTypeId = new SelectList(db.AvgNonVegPerMTypes, "Id", "Title");
            ViewBag.PKGPerMTypeId = new SelectList(db.PKGPerMTypes, "Id", "Title");

            return View(meals);
        }

        // GET: /Meals/Edit/5
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return RedirectToAction("AppError", "Home", new { msg = "Meals::Edit: id == null" });
            }
            Meals meals = db.Meals.Find(id);
            if (meals == null)
            {
                //return HttpNotFound();
                return RedirectToAction("AppError", "Home", new { msg = "Meals::Edit: invalid id" });
            }

            ViewBag.Beneficiary = db.Beneficiaries.Find(id);
            //ViewBag.MealTypeId = new SelectList(db.MealTypes, "Id", "Title", meals.MealTypeId);
            //ViewBag.FoodSourceTypesId = new SelectList(db.FoodSourceTypes, "Id", "Title", meals.FoodSourceTypesId);
            // Checkbox sets
            //ViewBag.MealTypesCBData = CheckBoxHelper.GetMealTypes(db, meals.MealTypes, MealTypesPrefix);
            ViewBag.FoodSourceTypesCBData = CheckBoxHelper.GetMealSources(db, meals.FoodSourceTypes, MealSourcesPrefix);
            ViewBag.CEGrainsCBData = CheckBoxHelper.GetCBData(CheckBoxHelper.GrainTypes2ComboData(db), meals.CEGrains, CEGrainsPrefix);
            ViewBag.CEPulseCBData = CheckBoxHelper.GetCBData(CheckBoxHelper.PulseTypes2ComboData(db), meals.CEPulse, CEPulsePrefix);
            ViewBag.CEVegetablesCBData = CheckBoxHelper.GetCBData(CheckBoxHelper.VegetableTypes2ComboData(db), meals.CEVegetables, CEVegetablesPrefix);
            ViewBag.AvgNonVegPerMTypeId = new SelectList(db.AvgNonVegPerMTypes, "Id", "Title", meals.AvgNonVegPerMTypeId);
            ViewBag.PKGPerMTypeId = new SelectList(db.PKGPerMTypes, "Id", "Title", meals.PKGPerMTypeId);

            return View(meals);
        }

        // POST: /Meals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "UpayaAdmin, PartnerAdmin, StaffMember")]
        public ActionResult Edit([Bind(Include = "Id,AvgMealsPerDay,AnyNonVegetarian,AvgNonVegPerMonth,FoodSourceTypes,MilkAsideAmount,CEGrains,CEPulse,CEVegetables,AvgNonVegPerMTypeId,PKGPerMTypeId")] Meals meals)
        {
            if (ModelState.IsValid)
            {
                db.Entry(meals).State = EntityState.Modified;
                // Get the checkbox values
                //meals.MealTypes = CheckBoxHelper.ExtractValues(Request.Form, CheckBoxHelper.GetMealTypeIds(db), MealTypesPrefix);
                meals.FoodSourceTypes = CheckBoxHelper.ExtractValues(Request.Form, CheckBoxHelper.GetFoodSourceIds(db), MealSourcesPrefix);
                meals.CEGrains = CheckBoxHelper.ExtractValues(Request.Form, CheckBoxHelper.GetIds(CheckBoxHelper.GrainTypes2ComboData(db)), CEGrainsPrefix);
                meals.CEPulse = CheckBoxHelper.ExtractValues(Request.Form, CheckBoxHelper.GetIds(CheckBoxHelper.PulseTypes2ComboData(db)), CEPulsePrefix);
                meals.CEVegetables = CheckBoxHelper.ExtractValues(Request.Form, CheckBoxHelper.GetIds(CheckBoxHelper.VegetableTypes2ComboData(db)), CEVegetablesPrefix);
                //
                meals.OrigEntryDate = FormatHelper.ExtractDate(Request.Form, "OrigEntryDate");

                db.SaveChanges();
                HistoryHelper.RecordHistory(meals);
                //return RedirectToAction("Index");
                return RedirectToAction("Details", new { id = meals.Id });
            }

            ViewBag.Beneficiary = db.Beneficiaries.Find(meals.Id);
            //ViewBag.MealTypeId = new SelectList(db.MealTypes, "Id", "Title", meals.MealTypeId);
            //ViewBag.FoodSourceTypesId = new SelectList(db.FoodSourceTypes, "Id", "Title", meals.FoodSourceTypesId);
            // Checkbox sets
            //ViewBag.MealTypesCBData = CheckBoxHelper.GetMealTypes(db, meals.MealTypes, MealTypesPrefix);
            ViewBag.FoodSourceTypesCBData = CheckBoxHelper.GetMealSources(db, meals.FoodSourceTypes, MealSourcesPrefix);
            ViewBag.CEGrainsCBData = CheckBoxHelper.GetCBData(CheckBoxHelper.GrainTypes2ComboData(db), meals.CEGrains, CEGrainsPrefix);
            ViewBag.CEPulseCBData = CheckBoxHelper.GetCBData(CheckBoxHelper.PulseTypes2ComboData(db), meals.CEPulse, CEPulsePrefix);
            ViewBag.CEVegetablesCBData = CheckBoxHelper.GetCBData(CheckBoxHelper.VegetableTypes2ComboData(db), meals.CEVegetables, CEVegetablesPrefix);
            ViewBag.AvgNonVegPerMTypeId = new SelectList(db.AvgNonVegPerMTypes, "Id", "Title", meals.AvgNonVegPerMTypeId);
            ViewBag.PKGPerMTypeId = new SelectList(db.PKGPerMTypes, "Id", "Title", meals.PKGPerMTypeId);

            return View(meals);
        }

        /* GET: /Meals/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meals meals = db.Meals.Find(id);
            if (meals == null)
            {
                return HttpNotFound();
            }
            return View(meals);
        }

        // POST: /Meals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Meals meals = db.Meals.Find(id);
            db.Meals.Remove(meals);
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
