using AddressSeparation.Helper;
using AddressSeparation.UnitTests.Data.Manipulations;
using NUnit.Framework;
using System.Linq;
using System.Reflection;

namespace AddressSeparation.UnitTests.Helper
{
    internal class InputManipulationHelperUnitTests
    {
        #region Fields

        private Assembly _testAssembly;

        #endregion Fields

        #region Methods

        [SetUp]
        public void Init()
        {
            _testAssembly = Assembly.GetExecutingAssembly();
        }

        [TestCase]
        public void HelperMethods_GetMappings_ReturnsAtLeastOneOutputFormat()
        {
            // arrange & act
            var result = InputManipulationHelper.GetMappings(_testAssembly);
            var resultTypes = result
                .Select(mapper => mapper.Type)
                .ToList();

            // assert
            Assert.GreaterOrEqual(result.Count(), 1);
            Assert.Contains(typeof(TrimInputManipulation), resultTypes);
        }

        #endregion Methods
    }
}
