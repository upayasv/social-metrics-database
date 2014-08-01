using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;

namespace UpayaWebApp
{
    public class CheckBoxData
    {
        public object Id;
        public string Title;
        public bool Checked;
    }

    public class CheckBoxHelper
    {
        public static char Separator = ',';

        public static List<CheckBoxData> GetDisabilities(DataModelContainer db, string sKeys, string keyPrefix)
        {
            List<CheckBoxData> res = new List<CheckBoxData>();
            if (sKeys == null) // Some protection
                sKeys = String.Empty;
            string[] keys = sKeys.Split(Separator);
            foreach(Disability d in db.Disabilities)
            {
                CheckBoxData cbd = new CheckBoxData();
                if(string.IsNullOrWhiteSpace(keyPrefix))
                    cbd.Id = d.Id;
                else
                    cbd.Id = keyPrefix + d.Id;
                cbd.Title = d.Title;
                cbd.Checked = keys.Contains(d.Id.ToString());
                res.Add(cbd);
            }
            return res;
        }

        public static List<object> GetDisabilityIds(DataModelContainer db)
        {
            List<object> res = new List<object>();
            foreach (Disability d in db.Disabilities)
                res.Add(d.Id);
            return res;
        }

        public static string ExtractValues(NameValueCollection formData, List<object> ids, string keyPrefix)
        {
            StringBuilder sb = new StringBuilder();
            string key;
            foreach(object id in ids)
            {
                if (string.IsNullOrWhiteSpace(keyPrefix))
                    key = id.ToString();
                else
                    key = keyPrefix + id;
                if (formData.AllKeys.Contains(key))
                {
                    string value = formData[key].ToString();
                    if (value.StartsWith("true"))
                    {
                        if (sb.Length > 0)
                            sb.Append(Separator);
                        sb.Append(id);
                    }
                }
            }
            return sb.ToString();
        }

        public static string DisabilitiesKeysToValues(string sKeys, DataModelContainer db)
        {
            StringBuilder sb = new StringBuilder();
            if(!string.IsNullOrWhiteSpace(sKeys))
            {
                string[] keys = sKeys.Split(Separator);
                foreach(string key in keys)
                {
                    Disability d = db.Disabilities.Find(short.Parse(key));
                    if (sb.Length > 0)
                        sb.Append(", ");
                    sb.Append(d.Title);
                }
            }
            return sb.ToString();
        }


        public static List<object> GetMealTypeIds(DataModelContainer db)
        {
            List<object> res = new List<object>();
            foreach (MealType m in db.MealTypes)
                res.Add(m.Id);
            return res;
        }

        public static List<CheckBoxData> GetMealTypes(DataModelContainer db, string sKeys, string keyPrefix)
        {
            List<CheckBoxData> res = new List<CheckBoxData>();
            if (sKeys == null) // Some protection
                sKeys = String.Empty;
            string[] keys = sKeys.Split(Separator);
            foreach (MealType m in db.MealTypes)
            {
                CheckBoxData cbd = new CheckBoxData();
                if (string.IsNullOrWhiteSpace(keyPrefix))
                    cbd.Id = m.Id;
                else
                    cbd.Id = keyPrefix + m.Id;
                cbd.Title = m.Title;
                cbd.Checked = keys.Contains(m.Id.ToString());
                res.Add(cbd);
            }
            return res;
        }

