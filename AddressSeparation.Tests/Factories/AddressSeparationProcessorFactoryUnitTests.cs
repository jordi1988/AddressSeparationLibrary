using AddressSeparation.Factories;
using AddressSeparation.Manipulations;
using AddressSeparation.Options;
using AddressSeparation.UnitTests.Data.OutputFormats;
using NUnit.Framework;
using System.Collections.Generic;

namespace AddressSeparation.UnitTests.Factories
{
    internal class AddressSeparationProcessorFactoryUnitTests
    {
        #region Methods

        [TestCase]
        public void Factory_CreateInstance_ReturnsInstanceOfCorrectType()
        {
            // arrange
            var outputFormatType = typeof(InputSameAsOutputOutputFormat);

            // act
            var instance = AddressSeparationProcessorFactory.CreateInstance(outputFormatType)
                as AddressSeparationProcessor<InputSameAsOutputOutputFormat>;

            // assert
            Assert.IsNotNull(instance);
            Assert.IsInstanceOf(typeof(AddressSeparationProcessor<InputSameAsOutputOutputFormat>), instance);
        }

        [TestCase]
        public void Factory_CreateInstance_HasNoOptionsAndQueue()
        {
            // arrange
            var outputFormatType = typeof(InputSameAsOutputOutputFormat);

            // act
            var instance = AddressSeparationProcessorFactory.CreateInstance(outputFormatType)
                as AddressSeparationProcessor<InputSameAsOutputOutputFormat>;

            // assert
            Assert.IsNotNull(instance);
            Assert.IsNull(instance.Options);
            Assert.IsNull(instance.InputManipulationQueue);
        }

        [TestCase]
        public void Factory_CreateInstance_HasOptions()
        {
            // arrange
            var outputFormatType = typeof(InputSameAsOutputOutputFormat);
            var options = new DontThrowProcessingOptions();

            // act
            var instance = AddressSeparationProcessorFactory.CreateInstance(outputFormatType, options)
                as AddressSeparationProcessor<InputSameAsOutputOutputFormat>;

            // assert
            Assert.IsNotNull(instance);
            Assert.IsNotNull(instance.Options);
        }

        [TestCase]
        public void Factory_CreateInstance_HasInputManipulationQueue()
        {
            // arrange
            var outputFormatType = typeof(InputSameAsOutputOutputFormat);
            var queue = new Queue<IInputManipulation>();

            // act
            var instance = AddressSeparationProcessorFactory.CreateInstance(outputFormatType, queue)
                as AddressSeparationProcessor<InputSameAsOutputOutputFormat>;

            // assert
            Assert.IsNotNull(instance);
            Assert.IsNotNull(instance.InputManipulationQueue);
        }

        #endregion Methods
    }
}
