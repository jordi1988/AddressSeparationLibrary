using AddressSeparation.Manipulations.Output;
using AddressSeparation.Mapper;
using AddressSeparation.UnitTests.Data.OutputFormats;
using NUnit.Framework;
using System.Reflection;

namespace AddressSeparation.UnitTests.Mapper
{
    internal class PropertyRegexGroupMapperUnitTests
    {
        [TestCase]
        public void Mapper_NoPropertyInfo_HasNoExceptionNoRegexGroupAttribute()
        {
            // arrange
            PropertyInfo obj = null;

            // act
            var mapper = new PropertyRegexGroupMapper(obj);

            // assert
            Assert.AreSame(obj, mapper.Property);
            Assert.IsFalse(mapper.HasRegexGroupAttribute);
        }

        [TestCase]
        public void Mapper_InputSameAsOutputProperty_HasOneRegexGroup()
        {
            // arrange
            var wholeAddressPropertyInfo = new InputSameAsOutputOutputFormat()
                .GetType()
                .GetProperty("WholeAddress");

            // act
            var mapper = new PropertyRegexGroupMapper(wholeAddressPropertyInfo);

            // assert
            Assert.AreSame(wholeAddressPropertyInfo, mapper.Property);
            Assert.IsTrue(mapper.HasRegexGroupAttribute);
            Assert.AreEqual(1, mapper.RegexGroupCollection.Count);
        }

        [TestCase]
        public void Mapper_InputSameAsOutputTwoRegexGroupsProperty_HasTwoRegexGroups()
        {
            // arrange
            var wholeAddressPropertyInfo = new InputSameAsOutputTwoGroupsOutputFormat()
                .GetType()
                .GetProperty("WholeAddress");

            var mapper = new PropertyRegexGroupMapper(wholeAddressPropertyInfo);

            // assert
            Assert.AreSame(wholeAddressPropertyInfo, mapper.Property);
            Assert.IsTrue(mapper.HasRegexGroupAttribute);
            Assert.AreEqual(2, mapper.RegexGroupCollection.Count);
        }

    }
}
