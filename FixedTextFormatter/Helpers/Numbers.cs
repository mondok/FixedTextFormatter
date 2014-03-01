using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FixedTextFormatter.Helpers
{
    public static class Numbers
    {
        public static int SafeInt(object value, int defaultValue)
        {
            int outval = 0;
            try
            {
                if (value == null) return 0;

                string strValue = value.ToString();

                if (!Int32.TryParse(strValue, out outval))
                    outval = defaultValue;
            }
            catch (Exception)
            {
                outval = defaultValue;
            }
            return outval;
        }

        public static int SafeInt(object value)
        {
            return SafeInt(value, 0);
        }

        public static float SafeFloat(object value, float defaultValue)
        {
            float outval = 0F;
            try
            {
                if (value == null) return 0;

                string strValue = value.ToString();

                if (!float.TryParse(strValue, out outval))
                    outval = defaultValue;
            }
            catch (Exception)
            {
                outval = defaultValue;
            }
            return outval;
        }

        public static float SafeFloat(object value)
        {
            return SafeFloat(value, 0F);
        }

        public static float SafeFloatRemoveAlphas(object value)
        {
            if (value == null) return 0F;
            else
            {
                return SafeFloat(TrimNonNums(value.ToString()), 0F);
            }
        }

        public static string TrimNonNums(string value)
        {
            string output = String.Empty;

            foreach (Char c in value)
            {
                if (Char.IsNumber(c) || Char.IsDigit(c) || c == '.')
                {
                    output += c.ToString();
                }
            }

            if (value.StartsWith("-"))
                output = "-" + output;

            return output;
        }

    }
}
