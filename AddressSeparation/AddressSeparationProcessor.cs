using AddressSeparation.Attributes;
using AddressSeparation.Manipulations;
using AddressSeparation.Mapper;
using AddressSeparation.Models;
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
    public class AddressSeparationProcessor<TOutputFormat> where TOutputFormat : class, IOutputFormat, new()
    {
        #region Properties

        /// <summary>
        /// Options the processor should consider.
        /// </summary>
        public IProcessingOptions Options { get; set; }

        /// <summary>
        /// Gets the queue containing user-defined functions for input manipulation.
        /// </summary>
        public Queue<IInputManipulation> InputManipulationQueue { get; private set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Creates a new instance of <see cref="AddressSeparationProcessor{TOutputFormat}"/>.
        /// </summary>
        public AddressSeparationProcessor() : this(null, null)
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="AddressSeparationProcessor{TOutputFormat}"/> with options.
        /// </summary>
        /// <param name="options">Options the processor should consider. <see cref="DefaultProcessingOptions"/> if none provided.</param>
        public AddressSeparationProcessor(IProcessingOptions options) : this(options, null)
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="AddressSeparationProcessor{TOutputFormat}" /> with user defined input manipulation functions.
        /// </summary>
        /// <param name="inputManipulationQueue">Queue with delegates that are called prior to RegEx matching.</param>
        public AddressSeparationProcessor(Queue<IInputManipulation> inputManipulationQueue) : this(null, inputManipulationQueue)
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="AddressSeparationProcessor{TOutputFormat}" /> with user defined input manipulation function.
        /// </summary>
        /// <param name="inputManipulation">Delegates that is called prior to RegEx matching.</param>
        public AddressSeparationProcessor(IInputManipulation inputManipulation) : this(null, null)
        {
            this.SetInputManipulation(inputManipulation);
        }

        /// <summary>
        /// Creates a new instance of <see cref="AddressSeparationProcessor{TOutputFormat}" /> with options and user defined input manipulation functions.
        /// </summary>
        /// <param name="options">Options the processor can work with. <see cref="DefaultProcessingOptions"/> if none provided.</param>
        /// <param name="inputManipulationQueue">Queue with delegates that are called prior to RegEx matching.</param>
        public AddressSeparationProcessor(IProcessingOptions options, Queue<IInputManipulation> inputManipulationQueue)
        {
            Options = options;
            InputManipulationQueue = inputManipulationQueue;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Processes <paramref name="rawAddressData"/> with the RegEx set up in <typeparamref name="TOutputFormat"/>.
        /// </summary>
        /// <remarks>The input and output manipulation functions will also be processed.</remarks>
        /// <param name="rawAddressData">The input string.</param>
        /// <exception cref="ArgumentNullException"><see cref="IOutputFormat.MatchingRegex"/> must be set up correctly in the generic class.</exception>
        /// <exception cref="MissingMemberException">Should contain at least one <see cref="RegexGroupAttribute"/>.</exception>
        /// <example>
        /// This sample shows how to call the <see cref="AddressSeparationProcessor{TOutputFormat}"/> constructor.
        /// <code>
        /// class TestClass
        /// {
        ///     static int Main()
        ///     {
        ///         var processor = new AddressSeparationProcessor{GermanSimpleOutputFormat}();
        ///         var result = processor.Process('Teststraße 123a');
        ///         var address = result.ResolvedAddress;
        ///
        ///         Console.WriteLine($"Name is {address.StreetName} with number {address.HouseNumber} and affix {address.HouseNumberAffix}");
        ///     }
        /// }
        /// </code>
        /// </example>
        /// <returns>The resolved address along with info about the processing.</returns>
        public OutputResult<TOutputFormat> Process(string rawAddressData)
        {
            // sanity check
            var outputFormatInstance = Activator.CreateInstance(typeof(TOutputFormat)) as IOutputFormat;
            _ = outputFormatInstance.MatchingRegex ?? throw new ArgumentNullException(nameof(outputFormatInstance.MatchingRegex));
            Options = Options ?? new DefaultProcessingOptions();

            // create output instance
            var outputResult = new OutputResult<TOutputFormat>(rawAddressData);

            // return empty result, if input is empty
            if (String.IsNullOrWhiteSpace(rawAddressData))
            {
                return outputResult;
            }

            // call user defined input functions
            rawAddressData = this.ProcessInputManipulationQueue(rawAddressData);

            // get all properties w/ RegexGroupAttribute and throw exception if there is none
            var propertyRegexGroupCollection = outputResult
                .GetInstance()
                .GetType()
                .GetProperties()
                .Select(prop => new PropertyRegexGroupMapper(prop))
                .Where(x => x.HasRegexGroupAttribute == true);

            if (Options.ThrowIfNoRegexGroupPropertyProvided &&
                propertyRegexGroupCollection.Any() == false)
            {
                throw new MissingMemberException($"Class {typeof(TOutputFormat).Name} has no property members with {nameof(RegexGroupAttribute)}.");
            }

            // assign to Regex bindings
            var match = outputFormatInstance.MatchingRegex.Match(rawAddressData);
            if (match.Success)
            {
                foreach (var prop in propertyRegexGroupCollection)
                {
                    // get first matching group value; null, if no group is matching
                    string valueOfGroup = null;
                    do
                    {
                        // get groups one by one and manipulate if necessary
                        var currentGroup = prop.RegexGroupCollection.Dequeue();
                        valueOfGroup = match.Groups[currentGroup.RegexGroupIndex]?.Value;
                        valueOfGroup = this.ProcessOutputManipulation(valueOfGroup, currentGroup);

                        // set value to instance member
                        this.SetPropertyValue(prop.Property, outputResult.GetInstance(), valueOfGroup);
                    } while (prop.RegexGroupCollection.Count > 0 && String.IsNullOrWhiteSpace(valueOfGroup));
                }

                // set success
                outputResult.AddressHasBeenResolved = true;
            }

            // return filled instance
            return outputResult;
        }

        /// <summary>
        /// Processes multiple <paramref name="rawAddressData"/> with the RegEx set up in <typeparamref name="TOutputFormat"/>.
        /// </summary>
        /// <remarks>The input and output manipulation functions will also be processed.</remarks>
        /// <param name="rawAddressesData">Array of raw input strings</param>
        /// <exception cref="ArgumentNullException"><see cref="IOutputFormat.MatchingRegex"/> must be set up correctly in the generic class.</exception>
        /// <exception cref="MissingMemberException">Should contain at least one <see cref="RegexGroupAttribute"/>.</exception>
        /// <example>
        /// This sample shows how to call the <see cref="AddressSeparationProcessor{TOutputFormat}"/> constructor.
        /// <code>
        /// class TestClass
        /// {
        ///     static int Main()
        ///     {
        ///         string[] inputAddresses = new string[] { "Teststraße 123a", "Teststr. 456" };
        ///
        ///         var processor = new AddressSeparationProcessor{GermanSimpleOutputFormat}();
        ///         var results = processor.Process(inputAddresses);
        ///
        ///         foreach (var result in results)
        ///         {
        ///             var address = result.ResolvedAddress;
        ///             Console.WriteLine($"Name is {address.StreetName} with number {address.HouseNumber} and affix {address.HouseNumberAffix}");
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        /// <returns>Collection of the resolved addresses along with info about the processing. Null if <paramref name="rawAddressesData"/> is null.</returns>
        public ICollection<OutputResult<TOutputFormat>> Process(string[] rawAddressesData)
        {
            // sanity check
            if (rawAddressesData == null)
            {
                return null;
            }

            // wrap the processing
            var outputCollection = new List<OutputResult<TOutputFormat>>();
            foreach (var rawAddress in rawAddressesData)
            {
                var singleResult = this.Process(rawAddress);
                outputCollection.Add(singleResult);
            }

            return outputCollection;
        }

        /// <summary>
        /// Sets a user defined function for processing prior to RegEx matching.
        /// </summary>
        /// <remarks>
        /// Appends an <see cref="IInputManipulation"/> delegate to the existing queue.
        /// <para>If queue does not exist, it will be created.</para>
        /// </remarks>
        /// <param name="inputManipulation">Delegate that ist called prior to RegEx matching.</param>
        public void SetInputManipulation(IInputManipulation inputManipulation)
        {
            if (this.InputManipulationQueue == null)
            {
                this.InputManipulationQueue = new Queue<IInputManipulation>();
            }

            this.InputManipulationQueue.Enqueue(inputManipulation);
        }

        /// <summary>
        /// Sets user defined functions in sequence for processing prior to RegEx matching.
        /// </summary>
        /// <param name="inputManipulation">Queue with delegates that are called prior to RegEx matching.</param>
        public void SetInputManipulation(Queue<IInputManipulation> inputManipulationQueue)
        {
            this.InputManipulationQueue = inputManipulationQueue;
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

        /// <summary>
        /// Call user defined output manipulation functions.
        /// </summary>
        /// <param name="value">Group value to be manipulated with given functions.</param>
        /// <param name="group">Group containing the manipulation functions.</param>
        /// <returns>Manipulated value</returns>
        private string ProcessOutputManipulation(string value, RegexGroupMapper group)
        {
            // manipulate value if possible
            if (group.HasManipulateFunction)
            {
                var manipulationInstance = group.RegexManipulationInstance;
                var manipulationMethod = manipulationInstance.GetType().GetMethod("Invoke");
                value = manipulationMethod.Invoke(manipulationInstance, new object[] { value }) as string;
            }

            return value;
        }

        /// <summary>
        /// Call user defined input manipulation functions.
        /// </summary>
        /// <param name="rawAddress">Raw address to be manipulated with given functions.</param>
        /// <returns>Manipulated address</returns>
        private string ProcessInputManipulationQueue(string rawAddress)
        {
            // complete every function in queue
            while (InputManipulationQueue != null && InputManipulationQueue.Count > 0)
            {
                var func = InputManipulationQueue.Dequeue();
                rawAddress = func.Invoke(rawAddress);
            }

            return rawAddress;
        }

        #endregion Methods
    }
}
