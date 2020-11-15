using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace AddressSeparation.Manipulations.Input
{
    /// <summary>
    /// Input manipulation class for removing parenthesis and everything inside it.
    /// </summary>
    [Description("Removes parenthesis and its content inside")]
    public class RemoveParenthesisInputManipulation : IInputManipulation
    {
        #region Properties

        /// <summary>
        /// Removes parenthesis and its content inside.
        /// </summary>
        public Func<string, string> Invoke =>
            (string raw) => Regex
                .Replace(raw, @"\(.*?\)", "")?
                .Replace("(", "")
                .Replace(")", "");

        #endregion Properties
    }
}
