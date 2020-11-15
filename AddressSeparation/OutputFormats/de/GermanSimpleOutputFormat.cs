using AddressSeparation.Attributes;
using AddressSeparation.Manipulations.Output;
using AddressSeparation.Models;
using AddressSeparation.Options;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace AddressSeparation.OutputFormats.de
{
    /// <summary>
    /// Matches simple german addresses without a zone in the format "Streetname 123a"
    /// </summary>
    [DisplayName("German, simple")]
    [Description("Matches german addresses in format `Streetname 123a`")]
    public class GermanSimpleOutputFormat : BasicAddress, IOutputFormat
    {
        #region Properties

        /// <inheritdoc cref="IProcessingOptions.MatchingRegex" />
        /// <remarks>
        /// <para>https://regex101.com/r/vflwhy/4</para>
        /// <para>if a number exists: everything up to that number is the street name, beyond that number is the affix</para>
        /// <para>else: everything is the street name</para>
        /// </remarks>
        public Regex MatchingRegex => new Regex(
            @"^(?(?=.*\d)((\D+))\s?(\d+)\s*([a-zA-Z]{0,2})[\D\d]*|(.*))$",
            RegexOptions.IgnoreCase
        );

        /// <summary>
        /// Matches group 1 (or 4 if 1 is empty) for the street name.
        /// </summary>
        [RegexGroup(1, typeof(TrimOutputManipulation))]
        [RegexGroup(4)]
        public override string StreetName { get => base.StreetName; set => base.StreetName = value; }

        /// <summary>
        /// Matches group 2 for the house number.
        /// </summary>
        [RegexGroup(2)]
        public override short? HouseNumber { get => base.HouseNumber; set => base.HouseNumber = value; }

        /// <summary>
        /// Matches group 3 for the affix of the house number.
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
