using AddressSeparation.Manipulations;
using AddressSeparation.Options;
using System;
using System.Collections.Generic;

namespace AddressSeparation.Factories
{
    /// <summary>
    /// Factory class for creating an instance of <see cref="AddressSeparationProcessor{TOutputFormat}"/>.
    /// </summary>
    public static class AddressSeparationProcessorFactory
    {
        #region Methods

        /// <summary>
        /// Method for creating an instance of <see cref="AddressSeparationProcessor{TOutputFormat}"/>.
        /// </summary>
        /// <param name="genericType">Generic type to create instance with.</param>
        /// <returns>Instance of class <see cref="AddressSeparationProcessor{TOutputFormat}"/>.</returns>
        public static object CreateInstance(Type genericType)
        {
            return CreateInstance(genericType, null, null);
        }

        /// <summary>
        /// Method for creating an instance of <see cref="AddressSeparationProcessor{TOutputFormat}"/>.
        /// </summary>
        /// <param name="genericType">Generic type to create instance with.</param>
        /// <param name="options">Options to create instance with.</param>
        /// <returns>Instance of class <see cref="AddressSeparationProcessor{TOutputFormat}"/>.</returns>
        public static object CreateInstance(Type genericType, IProcessingOptions options)
        {
            return CreateInstance(genericType, options, null);
        }

        /// <summary>
        /// Method for creating an instance of <see cref="AddressSeparationProcessor{TOutputFormat}"/>.
        /// </summary>
        /// <param name="genericType">Generic type to create instance with.</param>
        /// <param name="inputManipulationQueue">Queue with manipulation options to create instance with.</param>
        /// <returns>Instance of class <see cref="AddressSeparationProcessor{TOutputFormat}"/>.</returns>
        public static object CreateInstance(Type genericType, Queue<IInputManipulation> inputManipulationQueue)
        {
            return CreateInstance(genericType, null, inputManipulationQueue);
        }

        /// <summary>
        /// Method for creating an instance of <see cref="AddressSeparationProcessor{TOutputFormat}"/>.
        /// </summary>
        /// <param name="genericType">Generic type to create instance with.</param>
        /// <param name="options">Options to create instance with.</param>
        /// <param name="inputManipulationQueue">Queue with manipulation options to create instance with.</param>
        /// <returns>Instance of class <see cref="AddressSeparationProcessor{TOutputFormat}"/>.</returns>
        public static object CreateInstance(Type genericType, IProcessingOptions options, Queue<IInputManipulation> inputManipulationQueue)
        {
            // get and create processor instance
            Type processorType = typeof(AddressSeparationProcessor<>);
            Type genericProcessorType = processorType
                .MakeGenericType(genericType);

            var processorInstance = Activator
                .CreateInstance(genericProcessorType, options, inputManipulationQueue);

            // return instance
            return processorInstance;
        }

        #endregion Methods
    }
}
