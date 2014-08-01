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
    
    public partial class HousingInfo
    {
        public System.Guid Id { get; set; }
        public bool HasElectricity { get; set; }
        public byte NumberOfRoomsTypeId { get; set; }
        public byte RoofMaterialTypeId { get; set; }
        public string WaterSourceTypes { get; set; }
        public byte WallMaterialTypeId { get; set; }
        public byte CookingEnergyTypeId { get; set; }
        public byte ToiletTypeId { get; set; }
        public byte HouseQualityTypeId { get; set; }
        public short ReligionId { get; set; }
        public short CasteId { get; set; }
        public byte HouseOwnershipTypeId { get; set; }
        public byte WaterSourceDistanceTypeId { get; set; }
        public string SavingsTypes { get; set; }
        public string LoanPurposeTypes { get; set; }
        public Nullable<System.DateTime> OrigEntryDate { get; set; }
    
        public virtual NumberOfRoomsType NumberOfRoomsType { get; set; }
        public virtual RoofMaterialType RoofMaterialType { get; set; }
        public virtual WallMaterialType WallMaterialType { get; set; }
        public virtual CookingEnergyType CookingEnergyType { get; set; }
        public virtual Beneficiary Beneficiary { get; set; }
        public virtual ToiletType ToiletType { get; set; }
        public virtual HouseQualityType HouseQualityType { get; set; }
        public virtual Religion Religion { get; set; }
        public virtual Caste Caste { get; set; }
        public virtual HouseOwnershipType HouseOwnershipType { get; set; }
        public virtual WaterSourceDistanceType WaterSourceDistanceType { get; set; }
    }
}