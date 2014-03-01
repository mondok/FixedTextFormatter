using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FixedTextFormatter.Internal
{
    public class IntegerNumberFormatParser : INumberParser
    {
        private INumberParser _nextParser;

        public INumberParser SetParser(INumberParser nextParser)
        {
            _nextParser = nextParser;
            return this;
        }

        public string GetParsedNumber(Type type, object value, string format)
        {
            if (value == null) return null;

            if (type == typeof(int))
            {
                int outVal = 0;
                if (Int32.TryParse(value.ToString(), out outVal))
                {
                    return outVal.ToString(format);
                }
                else
                {
                    return TryConvert(value, format);
                }

            }
            if (_nextParser != null)
                return _nextParser.GetParsedNumber(type, value, format);
            else
                return null;
        }

        private string TryConvert(object value, string format)
        {
            try
            {
                int outVal = Convert.ToInt32(value);
                return outVal.ToString(format);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
