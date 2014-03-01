using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixedTextFormatter
{
    public enum OutputTextTrimStyle
    {
        DontTrim = 0,
        TrimStart = 1,
        TrimEnd = 2
    }
    public enum DateFormats
    {
        yyyyMMdd,
        yyyy,
        MMdd,
        MM_dd_yyyy,
        MM,
        dd
    }

    public enum OutputTextPaddingStyle
    {
        DontPad = 0,
        PadStart = 1,
        PadEnd = 2
    }

    public enum OutputTextAlphaNumericStyle
    {
        Ignore = 0,
        RemoveSpaces = 1,
        LeaveSpaces = 2
    }

}
