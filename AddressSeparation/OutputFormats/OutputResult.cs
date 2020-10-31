using System;
using System.Diagnostics;

namespace AddressSeparation.OutputFormats
{
    /// <summary>
    /// Output class of a proccessed address combining the resolved address and the final state.
    /// </summary>
    /// <typeparam name="TOutputFormat"></typeparam>
    [DebuggerDisplay("{RawAddress}: {AddressHasBeenResolved}")]
    public class OutputResult<TOutputFormat> where TOutputFormat : class, new()
    {
        #region Properties

        /// <summary>
        /// Input of the proccessing.
        /// </summary>
        public string RawAddress { get; }

        /// <summary>
        /// Output of the processing.
        /// </summary>
        public TOutputFormat ResolvedAddress { get; }

        /// <summary>
        /// State of the processing.
        /// </summary>
        public bool AddressHasBeenResolved { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Creates a new instance of the <typeparamref name="TOutputFormat"/>.
        /// </summary>
        /// <param name="rawAddress">Input to be processed.</param>
        public OutputResult(string rawAddress)
        {
            this.RawAddress = rawAddress;

            var instance = Activator.CreateInstance(typeof(TOutputFormat)) as TOutputFormat;
            this.ResolvedAddress = instance;
        }

        #endregion Constructors
    }
}
