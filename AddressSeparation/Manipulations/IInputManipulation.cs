using System;

namespace AddressSeparation.Manipulations
{
    /// <summary>
    /// Used for manipulating the input value before any other processes take place.
    /// </summary>
    public interface IInputManipulation
    {
        #region Properties

        /// <summary>
        /// User defined manipulation delegate is called before any other processes take place.
        /// </summary>
        Func<string, string> Invoke { get; }

        #endregion Properties
    }
}
