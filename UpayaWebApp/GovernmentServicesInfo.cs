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
    
    public partial class GovernmentServicesInfo
    {
        public System.Guid Id { get; set; }
        public string GovCards { get; set; }
        public string OtherCardDescr { get; set; }
        public Nullable<bool> RcvGovPension { get; set; }
        public Nullable<bool> RcvPrivatePension { get; set; }
        public string GovServices { get; set; }
        public Nullable<System.DateTime> OrigEntryDate { get; set; }
    
        public virtual Beneficiary Beneficiary { get; set; }
    }
}
