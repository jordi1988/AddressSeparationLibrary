using AddressSeparation.Attributes;
using AddressSeparation.Manipulations;

namespace AddressSeparation.OutputFormats
{
    public class GermanSimpleAddressFormat : BasicAddress
    {
        #region Properties

        [RegexGroup(1)]
        public override string StreetName { get => base.StreetName; set => base.StreetName = value; }

        [RegexGroup(2)]
        public override short? HouseNumber { get => base.HouseNumber; set => base.HouseNumber = value; }

        /// <summary>
        /// Affix of the house number
        /// </summary>
        /// <remarks>e. g. <c>A</c></remarks>
        [RegexGroup(3, typeof(ToUpperOutputManipulation))]
        public string HouseNumberAffix { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Format of a default German address
        /// </summary>
        public override string ToString()
        {
            return $"{StreetName} {HouseNumber}{HouseNumberAffix}";
        }

        #endregion Methods
    }
}
