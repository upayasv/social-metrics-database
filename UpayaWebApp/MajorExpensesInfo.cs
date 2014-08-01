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
    
    public partial class MajorExpensesInfo
    {
        public MajorExpensesInfo()
        {
            this.FoodM = 0;
            this.RentM = 0;
            this.SchoolFeesM = 0;
            this.WaterAndElecM = 0;
            this.CableTvDishM = 0;
            this.LoanRepaymentsM = 0;
            this.AlcoholM = 0;
            this.CinemaFestivFunctA = 0;
            this.LoomRelA = 0;
            this.OtherExpM = 0;
        }
    
        public System.Guid Id { get; set; }
        public int FoodM { get; set; }
        public int RentM { get; set; }
        public int SchoolFeesM { get; set; }
        public short WaterAndElecM { get; set; }
        public int CableTvDishM { get; set; }
        public int LoanRepaymentsM { get; set; }
        public short AlcoholM { get; set; }
        public int CinemaFestivFunctA { get; set; }
        public int LoomRelA { get; set; }
        public int OtherExpM { get; set; }
        public string OtherExpDescr { get; set; }
        public Nullable<System.DateTime> OrigEntryDate { get; set; }
    
        public virtual Beneficiary Beneficiary { get; set; }
    }
}