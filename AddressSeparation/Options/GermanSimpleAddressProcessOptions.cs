using System.Text.RegularExpressions;

namespace AddressSeparation.Options
{
    /// <summary>
    /// Matches simple german addresses in the format "Streetname 123a"
    /// </summary>
    public class GermanSimpleAddressProcessOptions : IProcessOptions
    {
        #region Properties

        /// <inheritdoc cref="IProcessOptions.MatchingRegex" />
        public Regex MatchingRegex => new Regex(
            @"^([\D\s,\.-]+?)(?=[\s\.\n])",
            // @"^([a-zäöüß\s,\.-]+?)[\s]+(\d{0,4})(?:\s?[-| +]\s?\d+)?\s*([a - z])?",
            RegexOptions.IgnoreCase);

        /// <summary>
        /// Currently has no effect.
        /// </summary>
        public bool ParallelProcessing { get; set; } = false;

        #endregion Properties
    }
}

// https://regex101.com/r/vflwhy/1

// ^\b[\w\s,-]+[\.\s*?]?\d*?\s*?\D*?
// ^([ -a-zßäüö\.]+)(:?\s{1,2}(\d+(\s{0,3}?\D$)?))$

// [a-zäöüß\s\d,.-]+?[\d\s]+(?:\s?[-| +/]\s?\d+)?\s*[a - z]?)?

// Wort bis zu: \.\s\d

// no berliner straße:
// ^([a-zäöüß\s,\.-]+?)[\s]+(\d{0,4})(?:\s?[-| +]\s?\d+)?\s*([a - z])?

// ^([a-zßöäü\.-]+)(?=[\s\d])
// ^\b.+?(?=[\. \d\Z])\.?\s

// Group1 fertig: ^([a-zöäüß\s,\.-]+?)(?=\s?[\d\n])
// Group2 fertig: ^([a-zöäüß\s,\.-]+?)(?=\s?[\d\n])\s+?(\d*)
