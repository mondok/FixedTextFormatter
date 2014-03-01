using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FixedTextFormatter.Internal
{
    public interface INumberParser
    {
        INumberParser SetParser(INumberParser nextParser);

        string GetParsedNumber(Type type, object value, string format);
        
    }
}
