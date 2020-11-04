namespace AddressSeparation.Options
{
    /// <summary>
    /// Default processing options class. Will be used if no other options are provided.
    /// </summary>
    public class DefaultProcessingOptions : IProcessingOptions
    {
        #region Properties

        /// <inheritdoc cref="IProcessingOptions.ThrowIfNoRegexGroupPropertyProvided"/>
        public bool ThrowIfNoRegexGroupPropertyProvided { get; set; } = true;

        #endregion Properties
    }
}
