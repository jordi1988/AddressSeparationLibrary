using AddressSeparation.Attributes;
using AddressSeparation.OutputFormats;
using System.Text.RegularExpressions;

namespace AddressSeparation.UnitTests.Data.OutputFormats
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
