namespace AddressSeparation.Models
{
    /// <summary>
    /// <see cref="BasicAddress"/> extended address format as a base class for classes implementing <see cref="IOutputFormat"/>.
    /// </summary>
    public class ZoneAddress : BasicAddress
    {
        #region Properties

        /// <summary>
        /// City.
        /// </summary>
        /// <remarks>e. g. <c>Essen</c></remarks>
        public virtual string City { get; set; }

        /// <summary>
        /// State.
        /// </summary>
        /// <remarks>e. g. <c>North Rhine-Westfalia</c></remarks>
        public virtual string State { get; set; }

        /// <summary>
        /// Postal code.
        /// </summary>
        /// <remarks>e. g. <c>45147</c></remarks>
        public virtual string PostalCode { get; set; }

        #endregion Properties
    }
}
