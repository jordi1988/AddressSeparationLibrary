using System.ComponentModel;
using System.Text.RegularExpressions;

namespace AddressSeparation.Options
{
    /// <summary>
    /// Matches simple german addresses in the format "Streetname 123a"
    /// </summary>
    [DisplayName("German, simple")]
    [Description("Matches simple german addresses in the format `Streetname 123a`")]
    public class GermanSimpleAddressProcessOptions : IProcessOptions
    {
        #region Properties

        /// <inheritdoc cref="IProcessOptions.MatchingRegex" />
        /// <remarks>
        /// <para>https://regex101.com/r/vflwhy/4</para>
        /// <para>if a number exists: everything up to that number is the street name, beyond that number is the affix</para>
        /// <para>else: everything is the street name</para>
        /// </remarks>
        public Regex MatchingRegex => new Regex(
            @"^(?(?=.*\d)((\D+))\s?(\d+)\s*(\D){0,2}|(.*))$",
            RegexOptions.IgnoreCase);

        /// <summary>
        /// Currently has no effect.
        /// </summary>
        public bool ParallelProcessing { get; set; } = false;

        #endregion Properties
    }
}
