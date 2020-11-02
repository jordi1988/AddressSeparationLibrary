using System.Text.RegularExpressions;

namespace AddressSeparation.OutputFormats
{
    /// <summary>
    /// Interface must be implemented for <see cref="AddressSeparationProcessor{TOutputFormat}"/> when setting up a new output format.
    /// </summary>
    public interface IOutputFormat
    {
        #region Properties

        /// <summary>
        /// Regular expression for matching the raw address and separating it into groups.
        /// </summary>
        Regex MatchingRegex { get; }

        #endregion Properties
    }
}
