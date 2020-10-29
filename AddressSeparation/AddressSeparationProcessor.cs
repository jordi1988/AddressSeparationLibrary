using AddressSeparation.Attributes;
using AddressSeparation.Helper;
using AddressSeparation.Manipulations;
using AddressSeparation.Options;
using AddressSeparation.OutputFormats;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace AddressSeparation
{
    /// <summary>
    /// Processor for separating a raw string into multiple groups.
    /// </summary>
    /// <typeparam name="TOutputFormat"></typeparam>
    public class AddressSeparationProcessor<TOutputFormat> where TOutputFormat : class, new()
    {
        #region Fields

        private readonly IProcessOptions _options;
        private readonly Queue<IInputManipulation> _inputManipulationQueue;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Creates a new instance of <see cref="AddressSeparationProcessor{TOutputFormat}"/>.
        /// </summary>
        /// <param name="options">Options the processor can work with.</param>
        public AddressSeparationProcessor(IProcessOptions options) : this(options, null)
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="AddressSeparationProcessor{TOutputFormat}" /> with user definedc input manipulation functions.
        /// </summary>
        /// <param name="options">Options the processor can work with.</param>
        /// <param name="inputManipulationQueue">Delegate that is called prior to RegEx matching.</param>
        public AddressSeparationProcessor(IProcessOptions options, Queue<IInputManipulation> inputManipulationQueue)
        {
            _options = options;
            _inputManipulationQueue = inputManipulationQueue;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Processes multiple input strings with the given RegEx.
        /// </summary>
        /// <remarks>The input and output manipulation functions will also be processed.</remarks>
        /// <param name="rawAddressesData">Array of raw input addresses</param>
        /// <exception cref="ArgumentException">Should contain at least one RegexGroup.</exception>
        /// <returns>Collection of processed addresses. Null if rawAddressesData[] is null.</returns>
        public ICollection<OutputResult<TOutputFormat>> Process(string[] rawAddressesData)
        {
            // sanity check
            if (rawAddressesData == null)
            {
                return null;
            }

            // wrap the processing
            var outputCollection = new List<OutputResult<TOutputFormat>>();
            foreach (var rawAddress in rawAddressesData) // AsParallel if chosen
            {
                var singleResult = this.Process(rawAddress);
                outputCollection.Add(singleResult);
            }

            return outputCollection;
        }

        /// <summary>
        /// Processes the input string with the given RegEx.
        /// </summary>
        /// <remarks>The input and output manipulation functions will also be processed.</remarks>
        /// <param name="rawAddressData">The input string.</param>
        /// <exception cref="ArgumentException">Should contain at least one RegexGroup.</exception>
        /// <returns>The processed address.</returns>
        public OutputResult<TOutputFormat> Process(string rawAddressData)
        {
            // sanity check
            _ = _options ?? throw new ArgumentNullException(nameof(_options));
            _ = _options.MatchingRegex ?? throw new ArgumentNullException(nameof(_options.MatchingRegex));

            // create output instance
            var outputResult = new OutputResult<TOutputFormat>(rawAddressData);

            // return null, if input is empty
            if (string.IsNullOrWhiteSpace(rawAddressData))
            {
                return outputResult;
            }

            // call user defined input functions
            while (_inputManipulationQueue != null && _inputManipulationQueue.Count > 0)
            {
                var func = _inputManipulationQueue.Dequeue();
                rawAddressData = func.Invoke(rawAddressData);
            }

            // get all properties w/ RegexGroupAttribute and throw exception if there is none
            var propertyRegexGroupCollection = outputResult.ResolvedAddress.GetType().GetProperties()
                .Select(prop => new PropertyRegexGroupMapper(prop))
                .Where(x => x.HasRegexGroupAttribute == true);

            if (propertyRegexGroupCollection.Any() == false)
            {
                throw new ArgumentException($"Class {nameof(TOutputFormat)} has no property members with {nameof(RegexGroupAttribute)}.");
            }

            // assign to Regex bindings
            var match = _options.MatchingRegex.Match(rawAddressData);
            if (match.Success)
            {
                foreach (var prop in propertyRegexGroupCollection)
                {
                    // get value; null, if group is not found
                    var valueOfGroup = match.Groups[prop.RegexGroupIndex]?.Value;

                    // manipulate value if possible
                    if (prop.HasManipulateFunction)
                    {
                        var manipulationInstance = prop.RegexManipulationInstance;
                        var manipulationMethod = manipulationInstance.GetType().GetMethod("Invoke");
                        valueOfGroup = manipulationMethod.Invoke(manipulationInstance, new object[] { valueOfGroup }) as string;
                    }

                    // set value to instance member
                    this.SetPropertyValue(prop.Property, outputResult.ResolvedAddress, valueOfGroup);
                }

                // set success
                outputResult.AddressHasBeenResolved = true;
            }

            // return filled instance
            return outputResult;
        }

        /// <summary>
        /// Securely sets the property to an instance based on the underlying value.
        /// </summary>
        /// <param name="property">The property which will be set.</param>
        /// <param name="instance">Instance of the class containing the <paramref name="property"/>.</param>
        /// <param name="value">Value which will be set.</param>
        private void SetPropertyValue(PropertyInfo property, TOutputFormat instance, string value)
        {
            // sanity check
            if (property == null || instance == null)
            {
                return;
            }

            // way of setting value depends on type
            if (Nullable.GetUnderlyingType(property.PropertyType) == null)
            {
                // it's a non-nullable type
                // object convertedValue = Convert.ChangeType(valueOfGroup?.Value, prop.Property.PropertyType);
                property.SetValue(instance, value);
            }
            else
            {
                // it's a nullable type: must convert before
                var converter = TypeDescriptor.GetConverter(property.PropertyType);
                if (converter != null)
                {
                    var result = converter.ConvertFromString(value);
                    property.SetValue(instance, result);
                }
            }
        }

        #endregion Methods
    }
}
