using AddressSeparation.Attributes;
using System;

namespace AddressSeparation.Options
{
    /// <summary>
    /// Optionset that won't throw a <see cref="MissingMemberException"/> if there are properties w/o <see cref="RegexGroupAttribute"/>'s.
    /// </summary>
    public class DontThrowProcessingOptions : IProcessingOptions
    {
        #region Properties

        /// <inheritdoc cref="IProcessingOptions.ThrowIfNoRegexGroupPropertyProvided"/>
        public bool ThrowIfNoRegexGroupPropertyProvided { get; set; } = false;

        #endregion Properties
    }
}
