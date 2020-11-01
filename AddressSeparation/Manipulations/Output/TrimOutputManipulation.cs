namespace AddressSeparation.Manipulations.Output
{
    /// <summary>
    /// Trims the found value.
    /// </summary>
    public class TrimOutputManipulation : IOutputManipulation<string>
    {
        #region Methods

        /// <summary>
        /// Securely trims a string.
        /// </summary>
        /// <param name="value">Value of group to manipulate.</param>
        public string Invoke(string value)
        {
            return value?.Trim();
        }

        #endregion Methods
    }
}
