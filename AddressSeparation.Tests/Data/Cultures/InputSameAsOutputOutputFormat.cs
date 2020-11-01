using AddressSeparation.Attributes;
using AddressSeparation.Cultures;
using System.Text.RegularExpressions;

namespace AddressSeparation.UnitTests.Data.Cultures
{
    internal class InputSameAsOutputOutputFormat : IOutputFormat
    {
        #region Properties

        public Regex MatchingRegex => new Regex("(.*)");

        [RegexGroup(1)]
        public string WholeAddress { get; set; }

        #endregion Properties
    }
}
