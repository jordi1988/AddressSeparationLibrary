namespace AddressSeparation.Manipulations.Output
{
    /// <summary>
    /// Transforms the found value to uppercase.
    /// </summary>
    public class ToUpperOutputManipulation : IOutputManipulation<string>
    {
        #region Methods

        /// <summary>
        /// Securely transform a string to uppercase letters.
        /// </summary>
        /// <param name="value">Value of group to manipulate.</param>
        public string Invoke(string value)
        {
            return value?.ToUpper();
        }

        #endregion Methods
    }
}
