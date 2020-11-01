using AddressSeparation.Manipulations.Output;
using AddressSeparation.Mapper;
using NUnit.Framework;

namespace AddressSeparation.UnitTests.Mapper
{
    internal class RegexGroupMapperUnitTests
    {
        [TestCase]
        public void EmptyMapper_HasRegexGroupAttribute_ReturnsFalse()
        {
            // arrange & act
            var mapper = new RegexGroupMapper(-1, null);

            // assert
            Assert.IsFalse(mapper.HasRegexGroupAttribute);
        }

        [TestCase]
        public void EmptyMapper_HasManipulationFunction_ReturnsFalse()
        {
            // arrange & act
            var mapper = new RegexGroupMapper(-1, null);

            // assert
            Assert.IsFalse(mapper.HasManipulateFunction);
        }

        [TestCase]
        public void Mapper_RegexGroupIndexGreaterEqualZero_ReturnsTrue()
        {
            // arrange & act
            var mapper = new RegexGroupMapper(1, null);

            // assert
            Assert.IsTrue(mapper.HasRegexGroupAttribute);
        }

        [TestCase]
        public void Mapper_TrimFunction_IsSameInstance()
        {
            // arrange
            var instance = new TrimOutputManipulation();
            
            // act
            var mapper = new RegexGroupMapper(1, instance);

            // assert
            Assert.IsTrue(mapper.HasManipulateFunction);
            Assert.IsInstanceOf<TrimOutputManipulation>(mapper.RegexManipulationInstance);
        }

    }
}
