using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Web;

namespace UpayaWebApp
{
    public class FormatHelper
    {                           //   1      2      3      4      5      6      7     8      9      10      11     12
        static string[] months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

        public static string FormatDate(byte? day, byte? month, Int16 year)
        {
            if(day.HasValue && month.HasValue)
            {
                int mindex = month.Value - 1;
                if (mindex >= 0 && mindex < 12)
                {
                    return string.Format("{0}/{1}/{2}", day, months[mindex], year);
                }
                return string.Format("{0}/{1}/{2}", year, day, month);
            }

            if(month.HasValue)
            {
                int mindex = month.Value - 1;
                if (mindex >= 0 && mindex < 12)
                {
                    return months[mindex] + "/" + year;
                }
            }

            return year.ToString();
        }

        public static string FormatBool(bool value)
        {
            if (value)
                return "Yes";
            else
                return "No";
        }

        public static string FormatBool(bool? value)
        {
            if(!value.HasValue)
                return "";

            if (value.Value)
                return "Yes";
            else
                return "No";
        }

        // OrigEntryDate helper functions
        public static DateTime? ExtractDate(NameValueCollection formData, string fieldName)
        {
            string value = formData[fieldName].ToString();
            if (string.IsNullOrWhiteSpace(value))
                return null;
            //DateTime dt = DateTime.ParseExact(value, "yyyy/MM/dd", CultureInfo.InvariantCulture);
            string[] parts = value.Split('/');
            if (parts.Length != 3)
                return null;
            DateTime dt = new DateTime(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]));
            return dt;
        }

        public static string FormatOrigDate(DateTime? dt)
        {
            if (dt.HasValue)
                return string.Format("{2}/{1}/{0}", dt.Value.Day, dt.Value.Month, dt.Value.Year);
            return "";
        }
        // GUID

        public static string FormatAsGuid(long value)
        {
            string left = "" + value;
            while (left.Length < 12)
                left = "0" + left;
            string res = "{00000000-0000-0000-0000-" + left + "}";
            return res;
        }
    }
}