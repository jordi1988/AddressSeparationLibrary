using System;

namespace AddressSeparation.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class RegexGroupAttribute : Attribute
    {
        #region Properties

        public int GroupIndex { get; }
        public Type ManipulationType { get; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Binds the RegEx group to the address property.
        /// </summary>
        /// <param name="groupIndex">Index of the matching group RegEx in <see cref="IProcessOptions">IProcessOptions</see>.</param>
        /// <remarks>Group 0 matches whole expression. First RegEx group starts at 1.</remarks>
        public RegexGroupAttribute(int groupIndex)
        {
            this.GroupIndex = groupIndex;
        }

        /// <summary>
        /// Binds the RegEx group to the address property.
        /// </summary>
        /// <param name="groupIndex">Index of the matching group RegEx in <see cref="IProcessOptions">IProcessOptions</see>.</param>
        /// <remarks>First RegEx group starts at 1.</remarks>
        public RegexGroupAttribute(int groupIndex, Type outputManipulationType)
        {
            this.GroupIndex = groupIndex;
            this.ManipulationType = outputManipulationType;
        }

        #endregion Constructors
    }
}
