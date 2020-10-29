using System;

namespace AddressSeparation.Manipulations
{
    public class TrimInputManipulation : IInputManipulation
    {
        #region Properties

        public Func<string, string> Invoke
            => (string raw) => raw?.Trim();

        #endregion Properties
    }
}
