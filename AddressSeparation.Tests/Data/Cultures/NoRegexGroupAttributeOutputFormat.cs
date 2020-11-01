using AddressSeparation.Cultures;
using System.Text.RegularExpressions;

namespace AddressSeparation.UnitTests.Data.Cultures
{
    internal class NoRegexGroupAttributeOutputFormat : IOutputFormat
    {
        #region Properties

        public Regex MatchingRegex => new Regex("(.*)");

        #endregion Properties
    }
}
