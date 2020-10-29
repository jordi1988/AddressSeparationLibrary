using AddressSeparation.Attributes;
using AddressSeparation.Manipulations;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace AddressSeparation.Helper
{
    /// <summary>
    /// Class for mapping the property and the RegexGroup
    /// </summary>
    [DebuggerDisplay("{Property.Name}: {RegexGroupIndex} [{HasRegexGroupAttribute}]")]
    public class PropertyRegexGroupMapper
    {
        #region Properties

        public PropertyInfo Property { get; }
        public int RegexGroupIndex { get; } = -1;
        public object RegexManipulationInstance { get; }

        public bool HasRegexGroupAttribute => this.RegexGroupIndex > -1;
        public bool HasManipulateFunction => this.RegexManipulationInstance != null;

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Maps the given property with the matching RegexGroup
        /// </summary>
        /// <param name="property"></param>
        public PropertyRegexGroupMapper(PropertyInfo property)
        {
            this.Property = property;

            // get attribute data, if exists
            CustomAttributeData regexGroupAttributeData = property
                .CustomAttributes
                .SingleOrDefault(x => x.AttributeType == typeof(RegexGroupAttribute));

            // if attribute was found, set instance members
            if (regexGroupAttributeData != null)
            {
                var ctorArguments = regexGroupAttributeData.ConstructorArguments;

                // get value of first ctor argument (group index)
                this.RegexGroupIndex = (int)ctorArguments
                    .FirstOrDefault()
                    .Value;

                // if available, get second argument (manipulation method)
                if (ctorArguments.Count > 1)
                {
                    // get type and underlying interface
                    var manipulationType = (Type)ctorArguments[1].Value;
                    var manipulationInterfaceName = manipulationType.GetInterfaces().FirstOrDefault()?.Name;

                    // is type of IManipulation interface? Then remember its method
                    if (manipulationInterfaceName == typeof(IOutputManipulation<>).Name &&
                        manipulationType.IsInterface == false)
                    {
                        this.RegexManipulationInstance = Activator.CreateInstance(manipulationType);
                    }
                }
            }
        }

        #endregion Constructors
    }
}
