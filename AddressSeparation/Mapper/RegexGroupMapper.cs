using AddressSeparation.Attributes;
using AddressSeparation.Manipulations;

namespace AddressSeparation.Mapper
{
    /// <summary>
    /// Class for mapping the group index and manipulation function out of a <see cref="RegexGroupAttribute"/>.
    /// </summary>
    public class RegexGroupMapper
    {
        #region Properties

        /// <summary>
        /// Index of the matching group RegEx.
        /// </summary>
        public int RegexGroupIndex { get; } = -1;

        /// <summary>
        /// Instance of the class implementing <see cref="IOutputManipulation{TInOut}"/>.
        /// </summary>
        public object RegexManipulationInstance { get; }

        /// <summary>
        /// Determines if the given property has set the <see cref="RegexGroupAttribute"/>.
        /// </summary>
        public bool HasRegexGroupAttribute => this.RegexGroupIndex != -1;

        /// <summary>
        /// Determines if the given property has also set up a class implementing <see cref="IOutputManipulation{TInOut}"/>.
        /// </summary>
        public bool HasManipulateFunction => this.RegexManipulationInstance != null;

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Creates a new instance of <see cref="RegexGroupMapper"/>.
        /// </summary>
        /// <param name="groupIndex">Index of the matching group RegEx.</param>
        /// <param name="manipulationInstance">Instance of the class implementing <see cref="IOutputManipulation{TInOut}"/>.</param>
        public RegexGroupMapper(int groupIndex, object manipulationInstance)
        {
            this.RegexGroupIndex = groupIndex;
            this.RegexManipulationInstance = manipulationInstance;
        }

        #endregion Constructors
    }
}
