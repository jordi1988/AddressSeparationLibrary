using AddressSeparation.Attributes;
using AddressSeparation.Extensions;
using AddressSeparation.Factories;
using AddressSeparation.Mapper;
using AddressSeparation.Options;
using AddressSeparation.OutputFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AddressSeparation.Helper
{
    /// <summary>
    /// Class for fetching output format types of type <see cref="IOutputFormat"/>.
    /// </summary>
    public sealed class OutputFormatHelper : DescriptionHelperBase<IOutputFormat>
    {
        #region Methods

        /// <summary>
        /// Find all pre-defined output formats that matches the given <paramref name="rawAddress"/>.
        /// </summary>
        /// <param name="rawAddress">Input to be checked.</param>
        /// <returns>IEnumerable with all <c>matching</c> output formats found. Empty if none found.</returns>
        public static IEnumerable<DescriptionMapper> GetCompatibleOutputFormats(string rawAddress)
        {
            return OutputFormatHelper.GetCompatibleOutputFormats(rawAddress, null);
        }

        /// <summary>
        /// Find all output formats that matches the given <paramref name="rawAddress"/> in a given assembly.
        /// </summary>
        /// <param name="rawAddress">Input to be checked.</param>
        /// <param name="assembly">
        /// Assembly that will be searched for output formats.
        /// <para>Defaults to the Address Separation Library assembly, meaning only pre-defined output formats showing up.</para>
        /// </param>
        /// <returns>IEnumerable with all <c>matching</c> output formats found. Empty if none found.</returns>
        public static IEnumerable<DescriptionMapper> GetCompatibleOutputFormats(string rawAddress, Assembly assembly = null)
        {
            var matchingOutputFormats = new List<DescriptionMapper>();

            // get all output formats based on assembly
            var allOutputFormatClasses = OutputFormatHelper
                .GetMappings(assembly)
                .RemoveNullRegexes();

            // process each found output format class with the given rawAddress
            foreach (var outputFormat in allOutputFormatClasses)
            {
                // get and create processor instance
                var processorInstance = AddressSeparationProcessorFactory.CreateInstance(outputFormat.Type, new DontThrowProcessingOptions(), null);

                // get and invoke method
                var processMethod = processorInstance.GetType()
                    .GetMethod("Process", new[] { typeof(string) });
                var methodResultInstance = processMethod
                    .Invoke(processorInstance, new object[] { rawAddress });

                // fetch and assert result
                bool isAddressResolved = (bool)methodResultInstance.GetType()
                    .GetProperty("AddressHasBeenResolved")?
                    .GetValue(methodResultInstance);

                if (isAddressResolved)
                {
                    matchingOutputFormats.Add(outputFormat);
                }
            }

            return matchingOutputFormats;
        }

        /// <summary>
        /// Get all properties containing <see cref="RegexGroupAttribute"/>.
        /// </summary>
        /// <param name="outputFormatType">Class type containing <see cref="IOutputFormat"/>.</param>
        /// <returns>List of mapped properties and regex groups</returns>
        public static IEnumerable<PropertyRegexGroupMapper> GetPropertyRegexGroups(Type outputFormatType)
        {
            return outputFormatType
                .GetProperties()
                .Select(prop => new PropertyRegexGroupMapper(prop))
                .Where(x => x.HasRegexGroupAttribute == true);
        }

        #endregion Methods
    }
}
