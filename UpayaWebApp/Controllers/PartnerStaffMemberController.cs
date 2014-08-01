using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UpayaWebApp;
using UpayaWebApp.Models;

namespace UpayaWebApp.Controllers
{
    public class PartnerStaffMemberController : Controller
    {
        private DataModelContainer db = new DataModelContainer();

        // GET: /PartnerStaffMember/
        [Authorize(Roles = "PartnerAdmin")]
        public ActionResult Index()
        {
            // Should we put the company Id in the cache?
            Guid curUserId = AccountHelper.GetCurUserId();
            Guid companyId = db.PartnerAdmins.Find(curUserId).PartnerCompanyId;

            var partnerstaffmembers = db.PartnerStaffMembers.Where(p => p.PartnerCompanyId == companyId).Include(p => p.Gender).Include(p => p.StaffType);
            return View(partnerstaffmembers.ToList());
        }

        // GET: /PartnerStaffMember/Details/5
        [Authorize(Roles = "PartnerAdmin")]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartnerStaffMember partnerstaffmember = db.PartnerStaffMembers.Find(id);
            if (partnerstaffmember == null)
            {
                return HttpNotFound();
            }
            return View(partnerstaffmember);
        }

        // GET: /PartnerStaffMember/Create
        [Authorize(Roles = "PartnerAdmin")]
        public ActionResult Create()
        {
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Title");
            ViewBag.StaffTypeId = new SelectList(db.StaffTypes, "Id", "Title");
            return View();
        }

        // POST: /PartnerStaffMember/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // NEED TO IMPROVE THE ALGO TO BETTER HANDLE ERRORS! Simeon 12.22.2013
        [Authorize(Roles = "PartnerAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include="Id,Name,GenderId,Address,Phone,Email,Title,BirthDay,BirthMonth,BirthYear,StaffTypeId,InternalPartnerEmployeeId")] PartnerStaffMember partnerstaffmember)
        public ActionResult Create([Bind(Include = "Id,Name,GenderId,Address,Phone,Email,Title,BirthDay,BirthMonth,BirthYear,StaffTypeId,InternalPartnerEmployeeId,UserName,Password,Password2")] PartnerStaffMember_VModel partnerstaffmemberModel)
        {
            if (ModelState.IsValid)
            {
                /* Old code
                partnerstaffmember.Id = Guid.NewGuid();
                db.PartnerStaffMembers.Add(partnerstaffmember);
                */ 

                // 1st check if such user exists

                // 2nd Create the system user
                UserManager<ApplicationUser> um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                ApplicationUser user = new ApplicationUser() { UserName = partnerstaffmemberModel.UserName };
                Guid uguid = Guid.NewGuid();
                user.Id = uguid.ToString();
                IdentityResult result = um.Create(user, partnerstaffmemberModel.Password);
                if (result.Succeeded)
                {
                    var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                    UserManager.AddToRole(user.Id, Constants.STAFF_MEMBER);

                    // 3rd Create the staff member
                    PartnerStaffMember smInfo = new PartnerStaffMember();
                    smInfo.Id = uguid;
                    smInfo.Name = partnerstaffmemberModel.Name;
                    smInfo.GenderId = partnerstaffmemberModel.GenderId;
                    smInfo.StaffTypeId = partnerstaffmemberModel.StaffTypeId;
                    smInfo.BirthDay = partnerstaffmemberModel.BirthDay;
                    smInfo.BirthMonth = partnerstaffmemberModel.BirthMonth;
                    smInfo.BirthYear = partnerstaffmemberModel.BirthYear;
                    smInfo.Address = partnerstaffmemberModel.Address;
                    smInfo.Email = partnerstaffmemberModel.Email;
                    smInfo.InternalPartnerEmployeeId = partnerstaffmemberModel.InternalPartnerEmployeeId;
                    smInfo.Phone = partnerstaffmemberModel.Phone;
                    smInfo.Title = partnerstaffmemberModel.Title;
                    // Assign to the same company as the Partner Admin
                    Guid curUserId = AccountHelper.GetCurUserId();
                    Guid companyId = db.PartnerAdmins.Find(curUserId).PartnerCompanyId;
                    smInfo.PartnerCompanyId = companyId;

                    db.PartnerStaffMembers.Add(smInfo);
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbEntityValidationException ex)
                    {
                        // Remove the system user
                        Membership.DeleteUser(user.UserName);
                        //
                        ViewBag.GenderId = new SelectList(db.Genders, "Id", "Title", partnerstaffmemberModel.GenderId);
                        ViewBag.StaffTypeId = new SelectList(db.StaffTypes, "Id", "Title", partnerstaffmemberModel.StaffTypeId);
                        return View(partnerstaffmemberModel);
                    }
                    HistoryHelper.StartHistory(smInfo);
                    return RedirectToAction("Index");
                }
            }

            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Title", partnerstaffmemberModel.GenderId);
            ViewBag.StaffTypeId = new SelectList(db.StaffTypes, "Id", "Title", partnerstaffmemberModel.StaffTypeId);
            return View(partnerstaffmemberModel);
        }

        // GET: /PartnerStaffMember/Edit/5
        [Authorize(Roles = "PartnerAdmin")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartnerStaffMember partnerstaffmember = db.PartnerStaffMembers.Find(id);
            if (partnerstaffmember == null)
            {
                return HttpNotFound();
            }
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Title", partnerstaffmember.GenderId);
            ViewBag.StaffTypeId = new SelectList(db.StaffTypes, "Id", "Title", partnerstaffmember.StaffTypeId);
            return View(partnerstaffmember);
        }

        // POST: /PartnerStaffMember/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "PartnerAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name,GenderId,Address,Phone,Email,Title,BirthDay,BirthMonth,BirthYear,StaffTypeId,InternalPartnerEmployeeId")] PartnerStaffMember partnerstaffmember)
        {
            if (ModelState.IsValid)
            {
                db.Entry(partnerstaffmember).State = EntityState.Modified;
                db.SaveChanges();
                HistoryHelper.RecordHistory(partnerstaffmember);
                return RedirectToAction("Index");
            }
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Title", partnerstaffmember.GenderId);
            ViewBag.StaffTypeId = new SelectList(db.StaffTypes, "Id", "Title", partnerstaffmember.StaffTypeId);
            return View(partnerstaffmember);
        }

        /* GET: /PartnerStaffMember/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartnerStaffMember partnerstaffmember = db.PartnerStaffMembers.Find(id);
            if (partnerstaffmember == null)
            {
                return HttpNotFound();
            }
            return View(partnerstaffmember);
        }

        // POST: /PartnerStaffMember/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            PartnerStaffMember partnerstaffmember = db.PartnerStaffMembers.Find(id);
            db.PartnerStaffMembers.Remove(partnerstaffmember);
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
