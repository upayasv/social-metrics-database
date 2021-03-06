//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UpayaWebApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class PartnerCompany
    {
        public PartnerCompany()
        {
            this.PartnerStaffMembers = new HashSet<PartnerStaffMember>();
            this.PartnerAdmins = new HashSet<PartnerAdmin>();
        }
    
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public Nullable<byte> StartDateDay { get; set; }
        public Nullable<byte> StartDateMonth { get; set; }
        public short StartDateYear { get; set; }
        public Nullable<byte> Status { get; set; }
    
        public virtual ICollection<PartnerStaffMember> PartnerStaffMembers { get; set; }
        public virtual ICollection<PartnerAdmin> PartnerAdmins { get; set; }
    }
}
