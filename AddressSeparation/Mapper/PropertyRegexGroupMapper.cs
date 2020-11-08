using AddressSeparation.Attributes;
using AddressSeparation.Manipulations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace AddressSeparation.Mapper
{
    /// <summary>
    /// Class for mapping the property and the <see cref="RegexGroupAttribute"/>'s
    /// </summary>
    [DebuggerDisplay("{Property.Name}: {RegexGroupIndex} [{HasRegexGroupAttribute}]")]
    public class PropertyRegexGroupMapper
    {
        #region Properties

        /// <summary>
        /// Reflected information about the property which will be analyzed.
        /// </summary>
        public PropertyInfo Property { get; }

        /// <summary>
        /// Bound <see cref="RegexGroupAttribute"/>'s on a property in a readable way.
        /// </summary>
        public Queue<RegexGroupMapper> RegexGroupCollection { get; } = new Queue<RegexGroupMapper>();

        /// <summary>
        /// Determines if the given property has set the <see cref="RegexGroupAttribute"/>.
        /// </summary>
        public bool HasRegexGroupAttribute => this.RegexGroupCollection.Any();

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Maps the given property with the matching RegexGroup
        /// </summary>
        /// <param name="property">Reflected information about the property which will be analyzed.</param>
        public PropertyRegexGroupMapper(PropertyInfo property)
        {
            this.Property = property;

            // sanity check
            if (this.Property == null) {
                return;
            }

            // get attribute data, if exists
            IEnumerable<CustomAttributeData> regexGroupAttributesData = property
                .CustomAttributes
                .Where(x => x.AttributeType == typeof(RegexGroupAttribute));

            // if one ore more attributes were found per property, set instance members
            foreach (var regexGroup in regexGroupAttributesData)
            {
                // get value of first ctor argument (group index)
                var ctorArguments = regexGroup.ConstructorArguments;
                int indexArgument = (int)ctorArguments
                    .FirstOrDefault()
                    .Value;

                // if available, get second argument (manipulation method)
                object manipulationInstance = null;
                if (ctorArguments.Count > 1)
                {
                    // get type and underlying interface
                    var manipulationType = (Type)ctorArguments[1].Value;
                    var manipulationInterfaceName = manipulationType.GetInterfaces().FirstOrDefault()?.Name;

                    // is type of IManipulation interface? Then remember its method
                    if (manipulationInterfaceName == typeof(IOutputManipulation<>).Name &&
                        manipulationType.IsInterface == false)
                    {
                        manipulationInstance = Activator.CreateInstance(manipulationType);
                    }
                }

                // enqueue match
                var group = new RegexGroupMapper(indexArgument, manipulationInstance);
                this.RegexGroupCollection.Enqueue(group);
            }
        }

        #endregion Constructors
    }
}
