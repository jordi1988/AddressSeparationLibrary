namespace AddressSeparation.OutputFormats
{
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
