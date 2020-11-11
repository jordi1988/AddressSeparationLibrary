using System;
using System.ComponentModel;

namespace AddressSeparation.Manipulations.Input
{
    /// <summary>
    /// Input manipulation class for shortening German addresses.
    /// </summary>
    [Description("Extends a German street from `Str.` to `Straße`")]
    public class ExtendGermanStreetInputManipulation : IInputManipulation
    {
        #region Properties

        /// <summary>
        /// Extends a German `Straße` to `Str.`.
        /// </summary>
        public Func<string, string> Invoke =>
            (string raw) => raw?
                .Replace("Str.", "Straße")
                .Replace("str.", "straße");

        #endregion Properties
    }
}
