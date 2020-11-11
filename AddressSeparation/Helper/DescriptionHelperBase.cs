using AddressSeparation.Mapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace AddressSeparation.Helper
{
    /// <summary>
    /// Abstract base class for writing description mappers of a given type
    /// </summary>
    /// <typeparam name="TDescription"></typeparam>
    public abstract class DescriptionHelperBase<TDescription> where TDescription : class
    {
        #region Methods

        /// <summary>
        /// Read out all pre-defined mappings.
        /// </summary>
        /// <returns>IEnumerable with all mappings found.</returns>
        public static IEnumerable<DescriptionMapper> GetMappings()
        {
            return DescriptionHelperBase<TDescription>.GetMappings(null);
        }

        /// <summary>
        /// Read out all mappings in a given assembly.
        /// </summary>
        /// <param name="assembly">
        /// Assembly that will be searched for mappings.
        /// <para>Defaults to the Address Separation Library assembly, meaning only pre-defined mappings showing up.</para>
        /// </param>
        /// <returns>IEnumerable with all mappings found. Empty if none found.</returns>
        public static IEnumerable<DescriptionMapper> GetMappings(Assembly assembly = null)
        {
            // default if no argument is passed
            if (assembly is null)
            {
                assembly = Assembly.GetExecutingAssembly();
            }

            // get all types of given generic type
            var allClassTypes = assembly
                .GetTypes()
                .Where(type => typeof(TDescription).IsAssignableFrom(type) && !type.IsInterface);

            // map them to DescriptionMapper
            var mappingDescriptors = allClassTypes
                .Select(type => new DescriptionMapper()
                {
                    DisplayName = GetAttributeValue(type, typeof(DisplayNameAttribute)) ?? type.Name,
                    Description = GetAttributeValue(type, typeof(DescriptionAttribute)),
                    Type = type
                });

            return mappingDescriptors;
        }

        /// <summary>
        /// Read the first and only constructor argument in a given class of a given attribute
        /// </summary>
        /// <param name="classType">Class type to look for <paramref name="attributeType"/>.</param>
        /// <param name="attributeType">Attribute type whose constructor value will be read.</param>
        /// <returns>Value of the constructed attribute in the class.</returns>
        protected static string GetAttributeValue(Type classType, Type attributeType)
        {
            string output = null;

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
