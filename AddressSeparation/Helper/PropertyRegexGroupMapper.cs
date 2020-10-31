using AddressSeparation.Attributes;
using AddressSeparation.Manipulations;
using System;
using System.Collections.Generic;
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
        public Queue<RegexGroup> RegexGroupCollection { get; } = new Queue<RegexGroup>();
        public bool HasRegexGroupAttribute => this.RegexGroupCollection.Any();

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
                var group = new RegexGroup(indexArgument, manipulationInstance);
                this.RegexGroupCollection.Enqueue(group);
            }
        }

        #endregion Constructors
    }
}
