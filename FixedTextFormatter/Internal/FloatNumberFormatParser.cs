﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FixedTextFormatter.Internal
{
    public class FloatNumberFormatParser : INumberParser
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

            if (type == typeof(float))
            {
                float outVal = 0;
                if (float.TryParse(value.ToString(), out outVal))
                {
                    return outVal.ToString(format);
                }
                else
                {
                    return null;
                }

            }
            if (_nextParser != null)
                return _nextParser.GetParsedNumber(type, value, format);
            else
                return null;
        }
    }
}
