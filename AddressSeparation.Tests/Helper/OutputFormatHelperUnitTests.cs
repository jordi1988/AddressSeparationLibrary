using AddressSeparation.Helper;
using AddressSeparation.UnitTests.Data.OutputFormats;
using NUnit.Framework;
using System.Linq;
using System.Reflection;

namespace AddressSeparation.UnitTests.Helper
{
    internal class OutputFormatHelperUnitTests
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
        public void HelperMethods_GetOutputFormats_ReturnsAtLeastOneOutputFormat()
        {
            // arrange & act
            var result = OutputFormatHelper.GetOutputFormats(_testAssembly);

            // assert
            Assert.GreaterOrEqual(result.Count(), 4);
        }

        [TestCase]
        public void HelperMethods_FindMatchingOutputFormats_ReturnsAtLeastOneOutputFormat()
        {
            // arrange & act
            var result = OutputFormatHelper.FindMatchingOutputFormats("Teststrasse 123", _testAssembly);
            var resultTypes = result
                .Select(mapper => mapper.Type)
                .ToList();

            // assert
            Assert.GreaterOrEqual(result.Count(), 3);
            Assert.Contains(typeof(InputSameAsOutputOutputFormat), resultTypes);
            Assert.Contains(typeof(InputSameAsOutputTwoGroupsOutputFormat), resultTypes);
            Assert.Contains(typeof(NoRegexGroupAttributeOutputFormat), resultTypes);
        }

        #endregion Methods
    }
}
