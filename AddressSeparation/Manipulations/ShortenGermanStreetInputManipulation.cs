using System;

namespace AddressSeparation.Manipulations
{
    public class ShortenGermanStreetInputManipulation : IInputManipulation
    {
        #region Properties

        public Func<string, string> Invoke =>
            (string raw) => raw?.Replace("Straße", "Str.")?.Replace("straße", "str.");

        #endregion Properties
    }
}
