using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FixedTextFormatter.Contract
{
    public interface ICustomOutputFormatter
    {
        bool ApplyFirst { get; }

        string ApplyFormat(object value);
    }
}
