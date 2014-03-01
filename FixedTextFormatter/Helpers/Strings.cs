using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FixedTextFormatter.Internal;

namespace FixedTextFormatter.Helpers
{
    public static class Strings
    {
        public static bool Eq(string v1, string v2)
        {
            if (v1 == null && v2 == null) return true;
            if (v1 == null || v2 == null) return false;
            return v1.Equals(v2, StringComparison.OrdinalIgnoreCase);
        }
        public static string SafeString(object value)
        {
            if (value == null) return SafeString(value, String.Empty);
            else return value.ToString();
        }

        public static string SafeString(object value, string defaultValue)
        {
            if (value == null) return defaultValue;
            else return value.ToString();
        }

        public static string NumberToString(Type type, object value, string format, string defaultIfNull)
        {
            return
                new IntegerNumberFormatParser().SetParser(
                    new DoubleNumberFormatParser().SetParser(new FloatNumberFormatParser())).GetParsedNumber(type, value, format) ??
                defaultIfNull;
        }

        public static string NumberToString(Type type, object value, int numZerosInFormat)
        {
            string format = String.Empty;
            for (int i = 0; i < numZerosInFormat; i++)
            {
                format += "0";
            }
            return
                new IntegerNumberFormatParser().SetParser(
                    new DoubleNumberFormatParser().SetParser(new FloatNumberFormatParser())).GetParsedNumber(type, value, format) ??
                format;
        }

        public static string GetSpaces(int length)
        {
            return new string(' ', length);
        }

        // Extensions
        public static string Remove(this string value, string text)
        {
            return value.Replace(text, String.Empty);
        }

        public static string StripAlphas(this string value)
        {
            string newOutput = String.Empty;

            for (int i = 0; i < value.Length; i++)
            {
                char c = value[i];
                if (Char.IsNumber(c))
                {
                    newOutput += c.ToString();
                }
            }
            return newOutput;
        }

    }
}