        public static string MealTypesKeysToValues(string sKeys, DataModelContainer db)
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(sKeys))
            {
                string[] keys = sKeys.Split(Separator);
                foreach (string key in keys)
                {
                    MealType m = db.MealTypes.Find(short.Parse(key));
                    if (sb.Length > 0)
                        sb.Append(", ");
                    sb.Append(m.Title);
                }
            }
            return sb.ToString();
        }


        public static List<object> GetFoodSourceIds(DataModelContainer db)
        {
            List<object> res = new List<object>();
            foreach (FoodSourceType f in db.FoodSourceTypes)
                res.Add(f.Id);
            return res;
        }

        public static List<CheckBoxData> GetMealSources(DataModelContainer db, string sKeys, string keyPrefix)
        {
            List<CheckBoxData> res = new List<CheckBoxData>();
            if (sKeys == null) // Some protection
                sKeys = String.Empty;
            string[] keys = sKeys.Split(Separator);
            foreach (FoodSourceType f in db.FoodSourceTypes)
            {
                CheckBoxData cbd = new CheckBoxData();
                if (string.IsNullOrWhiteSpace(keyPrefix))
                    cbd.Id = f.Id;
                else
                    cbd.Id = keyPrefix + f.Id;
                cbd.Title = f.Title;
                cbd.Checked = keys.Contains(f.Id.ToString());
                res.Add(cbd);
            }
            return res;
        }

        public static string FoodSourceTypesKeysToValues(string sKeys, DataModelContainer db)
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(sKeys))
            {
                string[] keys = sKeys.Split(Separator);
                foreach (string key in keys)
                {
                    FoodSourceType f = db.FoodSourceTypes.Find(short.Parse(key));
                    if (sb.Length > 0)
                        sb.Append(", ");
                    sb.Append(f.Title);
                }
            }
            return sb.ToString();
        }

        // -------------------------------------------------------------------------
        public static List<object> GetHcProviderIds(DataModelContainer db)
        {
            List<object> res = new List<object>();
            foreach (HcProviderType h in db.HcProviderTypes)
                res.Add(h.Id);
            return res;
        }

        public static List<CheckBoxData> GetHcProviders(DataModelContainer db, string sKeys, string keyPrefix)
        {
            List<CheckBoxData> res = new List<CheckBoxData>();
            if (sKeys == null) // Some protection
                sKeys = String.Empty;
            string[] keys = sKeys.Split(Separator);
            foreach (HcProviderType hcp in db.HcProviderTypes)
            {
                CheckBoxData cbd = new CheckBoxData();
                if (string.IsNullOrWhiteSpace(keyPrefix))
                    cbd.Id = hcp.Id;
                else
                    cbd.Id = keyPrefix + hcp.Id;
                cbd.Title = hcp.Title;
                cbd.Checked = keys.Contains(hcp.Id.ToString());
                res.Add(cbd);
            }
            return res;
        }

        public static string HcProviderKeysToValues(string sKeys, DataModelContainer db)
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(sKeys))
            {
                string[] keys = sKeys.Split(Separator);
                foreach (string key in keys)
                {
                    HcProviderType h = db.HcProviderTypes.Find(short.Parse(key));
                    if (sb.Length > 0)
                        sb.Append(", ");
                    sb.Append(h.Title);
                }
            }
            return sb.ToString();
        }

        // -------------------------------------------------------------------------
        public static List<object> GetHealthIssueIds(DataModelContainer db)
        {
            List<object> res = new List<object>();
            foreach (HealthIssueType h in db.HealthIssueTypes)
                res.Add(h.Id);
            return res;
        }

        public static List<CheckBoxData> GetHealthIssues(DataModelContainer db, string sKeys, string keyPrefix)
        {
            List<CheckBoxData> res = new List<CheckBoxData>();
            if (sKeys == null) // Some protection
                sKeys = String.Empty;
            string[] keys = sKeys.Split(Separator);
            foreach (HealthIssueType hip in db.HealthIssueTypes)
            {
                CheckBoxData cbd = new CheckBoxData();
                if (string.IsNullOrWhiteSpace(keyPrefix))
                    cbd.Id = hip.Id;
                else
                    cbd.Id = keyPrefix + hip.Id;
                cbd.Title = hip.Title;
                cbd.Checked = keys.Contains(hip.Id.ToString());
                res.Add(cbd);
            }
            return res;
        }

        public static string HealthIssueKeysToValues(string sKeys, DataModelContainer db)
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(sKeys))
            {
                string[] keys = sKeys.Split(Separator);
                foreach (string key in keys)
                {
                    HealthIssueType h = db.HealthIssueTypes.Find(short.Parse(key));
                    if (sb.Length > 0)
                        sb.Append(", ");
                    sb.Append(h.Title);
                }
            }
            return sb.ToString();
        }

        // -------------------------------------------------------------------------
        public static List<object> GetMedSourceIds(DataModelContainer db)
        {
            List<object> res = new List<object>();
            foreach (MedSourceType m in db.MedSourceTypes)
                res.Add(m.Id);
            return res;
        }

        public static List<CheckBoxData> GetMedSources(DataModelContainer db, string sKeys, string keyPrefix)
        {
            List<CheckBoxData> res = new List<CheckBoxData>();
            if (sKeys == null) // Some protection
                sKeys = String.Empty;
            string[] keys = sKeys.Split(Separator);
            foreach (MedSourceType ms in db.MedSourceTypes)
            {
                CheckBoxData cbd = new CheckBoxData();
                if (string.IsNullOrWhiteSpace(keyPrefix))
                    cbd.Id = ms.Id;
                else
                    cbd.Id = keyPrefix + ms.Id;
                cbd.Title = ms.Title;
                cbd.Checked = keys.Contains(ms.Id.ToString());
                res.Add(cbd);
            }
            return res;
        }

        public static string MedSourceKeysToValues(string sKeys, DataModelContainer db)
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(sKeys))
            {
                string[] keys = sKeys.Split(Separator);
                foreach (string key in keys)
                {
                    MedSourceType m = db.MedSourceTypes.Find(short.Parse(key));
                    if (sb.Length > 0)
                        sb.Append(", ");
                    sb.Append(m.Title);
                }
            }
            return sb.ToString();
        }
        // -------------------------------------------------------------------------
        public static string GovCardsKeysToValues(string sKeys, DataModelContainer db)
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(sKeys))
            {
                string[] keys = sKeys.Split(Separator);
                foreach (string key in keys)
                {
                    GovCardType gc = db.GovCardTypes.Find(short.Parse(key));
                    if (sb.Length > 0)
                        sb.Append(", ");
                    sb.Append(gc.Title);
                }
            }
            return sb.ToString();
        }

        public static List<CheckBoxData> GetGovCards(DataModelContainer db, string sKeys, string keyPrefix)
        {
            List<CheckBoxData> res = new List<CheckBoxData>();
            if (sKeys == null) // Some protection
                sKeys = String.Empty;
            string[] keys = sKeys.Split(Separator);
            foreach (GovCardType gc in db.GovCardTypes)
            {
                CheckBoxData cbd = new CheckBoxData();
                if (string.IsNullOrWhiteSpace(keyPrefix))
                    cbd.Id = gc.Id;
                else
                    cbd.Id = keyPrefix + gc.Id;
                cbd.Title = gc.Title;
                cbd.Checked = keys.Contains(gc.Id.ToString());
                res.Add(cbd);
            }
            return res;
        }

        public static List<object> GetGovCardIds(DataModelContainer db)
        {
            List<object> res = new List<object>();
            foreach (GovCardType gc in db.GovCardTypes)
                res.Add(gc.Id);
            return res;
        }

        // -------------------------------------------------------------------------
        public static string GovServicesKeysToValues(string sKeys, DataModelContainer db)
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(sKeys))
            {
                string[] keys = sKeys.Split(Separator);
                foreach (string key in keys)
                {
                    GovServiceType gs = db.GovServiceTypes.Find(short.Parse(key));
                    if (sb.Length > 0)
                        sb.Append(", ");
                    sb.Append(gs.Title);
                }
            }
            return sb.ToString();
        }

        public static List<CheckBoxData> GetGovServices(DataModelContainer db, string sKeys, string keyPrefix)
        {
            List<CheckBoxData> res = new List<CheckBoxData>();
            if (sKeys == null) // Some protection
                sKeys = String.Empty;
            string[] keys = sKeys.Split(Separator);
            foreach (GovServiceType gs in db.GovServiceTypes)
            {
                CheckBoxData cbd = new CheckBoxData();
                if (string.IsNullOrWhiteSpace(keyPrefix))
                    cbd.Id = gs.Id;
                else
                    cbd.Id = keyPrefix + gs.Id;
                cbd.Title = gs.Title;
                cbd.Checked = keys.Contains(gs.Id.ToString());
                res.Add(cbd);
            }
            return res;
        }

        public static List<object> GetGovServiceIds(DataModelContainer db)
        {
            List<object> res = new List<object>();
            foreach (GovServiceType gs in db.GovServiceTypes)
                res.Add(gs.Id);
            return res;
        }

        // --------------------------
        public static IEnumerable<IComboData> WaterSources2ComboData(DataModelContainer db)
        {
            List<IComboData> res = new List<IComboData>();
            foreach(WaterSourceType wst in db.WaterSourceTypes)
            {
                res.Add(new ComboData(wst.Id.ToString(), wst.Title));
            }
            return res;
        }

        // --------------------GENERIC VERSION--------------------------------
        public static List<CheckBoxData> GetCBData(IEnumerable<IComboData> data, string sKeys, string keyPrefix)
        {
            List<CheckBoxData> res = new List<CheckBoxData>();
            if (sKeys == null) // Some protection
                sKeys = String.Empty;
            string[] keys = sKeys.Split(Separator);
            foreach (IComboData ci in data)
            {
                CheckBoxData cbd = new CheckBoxData();
                if (string.IsNullOrWhiteSpace(keyPrefix))
                    cbd.Id = ci.Id;
                else
                    cbd.Id = keyPrefix + ci.Id;
                cbd.Title = ci.Title;
                cbd.Checked = keys.Contains(ci.Id.ToString());
                res.Add(cbd);
            }
            return res;
        }

        public static List<object> GetIds(IEnumerable<IComboData> data)
        {
            List<object> res = new List<object>();
            foreach (IComboData itm in data)
                res.Add(itm.Id);
            return res;
        }

        public static string KeysToValues(string sKeys, IEnumerable<IComboData> data)
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(sKeys))
            {
                string[] keys = sKeys.Split(Separator);
                foreach (string key in keys)
                {
                    IComboData dt = data.Where(x => x.Id == key).First();
                    if (sb.Length > 0)
                        sb.Append(", ");
                    sb.Append(dt.Title);
                }
            }
            return sb.ToString();
        }

        // --------------------------
        public static IEnumerable<IComboData> SavingsTypes2ComboData(DataModelContainer db)
        {
            List<IComboData> res = new List<IComboData>();
            foreach (SavingsType st in db.SavingsTypes)
            {
                res.Add(new ComboData(st.Id.ToString(), st.Title));
            }
            return res;
        }
        // --------------------------
        public static IEnumerable<IComboData> LoanPurposeType2ComboData(DataModelContainer db)
        {
            List<IComboData> res = new List<IComboData>();
            foreach (LoanPurposeType lpt in db.LoanPurposeTypes)
            {
                res.Add(new ComboData(lpt.Id.ToString(), lpt.Title));
            }
            return res;
        }
        // --------------------------
        public static IEnumerable<IComboData> GrainTypes2ComboData(DataModelContainer db)
        {
            List<IComboData> res = new List<IComboData>();
            foreach (GrainType gt in db.GrainTypes)
            {
                res.Add(new ComboData(gt.Id.ToString(), gt.Title));
            }
            return res;
        }
        // --------------------------
        public static IEnumerable<IComboData> PulseTypes2ComboData(DataModelContainer db)
        {
            List<IComboData> res = new List<IComboData>();
            foreach (PulseType pt in db.PulseTypes)
            {
                res.Add(new ComboData(pt.Id.ToString(), pt.Title));
            }
            return res;
        }
        // --------------------------
        public static IEnumerable<IComboData> VegetableTypes2ComboData(DataModelContainer db)
        {
            List<IComboData> res = new List<IComboData>();
            foreach (VegetableType vt in db.VegetableTypes)
            {
                res.Add(new ComboData(vt.Id.ToString(), vt.Title));
            }
            return res;
        }
        // --------------------------
        public static IEnumerable<IComboData> WhyNotInSchoolTypes2ComboData(DataModelContainer db)
        {
            List<IComboData> res = new List<IComboData>();
            foreach (WhyNotInSchoolType vt in db.WhyNotInSchoolTypes)
            {
                res.Add(new ComboData(vt.Id.ToString(), vt.Title));
            }
            return res;
        }



    }

}