using System;

namespace AddressSeparation.Manipulations.Input
{
    /// <summary>
    /// Input manipulation class for trimming the beginning and end of the input.
    /// </summary>
    public class TrimInputManipulation : IInputManipulation
    {
        #region Properties

        /// <summary>
        /// Trims the input at the beginning and end.
        /// </summary>
        public Func<string, string> Invoke
            => (string raw) => raw?.Trim();

        #endregion Properties
    }
}
