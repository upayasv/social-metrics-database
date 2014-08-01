using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UpayaWebApp
{
    [MetadataType(typeof(PartnerCompanyMetadata))]
    public partial class PartnerCompany
    {
    }

    public class PartnerCompanyMetadata
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [Range(2000, 2100)]
        [DisplayName("Start date: Year")]
        public short StartDateYear { get; set; }

        [Range(1, 31)]
        [DisplayName("Start date: Day")]
        public Nullable<byte> StartDateDay { get; set; }

        [Range(1, 12)]
        [DisplayName("Start date: Month")]
        public Nullable<byte> StartDateMonth { get; set; }
    }
//----------------------------------

    [MetadataType(typeof(PartnerAdmin_VModelMetadata))]
    public partial class PartnerAdmin_VModel
    {
    }

    public class PartnerAdmin_VModelMetadata
    {
        [Required]
        [DisplayName("Partner company")]
        public System.Guid PartnerCompanyId { get; set; }

        [Required]
        [DisplayName("User name")]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [DisplayName("Password")]
        public string Password2 { get; set; }
    }

//----------------------------------
    [MetadataType(typeof(TownMetadata))]
    public partial class Town
    {
    }

    public class TownMetadata
    {
        [Required]
        [DisplayName("Country")]
        public string CountryId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DisplayName("State")]
        public short StateId { get; set; }

        [Required]
        [DisplayName("District")]
        public short DistrictId { get; set; }
    }
    //----------------------------------
    [MetadataType(typeof(CountryMetadata))]
    public partial class Country
    {
    }

    public class CountryMetadata
    {
        [DisplayName("Country")]
        public string Name { get; set; }
    }
    //----------------------------------

    [MetadataType(typeof(PartnerStaffMember_VModelMetadata))]
    public partial class PartnerStaffMember_VModel
    {
    }

    public class PartnerStaffMember_VModelMetadata
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [DisplayName("Gender")]
        public byte GenderId { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Email { get; set; }

        [Range(1, 31)]
        [DisplayName("Birthday: Day")]
        public Nullable<byte> BirthDay { get; set; }

        [Range(1, 12)]
        [DisplayName("Birthday: Month")]
        public Nullable<byte> BirthMonth { get; set; }

        [Required]
        [Range(1900, 2100)]
        [DisplayName("Birthday: Year")]
        public short BirthYear { get; set; }

        [Required]
        [DisplayName("Staff type")]
        public byte StaffTypeId { get; set; }

        [Required]
        [DisplayName("Internal employee id")]
        public string InternalPartnerEmployeeId { get; set; }

        [Required]
        [DisplayName("User name")]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [DisplayName("Password")]
        public string Password2 { get; set; }
    }

    //----------------------------------
    [MetadataType(typeof(PartnerStaffMemberMetadata))]
    public partial class PartnerStaffMember
    {
    }

    public class PartnerStaffMemberMetadata
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [DisplayName("Gender")]
        public byte GenderId { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Email { get; set; }

        [Range(1, 31)]
        [DisplayName("Birthday: Day")]
        public Nullable<byte> BirthDay { get; set; }

        [Range(1, 12)]
        [DisplayName("Birthday: Month")]
        public Nullable<byte> BirthMonth { get; set; }

        [Required]
        [Range(1900, 2100)]
        [DisplayName("Birthday: Year")]
        public short BirthYear { get; set; }

        [Required]
        [DisplayName("Staff type")]
        public byte StaffTypeId { get; set; }

        [Required]
        [DisplayName("Internal employee id")]
        public string InternalPartnerEmployeeId { get; set; }
    }
    //----------------------------------

    [MetadataType(typeof(BeneficiaryMetadata))]
    public partial class Beneficiary
    {
    }

    public class BeneficiaryMetadata
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [DisplayName("Gender")]
        public byte GenderId { get; set; }

        [Required]
        [DisplayName("Village")]
        public int TownId { get; set; }

        //[Required]
        //public string Address { get; set; }

        public string Phone { get; set; }

        [Required]
        public bool Disability { get; set; }

        [Required]
        [DisplayName("Education level")]
        public short EducationLevelId { get; set; }

        [Range(1, 31)]
        [DisplayName("Birthday: Day")]
        public Nullable<byte> BirthDay { get; set; }

        [Range(1, 12)]
        [DisplayName("Birthday: Month")]
        public Nullable<byte> BirthMonth { get; set; }

        [Required]
        [Range(1900, 2100)]
        [DisplayName("Birthday: Year")]
        public short BirthYear { get; set; }

        [Required]
        [DisplayName("Primary Occupation")]
        public short PrimaryOccupationId { get; set; }

        [Required]
        [DisplayName("Secondary Occupation")]
        public short SecondaryOccupationId { get; set; }

        [Range(1, 31)]
        [DisplayName("Primary Occupation: Work days per month")]
        public byte PrimaryWorkDaysM { get; set; }

        [Range(0, 31)]
        [DisplayName("Secondary Occupation: Work days per month")]
        public byte SecondaryWorkDaysM { get; set; }

        [DisplayName("Primary daily wage")]
        public short PrimaryDailyWage { get; set; }

        [DisplayName("Secondary daily wage")]
        public short SecondaryDailyWage { get; set; }

        [Required]
        [StringLength(10)]
        [DisplayName("Unique Id (10 characters max)")]
        public string UniqueId { get; set; }

        [DisplayName("Assets")]
        public virtual ICollection<HouseholdAsset> HouseholdAssets { get; set; }

        [Range(2000, 2100)]
        [DisplayName("Start date: Year")]
        public short StartDateYear { get; set; }

        [Range(1, 31)]
        [DisplayName("Start date: Day")]
        public Nullable<byte> StartDateDay { get; set; }

        [Range(1, 12)]
        [DisplayName("Start date: Month")]
        public Nullable<byte> StartDateMonth { get; set; }

        [DisplayName("Original entry date (YYYY/MM/DD)")]
        public Nullable<System.DateTime> OrigEntryDate { get; set; }
    }
    //----------------------------------

    [MetadataType(typeof(AdultMetadata))]
    public partial class Adult
    {
    }

    public class AdultMetadata
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public byte GenderId { get; set; }
        [Required]
        [DisplayName("Education level")]
        public short EducationLevelId { get; set; }
        [Required]
        [DisplayName("Relationship")]
        public byte AdultRelationshipId { get; set; }
        [Required]
        public bool Disability { get; set; }

        [DisplayName("Beneficiary")]
        public System.Guid BeneficiaryId { get; set; }

        [Range(1, 31)]
        [DisplayName("Birthday: Day")]
        public Nullable<byte> BirthDay { get; set; }

        [Range(1, 12)]
        [DisplayName("Birthday: Month")]
        public Nullable<byte> BirthMonth { get; set; }

        [Required]
        [Range(1900, 2100)]
        [DisplayName("Birthday: Year")]
        public short BirthYear { get; set; }

        [Required]
        [DisplayName("Employed by the same company")]
        public bool EmplSameCompany { get; set; }
    }
    //----------------------------------
    [MetadataType(typeof(ChildMetadata))]
    public partial class Child
    {
    }

    public class ChildMetadata
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public byte GenderId { get; set; }

        [Required]
        public bool Disability { get; set; }

        [Range(1, 31)]
        [DisplayName("Birthday: Day")]
        public Nullable<byte> BirthDay { get; set; }

        [Range(1, 12)]
        [DisplayName("Birthday: Month")]
        public Nullable<byte> BirthMonth { get; set; }

        [Required]
        [Range(1900, 2100)]
        [DisplayName("Birthday: Year")]
        public short BirthYear { get; set; }

        //[Required]
        [DisplayName("Relationship")]
        public byte ChildRelationshipId { get; set; }

        [Required]
        [DisplayName("Enrolled in school")]
        public bool EnrolledInSchool { get; set; }

        [DisplayName("Why not in school")]
        public string WhyNotInSchool { get; set; }
    }
    //----------------------------------
    [MetadataType(typeof(GenderMetadata))]
    public partial class Gender
    {
    }

    public class GenderMetadata
    {
        [DisplayName("Gender")]
        public string Title { get; set; }
    }
    //----------------------------------
    [MetadataType(typeof(EducationLevelMetadata))]
    public partial class EducationLevel
    {
    }

    public class EducationLevelMetadata
    {
        [DisplayName("Education level")]
        public string Title { get; set; }
    }
    //----------------------------------
    [MetadataType(typeof(SchoolTypeMetadata))]
    public partial class SchoolType
    {
    }

    public class SchoolTypeMetadata
    {
        [DisplayName("Education level")]
        public string Title { get; set; }
    }
    //----------------------------------
    [MetadataType(typeof(SchoolDistanceMetadata))]
    public partial class SchoolDistance
    {
    }

    public class SchoolDistanceMetadata
    {
        [DisplayName("School distance")]
        public string Title { get; set; }
    }
    //----------------------------------
    [MetadataType(typeof(ClassLevelMetadata))]
    public partial class ClassLevel
    {
    }

    public class ClassLevelMetadata
    {
        [DisplayName("Class level")]
        public string Title { get; set; }
    }

    //----------------------------------
    [MetadataType(typeof(HousingInfoMetadata))]
    public partial class HousingInfo
    {
    }

    public class HousingInfoMetadata
    {
        [DisplayName("Has Electricity")]
        public bool HasElectricity { get; set; }

        [DisplayName("Number of Rooms")]
        public byte NumberOfRoomsTypeId { get; set; }

        [DisplayName("Roof Material")]
        public byte RoofMaterialTypeId { get; set; }

        [DisplayName("Water Source")]
        public string WaterSourceTypes { get; set; }

        [DisplayName("Water Source Distance")]
        public byte WaterSourceDistanceTypeId { get; set; }

        [DisplayName("Wall Material")]
        public byte WallMaterialTypeId { get; set; }

        [DisplayName("Cooking Energy Type")]
        public byte CookingEnergyTypeId { get; set; }

        [DisplayName("Toilet Type")]
        public byte ToiletTypeId { get; set; }

        [DisplayName("House quality")]
        public byte HouseQualityType { get; set; }

        [Required]
        [DisplayName("Household Religion")]
        public short ReligionId { get; set; }

        [Required]
        [DisplayName("Household Caste")]
        public short CasteId { get; set; }

        [Required]
        [DisplayName("Ownership Type")]
        public byte HouseOwnershipTypeId { get; set; }

        [DisplayName("Savings Type")]
        public string SavingsTypes { get; set; }

        [DisplayName("Loan Purpose")]
        public string LoanPurposeTypes { get; set; }
    }

    //----------------------------------
    [MetadataType(typeof(HealthCareInfoMetadata))]
    public partial class HealthCareInfo
    {
    }

    public class HealthCareInfoMetadata
    {
        //[Required] - we get weird state error otherwise
        [DisplayName("Health care providers")]
        public string HcProviders { get; set; }

        [DisplayName("Health care frequency (per quarter)")]
        public short HcFrequencyQ { get; set; }

        [DisplayName("Health care issues")]
        public string HealthIssues { get; set; }

        [DisplayName("Treatment costs (per quarter)")]
        public int TreatmentCostsQ { get; set; }

        [DisplayName("Medicine sources")]
        public string MedSources { get; set; }
    }
    //----------------------------------
    [MetadataType(typeof(MajorExpensesInfoMetadata))]
    public partial class MajorExpensesInfo
    {
    }

    public class MajorExpensesInfoMetadata
    {
        [DisplayName("Food (monthly)")]
        public int FoodM { get; set; }

        [DisplayName("Rent (monthly)")]
        public int RentM { get; set; }

        [DisplayName("School fees (monthly)")]
        public int SchoolFeesM { get; set; }

        [DisplayName("Water&electricity (monthly)")]
        public short WaterAndElecM { get; set; }

        [DisplayName("Cable/TV/Dish (monthly)")]
        public int CableTvDishM { get; set; }

        [DisplayName("Loan repayments (monthly)")]
        public int LoanRepaymentsM { get; set; }

        [DisplayName("Alcohol (monthly)")]
        public short AlcoholM { get; set; }

        [DisplayName("Cinema & festivals (annualy)")]
        public int CinemaFestivFunctA { get; set; }

        [DisplayName("Loom related (annualy)")]
        public int LoomRelA { get; set; }

        [DisplayName("Other expenses (monthly)")]
        public int OtherExpM { get; set; }

        [DisplayName("Other expenses description")]
        public string OtherExpDescr { get; set; }
    }

    //----------------------------------
    [MetadataType(typeof(GovernmentServicesInfoMetadata))]
    public partial class GovernmentServicesInfo
    {
    }

    public class GovernmentServicesInfoMetadata
    {
        [DisplayName("Gov. cards")]
        public string GovCards { get; set; }

        [DisplayName("'Other' card description")]
        public string OtherCardDescr { get; set; }

        [DisplayName("Receives gov. pension")]
        public bool RcvGovPension { get; set; }

        [DisplayName("Receives private pension")]
        public bool RcvPrivatePension { get; set; }

        [DisplayName("Gov. services")]
        public string GovServices { get; set; }
    }

    //----------------------------------

    [MetadataType(typeof(MealsMetadata))]
    public partial class Meals
    {
    }

    public class MealsMetadata
    {
        [Required]
        [Range(1, 3)]
        [DisplayName("Avg Meals per Day (1-3)")]
        public byte AvgMealsPerDay { get; set; }

        [DisplayName("Any Non Vegetarian")]
        public bool AnyNonVegetarian { get; set; }

        [DisplayName("Avg Non Vegetarian per month")]
        public byte AvgNonVegPerMTypeId { get; set; }

        [DisplayName("Meal Types")]
        public string MealTypes { get; set; }

        [DisplayName("Food Source Types")]
        public string FoodSourceTypes { get; set; }

        [DisplayName("Milk aside amount")]
        public Nullable<decimal> MilkAsideAmount { get; set; }

        [DisplayName("Commonly eaten grains")]
        public string CEGrains { get; set; }

        [DisplayName("Commonly eaten pulse")]
        public string CEPulse { get; set; }

        [DisplayName("Commonly eaten vegetables")]
        public string CEVegetables { get; set; }

        [DisplayName("How often did you eat paneer/khoya/ghee per month")]
        public byte PKGPerMTypeId { get; set; }
    }
    //----------------------------------
    [MetadataType(typeof(HouseholdAssetMetadata))]
    public partial class HouseholdAsset
    {
    }

    public class HouseholdAssetMetadata
    {
        [DisplayName("Asset type")]
        public short AssetTypeId { get; set; }

        public short Count { get; set; }

        public Nullable<int> TotalValue { get; set; }

        [DisplayName("Original entry date (YYYY/MM/DD)")]
        public Nullable<System.DateTime> OrigEntryDate { get; set; }
    }
    //----------------------------------
}