using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Helper.Extension
{
    public static class DateTimeExtension
    {
        //in config set date in any format
        static string defaultFormat = "dd/MM/yyyy";
        public static string ConvertDateToString(this DateTime date)
        {
            return date.ToString(defaultFormat);
            //String.Format("{0:MM/dd/yyyy}", date);
            //return date.ToString("MM/dd/yyyy");
        }

        public static DateTime ParseFormatDateTime(this string s)
        {
            string otherformate = defaultFormat.IndexOf("/") > -1 ? defaultFormat.Replace("/", "-") : defaultFormat.Replace("-", "/");
            string[] formats = { defaultFormat, otherformate }; //Support date in 21-12-2015 format. 
            var culture = System.Globalization.CultureInfo.CurrentCulture;
            return DateTime.ParseExact(s, formats, culture, System.Globalization.DateTimeStyles.None);
        }

        public static DateTime GetDateFromDateCount(this int dateCount)
        {
            DateTime julianFixed = new DateTime(1990, 1, 1);
            return julianFixed.AddDays(dateCount);

        }

        public static int ConvetDatetoDateCounter(this DateTime time)
        {
            DateTime julianFixed = new DateTime(1990, 1, 1);

            // Difference in days, hours, and minutes.
            TimeSpan ts = time - julianFixed;
            // Difference in days.
            return ts.Days;

        }

        public static int GetMonthFromJulianFixed(this DateTime currentDate)
        {
            DateTime julianFixed = new DateTime(1990, 1, 1);
            int monthdiff = ((currentDate.Year - julianFixed.Year) * 12) + currentDate.Month - julianFixed.Month;

            // Difference in days, hours, and minutes.
            // Difference in days.
            return monthdiff;

        }
    }
}
