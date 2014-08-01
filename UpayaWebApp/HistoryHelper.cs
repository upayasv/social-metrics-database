using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace UpayaWebApp
{
    public class HistoryHelper
    {
        public static bool RecordHistory(Object obj)
        {
            return RecordHistory(obj, false);
        }

        public static bool StartHistory(Object obj)
        {
            return RecordHistory(obj, true);
        }

        static bool RecordHistory(Object obj, bool create)
        {
            try
            {
                DataModelContainer db = new DataModelContainer();
                HistoryRec hr = new HistoryRec();
                hr.TimeStamp = DateTime.UtcNow;
                hr.User = AccountHelper.GetCurUserId();
                hr.Created = create;
                Dictionary<string, object> dict = null;
                if (obj is Beneficiary)
                {
                    Beneficiary bf = (Beneficiary)obj;
                    hr.Type = (byte)Constants.HistoryType.BENEFICIARY;
                    hr.RefId = bf.Id;
                    dict = getValue(bf);
                }
                else if (obj is PartnerStaffMember)
                {
                    PartnerStaffMember sm = (PartnerStaffMember)obj;
                    hr.Type = (byte)Constants.HistoryType.STAFF_MEMBER;
                    hr.RefId = sm.Id;
                    dict = getValue(sm);
                }
                else if (obj is PartnerAdmin)
                {
                    PartnerAdmin pa = (PartnerAdmin)obj;
                    hr.Type = (byte)Constants.HistoryType.PARTNER_ADMIN;
                    hr.RefId = pa.Id;
                    dict = getValue(pa);
                }
                else if (obj is Adult)
                {
                    Adult ad = (Adult)obj;
                    hr.Type = (byte)Constants.HistoryType.ADULT;
                    hr.RefId = ad.Id;
                    dict = getValue(ad);
                }
                else if (obj is Child)
                {
                    Child ch = (Child)obj;
                    hr.Type = (byte)Constants.HistoryType.CHILD;
                    hr.RefId = ch.Id;
                    dict = getValue(ch);
                }
                else if (obj is HousingInfo)
                {
                    HousingInfo hi = (HousingInfo)obj;
                    hr.Type = (byte)Constants.HistoryType.HOUSEHOLD_INFO;
                    hr.RefId = hi.Id;
                    dict = getValue(hi);
                }
                else if (obj is GovernmentServicesInfo)
                {
                    GovernmentServicesInfo si = (GovernmentServicesInfo)obj;
                    hr.Type = (byte)Constants.HistoryType.GOVERNMENT_SERVICES;
                    hr.RefId = si.Id;
                    dict = getValue(si);
                }
                else if (obj is MajorExpensesInfo)
                {
                    MajorExpensesInfo me = (MajorExpensesInfo)obj;
                    hr.Type = (byte)Constants.HistoryType.MAJOR_EXPENSES;
                    hr.RefId = me.Id;
                    dict = getValue(me);
                }
                else if (obj is HealthCareInfo)
                {
                    HealthCareInfo hci = (HealthCareInfo)obj;
                    hr.Type = (byte)Constants.HistoryType.HEALTHCARE;
                    hr.RefId = hci.Id;
                    dict = getValue(hci);
                }
                else if (obj is HouseholdAsset)
                {
                    HouseholdAsset ha = (HouseholdAsset)obj;
                    hr.Type = (byte)Constants.HistoryType.HOUSEHOLD_ASSET;
                    hr.RefId = new Guid(FormatHelper.FormatAsGuid(ha.Id));
                    dict = getValue(ha);
                }
                else if (obj is Town)
                {
                    Town t = (Town)obj;
                    hr.Type = (byte)Constants.HistoryType.TOWN;
                    hr.RefId = new Guid(FormatHelper.FormatAsGuid(t.Id));
                    dict = getValue(t);
                }
                else if (obj is Meals)
                {
                    Meals m = (Meals)obj;
                    hr.Type = (byte)Constants.HistoryType.MEALS;
                    hr.RefId = m.Id;
                    dict = getValue(m);
                }
                else
                {
                    // Unsupported type
                    return false;
                }
                hr.Value = (new JavaScriptSerializer()).Serialize(dict);
                db.HistoryRecs.Add(hr);
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                return false;
            }
            return true;
        }

        static Dictionary<string, object> getValue(Beneficiary bf)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["Name"] = bf.Name;
            data["Block"] = bf.Block;
            data["GenderId"] = bf.GenderId;
            data["BirthYear"] = bf.BirthYear;
            data["EducationLevelId"] = bf.EducationLevelId;
            data["Disability"] = bf.Disability;
            data["Disabilities"] = bf.Disabilities;
            data["PrimaryOccupationId"] = bf.PrimaryOccupationId;
            data["PrimaryWorkDaysM"] = bf.PrimaryWorkDaysM;
            data["PrimaryDailyWage"] = bf.PrimaryDailyWage;
            data["SecondaryOccupationId"] = bf.SecondaryOccupationId;
            data["SecondaryWorkDaysM"] = bf.SecondaryWorkDaysM;
            data["SecondaryDailyWage"] = bf.SecondaryDailyWage;
            data["UniqueId"] = bf.UniqueId;
            data["StartDateYear"] = bf.StartDateYear;
            data["OrigEntryDate"] = FormatHelper.FormatOrigDate(bf.OrigEntryDate);

            return data;
        }

        static Dictionary<string, object> getValue(PartnerStaffMember sm)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["Name"] = sm.Name;
            data["Address"] = sm.Address;
            data["Phone"] = sm.Phone;
            data["Title"] = sm.Title;
            data["StaffTypeId"] = sm.StaffTypeId;
            data["BirthYear"] = sm.BirthYear;
            data["BirthMonth"] = sm.BirthMonth;
            data["BirthDay"] = sm.BirthDay;
            data["GenderId"] = sm.GenderId;
            data["InternalPartnerEmployeeId"] = sm.InternalPartnerEmployeeId;
            data["Email"] = sm.Email;

            return data;
        }

        static Dictionary<string, object> getValue(PartnerAdmin pa)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            return data;
        }

        static Dictionary<string, object> getValue(Adult ad)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["BeneficiaryId"] = ad.BeneficiaryId;
            data["Name"] = ad.Name;
            data["AdultRelationshipId"] = ad.AdultRelationshipId;
            data["BirthYear"] = ad.BirthYear;
            data["BirthMonth"] = ad.BirthMonth;
            data["BirthDay"] = ad.BirthDay;
            data["GenderId"] = ad.GenderId;
            data["EducationLevelId"] = ad.EducationLevelId;
            data["PrimaryOccupationId"] = ad.PrimaryOccupationId;
            data["PrimaryWorkDaysM"] = ad.PrimaryWorkDaysM;
            data["PrimaryDailyWage"] = ad.PrimaryDailyWage;
            data["SecondaryOccupationId"] = ad.SecondaryOccupationId;
            data["SecondaryWorkDaysM"] = ad.SecondaryWorkDaysM;
            data["SecondaryDailyWage"] = ad.SecondaryDailyWage;
            data["EmplSameCompany"] = ad.EmplSameCompany;
            data["OrigEntryDate"] = FormatHelper.FormatOrigDate(ad.OrigEntryDate);

            return data;
        }

        static Dictionary<string, object> getValue(Child ch)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["BeneficiaryId"] = ch.BeneficiaryId;
            data["Name"] = ch.Name;
            data["BirthYear"] = ch.BirthYear;
            data["GenderId"] = ch.GenderId;
            data["ChildRelationshipId"] = ch.ChildRelationshipId;
            data["EnrolledInSchool"] = ch.EnrolledInSchool;
            data["SchoolTypeId"] = ch.SchoolTypeId;
            data["WhyNotInSchool"] = ch.WhyNotInSchool;
            data["Disability"] = ch.Disability;
            data["Disabilities"] = ch.Disabilities;
            data["MonthlyEduExpenses"] = ch.MonthlyEduExpenses;
            data["OrigEntryDate"] = FormatHelper.FormatOrigDate(ch.OrigEntryDate);

            return data;
        }

        static Dictionary<string, object> getValue(HousingInfo hi)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["CasteId"] = hi.CasteId;
            data["ReligionId"] = hi.ReligionId;
            data["RoofMaterialTypeId"] = hi.RoofMaterialTypeId;
            data["NumberOfRoomsTypeId"] = hi.NumberOfRoomsTypeId;
            data["HouseOwnershipTypeId"] = hi.HouseOwnershipTypeId;
            data["LoanPurposeTypes"] = hi.LoanPurposeTypes;
            data["WaterSourceDistanceTypeId"] = hi.WaterSourceDistanceTypeId;
            data["WaterSourceTypes"] = hi.WaterSourceTypes;
            data["HasElectricity"] = hi.HasElectricity;
            data["HouseQualityTypeId"] = hi.HouseQualityTypeId;
            data["CookingEnergyTypeId"] = hi.CookingEnergyTypeId;
            data["ToiletTypeId"] = hi.ToiletTypeId;
            data["OrigEntryDate"] = FormatHelper.FormatOrigDate(hi.OrigEntryDate);

            return data;
        }

        static Dictionary<string, object> getValue(GovernmentServicesInfo si)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["GovCards"] = si.GovCards;
            data["GovServices"] = si.GovServices;
            data["OtherCardDescr"] = si.OtherCardDescr;
            data["OrigEntryDate"] = FormatHelper.FormatOrigDate(si.OrigEntryDate);

            return data;
        }

        static Dictionary<string, object> getValue(MajorExpensesInfo me)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["FoodM"] = me.FoodM;
            data["RentM"] = me.RentM;
            data["WaterAndElecM"] = me.WaterAndElecM;
            data["SchoolFeesM"] = me.SchoolFeesM;
            data["LoanRepaymentsM"] = me.LoanRepaymentsM;
            data["CinemaFestivFunctA"] = me.CinemaFestivFunctA;
            data["CableTvDishM"] = me.CableTvDishM;
            data["LoomRelA"] = me.LoomRelA;
            data["AlcoholM"] = me.AlcoholM;
            data["OtherExpM"] = me.OtherExpM;
            data["OtherExpDescr"] = me.OtherExpDescr;
            data["OrigEntryDate"] = FormatHelper.FormatOrigDate(me.OrigEntryDate);

            return data;
        }

        static Dictionary<string, object> getValue(HealthCareInfo hi)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["HcProviders"] = hi.HcProviders;
            data["OrigEntryDate"] = FormatHelper.FormatOrigDate(hi.OrigEntryDate);

            return data;
        }

        static Dictionary<string, object> getValue(HouseholdAsset ha)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["BeneficiaryId"] = ha.BeneficiaryId;
            data["Count"] = ha.Count;
            data["TotalValue"] = ha.TotalValue;
            data["OrigEntryDate"] = FormatHelper.FormatOrigDate(ha.OrigEntryDate);

            return data;
        }

        static Dictionary<string, object> getValue(Meals m)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["AvgMealsPerDay"] = m.AvgMealsPerDay;
            data["AvgNonVegPerMTypeId"] = m.AvgNonVegPerMTypeId;
            data["FoodSourceTypes"] = m.FoodSourceTypes;
            data["CEGrains"] = m.CEGrains;
            data["CEPulse"] = m.CEPulse;
            data["CEVegetables"] = m.CEVegetables;
            data["MilkAsideAmount"] = m.MilkAsideAmount;
            data["PKGPerMTypeId"] = m.PKGPerMTypeId;
            data["OrigEntryDate"] = FormatHelper.FormatOrigDate(m.OrigEntryDate);

            return data;
        }

        static Dictionary<string, object> getValue(Town t)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["Name"] = t.Name;
            data["CountryId"] = t.CountryId;
            data["StateId"] = t.StateId;
            data["DistrictId"] = t.DistrictId;
            data["PostalCode"] = t.PostalCode;

            return data;
        }

    }
}