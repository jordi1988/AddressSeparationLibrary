using AddressSeparation.OutputFormats;

namespace AddressSeparation.Models
{
    /// <summary>
    /// Simplest address format as a base class for classes implementing <see cref="IOutputFormat"/>.
    /// </summary>
    public class BasicAddress
    {
        #region Properties

        /// <summary>
        /// Street name.
        /// </summary>
        /// <remarks>e. g. <c>Wendenstraße</c></remarks>
        public virtual string StreetName { get; set; }

        /// <summary>
        /// Number of the house in a street. Null, if not found.
        /// </summary>
        /// <remarks>e. g. <c>216</c></remarks>
        public virtual short? HouseNumber { get; set; }

        #endregion Properties
    }
}
