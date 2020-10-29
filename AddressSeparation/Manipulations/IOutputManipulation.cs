namespace AddressSeparation.Manipulations
{
    /// <summary>
    /// Used for manipulating the value of the found group before releasing the result.
    /// </summary>
    /// <typeparam name="TInOut">Should be the same as the defined property type.</typeparam>
    public interface IOutputManipulation<TInOut>
    {
        #region Methods

        /// <summary>
        /// User defined manipulation function is called upon the value of the found group and transforms <typeparamref name="TInOut"/>.
        /// </summary>
        TInOut Invoke(TInOut value);

        #endregion Methods
    }
}
