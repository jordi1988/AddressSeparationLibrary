namespace AddressSeparation.Manipulations
{
    /// <summary>
    /// Transforms the found value to lowercase.
    /// </summary>
    public class ToLowerOutputManipulation : IOutputManipulation<string>
    {
        #region Methods

        /// <summary>
        /// Securely transform a string to lowercase letters.
        /// </summary>
        /// <param name="value">Value of group to manipulate.</param>
        public string Invoke(string value)
        {
            return value?.ToLower();
        }

        #endregion Methods
    }
}
