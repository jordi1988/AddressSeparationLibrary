using AddressSeparation.UnitTests.Data.OutputFormats;
using NUnit.Framework;
using System;

namespace AddressSeparation.UnitTests.Options
{
    internal class ThrowIfNoRegexGroupOptionsUnitTests
    {
        #region Methods

        [TestCase]
        public void Options_NoRegexSetUp_ThrowsArgumentNullException()
        {
            // arrange
            var processor = new AddressSeparationProcessor<NoRegexOutputFormat>();

            // act & assert
            Assert.Throws<ArgumentNullException>(() => processor.Process("Teststreet 123"));
        }

        [TestCase]
        public void Options_NoRegexGroupAttributesSetUp_ThrowsMissingMemberException()
        {
            // arrange
            var processor = new AddressSeparationProcessor<NoRegexGroupAttributeOutputFormat>();

            // act & assert
            Assert.Throws<MissingMemberException>(() => processor.Process("Teststreet 123"));
        }

        #endregion Methods
    }
}
