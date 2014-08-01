using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpayaWebApp
{
    public class Constants
    {
        public const string SYS_ADMIN = "SysAdmin";
        public const string UPAYA_ADMIN = "UpayaAdmin";
        public const string PARTNER_ADMIN = "PartnerAdmin";
        public const string STAFF_MEMBER = "StaffMember";

        public enum HistoryType : byte
        { 
            PARTNER_COMPANY = 1, PARTNER_ADMIN, TOWN, STAFF_MEMBER, BENEFICIARY, ADULT, CHILD,
            HOUSEHOLD_INFO, MAJOR_EXPENSES, MEALS, HEALTHCARE, GOVERNMENT_SERVICES, HOUSEHOLD_ASSET
        }
    }
}