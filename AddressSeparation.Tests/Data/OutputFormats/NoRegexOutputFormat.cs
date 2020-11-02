using AddressSeparation.OutputFormats;
using System.Text.RegularExpressions;

namespace AddressSeparation.UnitTests.Data.OutputFormats
{
    internal class NoRegexOutputFormat : IOutputFormat
    {
        #region Properties

        public Regex MatchingRegex => null;

        #endregion Properties
    }
}
