using System;
using System.Collections.Generic;
using System.Linq;
using FixedTextFormatter.Contract;

namespace FixedTextFormatter
{
    public class OutputTextFormatter
    {
        private string _originalValue = String.Empty;
        private string _outputValue = String.Empty;
        private List<ICustomOutputFormatter> _formatters;

        /// <summary>
        /// Gets or sets the characters to trim.  This will remove 
        /// any unwanted characters from the output.  This executes
        /// prior to padding.
        /// </summary>
        /// <value>The characters to trim.</value>
        public string CharactersToTrim { get; set; }

        /// <summary>
        /// Gets or sets the padding character.  This will be used
        /// if text needs to be added to the output.  The default is a 
        /// space.
        /// </summary>
        /// <value>The padding character.</value>
        public char PaddingCharacter { get; set; }

        /// <summary>
        /// Gets or sets the trim style.  This will only be invoked
        /// if the text is too long.
        /// </summary>
        /// <value>The trim style.</value>
        public OutputTextTrimStyle TrimStyle { get; set; }

        /// <summary>
        /// Gets or sets the padding style.  This will only be invoked if the 
        /// text is too short.
        /// </summary>
        /// <value>The pad style.</value>
        public OutputTextPaddingStyle PadStyle { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [alpha numeric only] should be applied
        /// </summary>
        /// <value><c>true</c> if [alpha numeric only]; otherwise, <c>false</c>.</value>
        public OutputTextAlphaNumericStyle AlphaNumericStyle { get; set; }

        /// <summary>
        /// Gets or sets the length of the output.  Leave null to ignore length requirement.
        /// </summary>
        /// <value>The length of the output.</value>
        public int? OutputLength { get; set; }

        public OutputTextFormatter(int length, params ICustomOutputFormatter[] formatters) :
            this(length, OutputTextTrimStyle.DontTrim, OutputTextPaddingStyle.DontPad, formatters)
        {
        }

        public OutputTextFormatter(int length, OutputTextTrimStyle trimStyle, params ICustomOutputFormatter[] formatters) :
            this(length, trimStyle, OutputTextPaddingStyle.DontPad, formatters)
        {
        }

        public OutputTextFormatter(params ICustomOutputFormatter[] formatters) :
            this(null, OutputTextTrimStyle.DontTrim, OutputTextPaddingStyle.DontPad, formatters)
        {

        }

        public OutputTextFormatter(int? requiredLength, OutputTextTrimStyle trimStyle, OutputTextPaddingStyle paddingStyle, params ICustomOutputFormatter[] formatters) :
            this(requiredLength, trimStyle, paddingStyle, ' ', formatters)
        {
        }

        public OutputTextFormatter(int? requiredLength, OutputTextTrimStyle trimStyle, OutputTextPaddingStyle paddingStyle, char paddingCharacter, params ICustomOutputFormatter[] formatters)
        {
            this.PaddingCharacter = paddingCharacter;
            _formatters = formatters.ToList();
            this.PadStyle = paddingStyle;
            this.TrimStyle = trimStyle;
            this.OutputLength = requiredLength;
        }

        /// <summary>
        /// Gets the formatted ouptut.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public string GetFormattedOuptut(object value)
        {
            _originalValue = value == null ? String.Empty : value.ToString();
            _outputValue = _originalValue;

            ApplyEarlyCustomFormatter();
            ApplyRemoveCharacters();
            ApplyAlphaNumericOnly();

            ApplyTrimming();
            ApplyPadding();

            ApplyLateCustomFormatter();
            return _outputValue;
        }

        /// <summary>
        /// Applies the trimming to the value
        /// </summary>
        private void ApplyTrimming()
        {
            if (!this.OutputLength.HasValue || _outputValue == null || this.TrimStyle == OutputTextTrimStyle.DontTrim) return;

            int finLen = this.OutputLength.Value;

            if (_outputValue.Length <= finLen) return;

            string newValue = _outputValue;

            while (newValue.Length != finLen)
            {
                if (this.TrimStyle == OutputTextTrimStyle.TrimEnd)
                {
                    newValue = newValue.Remove(newValue.Length - 1);
                }
                if (this.TrimStyle == OutputTextTrimStyle.TrimStart)
                {
                    newValue = newValue.Remove(0, 1);
                }
            }
            _outputValue = newValue;
        }

        /// <summary>
        /// Applies the padding to the value
        /// </summary>
        private void ApplyPadding()
        {
            if (!this.OutputLength.HasValue || this.PadStyle == OutputTextPaddingStyle.DontPad) return;

            if (_outputValue == null)
                _outputValue = String.Empty;

            while (_outputValue.Length != this.OutputLength.Value)
            {
                if (this.PadStyle == OutputTextPaddingStyle.PadStart)
                {
                    _outputValue = this.PaddingCharacter.ToString() + _outputValue;
                }
                else
                {
                    _outputValue += this.PaddingCharacter.ToString();
                }
            }
        }

        private void ApplyAlphaNumericOnly()
        {
            if (_outputValue == null || this.AlphaNumericStyle == OutputTextAlphaNumericStyle.Ignore) return;

            string newValue = String.Empty;

            foreach (char c in _outputValue)
            {
                if (Char.IsLetterOrDigit(c) || (Char.IsWhiteSpace(c) && this.AlphaNumericStyle == OutputTextAlphaNumericStyle.LeaveSpaces))
                    newValue += c.ToString();
            }

            _outputValue = newValue;

        }

        private void ApplyRemoveCharacters()
        {
            if (_outputValue == null || this.CharactersToTrim == null) return;

            foreach (char c in this.CharactersToTrim)
            {
                _outputValue = _outputValue.Replace(c.ToString(), "");
            }
        }

        private void ApplyEarlyCustomFormatter()
        {
            var formatters = _formatters.Where(f => f.ApplyFirst);
            foreach (var f in formatters)
                _outputValue = f.ApplyFormat(_outputValue);

        }

        private void ApplyLateCustomFormatter()
        {
            var formatters = _formatters.Where(f => !f.ApplyFirst);
            foreach (var f in formatters)
                _outputValue = f.ApplyFormat(_outputValue);

        }

    }
}
