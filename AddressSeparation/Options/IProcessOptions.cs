using System.Text.RegularExpressions;

namespace AddressSeparation.Options
{
    /// <summary>
    /// Interface must be implemented for setting up a new culture with a matching RegEx.
    /// </summary>
    public interface IProcessOptions
    {
        #region Properties

        /// <summary>
        /// Regular expression for matching the raw address and separating it into groups.
        /// </summary>
        Regex MatchingRegex { get; }

        /// <summary>
        /// Multithreaded processing when processing an array in <see cref="AddressSeparationProcessor{TOutputFormat}"/>.
        /// </summary>
        /// <remarks>Not implemented yet.</remarks>
        bool ParallelProcessing { get; set; }

        #endregion Properties
    }
}
