using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpayaWebApp
{
    public class AccountHelper
    {
        static AccountTablesDataContext dbc;
        static AccountTablesDataContext DbContext
        {
            get {
                if(dbc == null)
                {
                    dbc = new AccountTablesDataContext();
                }
                return dbc;
            }
        }

        public static List<string> GetUserNamesInRole(string roleName)
        {
            List<string> userNames = new List<string>();

            string roleId = (from r in DbContext.AspNetRoles
                             where r.Name == roleName
                             select r.Id).FirstOrDefault();

            var userIds = from u in DbContext.AspNetUserRoles
                          where u.RoleId == roleId
                          select u.UserId;

            foreach(string uid in userIds)
                userNames.Add(GetUserNameForId(uid));

            return userNames;
        }

        public static string GetUserNameForId(string userId)
        {
            string uname = (from u in DbContext.AspNetUsers
                    where u.Id == userId
                    select u.UserName).FirstOrDefault();
            return uname;
        }

        public static string GetUserNameForId(Guid userId)
        {
            return GetUserNameForId(userId.ToString());
        }

        public static string GetCurUserName()
        {
            return HttpContext.Current.User.Identity.Name;
        }

        public static string GetUserIdForName(string userName)
        {
            string uid = (from u in DbContext.AspNetUsers
                          where u.UserName == userName
                          select u.Id).First();
            return uid;
        }

        public static Guid GetCurUserId()
        {
            //Membership.
            // Should improve algo, there must be a better way. Simeon 12.22.2013
            //return Guid.NewGuid(); // HttpContext.Current.User.Identity.Name;
            return new Guid(GetUserIdForName(GetCurUserName()));
        }

        public static Guid GetCurCompanyId(DataModelContainer db)
        {
            Guid curUserId = AccountHelper.GetCurUserId();
            PartnerAdmin pa = db.PartnerAdmins.Find(curUserId);
            if (pa != null)
                return pa.PartnerCompanyId;

            PartnerStaffMember psm = db.PartnerStaffMembers.Find(curUserId);
            if (psm != null)
                return psm.PartnerCompanyId;

            throw new Exception("Can't get company Id");
        }
    }
}