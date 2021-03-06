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
    
    public partial class Meals
    {
        public System.Guid Id { get; set; }
        public byte AvgMealsPerDay { get; set; }
        public bool AnyNonVegetarian { get; set; }
        public string MealTypes { get; set; }
        public string FoodSourceTypes { get; set; }
        public Nullable<decimal> MilkAsideAmount { get; set; }
        public string CEGrains { get; set; }
        public string CEPulse { get; set; }
        public string CEVegetables { get; set; }
        public byte AvgNonVegPerMTypeId { get; set; }
        public byte PKGPerMTypeId { get; set; }
        public Nullable<System.DateTime> OrigEntryDate { get; set; }
    
        public virtual Beneficiary Beneficiary { get; set; }
        public virtual AvgNonVegPerMType AvgNonVegPerMType { get; set; }
        public virtual PKGPerMType PKGPerMType { get; set; }
    }
}
