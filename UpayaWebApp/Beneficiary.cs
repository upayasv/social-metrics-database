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
    
    public partial class Beneficiary
    {
        public Beneficiary()
        {
            this.ReligionId = 0;
            this.LanguageId = 0;
            this.CasteId = 0;
            this.Adults = new HashSet<Adult>();
            this.Children = new HashSet<Child>();
            this.HouseholdAssets = new HashSet<HouseholdAsset>();
        }
    
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public byte GenderId { get; set; }
        public short ReligionId { get; set; }
        public int TownId { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool Disability { get; set; }
        public string Disabilities { get; set; }
        public short EducationLevelId { get; set; }
        public Nullable<byte> BirthDay { get; set; }
        public Nullable<byte> BirthMonth { get; set; }
        public short BirthYear { get; set; }
        public short LanguageId { get; set; }
        public short CasteId { get; set; }
        public string UniqueId { get; set; }
        public Nullable<byte> Status { get; set; }
        public string PhotoUrl { get; set; }
        public string Block { get; set; }
        public byte PrimaryOccupationId { get; set; }
        public byte SecondaryOccupationId { get; set; }
        public Nullable<byte> PrimaryWorkDaysM { get; set; }
        public Nullable<byte> SecondaryWorkDaysM { get; set; }
        public Nullable<short> PrimaryDailyWage { get; set; }
        public Nullable<short> SecondaryDailyWage { get; set; }
        public Nullable<byte> StartDateDay { get; set; }
        public Nullable<byte> StartDateMonth { get; set; }
        public Nullable<short> StartDateYear { get; set; }
        public Nullable<System.DateTime> OrigEntryDate { get; set; }
        public System.Guid PCompanyId { get; set; }
    
        public virtual Gender Gender { get; set; }
        public virtual Religion Religion { get; set; }
        public virtual Town Town { get; set; }
        public virtual EducationLevel EducationLevel { get; set; }
        public virtual ICollection<Adult> Adults { get; set; }
        public virtual ICollection<Child> Children { get; set; }
        public virtual Language Language { get; set; }
        public virtual Caste Caste { get; set; }
        public virtual ICollection<HouseholdAsset> HouseholdAssets { get; set; }
        public virtual Meals Meals { get; set; }
        public virtual MajorExpensesInfo MajorExpensesInfo { get; set; }
        public virtual HealthCareInfo HealthCareInfo { get; set; }
        public virtual GovernmentServicesInfo GovernmentServicesInfo { get; set; }
        public virtual HousingInfo HousingInfo { get; set; }
        public virtual Occupation PrimaryOccupation { get; set; }
        public virtual Occupation SecondaryOccupation { get; set; }
    }
}
