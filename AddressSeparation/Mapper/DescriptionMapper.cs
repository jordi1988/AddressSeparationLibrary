using System;

namespace AddressSeparation.Mapper
{
    /// <summary>
    /// Describes an output format.
    /// </summary>
    public class DescriptionMapper
    {
        #region Properties

        /// <summary>
        /// Short name of the output format for display.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Short description of the output format.
        /// </summary>
        /// <remarks>Should contain the format as well.</remarks>
        public string Description { get; set; }

        /// <summary>
        /// Reflected type of the output format.
        /// </summary>
        public Type Type { get; set; }

        #endregion Properties
    }
}
