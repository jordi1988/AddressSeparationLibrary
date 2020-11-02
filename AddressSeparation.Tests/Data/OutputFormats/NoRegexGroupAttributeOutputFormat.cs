using AddressSeparation.OutputFormats;
using System.Text.RegularExpressions;

namespace AddressSeparation.UnitTests.Data.OutputFormats
{
    internal class NoRegexGroupAttributeOutputFormat : IOutputFormat
    {
        #region Properties

        public Regex MatchingRegex => new Regex("(.*)");

        #endregion Properties
    }
}
