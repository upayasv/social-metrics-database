using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UpayaWebApp;
using UpayaWebApp.Models;

namespace UpayaWebApp.Controllers
{
    public class PartnerAdminController : Controller
    {
        private DataModelContainer db = new DataModelContainer();

        // GET: /PartnerAdmin/
        public ActionResult Index()
        {
            var partneradmins = db.PartnerAdmins.Include(p => p.PartnerCompany);
            List<PartnerAdmin> padmins = partneradmins.ToList();
            List<PartnerAdmin_VModel> model = new List<PartnerAdmin_VModel>();
            foreach(PartnerAdmin pa in padmins)
            {
                PartnerAdmin_VModel nitm = new PartnerAdmin_VModel();
                nitm.Id = pa.Id;
                nitm.PartnerCompanyId = pa.PartnerCompanyId;
                nitm.PartnerCompany = pa.PartnerCompany;
                nitm.UserName = AccountHelper.GetUserNameForId(pa.Id);
                model.Add(nitm);
            }

            return View(model);
        }

        // GET: /PartnerAdmin/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return RedirectToAction("AppError", "Home", new { msg = "PartnerAdmin::Details: id == null" });
            }
            PartnerAdmin partneradmin = db.PartnerAdmins.Find(id);
            if (partneradmin == null)
            {
                //return HttpNotFound();
                return RedirectToAction("AppError", "Home", new { msg = "PartnerAdmin::Details: invalid id" });
            }
            return View(partneradmin);
        }

        // GET: /PartnerAdmin/Create
        [Authorize(Roles = "UpayaAdmin")]
        public ActionResult Create()
        {
            if(db.PartnerCompanies.Count() == 0)
            {
                return RedirectToAction("AppWarning", "Home", new { msg = "There are no partner companies; please create a parner company first." });
            }

            ViewBag.PartnerCompanyId = new SelectList(db.PartnerCompanies, "Id", "Name");
            return View();
        }

        // POST: /PartnerAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "UpayaAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        // NEED TO IMPROVE THE ALGO TO BETTER HANDLE ERRORS! Simeon 12.22.2013
        public ActionResult Create([Bind(Include = "Id,PartnerCompanyId,UserName,Password,Password2")] PartnerAdmin_VModel partneradminModel)
        {
            if (ModelState.IsValid)
            {
                // 1st check if such user exists

                // 2nd Create the system user
                UserManager<ApplicationUser> um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                ApplicationUser user = new ApplicationUser() { UserName = partneradminModel.UserName };
                Guid uguid = Guid.NewGuid();
                user.Id = uguid.ToString();
                IdentityResult result = um.Create(user, partneradminModel.Password);
                if (result.Succeeded)
                {
                    var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                    UserManager.AddToRole(user.Id, Constants.PARTNER_ADMIN);

                    // 3rd Create the staff member
                    PartnerAdmin pa = new PartnerAdmin();
                    pa.Id = uguid;
                    pa.PartnerCompanyId = partneradminModel.PartnerCompanyId;
                    db.PartnerAdmins.Add(pa);
                    db.SaveChanges();
                    HistoryHelper.StartHistory(pa);
                    return RedirectToAction("Index");
                }
            }

            ViewBag.PartnerCompanyId = new SelectList(db.PartnerCompanies, "Id", "Name", partneradminModel.PartnerCompanyId);
            return View(partneradminModel);
        }

        // GET: /PartnerAdmin/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartnerAdmin partneradmin = db.PartnerAdmins.Find(id);
            if (partneradmin == null)
            {
                return HttpNotFound();
            }
            ViewBag.PartnerCompanyId = new SelectList(db.PartnerCompanies, "Id", "Name", partneradmin.PartnerCompanyId);
            return View(partneradmin);
        }

        // POST: /PartnerAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,PartnerCompanyId")] PartnerAdmin partneradmin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(partneradmin).State = EntityState.Modified;
                db.SaveChanges();
                HistoryHelper.RecordHistory(partneradmin);
                return RedirectToAction("Index");
            }
            ViewBag.PartnerCompanyId = new SelectList(db.PartnerCompanies, "Id", "Name", partneradmin.PartnerCompanyId);
            return View(partneradmin);
        }

        /* GET: /PartnerAdmin/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartnerAdmin partneradmin = db.PartnerAdmins.Find(id);
            if (partneradmin == null)
            {
                return HttpNotFound();
            }
            return View(partneradmin);
        }

        // POST: /PartnerAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            PartnerAdmin partneradmin = db.PartnerAdmins.Find(id);
            db.PartnerAdmins.Remove(partneradmin);
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
