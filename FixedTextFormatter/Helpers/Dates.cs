using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FixedTextFormatter;

namespace FixedTextFormatter.Helpers
{
    /// <summary>
    /// Date helper classes
    /// </summary>
    public static class Dates
    {
        public static DateTime ParseDate(object value)
        {
            if (value == null) return DateTime.MinValue;

            DateTime outDate;
            if (!DateTime.TryParse(value.ToString(), out outDate))
                outDate = DateTime.MinValue;

            return outDate;
        }

        public static DateTime? ParseDateWithNull(object value)
        {
            if (value == null) return null;

            DateTime outDate;
            if (!DateTime.TryParse(value.ToString(), out outDate))
                return null;
            return outDate;
        }

        public static string ToDateString(DateTime value, DateFormats format)
        {
            return value.ToString(format.ToString().Replace("_", "/"));
        }

        public static string ToDateString(string value, DateFormats format)
        {
            DateTime myDate = ParseDate(value);

            return myDate.ToString(format.ToString().Replace("_", "/"));
        }

        public static string ToDateString(string value, string format, string defaultValueIfInvalid)
        {
            DateTime myDate = ParseDate(value);

            if (myDate == DateTime.MinValue) return defaultValueIfInvalid;

            return myDate.ToString(format.ToString().Replace("_", "/"));
        }

        public static string ToDateString(string value, DateFormats format, string defaultValueIfInvalid)
        {
            DateTime myDate = ParseDate(value);

            if (myDate == DateTime.MinValue) return defaultValueIfInvalid;

            return myDate.ToString(format.ToString().Replace("_", "/"));
        }

        public static string ToDateString(DateTime? value, DateFormats format, string defaultValueIfInvalid)
        {
            DateTime myDate = ParseDate(value);

            if (myDate == DateTime.MinValue) return defaultValueIfInvalid;

            return myDate.ToString(format.ToString().Replace("_", "/"));
        }

        public static string ToDateString(DateTime? value, string format, string defaultValueIfInvalid)
        {
            DateTime myDate = ParseDate(value);

            if (myDate == DateTime.MinValue) return defaultValueIfInvalid;

            return myDate.ToString(format);
        }
    }
}
