using AddressSeparation.Cultures;
using System.Text.RegularExpressions;

namespace AddressSeparation.UnitTests.Data.Cultures
{
    internal class NoRegexOutputFormat : IOutputFormat
    {
        #region Properties

        public Regex MatchingRegex => null;

        #endregion Properties
    }
}
