﻿using System;
using System.ComponentModel;

namespace AddressSeparation.Manipulations.Input
{
    /// <summary>
    /// Input manipulation class for shortening German addresses.
    /// </summary>
    [Description("Shortens a German street from `Straße` to `Str.`")]
    public class ShortenGermanStreetInputManipulation : IInputManipulation
    {
        #region Properties

        /// <summary>
        /// Shortens a German `Straße` to `Str.`.
        /// </summary>
        public Func<string, string> Invoke =>
            (string raw) => raw?
                .Replace("Straße", "Str.")?
                .Replace("straße", "str.")?
                .Replace("Strasse", "Str.")?
                .Replace("strasse", "str.");

        #endregion Properties
    }
}
