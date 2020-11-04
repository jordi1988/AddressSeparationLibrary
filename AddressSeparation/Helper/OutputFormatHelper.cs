using AddressSeparation.Extensions;
using AddressSeparation.Mapper;
using AddressSeparation.Options;
using AddressSeparation.OutputFormats;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace AddressSeparation.Helper
{
    public static class OutputFormatHelper
    {
        #region Methods

        /// <summary>
        /// Read out all pre-defined output formats.
        /// </summary>
        /// <returns>IEnumerable with all output formats found.</returns>
        public static IEnumerable<OutputFormatDescriptionMapper> GetOutputFormats()
        {
            return OutputFormatHelper.GetOutputFormats(null);
        }

        /// <summary>
        /// Read out all output formats in a given assembly.
        /// </summary>
        /// <param name="assembly">
        /// Assembly that will be searched for output formats.
        /// <para>Defaults to the Address Separation Library assembly, meaning only pre-defined output formats showing up.</para>
        /// </param>
        /// <returns>IEnumerable with all output formats found. Empty if none found.</returns>
        public static IEnumerable<OutputFormatDescriptionMapper> GetOutputFormats(Assembly assembly = null)
        {
            // default if no argument is passed
            if (assembly is null)
            {
                assembly = Assembly.GetExecutingAssembly();
            }

            // get all types of IOutputFormat classes
            var outputFormatClasses = assembly
                .GetTypes()
                .Where(type => typeof(IOutputFormat).IsAssignableFrom(type) && !type.IsInterface);

            // map them to OutputFormatMapper
            var outputFormatDescriptors = outputFormatClasses
                .Select(outputFormatType => new OutputFormatDescriptionMapper()
                {
                    DisplayName = GetAttributeValue(outputFormatType, typeof(DisplayNameAttribute)),
                    Description = GetAttributeValue(outputFormatType, typeof(DescriptionAttribute)),
                    Type = outputFormatType
                });

            return outputFormatDescriptors;
        }

        /// <summary>
        /// Find all pre-defined output formats that matches the given <paramref name="rawAddress"/>.
        /// </summary>
        /// <param name="rawAddress">Input to be checked.</param>
        /// <returns>IEnumerable with all <c>matching</c> output formats found. Empty if none found.</returns>
        public static IEnumerable<OutputFormatDescriptionMapper> FindMatchingOutputFormats(string rawAddress)
        {
            return OutputFormatHelper.FindMatchingOutputFormats(rawAddress, null);
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
        public static IEnumerable<OutputFormatDescriptionMapper> FindMatchingOutputFormats(string rawAddress, Assembly assembly = null)
        {
            var matchingOutputFormats = new List<OutputFormatDescriptionMapper>();

            // get all output formats based on assembly
            var allOutputFormatClasses = OutputFormatHelper
                .GetOutputFormats(assembly)
                .RemoveNullRegexes();

            // process each found output format class with the given rawAddress
            foreach (var outputFormat in allOutputFormatClasses)
            {
                // get and create processor instance
                Type processorType = typeof(AddressSeparationProcessor<>);
                Type genericProcessorType = processorType
                    .MakeGenericType(outputFormat.Type);
                var processorInstance = Activator
                    .CreateInstance(genericProcessorType, new DontThrowProcessingOptions(), null);

                // get and invoke method
                var processMethod = genericProcessorType
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
        /// Read the first and only constructor argument in a given class of a given attribute
        /// </summary>
        /// <param name="classType">Class type to look for <paramref name="attributeType"/>.</param>
        /// <param name="attributeType">Attribute type whose constructor value will be read.</param>
        /// <returns>Value of the constructed attribute in the class.</returns>
        private static string GetAttributeValue(Type classType, Type attributeType)
        {
            var output = String.Empty;

            // fetch all custom attributes of the given type
            var attributeData = classType
                .CustomAttributes
                .SingleOrDefault(attr => attr.AttributeType == attributeType);

            // get first and only argument (if there is one)
            if (attributeData != null)
            {
                var ctorFirstArgument = attributeData.ConstructorArguments?.FirstOrDefault();
                if (ctorFirstArgument != null)
                {
                    // set the output
                    output = ctorFirstArgument.Value.ToString();
                }
            }

            return output;
        }

        #endregion Methods
    }
}
