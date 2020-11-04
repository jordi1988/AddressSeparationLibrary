using AddressSeparation.Attributes;
using AddressSeparation.OutputFormats;
using System;

namespace AddressSeparation.Options
{
    /// <summary>
    /// Interface for setting up the processor.
    /// </summary>
    public interface IProcessingOptions
    {
        #region Properties

        /// <summary>
        /// Throws a <see cref="MissingMemberException"/> if the <see cref="IOutputFormat"/> class has no <see cref="RegexGroupAttribute"/> defined.
        /// </summary>
        bool ThrowIfNoRegexGroupPropertyProvided { get; set; }

        #endregion Properties
    }
}
