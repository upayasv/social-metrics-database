using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UpayaWebApp.Models;
using WebMatrix.WebData;

namespace UpayaWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //RolePrincipal r = (RolePrincipal)User;
            //var roles = r.GetRoles();

            //string[] roleNames = Roles.GetRolesForUser();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize(Roles = "SysAdmin")]
        public ActionResult Test()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        /*
        [Authorize(Roles = "SysAdmin, UpayaAdmin")]
        public ActionResult CreateNextLevelAdmin()
        {
            // Reuse the register page for now
            return RedirectToAction("Register", "Account");
            //return View();
        }
        */

        public ActionResult Test(string msg)
        {
            ViewBag.ErrorMessage = msg;
            return View();
        }

        [Authorize(Roles = "SysAdmin, UpayaAdmin")]
        public ActionResult ShowRecentUsers()
        {
            List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
            DataModelContainer db = new DataModelContainer();
            int count = 0;
            foreach(Logins lg in db.Logins.OrderByDescending(item => item.LastLoginTime))
            {
                if (lg.Id == AccountHelper.GetCurUserId())
                    continue;
                TimeSpan ts = DateTime.UtcNow - lg.LastLoginTime;
                if (ts.TotalDays > 2)
                    continue;
                Dictionary<string, object> item = new Dictionary<string, object>();
                item["Name"] = AccountHelper.GetUserNameForId(lg.Id);
                item["Time"] = lg.LastLoginTime;
                item["Ago"] = ts;
                data.Add(item);
                if (++count > 50)
                    break;
            }

            return View(data);
        }

        string GetBasicStats(DataModelContainer db, Guid companyId)
        {
            int benefCount = db.Beneficiaries.Where(x => x.PCompanyId == companyId).Count();
            string msg = "Beneficiaries: " + benefCount;

            int totalAdults = 0, totalChildren = 0;
            int withAdult = 0, withChild = 0, withHomeInfo = 0, withHealthInfo = 0, withMealsInfo = 0;
            foreach (Beneficiary b in db.Beneficiaries.Where(x => x.PCompanyId == companyId))
            {
                if (b.Adults.Count() > 0)
                {
                    withAdult++;
                    totalAdults += b.Adults.Count();
                }
                if (b.Children.Count() > 0)
                {
                    withChild++;
                    totalChildren += b.Children.Count();
                }
                if (b.HousingInfo != null)
                    withHomeInfo++;
                if (b.HealthCareInfo != null)
                    withHealthInfo++;
                if (b.Meals != null)
                    withMealsInfo++;
            }
            msg += "<br />Beneficiaries with adult records: " + withAdult;
            msg += "<br />Beneficiaries with child records: " + withChild;
            msg += "<br />Total adults: " + totalAdults;
            msg += "<br />Total children: " + totalChildren;
            msg += "<br />Beneficiaries with housing info: " + withHomeInfo;
            msg += "<br />Beneficiaries with healthcare info: " + withHealthInfo;
            msg += "<br />Beneficiaries with meals info: " + withMealsInfo;
            return msg;
        }

        [Authorize(Roles = "PartnerAdmin, UpayaAdmin")]
        public ActionResult BasicStats()
        {
            DataModelContainer db = new DataModelContainer();
            if (User.IsInRole(Constants.UPAYA_ADMIN))
            {
                StringBuilder sb = new StringBuilder();
                foreach (PartnerCompany comp in db.PartnerCompanies)
                {
                    sb.Append("<b>"+comp.Name+"</b><br />");
                    sb.Append(GetBasicStats(db, comp.Id) + "<br /><br />");
                }
                ViewBag.Message = sb.ToString();
            }
            else if (User.IsInRole(Constants.PARTNER_ADMIN))
            {
                Guid compId = AccountHelper.GetCurCompanyId(db);
                string msg = GetBasicStats(db, compId);
                ViewBag.Message = msg;
            }
            return View();
        }

        public ActionResult AppError(string msg)
        {
            ViewBag.ErrorMessage = msg;
            return View();
        }

        public ActionResult AppWarning(string msg)
        {
            ViewBag.Warning = msg;
            return View();
        }

        [Authorize(Roles = "SysAdmin")]
        public ActionResult InitDB()
        { 
            DataModelContainer db = new DataModelContainer();
            bool needSaving = false;

            //-----------------------------------------------------------------
            if (!db.Genders.Any(g => g.Title == "Male"))
            {
                db.Genders.Add(new Gender() { Title = "Male" });
                needSaving = true;
            }
            if (!db.Genders.Any(g => g.Title == "Female"))
            {
                db.Genders.Add(new Gender() { Title = "Female" });
                needSaving = true;
            }
            //-----------------------------------------------------------------
            if (!db.Countries.Any(c => c.Name == "India"))
            {
                db.Countries.Add(new Country() { Id = "in", Name = "India" });
                needSaving = true;
            }
            //-----------------------------------------------------------------
            string[] items = new string[] { 
                               "ap", "Andhra Pradesh", "in",      "up", "Uttar Pradesh", "in",
                               "ar", "Arunachal Pradesh", "in",   "cg", "Chhattisgarh", "in",
                               "as", "Assam", "in",               "tr", "Tripura", "in",
                               "br", "Bihar", "in",               "tn", "Tamil Nadu", "in",
                               "mh", "Maharashtra", "in",         "py", "Puducherry", "in",
                               "pb", "Punjab", "in",              "nl", "Nagaland", "in",
                               "sk", "Sikkim", "in",              "or", "Odisha", "in",
                               "ga", "Goa", "in",                 "ka", "Karnataka", "in",
                               "gj", "Gujarat", "in",             "hr", "Haryana", "in",
                               "hp", "Himachal Pradesh", "in",    "jk", "Jammu & Kashmir", "in",
                               "jh", "Jharkhand", "in",           "ka", "Karnataka", "in",
                               "rj", "Rajasthan", "in",           "ml", "Meghalaya", "in",
                               "kl", "Kerala", "in",              "mp", "Madhya Pradesh", "in",
                               "mn", "Manipur", "in",             "wb", "West Bengal", "in"
                             };
            for(int i=0; i<items.Length; i+=3)
            {
                string abbr = items[i], name = items[i+1], cid = items[i + 2];
                if (!db.States.Any(sx => sx.CountryId == cid && (sx.Abbr == abbr || sx.Name == name)))
                {
                    db.States.Add(new State() { Abbr = abbr, Name = name, CountryId = cid });
                    needSaving = true;
                }
            }
            if (needSaving)
            {
                db.SaveChanges();
                needSaving = false;
            }
            //-----------------------------------------------------------------
            items = new string[] { "Unknown", "Not religious", "Hindu", "Muslim", "Christian", "Other" };
            foreach(string itm in items)
                if (!db.Religions.Any(r => r.Title == itm))
                {
                    db.Religions.Add(new Religion() { Title = itm });
                    needSaving = true;
                }
            //-----------------------------------------------------------------------
            items = new string[] { /*"Not disabled",*/ "Deaf", "Dumb", "Blind", "Dysfunctional hand", "Dysfunctional leg", "Other"};
            foreach(string itm in items)
                if (!db.Disabilities.Any(d => d.Title == itm))
                {
                    db.Disabilities.Add(new Disability() { Title = itm });
                    needSaving = true;
                }
            //-----------------------------------------------------------------
            if (!db.StaffTypes.Any(st => st.Title == "Field"))
            {
                db.StaffTypes.Add(new StaffType() { Title = "Field" });
                needSaving = true;
            }
            if (!db.StaffTypes.Any(st => st.Title == "Office"))
            {
                db.StaffTypes.Add(new StaffType() { Title = "Office" });
                needSaving = true;
            }
            //-----------------------------------------------------------------
            if (!db.Languages.Any(e => e.Title == "English"))
            {
                db.Languages.Add(new Language() { Title = "English" });
                needSaving = true;
            }
            //-----------------------------------------------------------------
            items = new string[] { "Unknown", "Government school", "Private school", "Madrasa", "Anganwadi" };
            foreach (string itm in items)
                if (!db.SchoolTypes.Any(st => st.Title == itm))
                {
                    db.SchoolTypes.Add(new SchoolType() { Title = itm });
                    needSaving = true;
                }
            if (!db.SchoolTypes.Any(x => x.Title == "Other" || x.Id == 255))
            {
                db.SchoolTypes.Add(new SchoolType() { Id = 255, Title = "Other" });
                needSaving = true;
            }
            //-----------------------------------------------------------------
            items = new string[] { "Unknown", "Schedule Caste (SC)", "Schedule Tribe (ST)", "Other Backward Caste (OBC)", "General" };
            foreach (string itm in items)
                if (!db.Castes.Any(c => c.Title == itm))
                {
                    db.Castes.Add(new Caste() { Title = itm });
                    needSaving = true;
                }

            //-----------------------------------------------------------------
            items = new string[] { "Unknown", "1", "2", "3", "4", "5", "8", "10", "12", "Graduate", "Illiterate" };
            foreach (string itm in items)
                if (!db.EducationLevels.Any(e => e.Title == itm))
                {
                    db.EducationLevels.Add(new EducationLevel() { Title = itm });
                    needSaving = true;
                }
            //-----------------------------------------------------------------
            items = new string[] { "Unknown", "1", "2", "3", "4", "5", "8", "10", "12", "Graduate", "Illiterate" };
            foreach (string itm in items)
                if (!db.ClassLevels.Any(e => e.Title == itm))
                {
                    db.ClassLevels.Add(new ClassLevel() { Title = itm });
                    needSaving = true;
                }
            //-----------------------------------------------------------------
            items = new string[] { "Unknown", "Son", "Daughter", "Brother", "Sister", "Grandchild", "In-law", "Niece", "Nephew", "Adopted" };
            foreach (string itm in items)
                if (!db.ChildRelationships.Any(r => r.Title == itm))
                {
                    db.ChildRelationships.Add(new ChildRelationship() { Title = itm });
                    needSaving = true;
                }
            if (!db.ChildRelationships.Any(x => x.Title == "Other" || x.Id == 255))
            {
                db.ChildRelationships.Add(new ChildRelationship() { Id = 255, Title = "Other" });
                needSaving = true;
            }
            //-----------------------------------------------------------------
            items = new string[] { "Husband", "Wife", "Brother", "Sister", "Son", "Daughter", "Mother-in-Law", "Father-in-Law",
                                   "Daughter-in-Law", "Son-in-Law", "Sister-in-Law", "Brother-in-Law", "Niece", "Nephew", "Grandmother", "Grandfather" };
            foreach (string itm in items)
                if (!db.AdultRelationships.Any(r => r.Title == itm))
                {
                    db.AdultRelationships.Add(new AdultRelationship() { Title = itm });
                    needSaving = true;
                }
            if (!db.AdultRelationships.Any(x => x.Title == "Other" || x.Id == 255))
            {
                db.AdultRelationships.Add(new AdultRelationship() { Id = 255, Title = "Other" });
                needSaving = true;
            }
            //-----------------------------------------------------------------
            items = new string[] { "Raised bed", /*"Bed net",*/ "Chair", /*"Mirror",*/ "Mobile phone", /*"Telephone (land line)",*/ "Refrigerator",
                "Storage drum", "Cook stove", "Door with lock", /*"Window with lock",*/ "Television", "VCR/DVD/VCD", "Bicycle", "Car",
                "MC/scooter", "Generator", /*"Inverter",*/ "Almira", "Pressure cooker/pan", "Casserole/thermos/thermoware", /*"Pushcard/rickshaw",*/
                "Sawing machine", /*"Electric fan",*/ "Chicken", "Goats/pig", "Cow (female, dairy)", "Cow (male)", "Buffalo", "Room", "Radio",
                // Partner specific?
                "Plow", "Diesel pump", "Loom (hand)", "Loom (powered)", "Warping machine", "Spindle", "Bobbin", "Shuttle"
            };
            foreach (string itm in items)
                if (!db.AssetTypes.Any(e => e.Title == itm))
                {
                    db.AssetTypes.Add(new AssetType() { Title = itm });
                    needSaving = true;
                }
            //-----------------------------------------------------------------
            items = new string[] { "Rice", "Roti", "Leafy Vegetables", "Potato", "Cauliflower", "Tomato" };
            foreach (string itm in items)
                if (!db.MealTypes.Any(mt => mt.Title == itm))
                {
                    db.MealTypes.Add(new MealType() { Title = itm });
                    needSaving = true;
                }
            //-----------------------------------------------------------------
            items = new string[] { "Kirana Shop", "Fair Price Shop (Ration Shop)", "Weekly Village Market", "Local Mandi" };
            foreach (string itm in items)
                if (!db.FoodSourceTypes.Any(fs => fs.Title == itm))
                {
                    db.FoodSourceTypes.Add(new FoodSourceType() { Title = itm });
                    needSaving = true;
                }
            //-----------------------------------------------------------------
            items = new string[] { "Unknown", "Under 0.5km", "0.5 - 2km", "2 - 7km", "7 - 10km", "More than 10km" };
            foreach (string itm in items)
                if (!db.SchoolDistances.Any(sd => sd.Title == itm))
                {
                    db.SchoolDistances.Add(new SchoolDistance() { Title = itm });
                    needSaving = true;
                }
            //-----------------------------------------------------------------
            items = new string[] { "Unknown", "Non-Working", "Cultivator", "Self Employed in Agriculture", "Beedi Maker", 
                                   "NREGA Worker", "Other Casual Labor (Coolie)", "Cattle/Animal Rearing", "Weaver", "Handicraftsman", "Small business owner",
                                   "Student", "Riskshaw Puller", "Housewife", "Factory Worker", "Helper-Weaving activity" };

            foreach (string itm in items)
                if (!db.Occupations.Any(ao => ao.Title == itm))
                {
                    db.Occupations.Add(new Occupation() { Title = itm });
                    needSaving = true;
                }
            if (!db.Occupations.Any(x => x.Title == "Other" || x.Id == 255))
            {
                db.Occupations.Add(new Occupation() { Id = 255, Title = "Other" });
                needSaving = true;
            }
            //-----------------------------------------------------------------
            for (byte i = 1; i < 6; i++)
                if (!db.SchoolDaysPerWeek.Any(ao => ao.Id == i))
                {
                    db.SchoolDaysPerWeek.Add(new SchoolDayPerWeek() { Id = i, Title = ""+i });
                    needSaving = true;
                }
            if(needSaving)
            {
                db.SaveChanges();
                needSaving = false;
            }
            //-----------------------------------------------------------------
            items = GetIndianDistricts();
            for (int i = 0; i < items.Length; i += 4)
            {
                string abbr = items[i].ToLower(), name = items[i + 1], stabbr = items[i + 2].ToLower(), cid = items[i + 3].ToLower();
                State st = db.States.Where(x => x.CountryId == cid && x.Abbr == stabbr).FirstOrDefault();
                if (st != null)
                {
                    if (!db.Districts.Any(d => d.StateId == st.Id && (d.Abbr == abbr || d.Name == name)))
                    {
                        db.Districts.Add(new District() { Abbr = abbr, Name = name, StateId = st.Id });
                        needSaving = true;
                        //db.SaveChanges();
                    }
                }
            }
            //-----------------------------------------------------------------
            items = new string[] { "Spiritual Healer / Tantrik", "Homoepathic / Ayurvedic Doctor", "RMP", "Primary Health Centre (PHC) / Govt Doctor", 
                                   "Pvt Clinic / MBBS", "Hospital" };
            foreach (string itm in items)
                if (!db.HcProviderTypes.Any(hp => hp.Title == itm))
                {
                    db.HcProviderTypes.Add(new HcProviderType() { Title = itm });
                    needSaving = true;
                }
            if (!db.HcProviderTypes.Any(x => x.Title == "Other" || x.Id == 255))
            {
                db.HcProviderTypes.Add(new HcProviderType() { Id = 255, Title = "Other" });
                needSaving = true;
            }
            //-----------------------------------------------------------------
            items = new string[] { "Fever", "Cough", "Diarrehea", "Joint pain", "Fatigue", "Wound", "Skin rash or lesion", "Malaria", "Tuberculosis",
                                   "Animal bite", "Dehydration", "Headache", "Infection", "Gynecological problems", "Tumor/growth" };
            foreach (string itm in items)
                if (!db.HealthIssueTypes.Any(hi => hi.Title == itm))
                {
                    db.HealthIssueTypes.Add(new HealthIssueType() { Title = itm });
                    needSaving = true;
                }
            if (!db.HealthIssueTypes.Any(x => x.Title == "Other" || x.Id == 255))
            {
                db.HealthIssueTypes.Add(new HealthIssueType() { Id = 255, Title = "Other" });
                needSaving = true;
            }
            //-----------------------------------------------------------------
            items = new string[] { "Spiritual Healer / Tantrik", "Homoepathic / Ayurvedic Doctor", "RMP", "Primary Health Centre (PHC) / Govt Doctor",
                                   "Pvt Clinic / MBBS", "Hospital" };
            foreach (string itm in items)
                if (!db.MedSourceTypes.Any(ms => ms.Title == itm))
                {
                    db.MedSourceTypes.Add(new MedSourceType() { Title = itm });
                    needSaving = true;
                }
            if (!db.MedSourceTypes.Any(x => x.Title == "Other" || x.Id == 255))
            {
                db.MedSourceTypes.Add(new MedSourceType() { Id = 255, Title = "Other" });
                needSaving = true;
            }
            //-----------------------------------------------------------------
            items = new string[] { "Yellow Ration Card", "Red Ration Card", "Blue Ration Card", "Health Card", "Voter ID Card", "Weaver's Card" };
            foreach (string itm in items)
                if (!db.GovCardTypes.Any(gc => gc.Title == itm))
                {
                    db.GovCardTypes.Add(new GovCardType() { Title = itm });
                    needSaving = true;
                }
            if (!db.GovCardTypes.Any(x => x.Title == "Other" || x.Id == 255))
            {
                db.GovCardTypes.Add(new GovCardType() { Id = 255, Title = "Other" });
                needSaving = true;
            }
            //-----------------------------------------------------------------
            items = new string[] { "Housing (Indira Awaas Yojana)", "Finance/SHG", "Employment/training", "Childcare/ICDS (Anaganwadi)/Mid-day Meal", 
                                   "100 days Work Guarantee (NREGA)", "Ration Shop (PDS)", "Aadhar/Voter ID Card", "Subsidized Yarn or Loom (Weaver's Card)",
                                   "Health Insurance (RSBY)" };

            foreach (string itm in items)
                if (!db.GovServiceTypes.Any(gs => gs.Title == itm))
                {
                    db.GovServiceTypes.Add(new GovServiceType() { Title = itm });
                    needSaving = true;
                }
            if (!db.GovServiceTypes.Any(x => x.Title == "Other" || x.Id == 255))
            {
                db.GovServiceTypes.Add(new GovServiceType() { Id = 255, Title = "Other" });
                needSaving = true;
            }
            //-----------------------------------------------------------------
            items = new string[] { "1", "2 - 3", "4 - 5", "> 5" };
            foreach (string itm in items)
                if (!db.NumberOfRoomsTypes.Any(nx => nx.Title == itm))
                {
                    db.NumberOfRoomsTypes.Add(new NumberOfRoomsType() { Title = itm });
                    needSaving = true;
                }
            //-----------------------------------------------------------------
            items = new string[] { "Thatch/Mud", "Tiles", "Stones", "Cement" };
            foreach (string itm in items)
                if (!db.WallMaterialTypes.Any(wx => wx.Title == itm))
                {
                    db.WallMaterialTypes.Add(new WallMaterialType() { Title = itm });
                    needSaving = true;
                }
            if (!db.WallMaterialTypes.Any(x => x.Title == "Other" || x.Id == 255))
            {
                db.WallMaterialTypes.Add(new WallMaterialType() { Id = 255, Title = "Other" });
                needSaving = true;
            }
            //-----------------------------------------------------------------
            items = new string[] { "Thatch/Mud", "Tiles", "Stones", "Cement" };
            foreach (string itm in items)
                if (!db.RoofMaterialTypes.Any(wx => wx.Title == itm))
                {
                    db.RoofMaterialTypes.Add(new RoofMaterialType() { Title = itm });
                    needSaving = true;
                }
            if (!db.RoofMaterialTypes.Any(x => x.Title == "Other" || x.Id == 255))
            {
                db.RoofMaterialTypes.Add(new RoofMaterialType() { Id = 255, Title = "Other" });
                needSaving = true;
            }
            //-----------------------------------------------------------------
            items = new string[] { "Public Well/Tap", "Private Tap", "Private Pump", "Borewell" };
            foreach (string itm in items)
                if (!db.WaterSourceTypes.Any(wx => wx.Title == itm))
                {
                    db.WaterSourceTypes.Add(new WaterSourceType() { Title = itm });
                    needSaving = true;
                }
            if (!db.WaterSourceTypes.Any(x => x.Title == "Other" || x.Id == 255))
            {
                db.WaterSourceTypes.Add(new WaterSourceType() { Id = 255, Title = "Other" });
                needSaving = true;
            }
            //-----------------------------------------------------------------
            items = new string[] { "Open", "Pit", "Bricks", "Flush" };
            foreach (string itm in items)
                if (!db.ToiletTypes.Any(wx => wx.Title == itm))
                {
                    db.ToiletTypes.Add(new ToiletType() { Title = itm });
                    needSaving = true;
                }
            //-----------------------------------------------------------------
            items = new string[] { "Firewood", "Coal", "LPG", "Electricity", "Kerosene" };
            foreach (string itm in items)
                if (!db.CookingEnergyTypes.Any(wx => wx.Title == itm))
                {
                    db.CookingEnergyTypes.Add(new CookingEnergyType() { Title = itm });
                    needSaving = true;
                }
            if (!db.CookingEnergyTypes.Any(x => x.Title == "Other" || x.Id == 255))
            {
                db.CookingEnergyTypes.Add(new CookingEnergyType() { Id = 255, Title = "Other" });
                needSaving = true;
            }
            //-----------------------------------------------------------------
            if (!db.HouseQualityTypes.Any(g => g.Title == "Unknown" || g.Id == 0))
            {
                db.HouseQualityTypes.Add(new HouseQualityType() { Id = 0, Title = "Unknown" });
                needSaving = true;
            }
            /*
            if (!db.HouseQualityTypes.Any(g => g.Title == "Pucca" || g.Id == (byte)'p'))
            {
                db.HouseQualityTypes.Add(new HouseQualityType() { Id = (byte)'p', Title = "Pucca" });
                needSaving = true;
            }
            if (!db.HouseQualityTypes.Any(g => g.Title == "Semi-pucca" || g.Id == (byte)'s'))
            {
                db.HouseQualityTypes.Add(new HouseQualityType() { Id = (byte)'s', Title = "Semi-pucca" });
                needSaving = true;
            }
            if (!db.HouseQualityTypes.Any(g => g.Title == "Kacha" || g.Id == (byte)'k'))
            {
                db.HouseQualityTypes.Add(new HouseQualityType() { Id = (byte)'k', Title = "Kacha" });
                needSaving = true;
            }
            */ 
            //-----------------------------------------------------------------
            items = new string[] { "Unknown", "Self owned", "Rented", "Religious grant", "Government housing" };
            foreach (string itm in items)
                if (!db.HouseOwnershipTypes.Any(ot => ot.Title == itm))
                {
                    db.HouseOwnershipTypes.Add(new HouseOwnershipType() { Title = itm });
                    needSaving = true;
                }
            //-----------------------------------------------------------------
            items = new string[] { "Unknown", "0 - 15 minutes", "15 - 30 minutes", "30 - 60 minutes", "More than 1 hour" };
            foreach (string itm in items)
                if (!db.WaterSourceDistanceTypes.Any(wd => wd.Title == itm))
                {
                    db.WaterSourceDistanceTypes.Add(new WaterSourceDistanceType() { Title = itm });
                    needSaving = true;
                }
            //-----------------------------------------------------------------
            items = new string[] { "No loan", "Food", "Medical expense", "School fee", "Asset purchase/maintenance", "Housing",
                                    "Agricultural purpose" };
            foreach (string itm in items)
                if (!db.LoanPurposeTypes.Any(wd => wd.Title == itm))
                {
                    db.LoanPurposeTypes.Add(new LoanPurposeType() { Title = itm });
                    needSaving = true;
                }
            if (!db.LoanPurposeTypes.Any(x => x.Title == "Other" || x.Id == 255))
            {
                db.LoanPurposeTypes.Add(new LoanPurposeType() { Id = 255, Title = "Other" });
                needSaving = true;
            }
            //-----------------------------------------------------------------
            items = new string[] { "No savings", "Bank Account", "Post Office Account", "SHG", "Savings in the household", "Government bonds/Certificates",
                                    "Chit funds" };
            foreach (string itm in items)
                if (!db.SavingsTypes.Any(wd => wd.Title == itm))
                {
                    db.SavingsTypes.Add(new SavingsType() { Title = itm });
                    needSaving = true;
                }
            if (!db.SavingsTypes.Any(x => x.Title == "Others" || x.Id == 255))
            {
                db.SavingsTypes.Add(new SavingsType() { Id = 255, Title = "Others" });
                needSaving = true;
            }
            //-----------------------------------------------------------------
            /*
            items = new string[] { "Unknown" };
            foreach (string itm in items)
                if (!db.WorkDaysPerMonthTypes.Any(wd => wd.Title == itm))
                {
                    db.WorkDaysPerMonthTypes.Add(new WorkDaysPerMonthType() { Title = itm });
                    needSaving = true;
                }
            */ 
            //-----------------------------------------------------------------
            items = new string[] { "Ragi/Bajra", "Rice", "Wheat", "Coarse Millets" };
            foreach (string itm in items)
                if (!db.GrainTypes.Any(wd => wd.Title == itm))
                {
                    db.GrainTypes.Add(new GrainType() { Title = itm });
                    needSaving = true;
                }
            if (!db.GrainTypes.Any(x => x.Title == "Others" || x.Id == 255))
            {
                db.GrainTypes.Add(new GrainType() { Id = 255, Title = "Others" });
                needSaving = true;
            }
            //-----------------------------------------------------------------
            items = new string[] { "Chana Dal", "Moong Dal", "Arhar Dal", "Soya Beans", "Masoor Dal", "Black Eyed Dal" };
            foreach (string itm in items)
                if (!db.PulseTypes.Any(wd => wd.Title == itm))
                {
                    db.PulseTypes.Add(new PulseType() { Title = itm });
                    needSaving = true;
                }
            if (!db.PulseTypes.Any(x => x.Title == "Others" || x.Id == 255))
            {
                db.PulseTypes.Add(new PulseType() { Id = 255, Title = "Others" });
                needSaving = true;
            }
            //-----------------------------------------------------------------
            items = new string[] { "Tubers (Potato, Sweet potato, Yam, Onion)", "Green leafy vegetables (Spinach, Meethi, Saag)", 
                                    "Root vegetables (Radish, Carrot, Tapioca)", "Tomato", "Jackfruit", "Beans" };
            foreach (string itm in items)
                if (!db.VegetableTypes.Any(wd => wd.Title == itm))
                {
                    db.VegetableTypes.Add(new VegetableType() { Title = itm });
                    needSaving = true;
                }
            if (!db.VegetableTypes.Any(x => x.Title == "Others" || x.Id == 255))
            {
                db.VegetableTypes.Add(new VegetableType() { Id = 255, Title = "Others" });
                needSaving = true;
            }
            //-----------------------------------------------------------------
            items = new string[] { "Unknown", "Mostly (more than 5 times per month)", "Often (3-4 times per month)", 
                                   "Sometimes (2-3 times per month)", "Rarely", "Never (Don't consume non-veg)" };
            foreach (string itm in items)
                if (!db.AvgNonVegPerMTypes.Any(x => x.Title == itm))
                {
                    db.AvgNonVegPerMTypes.Add(new AvgNonVegPerMType() { Title = itm });
                    needSaving = true;
                }
            //-----------------------------------------------------------------
            items = new string[] { "Unknown", "Mostly", "Often", "Sometimes", "Rarely", "Never" };
            foreach (string itm in items)
                if (!db.PKGPerMTypes.Any(x => x.Title == itm))
                {
                    db.PKGPerMTypes.Add(new PKGPerMType() { Title = itm });
                    needSaving = true;
                }
            //-----------------------------------------------------------------
            items = new string[] { "School availability", "Can't afford school", "Works for you", "Works for  someone else", "Attended school before",
                                    "Not interested in school", "Not old enough to attend" };
            foreach (string itm in items)
                if (!db.WhyNotInSchoolTypes.Any(wx => wx.Title == itm))
                {
                    db.WhyNotInSchoolTypes.Add(new WhyNotInSchoolType() { Title = itm });
                    needSaving = true;
                }
            //-----------------------------------------------------------------
            
            if(needSaving)
                db.SaveChanges();

            return View();
        }

        [Authorize(Roles = "SysAdmin, UpayaAdmin")]
        public ActionResult ListNextLevelAdmins()
        {
            return RedirectToAction("ListUsersInRole", "Account");
        }

        [Authorize(Roles = "SysAdmin")]
        public ActionResult CreateTempRecs()
        {
            /* ONLY ENABLE TEMPORARY - DO NOT DEPLOY
            DataModelContainer db = new DataModelContainer();

            if (!db.PartnerCompanies.Any(x => x.Name == "Test Company One"))
            {
                PartnerCompany pc = new PartnerCompany();
                pc.Name = "Test Company One";
                pc.Id = Guid.NewGuid();
                pc.Phone = "123";
                pc.Email = "info@company1.com";
                pc.StartDateYear = 2012;
                db.PartnerCompanies.Add(pc);
                db.SaveChanges();
            }

            Guid compId = db.PartnerCompanies.Where(x => x.Name == "Test Company One").First().Id;
            UserManager<ApplicationUser> um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            if (!db.PartnerAdmins.Any(x => x.PartnerCompanyId == compId))
            {
                ApplicationUser user = new ApplicationUser() { UserName = "padmin1" };
                Guid uguid = Guid.NewGuid();
                user.Id = uguid.ToString();
                IdentityResult result = um.Create(user, "123456");
                if (result.Succeeded)
                {
                    var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                    UserManager.AddToRole(user.Id, Constants.PARTNER_ADMIN);

                    PartnerAdmin pa = new PartnerAdmin();
                    pa.Id = uguid;
                    pa.PartnerCompanyId = compId;
                    db.PartnerAdmins.Add(pa);
                    db.SaveChanges();
                }
            }

            if (!db.PartnerStaffMembers.Any(x => x.PartnerCompanyId == compId))
            {
                ApplicationUser user = new ApplicationUser() { UserName = "staff1" };
                Guid uguid = Guid.NewGuid();
                user.Id = uguid.ToString();
                IdentityResult result = um.Create(user, "123456");
                if (result.Succeeded)
                {
                    var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                    UserManager.AddToRole(user.Id, Constants.STAFF_MEMBER);

                    // 3rd Create the staff member
                    PartnerStaffMember smInfo = new PartnerStaffMember();
                    smInfo.Id = uguid;
                    smInfo.Name = "John Doe";
                    smInfo.GenderId = 1;
                    smInfo.StaffTypeId = 1;
                    smInfo.BirthYear = 1990;
                    smInfo.Address = "123 Abc Str.";
                    smInfo.Email = "staff1@comp1.com";
                    smInfo.InternalPartnerEmployeeId = "C1S1";
                    smInfo.Phone = "123";
                    smInfo.Title = "Title";
                    smInfo.PartnerCompanyId = compId;

                    db.PartnerStaffMembers.Add(smInfo);
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbEntityValidationException ex)
                    {
                        // Remove the system user
                        Membership.DeleteUser(user.UserName);
                    }
                }
            }

            if (!db.Towns.Any())
            {
                Town t = new Town();
                t.CountryId = "in";
                t.Name = "Village 1";
                t.StateId = 1;
                t.DistrictId = 1;
                db.Towns.Add(t);
                db.SaveChanges();
            }
            */ 
            /*
            if (!db.Beneficiaries.Any())
            {
                Beneficiary b = new Beneficiary();

                b.ReligionId = b.CasteId = b.LanguageId = 1;
                b.Name = "Benef One";
                b.BirthYear = 1980;
                b.Address = "Somewhere Str.";
                b.Block = "5";
                b.EducationLevelId = 2;
                b.GenderId = 1;
                b.Phone = "123";
                b.TownId = db.Towns.First().Id;
                b.UniqueId = "C1B1";
                b.PrimaryOccupationId = b.SecondaryOccupationId = 1;
                
                db.Beneficiaries.Add(b);
                db.SaveChanges();
            }
            */

            return View();
        }

        static string[] GetIndianDistricts()
        {
            return new string[] { 
                "AD"	,	"Adilabad"	,	    "AP"	,	"IN"	,
                "AN"	,	"Anantapur"	,	    "AP"	,	"IN"	,
                "CH"	,	"Chittoor"	,	    "AP"	,	"IN"	,
                "EG"	,	"East Godavari"	,	"AP"	,	"IN"	,
                "GU"	,	"Guntur"	,	    "AP"	,	"IN"	,
                "HY"	,	"Hyderabad"	,	    "AP"	,	"IN"	,
                "CU"	,	"Cuddapah"	,	    "AP"	,	"IN"	,
                "KA"	,	"Karimnagar"	,	"AP"	,	"IN"	,
                "KH"	,	"Khammam"	,	    "AP"	,	"IN"	,
                "KR"	,	"Krishna"	,	    "AP"	,	"IN"	,
                "KU"	,	"Kurnool"	,	    "AP"	,	"IN"	,
                "MA"	,	"Mahbubnagar"	,	"AP"	,	"IN"	,
                "ME"	,	"Medak"	,	        "AP"	,	"IN"	,
                "NA"	,	"Nalgonda"	,	    "AP"	,	"IN"	,
                "NE"	,	"Nellore"	,	    "AP"	,	"IN"	,
                "NI"	,	"Nizamabad"	,	    "AP"	,	"IN"	,
                "PR"	,	"Prakasam"	,	    "AP"	,	"IN"	,
                "RA"	,	"Rangareddy"	,	"AP"	,	"IN"	,
                "SR"	,	"Srikakulam"	,	"AP"	,	"IN"	,
                "VS"	,	"Vishakhapatnam",	"AP"	,	"IN"	,
                "VZ"	,	"Vizianagaram"	,	"AP"	,	"IN"	,
                "WA"	,	"Warangal"	,	    "AP"	,	"IN"	,
                "WG"	,	"West Godavari"	,	"AP"	,	"IN"	,
                "AJ"	,	"Anjaw"	,	        "AP"	,	"IN"	,
                "CH"	,	"Changlang"	,	    "AP"	,	"IN"	,
                "EK"	,	"East Kameng"	,	"AP"	,	"IN"	,
                "ES"	,	"East Siang"	,	"AP"	,	"IN"	,
                "EL"	,	"Lohit"	,	        "AP"	,	"IN"	,
                //""	,	"Longding"	,	"AP"	,	"IN"	,
                "LB"	,	"Lower Subansiri",	"AP"	,	"IN"	,
                "PA"	,	"Papum Pare"	,	"AP"	,	"IN"	,
                "TA"	,	"Tawang"	,	    "AP"	,	"IN"	,
                "TI"	,	"Tirap"	,	        "AP"	,	"IN"	,
                "UD"	,"Lower Dibang Valley",	"AP"	,	"IN"	,
                "US"	,	"Upper Siang"	,	"AP"	,	"IN"	,
                "UB"	,	"Upper Subansiri",	"AP"	,	"IN"	,
                "WK"	,	"West Kameng"	,	"AP"	,	"IN"	,
                "WS"	,	"West Siang"	,	"AP"	,	"IN"	,
                //""	,	"Upper Dibang Valley"	,	"AP"	,	"IN"	,
                //""	,	"Kurung Kumey"	,	"AP"	,	"IN"	,
                "BA"	,	"Barpeta"	,	    "AS"	,	"IN"	,
                "BA"	,	"Bongaigaon"	,	"AS"	,	"IN"	,
                "CA"	,	"Cachar"	,	    "AS"	,	"IN"	,
                "DA"	,	"Darrang"	,	    "AS"	,	"IN"	,
                "DM"	,	"Dhemaji"	,	    "AS"	,	"IN"	,
                "DB"	,	"Dhubri"	,	    "AS"	,	"IN"	,
                "DI"	,	"Dibrugarh"	,	    "AS"	,	"IN"	,
                "GP"	,	"Goalpara"	,	    "AS"	,	"IN"	,
                "GG"	,	"Golaghat"	,	    "AS"	,	"IN"	,
                "HA"	,	"Hailakandi"	,	"AS"	,	"IN"	,
                "JO"	,	"Jorhat"	,	    "AS"	,	"IN"	,
                "KA"	,	"Karbi Anglong"	,	"AS"	,	"IN"	,
                "KR"	,	"Karimganj"	,	    "AS"	,	"IN"	,
                "KK"	,	"Kokrajhar"	,	    "AS"	,	"IN"	,
                "LA"	,	"Lakhimpur"	,	    "AS"	,	"IN"	,
                "MA"	,	"Morigaon"	,	    "AS"	,	"IN"	,
                "NG"	,	"Nagaon"	,	    "AS"	,	"IN"	,
                "NL"	,	"Nalbari"	,	    "AS"	,	"IN"	,
                "NC"	,	"Dima Hasao"	,	"AS"	,	"IN"	,
                "SI"	,	"Sivasagar"	,	    "AS"	,	"IN"	,
                "SO"	,	"Sonitpur"	,	    "AS"	,	"IN"	,
                "TI"	,	"Tinsukia"	,	    "AS"	,	"IN"	,
                //""	,	"Kamrup"	,	    "AS"	,	"IN"	,
                //""	,	"Kamrup Metropolitan"	,	"AS"	,	"IN"	,
                //""	,	"Baksa"	,	"AS"	,	"IN"	,
                //""	,	"Udalguri"	,	"AS"	,	"IN"	,
                //""	,	"Chirang"	,	"AS"	,	"IN"	,
                "AR"	,	"Araria"	,	    "BR"	,	"IN"	,
                "AR"	,	"Arwal"	,	        "BR"	,	"IN"	,
                "AU"	,	"Aurangabad"	,	"BR"	,	"IN"	,
                "BA"	,	"Banka"	,	        "BR"	,	"IN"	,
                "BE"	,	"Begusarai"	,	    "BR"	,	"IN"	,
                "BG"	,	"Bhagalpur"	,	    "BR"	,	"IN"	,
                "BJ"	,	"Bhojpur"	,	    "BR"	,	"IN"	,
                "BU"	,	"Buxar"	,	        "BR"	,	"IN"	,
                "DA"	,	"Darbhanga"	,	    "BR"	,	"IN"	,
                "EC"	,	"East Champaran",	"BR"	,	"IN"	,
                "GA"	,	"Gaya"	,	        "BR"    ,	"IN"	,
                "GO"	,	"Gopalganj"	,	    "BR"	,	"IN"	,
                "JA"	,	"Jamui"	,	        "BR"	,	"IN"	,
                "JE"	,	"Jehanabad"	,	    "BR"	,	"IN"	,
                "KH"	,	"Khagaria"	,	    "BR"	,	"IN"	,
                "KI"	,	"Kishanganj"	,	"BR"	,	"IN"	,
                "KM"	,	"Kaimur"	,	    "BR"	,	"IN"	,
                "KT"	,	"Katihar"	,	    "BR"	,	"IN"	,
                "LA"	,	"Lakhisarai"	,	"BR"	,	"IN"	,
                "MB"	,	"Madhubani"	,	    "BR"	,	"IN"	,
                "MG"	,	"Munger"	,	    "BR"	,	"IN"	,
                "MP"	,	"Madhepura"	,	    "BR"	,	"IN"	,
                "MZ"	,	"Muzaffarpur"	,	"BR"	,	"IN"	,
                "NL"	,	"Nalanda"	,	    "BR"	,	"IN"	,
                "NW"	,	"Nawada"	,	    "BR"	,	"IN"	,
                "PA"	,	"Patna"	,	        "BR"	,	"IN"	,
                "PU"	,	"Purnia"	,	    "BR"	,	"IN"	,
                "RO"	,	"Rohtas"	,	    "BR"	,	"IN"	,
                "SH"	,	"Saharsa"	,	    "BR"	,	"IN"	,
                "SM"	,	"Samastipur"	,	"BR"	,	"IN"	,
                "SO"	,	"Sheohar"	,	    "BR"	,	"IN"	,
                "SP"	,	"Sheikhpura"	,	"BR"	,	"IN"	,
                "SR"	,	"Saran"	,	        "BR"	,	"IN"	,
                "ST"	,	"Sitamarhi"	,	    "BR"	,	"IN"	,
                "SU"	,	"Supaul"	,	    "BR"	,	"IN"	,
                "SW"	,	"Siwan"	,	        "BR"	,	"IN"	,
                "VA"	,	"Vaishali"	,	    "BR"	,	"IN"	,
                "WC"	,	"West Champaran",	"BR"	,	"IN"	,
                "CH"	,	"Chandigarh"	,	"CG"	,	"IN"	,
                "BA"	,	"Bastar"	,	    "CG"	,	"IN"	,
                "BJ"	,	"Bijapur"	,	    "CG"	,	"IN"	,
                "BI"	,	"Bilaspur"	,	    "CG"	,	"IN"	,
                "DA"	,	"Dantewada"	,	    "CG"	,	"IN"	,
                "DH"	,	"Dhamtari"	,	    "CG"	,	"IN"	,
                "DU"	,	"Durg"	,	        "CG"	,	"IN"	,
                "JA"	,	"Jashpur"	,	    "CG"	,	"IN"	,
                "JC"	,	"Janjgir-Champa",	"CG"	,	"IN"	,
                "KB"	,	"Korba"	,	        "CG"	,	"IN"	,
                "KJ"	,	"Koriya"	,	    "CG"	,	"IN"	,
                "KK"	,	"Kanker"	,	    "CG"	,	"IN"	,
                "KW"	,	"Kabirdham (formerly Kawardha)"	,	"CG"	,	"IN"	,
                "MA"	,	"Mahasamund"	,	"CG"	,	"IN"	,
                "NR"	,	"Narayanpur"	,	"CG"	,	"IN"	,
                "RG"	,	"Raigarh"	,	    "CG"	,	"IN"	,
                "RN"	,	"Rajnandgaon"	,	"CG"	,	"IN"	,
                "RP"	,	"Raipur"	,	    "CG"	,	"IN"	,
                "SJ"	,	"Surguja"	,	    "CG"	,	"IN"	,
                "NG"	,	"North Goa"	,	    "GA"	,	"IN"	,
                "SG"	,	"South Goa"	,	    "GA"	,	"IN"	,
                "AH"	,	"Ahmedabad"	,	    "GJ"	,	"IN"	,
                "AM"	,	"Amreli district",	"GJ"	,	"IN"	,
                "AN"	,	"Anand"	,	        "GJ"	,	"IN"	,
                "AR"	,	"Aravalli"	,	    "GJ"	,	"IN"	,
                "BK"	,	"Banaskantha"	,	"GJ"	,	"IN"	,
                "BR"	,	"Bharuch"	,	    "GJ"	,	"IN"	,
                "BV"	,	"Bhavnagar"	,	    "GJ"	,	"IN"	,
                "DA"	,	"Dahod"	,	        "GJ"	,	"IN"	,
                "DG"	,	"Dang"	,	        "GJ"	,	"IN"	,
                "GA"	,	"Gandhinagar"	,	"GJ"	,	"IN"	,
                "JA"	,	"Jamnagar"	,	    "GJ"	,	"IN"	,
                "JU"	,	"Junagadh"	,	    "GJ"	,	"IN"	,
                "KA"	,	"Kutch"	,	        "GJ"	,	"IN"	,
                "KH"	,	"Kheda"	,	        "GJ"	,	"IN"	,
                "MA"	,	"Mehsana"	,	    "GJ"	,	"IN"	,
                "NR"	,	"Narmada"	,	    "GJ"	,	"IN"	,
                "NV"	,	"Navsari"	,	    "GJ"	,	"IN"	,
                "PA"	,	"Patan"	,	        "GJ"	,	"IN"	,
                "PM"	,	"Panchmahal"	,	"GJ"	,	"IN"	,
                "PO"	,	"Porbandar"	,	    "GJ"	,	"IN"	,
                "RA"	,	"Rajkot"	,	    "GJ"	,	"IN"	,
                "SK"	,	"Sabarkantha"	,	"GJ"	,	"IN"	,
                "SN"	,	"Surendranagar"	,	"GJ"	,	"IN"	,
                "ST"	,	"Surat"	,	        "GJ"	,	"IN"	,
                "TA"	,	"Tapi"	,	        "GJ"	,	"IN"	,
                "VD"	,	"Vadodara"	,	    "GJ"	,	"IN"	,
                "VL"	,	"Valsad"	,	    "GJ"	,	"IN"	,
                "AM"	,	"Ambala"	,	    "HR"	,	"IN"	,
                "BH"	,	"Bhiwani"	,	"HR"	,	"IN"	,
                "FR"	,	"Faridabad"	,	"HR"	,	"IN"	,
                "FT"	,	"Fatehabad"	,	"HR"	,	"IN"	,
                "GU"	,	"Gurgaon"	,	"HR"	,	"IN"	,
                "HI"	,	"Hissar"	,	"HR"	,	"IN"	,
                "JH"	,	"Jhajjar"	,	"HR"	,	"IN"	,
                "JI"	,	"Jind"	,	    "HR"	,	"IN"	,
                "KR"	,	"Karnal"	,	"HR"	,	"IN"	,
                "KT"	,	"Kaithal"	,	"HR"	,	"IN"	,
                "KU"	,	"Kurukshetra"	,	"HR"	,	"IN"	,
                "MA"	,	"Mahendragarh"	,	"HR"	,	"IN"	,
                "MW"	,	"Mewat"	,	    "HR"	,	"IN"	,
                "PW"	,	"Palwal"	,	"HR"	,	"IN"	,
                "PK"	,	"Panchkula"	,	"HR"	,	"IN"	,
                "PP"	,	"Panipat"	,	"HR"	,	"IN"	,
                "RE"	,	"Rewari"	,	"HR"	,	"IN"	,
                "RO"	,	"Rohtak"	,	"HR"	,	"IN"	,
                "SI"	,	"Sirsa"	,	    "HR"	,	"IN"	,
                "SNP"	,	"Sonipat"	,	"HR"	,	"IN"	,
                "YN"	,	"Yamuna Nagar"	,	"HR"	,	"IN"	,
                "BI"	,	"Bilaspur"	,	"HP"	,	"IN"	,
                "CH"	,	"Chamba"	,	"HP"	,	"IN"	,
                "HA"	,	"Hamirpur"	,	"HP"	,	"IN"	,
                "KA"	,	"Kangra"	,	"HP"	,	"IN"	,
                "KI"	,	"Kinnaur"	,	"HP"	,	"IN"	,
                "KU"	,	"Kullu"	,	    "HP"	,	"IN"	,
                "LS"	,	"Lahaul and Spiti"	,	"HP"	,	"IN"	,
                "MA"	,	"Mandi"	,	    "HP"	,	"IN"	,
                "SH"	,	"Shimla"	,	"HP"	,	"IN"	,
                "SI"	,	"Sirmaur"	,	"HP"	,	"IN"	,
                "SO"	,	"Solan"	,	    "HP"	,	"IN"	,
                "UNA"	,	"Una"	,	    "HP"	,	"IN"	,
                "AN"	,	"Anantnag"	,	"JK"	,	"IN"	,
                "BD"	,	"Badgam"	,	"JK"	,	"IN"	,
                "BPR"	,	"Bandipora"	,	"JK"	,	"IN"	,
                "BR"	,	"Baramulla"	,	"JK"	,	"IN"	,
                "DO"	,	"Doda"	,	    "JK"	,	"IN"	,
                "GB"	,	"Ganderbal"	,	"JK"	,	"IN"	,
                "JA"	,	"Jammu"	,	    "JK"	,	"IN"	,
                "KR"	,	"Kargil"	,	"JK"	,	"IN"	,
                "KT"	,	"Kathua"	,	"JK"	,	"IN"	,
                "KW"	,	"Kishtwar"	,	"JK"	,	"IN"	,
                "KU"	,	"Kupwara"	,	"JK"	,	"IN"	,
                "KG"	,	"Kulgam"	,	"JK"	,	"IN"	,
                "LE"	,	"Leh"	,	    "JK"	,	"IN"	,
                "PO"	,	"Poonch"	,	"JK"	,	"IN"	,
                "PU"	,	"Pulwama"	,	"JK"	,	"IN"	,
                "RA"	,	"Rajouri"	,	"JK"	,	"IN"	,
                "RB"	,	"Ramban"	,	"JK"	,	"IN"	,
                "RS"	,	"Reasi"	,	    "JK"	,	"IN"	,
                "SB"	,	"Samba"	,	    "JK"	,	"IN"	,
                "SH"	,	"Shopian"	,	"JK"	,	"IN"	,
                "SR"	,	"Srinagar"	,	"JK"	,	"IN"	,
                "UD"	,	"Udhampur"	,	"JK"	,	"IN"	,
                "BO"	,	"Bokaro"	,	"JH"	,	"IN"	,
                "CH"	,	"Chatra"	,	"JH"	,	"IN"	,
                "DE"	,	"Deoghar"	,	"JH"	,	"IN"	,
                "DH"	,	"Dhanbad"	,	"JH"	,	"IN"	,
                "DU"	,	"Dumka"	,	    "JH"	,	"IN"	,
                "ES"	,	"East Singhbhum"	,	"JH"	,	"IN"	,
                "GA"	,	"Garhwa"	,	"JH"	,	"IN"	,
                "GI"	,	"Giridih"	,	"JH"	,	"IN"	,
                "GO"	,	"Godda"	,	    "JH"	,	"IN"	,
                "GU"	,	"Gumla"	,	    "JH"	,	"IN"	,
                "HA"	,	"Hazaribag"	,	"JH"	,	"IN"	,
                "JA"	,	"Jamtara"	,	"JH"	,	"IN"	,
                "KH"	,	"Khunti"	,	"JH"	,	"IN"	,
                "KO"	,	"Koderma"	,	"JH"	,	"IN"	,
                "LA"	,	"Latehar"	,	"JH"	,	"IN"	,
                "LO"	,	"Lohardaga"	,	"JH"	,	"IN"	,
                "PK"	,	"Pakur"	,	    "JH"	,	"IN"	,
                "PL"	,	"Palamu"	,	"JH"	,	"IN"	,
                "RM"	,	"Ramgarh"	,	"JH"	,	"IN"	,
                "RA"	,	"Ranchi"	,	"JH"	,	"IN"	,
                "SA"	,	"Sahibganj"	,	"JH"	,	"IN"	,
                "SK"	,	"Seraikela Kharsawan"	,	"JH"	,	"IN"	,
                "SI"	,	"Simdega"	,	"JH"	,	"IN"	,
                "WS"	,	"West Singhbhum"	,	"JH"	,	"IN"	,
                "BK"	,	"Bagalkot"	,	"KA"	,	"IN"	,
                "BR"	,	"Bangalore Rural"	,	"KA"	,	"IN"	,
                "BN"	,	"Bangalore Urban"	,	"KA"	,	"IN"	,
                "BG"	,	"Belgaum"	,	"KA"	,	"IN"	,
                "BL"	,	"Bellary"	,	"KA"	,	"IN"	,
                "BD"	,	"Bidar"	,	    "KA"	,	"IN"	,
                "BJ"	,	"Bijapur"	,	"KA"	,	"IN"	,
                "CJ"	,	"Chamarajnagar"	,	"KA"	,	"IN"	,
                "CK"	,	"Chikkamagaluru"	,	"KA"	,	"IN"	,
                "CK"	,	"Chikkaballapur"	,	"KA"	,	"IN"	,
                "CT"	,	"Chitradurga"	,	"KA"	,	"IN"	,
                "DA"	,	"Davanagere"	,	"KA"	,	"IN"	,
                "DH"	,	"Dharwad"	,	"KA"	,	"IN"	,
                "DK"	,	"Dakshina Kannada"	,	"KA"	,	"IN"	,
                "GA"	,	"Gadag"	,	    "KA"	,	"IN"	,
                "GU"	,	"Gulbarga"	,	"KA"	,	"IN"	,
                "HS"	,	"Hassan"	,	"KA"	,	"IN"	,
                "HV"	,	"Haveri district"	,	"KA"	,	"IN"	,
                "KD"	,	"Kodagu"	,	"KA"	,	"IN"	,
                "KL"	,	"Kolar"	,	    "KA"	,	"IN"	,
                "KP"	,	"Koppal"	,	"KA"	,	"IN"	,
                "MA"	,	"Mandya"	,	"KA"	,	"IN"	,
                "MY"	,	"Mysore"	,	"KA"	,	"IN"	,
                "RA"	,	"Raichur"	,	"KA"	,	"IN"	,
                "SH"	,	"Shimoga"	,	"KA"	,	"IN"	,
                "TU"	,	"Tumkur"	,	"KA"	,	"IN"	,
                "UD"	,	"Udupi"	,	    "KA"	,	"IN"	,
                "UK"	,	"Uttara Kannada"	,	"KA"	,	"IN"	,
                "RM"	,	"Ramanagara"	,	"KA"	,	"IN"	,
                "YG"	,	"Yadgir"	,	"KA"	,	"IN"	,
                "AL"	,	"Alappuzha"	,	"KL"	,	"IN"	,
                "ER"	,	"Ernakulam"	,	"KL"	,	"IN"	,
                "ID"	,	"Idukki"	,	"KL"	,	"IN"	,
                "KN"	,	"Kannur"	,	"KL"	,	"IN"	,
                "KS"	,	"Kasaragod"	,	"KL"	,	"IN"	,
                "KL"	,	"Kollam"	,	"KL"	,	"IN"	,
                "KT"	,	"Kottayam"	,	"KL"	,	"IN"	,
                "KZ"	,	"Kozhikode"	,	"KL"	,	"IN"	,
                "MA"	,	"Malappuram"	,	"KL"	,	"IN"	,
                "PL"	,	"Palakkad"	,	"KL"	,	"IN"	,
                "PT"	,	"Pathanamthitta"	,	"KL"	,	"IN"	,
                "TS"	,	"Thrissur"	,	"KL"	,	"IN"	,
                "TV"	,	"Thiruvananthapuram"	,	"KL"	,	"IN"	,
                "WA"	,	"Wayanad"	,	"KL"	,	"IN"	,
                "AG"	,	"Agar"	,	    "MP"	,	"IN"	,
                "AL"	,	"Alirajpur"	,	"MP"	,	"IN"	,
                "AP"	,	"Anuppur"	,	"MP"	,	"IN"	,
                "AS"	,	"Ashok Nagar"	,	"MP"	,	"IN"	,
                "BL"	,	"Balaghat"	,	"MP"	,	"IN"	,
                "BR"	,	"Barwani"	,	"MP"	,	"IN"	,
                "BE"	,	"Betul"	,	    "MP"	,	"IN"	,
                "BD"	,	"Bhind"	,	    "MP"	,	"IN"	,
                "BP"	,	"Bhopal"	,	"MP"	,	"IN"	,
                "BU"	,	"Burhanpur"	,	"MP"	,	"IN"	,
                "CT"	,	"Chhatarpur"	,	"MP"	,	"IN"	,
                "CN"	,	"Chhindwara"	,	"MP"	,	"IN"	,
                "DM"	,	"Damoh"	,	"MP"	,	"IN"	,
                "DT"	,	"Datia"	,	"MP"	,	"IN"	,
                "DE"	,	"Dewas"	,	"MP"	,	"IN"	,
                "DH"	,	"Dhar"	,	"MP"	,	"IN"	,
                "DI"	,	"Dindori"	,	"MP"	,	"IN"	,
                "GU"	,	"Guna"	,	    "MP"	,	"IN"	,
                "GW"	,	"Gwalior"	,	"MP"	,	"IN"	,
                "HA"	,	"Harda"	,	    "MP"	,	"IN"	,
                "HO"	,	"Hoshangabad"	,	"MP"	,	"IN"	,
                "IN"	,	"Indore"	,	"MP"	,	"IN"	,
                "JA"	,	"Jabalpur"	,	"MP"	,	"IN"	,
                "JH"	,	"Jhabua"	,	"MP"	,	"IN"	,
                "KA"	,	"Katni"	,	    "MP"	,	"IN"	,
                //"EN"	,	"KhandwaÊ(East Nimar)"	,	"MP"	,	"IN"	,
                //"WN"	,	"KhargoneÊ(West Nimar)"	,	"MP"	,	"IN"	,
                "ML"	,	"Mandla"	,	"MP"	,	"IN"	,
                "MS"	,	"Mandsaur"	,	"MP"	,	"IN"	,
                "MO"	,	"Morena"	,	"MP"	,	"IN"	,
                "NA"	,	"Narsinghpur"	,	"MP"	,	"IN"	,
                "NE"	,	"Neemuch"	,	"MP"	,	"IN"	,
                "PA"	,	"Panna"	,	    "MP"	,	"IN"	,
                "RS"	,	"Raisen"	,	"MP"	,	"IN"	,
                "RG"	,	"Rajgarh"	,	"MP"	,	"IN"	,
                "RL"	,	"Ratlam"	,	"MP"	,	"IN"	,
                "RE"	,	"Rewa"	,	    "MP"	,	"IN"	,
                "SG"	,	"Sagar"	,	    "MP"	,	"IN"	,
                "ST"	,	"Satna"	,	    "MP"	,	"IN"	,
                "SR"	,	"Sehore"	,	"MP"	,	"IN"	,
                "SO"	,	"Seoni"	,	    "MP"	,	"IN"	,
                "SH"	,	"Shahdol"	,	"MP"	,	"IN"	,
                "SJ"	,	"Shajapur"	,	"MP"	,	"IN"	,
                "SP"	,	"Sheopur"	,	"MP"	,	"IN"	,
                "SV"	,	"Shivpuri"	,	"MP"	,	"IN"	,
                "SI"	,	"Sidhi"	,	    "MP"	,	"IN"	,
                "SN"	,	"Singrauli"	,	"MP"	,	"IN"	,
                "TI"	,	"Tikamgarh"	,	"MP"	,	"IN"	,
                "UJ"	,	"Ujjain"	,	"MP"	,	"IN"	,
                "UM"	,	"Umaria"	,	"MP"	,	"IN"	,
                "VI"	,	"Vidisha"	,	"MP"	,	"IN"	,
                "AH"	,	"Ahmednagar"	,	"MH"	,	"IN"	,
                "AK"	,	"Akola"	,	    "MH"	,	"IN"	,
                "AM"	,	"Amravati"	,	"MH"	,	"IN"	,
                "AU"	,	"Aurangabad"	,	"MH"	,	"IN"	,
                "BI"	,	"Beed"	,	    "MH"	,	"IN"	,
                "BH"	,	"Bhandara"	,	"MH"	,	"IN"	,
                "BU"	,	"Buldhana"	,	"MH"	,	"IN"	,
                "CH"	,	"Chandrapur"	,	"MH"	,	"IN"	,
                "DH"	,	"Dhule"	,	    "MH"	,	"IN"	,
                "GA"	,	"Gadchiroli"	,	"MH"	,	"IN"	,
                "GO"	,	"Gondia"	,	"MH"	,	"IN"	,
                "HI"	,	"Hingoli"	,	"MH"	,	"IN"	,
                "JG"	,	"Jalgaon"	,	"MH"	,	"IN"	,
                "JN"	,	"Jalna"	,	    "MH"	,	"IN"	,
                "KO"	,	"Kolhapur"	,	"MH"	,	"IN"	,
                "LA"	,	"Latur"	,	    "MH"	,	"IN"	,
                "MC"	,	"Mumbai City"	,	"MH"	,	"IN"	,
                "MU"	,	"Mumbai suburban"	,	"MH"	,	"IN"	,
                "ND"	,	"Nanded"	,	"MH"	,	"IN"	,
                "NB"	,	"Nandurbar"	,	"MH"	,	"IN"	,
                "NG"	,	"Nagpur"	,	"MH"	,	"IN"	,
                "NS"	,	"Nashik"	,	"MH"	,	"IN"	,
                "OS"	,	"Osmanabad"	,	"MH"	,	"IN"	,
                "PA"	,	"Parbhani"	,	"MH"	,	"IN"	,
                "PU"	,	"Pune"	,	    "MH"	,	"IN"	,
                "RG"	,	"Raigad"	,	"MH"	,	"IN"	,
                "RT"	,	"Ratnagiri"	,	"MH"	,	"IN"	,
                "SN"	,	"Sangli"	,	"MH"	,	"IN"	,
                "ST"	,	"Satara"	,	"MH"	,	"IN"	,
                "SI"	,	"Sindhudurg"	,	"MH"	,	"IN"	,
                "SO"	,	"Solapur"	,	"MH"	,	"IN"	,
                "TH"	,	"Thane"	,	    "MH"	,	"IN"	,
                "WR"	,	"Wardha"	,	"MH"	,	"IN"	,
                "WS"	,	"Washim"	,	"MH"	,	"IN"	,
                "YA"	,	"Yavatmal"	,	"MH"	,	"IN"	,
                "BI"	,	"Bishnupur"	,	"MN"	,	"IN"	,
                "CC"	,	"Churachandpur"	,	"MN"	,	"IN"	,
                "CD"	,	"Chandel"	,	"MN"	,	"IN"	,
                "EI"	,	"Imphal East"	,	"MN"	,	"IN"	,
                "SE"	,	"Senapati"	,	"MN"	,	"IN"	,
                "TA"	,	"Tamenglong"	,	"MN"	,	"IN"	,
                "TH"	,	"Thoubal"	,	"MN"	,	"IN"	,
                "UK"	,	"Ukhrul"	,	"MN"	,	"IN"	,
                "WI"	,	"Imphal West"	,	"MN"	,	"IN"	,
                "EG"	,	"East Garo Hills"	,	"ML"	,	"IN"	,
                "EK"	,	"East Khasi Hills"	,	"ML"	,	"IN"	,
                "JH"	,	"Jaintia Hills"	,	"ML"	,	"IN"	,
                "RB"	,	"Ri Bhoi"	,	"ML"	,	"IN"	,
                "SG"	,	"South Garo Hills"	,	"ML"	,	"IN"	,
                "WG"	,	"West Garo Hills"	,	"ML"	,	"IN"	,
                "WK"	,	"West Khasi Hills"	,	"ML"	,	"IN"	,
                "AI"	,	"Aizawl"	,	"MZ"	,	"IN"	,
                "CH"	,	"Champhai"	,	"MZ"	,	"IN"	,
                "KO"	,	"Kolasib"	,	"MZ"	,	"IN"	,
                "LA"	,	"Lawngtlai"	,	"MZ"	,	"IN"	,
                "LU"	,	"Lunglei"	,	"MZ"	,	"IN"	,
                "MA"	,	"Mamit"	,	    "MZ"	,	"IN"	,
                "SA"	,	"Saiha"	,	    "MZ"	,	"IN"	,
                "SE"	,	"Serchhip"	,	"MZ"	,	"IN"	,
                "DI"	,	"Dimapur"	,	"NL"	,	"IN"	,
                "KI"	,	"Kiphire"	,	"NL"	,	"IN"	,
                "KO"	,	"Kohima"	,	"NL"	,	"IN"	,
                "LO"	,	"Longleng"	,	"NL"	,	"IN"	,
                "MK"	,	"Mokokchung"	,	"NL"	,	"IN"	,
                "MN"	,	"Mon"	,	    "NL"	,	"IN"	,
                "PE"	,	"Peren"	,	    "NL"	,	"IN"	,
                "PH"	,	"Phek"	,	    "NL"	,	"IN"	,
                "TU"	,	"Tuensang"	,	"NL"	,	"IN"	,
                "WO"	,	"Wokha"	,	    "NL"	,	"IN"	,
                "ZU"	,	"Zunheboto"	,	"NL"	,	"IN"	,
                "AN"	,	"Angul"	,	    "OR"	,	"IN"	,
                //"BD"	,	"BoudhÊ(Bauda)"	,	"OR"	,	"IN"	,
                "BH"	,	"Bhadrak"	,	"OR"	,	"IN"	,
                "BL"	,	"Balangir"	,	"OR"	,	"IN"	,
                //"BR"	,	"BargarhÊ(Baragarh)"	,	"OR"	,	"IN"	,
                "BW"	,	"Balasore"	,	"OR"	,	"IN"	,
                "CU"	,	"Cuttack"	,	"OR"	,	"IN"	,
                "DE"	,	"Debagarh(Deogarh)"	,	"OR"	,	"IN"	,
                "DH"	,	"Dhenkanal"	,	"OR"	,	"IN"	,
                "GN"	,	"Ganjam"	,	"OR"	,	"IN"	,
                "GP"	,	"Gajapati"	,	"OR"	,	"IN"	,
                "JH"	,	"Jharsuguda"	,	"OR"	,	"IN"	,
                "JP"	,	"Jajpur"	,	"OR"	,	"IN"	,
                "JS"	,	"Jagatsinghpur"	,	"OR"	,	"IN"	,
                "KH"	,	"Khordha"	,	"OR"	,	"IN"	,
                "KJ"	,	"Kendujhar(Keonjhar)"	,	"OR"	,	"IN"	,
                "KL"	,	"Kalahandi"	,	"OR"	,	"IN"	,
                "KN"	,	"Kandhamal"	,	"OR"	,	"IN"	,
                "KO"	,	"Koraput"	,	"OR"	,	"IN"	,
                "KP"	,	"Kendrapara"	,	"OR"	,	"IN"	,
                "ML"	,	"Malkangiri"	,	"OR"	,	"IN"	,
                "MY"	,	"Mayurbhanj"	,	"OR"	,	"IN"	,
                "NB"	,	"Nabarangpur"	,	"OR"	,	"IN"	,
                "NU"	,	"Nuapada"	,	"OR"	,	"IN"	,
                "NY"	,	"Nayagarh"	,	"OR"	,	"IN"	,
                "PU"	,	"Puri"	,	    "OR"	,	"IN"	,
                "RA"	,	"Rayagada"	,	"OR"	,	"IN"	,
                "SA"	,	"Sambalpur"	,	"OR"	,	"IN"	,
                "SO"	,	"Subarnapur (Sonepur)"	,	"OR"	,	"IN"	,
                "SU"	,	"Sundergarh"	,	"OR"	,	"IN"	,
                "KA"	,	"Karaikal"	,	"PY"	,	"IN"	,
                "MA"	,	"Mahe"	,	    "PY"	,	"IN"	,
                "PO"	,	"Pondicherry"	,	"PY"	,	"IN"	,
                "YA"	,	"Yanam"	,	    "PY"	,	"IN"	,
                "AM"	,	"Amritsar"	,	"PB"	,	"IN"	,
                "BNL"	,	"Barnala"	,	"PB"	,	"IN"	,
                "BA"	,	"Bathinda"	,	"PB"	,	"IN"	,
                "FI"	,	"Firozpur"	,	"PB"	,	"IN"	,
                "FR"	,	"Faridkot"	,	"PB"	,	"IN"	,
                "FT"	,	"Fatehgarh Sahib"	,	"PB"	,	"IN"	,
                "FA"	,	"Fazilka"	,	"PB"	,	"IN"	,
                "GU"	,	"Gurdaspur"	,	"PB"	,	"IN"	,
                "HO"	,	"Hoshiarpur"	,	"PB"	,	"IN"	,
                "JA"	,	"Jalandhar"	,	"PB"	,	"IN"	,
                "KA"	,	"Kapurthala"	,	"PB"	,	"IN"	,
                "LU"	,	"Ludhiana"	,	"PB"	,	"IN"	,
                "MA"	,	"Mansa"	,	    "PB"	,	"IN"	,
                "MO"	,	"Moga"	,	    "PB"	,	"IN"	,
                "MU"	,	"Sri Muktsar Sahib"	,	"PB"	,	"IN"	,
                "PA"	,	"Pathankot"	,	"PB"	,	"IN"	,
                "PA"	,	"Patiala"	,	"PB"	,	"IN"	,
                "RU"	,	"Rupnagar"	,	"PB"	,	"IN"	,
                "SAS"	,	"Ajitgarh (Mohali)"	,	"PB"	,	"IN"	,
                "SA"	,	"Sangrur"	,	"PB"	,	"IN"	,
                "PB"	,	"Shahid Bhagat Singh Nagar"	,	"PB"	,	"IN"	,
                "TT"	,	"Tarn Taran"	,	"PB"	,	"IN"	,
                "AJ"	,	"Ajmer"	,	    "RJ"	,	"IN"	,
                "AL"	,	"Alwar"	,	    "RJ"	,	"IN"	,
                "BI"	,	"Bikaner"	,	"RJ"	,	"IN"	,
                "BM"	,	"Barmer"	,	"RJ"	,	"IN"	,
                "BN"	,	"Banswara"	,	"RJ"	,	"IN"	,
                "BP"	,	"Bharatpur"	,	"RJ"	,	"IN"	,
                "BR"	,	"Baran"	,	    "RJ"	,	"IN"	,
                "BU"	,	"Bundi"	,	    "RJ"	,	"IN"	,
                "BW"	,	"Bhilwara"	,	"RJ"	,	"IN"	,
                "CR"	,	"Churu"	,	    "RJ"	,	"IN"	,
                "CT"	,	"Chittorgarh"	,	"RJ"	,	"IN"	,
                "DA"	,	"Dausa"	,	    "RJ"	,	"IN"	,
                "DH"	,	"Dholpur"	,	"RJ"	,	"IN"	,
                "DU"	,	"Dungapur"	,	"RJ"	,	"IN"	,
                "GA"	,	"Ganganagar"	,	"RJ"	,	"IN"	,
                "HA"	,	"Hanumangarh"	,	"RJ"	,	"IN"	,
                "JJ"	,	"Jhunjhunu"	,	"RJ"	,	"IN"	,
                "JL"	,	"Jalore"	,	"RJ"	,	"IN"	,
                "JO"	,	"Jodhpur"	,	"RJ"	,	"IN"	,
                "JP"	,	"Jaipur"	,	"RJ"	,	"IN"	,
                "JS"	,	"Jaisalmer"	,	"RJ"	,	"IN"	,
                "JW"	,	"Jhalawar"	,	"RJ"	,	"IN"	,
                "KA"	,	"Karauli"	,	"RJ"	,	"IN"	,
                "KO"	,	"Kota"	,	    "RJ"	,	"IN"	,
                "NA"	,	"Nagaur"	,	"RJ"	,	"IN"	,
                "PA"	,	"Pali"	,	        "RJ"	,	"IN"	,
                "PG"	,	"Pratapgarh"	,	"RJ"	,	"IN"	,
                "RA"	,	"Rajsamand"	,	    "RJ"	,	"IN"	,
                "SK"	,	"Sikar"	,	        "RJ"	,	"IN"	,
                "SM"	,	"Sawai Madhopur",	"RJ"	,	"IN"	,
                "SR"	,	"Sirohi"	,	    "RJ"	,	"IN"	,
                "TO"	,	"Tonk"	,	        "RJ"	,	"IN"	,
                "UD"	,	"Udaipur"	,	    "RJ"	,	"IN"	,
                "ES"	,	"East Sikkim"	,	"SK"	,	"IN"	,
                "NS"	,	"North Sikkim"	,	"SK"	,	"IN"	,
                "SS"	,	"South Sikkim"	,	"SK"	,	"IN"	,
                "WS"	,	"West Sikkim"	,	"SK"	,	"IN"	,
                "AY"	,	"Ariyalur"	,	    "TN"	,	"IN"	,
                "CH"	,	"Chennai"	,	    "TN"	,	"IN"	,
                "CO"	,	"Coimbatore"	,	"TN"	,	"IN"	,
                "CU"	,	"Cuddalore"	,	    "TN"	,	"IN"	,
                "DH"	,	"Dharmapuri"	,	"TN"	,	"IN"	,
                "DI"	,	"Dindigul"	,	    "TN"	,	"IN"	,
                "ER"	,	"Erode"	,	        "TN"	,	"IN"	,
                "KC"	,	"Kanchipuram"	,	"TN"	,	"IN"	,
                "KK"	,	"Kanyakumari"	,	"TN"	,	"IN"	,
                "KR"	,	"Karur"	,	"TN"	,	"IN"	,
                "KR"	,	"Krishnagiri"	,	"TN"	,	"IN"	,
                "MA"	,	"Madurai"	,	    "TN"	,	"IN"	,
                "NG"	,	"Nagapattinam"	,	"TN"	,	"IN"	,
                "NI"	,	"Nilgiris"	,	    "TN"	,	"IN"	,
                "NM"	,	"Namakkal"	,	    "TN"	,	"IN"	,
                "PE"	,	"Perambalur"	,	"TN"	,	"IN"	,
                "PU"	,	"Pudukkottai"	,	"TN"	,	"IN"	,
                "RA"	,	"Ramanathapuram"	,	"TN"	,	"IN"	,
                "SA"	,	"Salem"	,	        "TN"	,	"IN"	,
                "SI"	,	"Sivaganga"	,	    "TN"	,	"IN"	,
                "TP"	,	"Tirupur"	,	    "TN"	,	"IN"	,
                "TC"	,	"Tiruchirappalli"	,	"TN"	,	"IN"	,
                "TH"	,	"Theni"	,	        "TN"	,	"IN"	,
                "TI"	,	"Tirunelveli"	,	"TN"	,	"IN"	,
                "TJ"	,	"Thanjavur"	,	    "TN"	,	"IN"	,
                "TK"	,	"Thoothukudi"	,	"TN"	,	"IN"	,
                "TL"	,	"Tiruvallur"	,	"TN"	,	"IN"	,
                "TR"	,	"Tiruvarur"	,	    "TN"	,	"IN"	,
                "TV"	,	"Tiruvannamalai",	"TN"	,	"IN"	,
                "VE"	,	"Vellore"	,	    "TN"	,	"IN"	,
                "VL"	,	"Viluppuram"	,	"TN"	,	"IN"	,
                "VR"	,	"Virudhunagar"	,	"TN"	,	"IN"	,
                "DH"	,	"Dhalai"	,	    "TR"	,	"IN"	,
                "NT"	,	"North Tripura"	,	"TR"	,	"IN"	,
                "ST"	,	"South Tripura"	,	"TR"	,	"IN"	,
                "ST"	,	"Khowai"	,	    "TR"	,	"IN"	,
                "WT"	,	"West Tripura"	,	"TR"	,	"IN"	,
                "AL"	,	"Almora"	,	    "UK"	,	"IN"	,
                "BA"	,	"Bageshwar"	,	    "UK"	,	"IN"	,
                "CL"	,	"Chamoli"	,	    "UK"	,	"IN"	,
                "CP"	,	"Champawat"	,	    "UK"	,	"IN"	,
                "DD"	,	"Dehradun"	,	    "UK"	,	"IN"	,
                "HA"	,	"Haridwar"	,	    "UK"	,	"IN"	,
                "NA"	,	"Nainital"	,	    "UK"	,	"IN"	,
                "PG"	,	"Pauri Garhwal"	,	"UK"	,	"IN"	,
                "PI"	,	"Pithoragarh"	,	"UK"	,	"IN"	,
                "RP"	,	"Rudraprayag"	,	"UK"	,	"IN"	,
                "TG"	,	"Tehri Garhwal"	,	"UK"	,	"IN"	,
                "US"	,	"Udham Singh Nagar"	,	"UK"	,	"IN"	,
                "UT"	,	"Uttarkashi"	,	"UK"	,	"IN"	,
                "AG"	,	"Agra"	,	        "UP"	,	"IN"	,
                "AL"	,	"Aligarh"	,	    "UP"	,	"IN"	,
                "AH"	,	"Allahabad"	,	    "UP"	,	"IN"	,
                "AN"	,	"Ambedkar Nagar",	"UP"	,	"IN"	,
                "AU"	,	"Auraiya"	,	    "UP"	,	"IN"	,
                "AZ"	,	"Azamgarh"	,	    "UP"	,	"IN"	,
                "BG"	,	"Bagpat"	,	    "UP"	,	"IN"	,
                "BH"	,	"Bahraich"	,	    "UP"	,	"IN"	,
                "BL"	,	"Ballia"	,	    "UP"	,	"IN"	,
                "BP"	,	"Balrampur"	,	    "UP"	,	"IN"	,
                "BN"	,	"Banda"	,	        "UP"	,	"IN"	,
                "BB"	,	"Barabanki"	,	    "UP"	,	"IN"	,
                "BR"	,	"Bareilly"	,	    "UP"	,	"IN"	,
                "BS"	,	"Basti"	,	        "UP"	,	"IN"	,
                "BI"	,	"Bijnor"	,	    "UP"	,	"IN"	,
                "BD"	,	"Budaun"	,	    "UP"	,	"IN"	,
                "BU"	,	"Bulandshahr"	,	"UP"	,	"IN"	,
                "CD"	,	"Chandauli"	,	    "UP"	,	"IN"	,
                "CS"	,	"Chhatrapati Shahuji Maharaj Nagar"	,	"UP"	,	"IN"	,
                "CT"	,	"Chitrakoot"	,	"UP"	,	"IN"	,
                "DE"	,	"Deoria"	,	    "UP"	,	"IN"	,
                "ET"	,	"Etah"	,	        "UP"	,	"IN"	,
                "EW"	,	"Etawah"	,	    "UP"	,	"IN"	,
                "FZ"	,	"Faizabad"	,	    "UP"	,	"IN"	,
                "FR"	,	"Farrukhabad"	,	"UP"	,	"IN"	,
                "FT"	,	"Fatehpur"	,	    "UP"	,	"IN"	,
                "FI"	,	"Firozabad"	,	    "UP"	,	"IN"	,
                "GB"	,	"Gautam Buddh Nagar"	,	"UP"	,	"IN"	,
                "GZ"	,	"Ghaziabad"	,	    "UP"	,	"IN"	,
                "GP"	,	"Ghazipur"	,	    "UP"	,	"IN"	,
                "GN"	,	"Gonda"	,	        "UP"	,	"IN"	,
                "GR"	,	"Gorakhpur"	,	    "UP"	,	"IN"	,
                "HM"	,	"Hamirpur"	,	    "UP"	,	"IN"	,
                "HR"	,	"Hardoi"	,	    "UP"	,	"IN"	,
                "HT"	,	"Hathras"	,	    "UP"	,	"IN"	,
                "JL"	,	"Jalaun"	,	    "UP"	,	"IN"	,
                "JU"	,	"Jaunpur district"	,	"UP"	,	"IN"	,
                "JH"	,	"Jhansi"	,	    "UP"	,	"IN"	,
                "JP"	,	"Jyotiba Phule Nagar"	,	"UP"	,	"IN"	,
                "KJ"	,	"Kannauj"	,	    "UP"	,	"IN"	,
                "KN"	,	"Kanpur"	,	    "UP"	,	"IN"	,
                "KR"	,	"Kanshi Ram Nagar"	,	"UP"	,	"IN"	,
                "KS"	,	"Kaushambi"	,	    "UP"	,	"IN"	,
                "KU"	,	"Kushinagar"	,	"UP"	,	"IN"	,
                "LK"	,	"Lakhimpur Kheri"	,	"UP"	,	"IN"	,
                "LA"	,	"Lalitpur"	,	    "UP"	,	"IN"	,
                "LU"	,	"Lucknow"	,	    "UP"	,	"IN"	,
                "MG"	,	"Maharajganj"	,	"UP"	,	"IN"	,
                "MH"	,	"Mahoba"	,	    "UP"	,	"IN"	,
                "MP"	,	"Mainpuri"	,	    "UP"	,	"IN"	,
                "MT"	,	"Mathura"	,	    "UP"	,	"IN"	,
                "MB"	,	"Mau"	,	        "UP"	,	"IN"	,
                "ME"	,	"Meerut"	,	    "UP"	,	"IN"	,
                "MI"	,	"Mirzapur"	,	    "UP"	,	"IN"	,
                "MO"	,	"Moradabad"	,	    "UP"	,	"IN"	,
                "MU"	,	"Muzaffarnagar"	,	    "UP"	,	"IN"	,
                "PN"	,	"Panchsheel Nagar district (Hapur)"	,	"UP"	,	"IN"	,
                "PI"	,	"Pilibhit"	,	        "UP"	,	"IN"	,
                "PR"	,	"Pratapgarh"	,	    "UP"	,	"IN"	,
                "RB"	,	"Raebareli"	,	        "UP"	,	"IN"	,
                "KD"	,	"Ramabai Nagar (Kanpur Dehat)"	,	"UP"	,	"IN"	,
                "RA"	,	"Rampur"	,	        "UP"	,	"IN"	,
                "SA"	,	"Saharanpur"	,	    "UP"	,	"IN"	,
                "SK"	,	"Sant Kabir Nagar"	,	"UP"	,	"IN"	,
                "SR"	,	"Sant Ravidas Nagar",	"UP"	,	"IN"	,
                "SJ"	,	"Shahjahanpur"	,	    "UP"	,	"IN"	,
                "SH"	,	"Shamli"	,	        "UP"	,	"IN"	,
                "SV"	,	"Shravasti"	,	        "UP"	,	"IN"	,
                "SN"	,	"Siddharthnagar"	,	"UP"	,	"IN"	,
                "SI"	,	"Sitapur"	,	        "UP"	,	"IN"	,
                "SO"	,	"Sonbhadra"	,	        "UP"	,	"IN"	,
                "SU"	,	"Sultanpur"	,	        "UP"	,	"IN"	,
                "UN"	,	"Unnao"	,	            "UP"	,	"IN"	,
                "VA"	,	"Varanasi"	,	        "UP"	,	"IN"	,
                "BN"	,	"Bankura"	,	        "WB"	,	"IN"	,
                "BR"	,	"Bardhaman"	,	        "WB"	,	"IN"	,
                "BI"	,	"Birbhum"	,	        "WB"	,	"IN"	,
                "KB"	,	"Cooch Behar"	,	    "WB"	,	"IN"	,
                "DD"	,	"Dakshin Dinajpur"	,	"WB"	,	"IN"	,
                "DA"	,	"Darjeeling"	,	    "WB"	,	"IN"	,
                "HG"	,	"Hooghly"	,	        "WB"	,	"IN"	,
                "HR"	,	"Howrah"	,	        "WB"	,	"IN"	,
                "JA"	,	"Jalpaiguri"	,	    "WB"	,	"IN"	,
                "KO"	,	"Kolkata"	,	        "WB"	,	"IN"	,
                "MA"	,	"Maldah"	,	        "WB"	,	"IN"	,
                "MSD"	,	"Murshidabad"	,	    "WB"	,	"IN"	,
                "NA"	,	"Nadia"	,	            "WB"	,	"IN"	,
                "PN"	,	"North 24 Parganas"	,	"WB"	,	"IN"	,
                "PM"	,	"Paschim Medinipur"	,	"WB"	,	"IN"	,
                "PR"	,	"Purba Medinipur"	,	"WB"	,	"IN"	,
                "PU"	,	"Purulia"	,	        "WB"	,	"IN"	,
                "PS"	,	"South 24 Parganas"	,	"WB"	,	"IN"	,
                "UD"	,	"Uttar Dinajpur"	,	"WB"	,	"IN"
                };
        }
    }
}