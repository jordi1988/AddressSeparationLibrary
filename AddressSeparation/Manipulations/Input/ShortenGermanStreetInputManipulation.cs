using System;

namespace AddressSeparation.Manipulations.Input
{
    /// <summary>
    /// Input manipulation class for shortening German addresses.
    /// </summary>
    public class ShortenGermanStreetInputManipulation : IInputManipulation
    {
        #region Properties

        /// <summary>
        /// Shortens a German `Straße` to `Str.`.
        /// </summary>
        public Func<string, string> Invoke =>
            (string raw) => raw?.Replace("Straße", "Str.")?.Replace("straße", "str.");

        #endregion Properties
    }
}
