using AddressSeparation.Mapper;
using AddressSeparation.OutputFormats;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AddressSeparation.Extensions
{
    /// <summary>
    /// Class for extending <see cref="IEnumerable{OutputFormatMapper}"/>.
    /// </summary>
    internal static class IEnumerableOfOutputFormatMapperExtensions
    {
        #region Methods

        /// <summary>
        /// Removes all types with RegExes with a value of null
        /// </summary>
        /// <param name="outputFormats"></param>
        /// <returns>Filtered IEnumerable without empty RegExes</returns>
        public static IEnumerable<OutputFormatDescriptionMapper> RemoveNullRegexes(this IEnumerable<OutputFormatDescriptionMapper> outputFormats)
        {
            // filter only types with non-null RegEx
            outputFormats = outputFormats
                .Where(format =>
                {
                    // create instance of class implementing IOutputFormat
                    var outputFormatInstance = Activator.CreateInstance(format.Type) as IOutputFormat;

                    // take it if it has a RegEx
                    if (outputFormatInstance != null && outputFormatInstance.MatchingRegex != null)
                    {
                        return true;
                    }

                    return false;
                });

            // return to calling function
            return outputFormats;
        }

        #endregion Methods
    }
}
