using AddressSeparation.Attributes;
using AddressSeparation.OutputFormats;
using System.Text.RegularExpressions;

namespace AddressSeparation.UnitTests.Data.OutputFormats
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
