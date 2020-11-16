using AddressSeparation.Manipulations.Input;
using AddressSeparation.Options;
using AddressSeparation.UnitTests.Data.OutputFormats;
using NUnit.Framework;
using System;

namespace AddressSeparation.UnitTests.Options
{
    internal class RefillInputManipulationsQueueOptionsUnitTests
    {

        private AddressSeparationProcessor<InputSameAsOutputOutputFormat> _processor;

        #region Methods

        [SetUp]
        public void Setup()
        {
            // arrange
            this._processor = new AddressSeparationProcessor<InputSameAsOutputOutputFormat>(new DefaultProcessingOptions());
        }

        [TestCase]
        public void Options_NotInitializedQueue_ReturnsNull()
        {
            // arrange
            _processor.Options.RefillInputmanipulationsQueue = true;

            // act
            _ = _processor.Process("Teststreet 123");

            // assert
            Assert.IsNull(_processor.InputManipulationQueue);
        }

        [TestCase]
        public void Options_SingleFunctionNoProcessing_ReturnsSingleFunction()
        {
            // arrange
            _processor.Options.RefillInputmanipulationsQueue = true;
            _processor.SetInputManipulation(new TrimInputManipulation());

            // assert
            Assert.IsNotNull(_processor.InputManipulationQueue);
            Assert.AreEqual(1, _processor.InputManipulationQueue.Count);
            Assert.IsInstanceOf<TrimInputManipulation>(_processor.InputManipulationQueue.Peek());
        }

        [TestCase]
        public void Options_SingleFunctionProcessing_ReturnsSingleFunction()
        {
            // arrange
            _processor.Options.RefillInputmanipulationsQueue = true;
            _processor.SetInputManipulation(new TrimInputManipulation());

            // act
            _ = _processor.Process("Teststreet 123");

            // assert
            Assert.IsNotNull(_processor.InputManipulationQueue);
            Assert.AreEqual(1, _processor.InputManipulationQueue.Count);
            Assert.IsInstanceOf<TrimInputManipulation>(_processor.InputManipulationQueue.Peek());
        }

        [TestCase]
        public void Options_NoRefillQueueWithoutProcessing_ReturnsSingleFunction()
        {
            // arrange
            _processor.Options.RefillInputmanipulationsQueue = false;
            _processor.SetInputManipulation(new TrimInputManipulation());

            // assert
            Assert.IsNotNull(_processor.InputManipulationQueue);
            Assert.AreEqual(1, _processor.InputManipulationQueue.Count);
            Assert.IsInstanceOf<TrimInputManipulation>(_processor.InputManipulationQueue.Peek());
        }

        [TestCase]
        public void Options_NoRefillQueueWithProcessing_ReturnsEmptyQueue()
        {
            // arrange
            _processor.Options.RefillInputmanipulationsQueue = false;
            _processor.SetInputManipulation(new TrimInputManipulation());

            // act
            _ = _processor.Process("Teststreet 123");

            // assert
            Assert.IsNotNull(_processor.InputManipulationQueue);
            Assert.AreEqual(0, _processor.InputManipulationQueue.Count);
        }

        #endregion Methods
    }
}
