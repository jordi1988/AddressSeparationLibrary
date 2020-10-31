using System;
using System.Collections.Generic;
using System.Text;

namespace AddressSeparation.Attributes
{
    public class RegexGroup
    {
        public int RegexGroupIndex { get; } = -1;
        public object RegexManipulationInstance { get; }

        public bool HasRegexGroupAttribute => this.RegexGroupIndex != -1;
        public bool HasManipulateFunction => this.RegexManipulationInstance != null;

        public RegexGroup(int groupIndex, object manipulationInstance)
        {
            this.RegexGroupIndex = groupIndex;
            this.RegexManipulationInstance = manipulationInstance;
        }
    }
}
