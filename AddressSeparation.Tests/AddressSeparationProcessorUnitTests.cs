using AddressSeparation.Manipulations;
using AddressSeparation.Manipulations.Input;
using AddressSeparation.Options;
using AddressSeparation.UnitTests.Data.OutputFormats;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AddressSeparation.UnitTests
{
    internal class AddressSeparationProcessorUnitTests
    {
        #region Methods

        [TestCase]
        public void NoRegexSetUp_ThrowsArgumentNullException()
        {
            // arrange
            var processor = new AddressSeparationProcessor<NoRegexOutputFormat>();

            // act & assert
            Assert.Throws<ArgumentNullException>(() => processor.Process("Teststreet 123"));
        }

        [TestCase]
        public void NoRegexGroupAttributesSetUp_ThrowsMissingMemberException()
        {
            // arrange
            var processor = new AddressSeparationProcessor<NoRegexGroupAttributeOutputFormat>();

            // act & assert
            Assert.Throws<MissingMemberException>(() => processor.Process("Teststreet 123"));
        }

        [TestCase]
        public void EmptyInput_IsSameAsOutputRegex_ReturnsNotResolvedOutputResult()
        {
            // arrange
            var input = String.Empty;
            var processor = new AddressSeparationProcessor<InputSameAsOutputOutputFormat>();

            // act
            var result = processor.Process(input);

            // assert
            Assert.IsFalse(result.AddressHasBeenResolved);
            Assert.IsEmpty(result.RawAddress);
            Assert.IsNull(result.ResolvedAddress.WholeAddress);
        }

        [TestCase]
        public void Input_IsSameAsOutputRegex_ReturnsCorrectResolvedOutputResult()
        {
            // arrange
            var input = "Teststraße 123";
            var processor = new AddressSeparationProcessor<InputSameAsOutputOutputFormat>();

            // act
            var result = processor.Process(input);

            // assert
            Assert.IsTrue(result.AddressHasBeenResolved);
            Assert.AreEqual(input, result.RawAddress);
            Assert.AreEqual(input, result.ResolvedAddress.WholeAddress);
        }

        [TestCase]
        public void ResolvedAddress_IsSameInstance_ReturnsTrue()
        {
            // arrange
            var input = "Teststraße 123";
            var processor = new AddressSeparationProcessor<InputSameAsOutputOutputFormat>();

            // act
            var result = processor.Process(input);

            // assert
            Assert.AreSame(result.ResolvedAddress, result.GetInstance());
        }

        [TestCase]
        public void ResolvedAddress_InstanceIsNotNull_ReturnsTrue()
        {
            // arrange
            var input = "Teststraße 123";
            var processor = new AddressSeparationProcessor<InputSameAsOutputOutputFormat>();

            // act
            var result = processor.Process(input);

            // assert
            Assert.IsNotNull(result.ResolvedAddress);
            Assert.IsNotNull(result.GetInstance());
        }

        [TestCase]
        public void EmptyConstructor_ProcessingNoOptions_ReturnsDefaultProcessingOptions()
        {
            // arrange
            var input = "    Teststraße 123     ";
            var processor = new AddressSeparationProcessor<InputSameAsOutputOutputFormat>(null, null);

            // act
            processor.SetInputManipulation(new TrimInputManipulation());
            var result = processor.Process(input);

            // assert
            Assert.IsInstanceOf(typeof(DefaultProcessingOptions), processor.Options,
                "Options should always be set at the beginning of Process().");
        }

        [TestCase]
        public void EmptyConstructor_SetInputManipulation_WillProcessFunction()
        {
            // arrange
            var input = "    Teststraße 123     ";
            var processor = new AddressSeparationProcessor<InputSameAsOutputOutputFormat>(null, null);

            // act
            processor.SetInputManipulation(new TrimInputManipulation());
            var result = processor.Process(input);

            // assert
            string expectedOutput = input.Trim();
            Assert.IsTrue(result.AddressHasBeenResolved);
            Assert.AreEqual(input, result.RawAddress);
            Assert.AreEqual(expectedOutput, result.ResolvedAddress.WholeAddress);
        }

        [TestCase]
        public void EmptyConstructor_SetInputManipulationQueue_WillProcessFunction()
        {
            // arrange
            var input = "    Teststraße 123     ";
            var processor = new AddressSeparationProcessor<InputSameAsOutputOutputFormat>(null, null);
            var queue = new Queue<IInputManipulation>();
            queue.Enqueue(new TrimInputManipulation());

            // act
            processor.SetInputManipulation(queue);
            var result = processor.Process(input);

            // assert
            string expectedOutput = input.Trim();
            Assert.IsTrue(result.AddressHasBeenResolved);
            Assert.AreEqual(input, result.RawAddress);
            Assert.AreEqual(expectedOutput, result.ResolvedAddress.WholeAddress);
        }

        [TestCase]
        public void ConstructorWithInputManipulationArgument_WillProcessFunction()
        {
            // arrange
            var input = "    Teststraße 123     ";
            var processor = new AddressSeparationProcessor<InputSameAsOutputOutputFormat>(new TrimInputManipulation());

            // act
            var result = processor.Process(input);

            // assert
            string expectedOutput = input.Trim();
            Assert.IsTrue(result.AddressHasBeenResolved);
            Assert.AreEqual(input, result.RawAddress);
            Assert.AreEqual(expectedOutput, result.ResolvedAddress.WholeAddress);
        }

        [TestCase]
        public void ConstructorWithInputManipulationQueueArgument_WillProcessFunction()
        {
            // arrange
            var input = "    Teststraße 123     ";
            var queue = new Queue<IInputManipulation>();
            queue.Enqueue(new TrimInputManipulation());
            var processor = new AddressSeparationProcessor<InputSameAsOutputOutputFormat>(queue);

            // act
            var result = processor.Process(input);

            // assert
            string expectedOutput = input.Trim();
            Assert.IsTrue(result.AddressHasBeenResolved);
            Assert.AreEqual(input, result.RawAddress);
            Assert.AreEqual(expectedOutput, result.ResolvedAddress.WholeAddress);
        }

        #endregion Methods
    }
}
