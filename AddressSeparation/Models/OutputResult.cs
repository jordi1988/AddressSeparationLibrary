using AddressSeparation.Cultures;
using System;
using System.Diagnostics;

namespace AddressSeparation.Models
{
    /// <summary>
    /// Output class of a proccessed address combining the resolved address and the final state.
    /// </summary>
    /// <typeparam name="TOutputFormat"></typeparam>
    [DebuggerDisplay("{RawAddress}: {AddressHasBeenResolved}")]
    public class OutputResult<TOutputFormat> where TOutputFormat : class, IOutputFormat, new()
    {
        #region Properties

        /// <summary>
        /// Input of the proccessing.
        /// </summary>
        public string RawAddress { get; }

        /// <summary>
        /// Output of the processing.
        /// </summary>
        public TOutputFormat ResolvedAddress => this._instance;

        /// <summary>
        /// State of the processing.
        /// </summary>
        public bool AddressHasBeenResolved { get; set; }

        /// <summary>
        /// Same as <see cref="ResolvedAddress"/>. This way, it's more user friendly.
        /// </summary>
        private TOutputFormat _instance { get; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Creates a new instance of the <typeparamref name="TOutputFormat"/>.
        /// </summary>
        /// <param name="rawAddress">Input to be processed.</param>
        public OutputResult(string rawAddress)
        {
            this.RawAddress = rawAddress;
            this._instance = Activator.CreateInstance(typeof(TOutputFormat)) as TOutputFormat;
        }

        /// <summary>
        /// Get the instance of the underlying type <see cref="TOutputFormat"/>.
        /// </summary>
        /// <returns></returns>
        public TOutputFormat GetInstance()
        {
            return _instance;
        }

        #endregion Constructors
    }
}
