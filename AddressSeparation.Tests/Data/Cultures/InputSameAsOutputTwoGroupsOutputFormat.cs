using AddressSeparation.Attributes;
using AddressSeparation.Cultures;
using System.Text.RegularExpressions;

namespace AddressSeparation.UnitTests.Data.Cultures
{
    internal class InputSameAsOutputTwoGroupsOutputFormat : IOutputFormat
    {
        #region Properties

        public Regex MatchingRegex => new Regex("((.*))");

        [RegexGroup(1)]
        [RegexGroup(2)]
        public string WholeAddress { get; set; }

        #endregion Properties
    }
}
