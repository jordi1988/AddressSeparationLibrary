namespace AddressSeparation.OutputFormats
{
    public class ZoneAddress : BasicAddress
    {
        #region Properties

        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual string PostalCode { get; set; }

        #endregion Properties
    }
}
