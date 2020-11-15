using AddressSeparation.OutputFormats.de;
using AddressSeparation.Manipulations.Input;
using AddressSeparation.Models;
using NUnit.Framework;

namespace AddressSeparation.UnitTests.OutputFormats
{
    internal class GermanSimpleAddressUnitTests
    {
        #region Fields

        private static object[] TestAddressesNoInputManipulation =
        {
            // Streets only
            new object[] { "Testmannstr.", "Testmannstr.", null, "" },
            new object[] { "Testmannstraße", "Testmannstraße", null, "" },
            new object[] { "Testmannstrasse", "Testmannstrasse", null, "" },
            new object[] { "Berliner-Straße", "Berliner-Straße", null, "" },
            new object[] { "Berliner Straße", "Berliner Straße", null, "" },

            // Normal ones
            new object[] { "Testmannstr. 11", "Testmannstr.", (short)11, "" },
            new object[] { "Testmannstraße 11", "Testmannstraße", (short)11, "" },
            new object[] { "Berliner Strasse 11", "Berliner Strasse", (short)11, "" },
            new object[] { "Berliner-Strasse 11", "Berliner-Strasse", (short)11, "" },

            // With affix
            new object[] { "Testmannstr. 11a", "Testmannstr.", (short)11, "A" },
            new object[] { "Testmannstr. 11 A", "Testmannstr.", (short)11, "A" },
            new object[] { "Testmannstraße 11a", "Testmannstraße", (short)11, "A" },
            new object[] { "Testmannstraße 11 A", "Testmannstraße", (short)11, "A" },
            new object[] { "Berliner Strasse 11a", "Berliner Strasse", (short)11, "A" },
            new object[] { "Berliner Strasse 11 A", "Berliner Strasse", (short)11, "A" },
            new object[] { "Berliner-Strasse 11a", "Berliner-Strasse", (short)11, "A" },
            new object[] { "Berliner-Strasse 11 A", "Berliner-Strasse", (short)11, "A" },
            new object[] { "Testmannstr. 11 AB", "Testmannstr.", (short)11, "AB" },

            // No whitespace
            new object[] { "Testmannstr.11a", "Testmannstr.", (short)11, "A" },
            new object[] { "Testmannstr.11 a", "Testmannstr.", (short)11, "A" },
            new object[] { "Testmannstr.11", "Testmannstr.", (short)11, "" },

            // House number range
            new object[] { "Testmannstr. 11-13", "Testmannstr.", (short)11, "" },
            new object[] { "Testmannstraße 11 - 13", "Testmannstraße", (short)11, "" },
            new object[] { "Berliner Strasse 11- 13", "Berliner Strasse", (short)11, "" },
            new object[] { "Berliner-Strasse 11 -13", "Berliner-Strasse", (short)11, "" },

            // Faulty affix
            new object[] { "Testmannstr. 11 (gegenüber Musterstr.)", "Testmannstr.", (short)11, "" },
            new object[] { "Testmannstr. 11 A (gegenüber Musterstr.)", "Testmannstr.", (short)11, "A" },
            new object[] { "Testmannstr. 11A (gegenüber Musterstr.)", "Testmannstr.", (short)11, "A" },
            new object[] { "Testmannstr.11A (gegenüber Musterstr.)", "Testmannstr.", (short)11, "A" },
            new object[] { "Testmannstr.11 a (gegenüber Musterstr.)", "Testmannstr.", (short)11, "A" },
            new object[] { "Testmannstraße11 a (gegenüber Musterstr.)", "Testmannstraße", (short)11, "A" },
        };

        private static object[] TestAddressesTrimManipulation =
        {
            new object[] { "    Testmannstraße 11", "Testmannstraße", (short)11, "" },
            new object[] { "   Testmannstraße 11    ", "Testmannstraße", (short)11, "" },
            new object[] { "Testmannstraße 11    ", "Testmannstraße", (short)11, "" },
            new object[] { "    Testmannstraße     11    ", "Testmannstraße", (short)11, "" },
        };


        private static object[] TestAddressesRemoveParenthesisManipulation =
        {
            new object[] { "Testmannstraße 11 ()", "Testmannstraße", (short)11, "" },
            new object[] { "Testmannstraße 11 (gegenüber Musterstr.)", "Testmannstraße", (short)11, "" },
            new object[] { ")Testmannstraße 11(", "Testmannstraße", (short)11, "" },
            new object[] { "())Testmannstraße 11))))(", "Testmannstraße", (short)11, "" },
            new object[] { "Testmannstraße (11)", "Testmannstraße ", null, "" },
        };

        private static object[] TestAddressesShortenManipulation =
        {
            // Streets only
            new object[] { "Testmannstr.", "Testmannstr.", null, "" },
            new object[] { "Testmannstrasse", "Testmannstr.", null, "" },
            new object[] { "Testmannstrasse", "Testmannstr.", null, "" },
            new object[] { "Berliner-Strasse", "Berliner-Str.", null, "" },
            new object[] { "Berliner Strasse", "Berliner Str.", null, "" },

            // Normal ones
            new object[] { "Testmannstr. 11", "Testmannstr.", (short)11, "" },
            new object[] { "Testmannstrasse 11", "Testmannstr.", (short)11, "" },
            new object[] { "Berliner Strasse 11", "Berliner Str.", (short)11, "" },
            new object[] { "Berliner-Strasse 11", "Berliner-Str.", (short)11, "" },

            // With affix
            new object[] { "Testmannstr. 11a", "Testmannstr.", (short)11, "A" },
            new object[] { "Testmannstr. 11 A", "Testmannstr.", (short)11, "A" },
            new object[] { "Testmannstrasse 11a", "Testmannstr.", (short)11, "A" },
            new object[] { "Testmannstrasse 11 A", "Testmannstr.", (short)11, "A" },
            new object[] { "Berliner Strasse 11a", "Berliner Str.", (short)11, "A" },
            new object[] { "Berliner Strasse 11 A", "Berliner Str.", (short)11, "A" },
            new object[] { "Berliner-Strasse 11a", "Berliner-Str.", (short)11, "A" },
            new object[] { "Berliner-Strasse 11 A", "Berliner-Str.", (short)11, "A" },

            // No whitespace
            new object[] { "Testmannstr.11a", "Testmannstr.", (short)11, "A" },
            new object[] { "Testmannstr.11 a", "Testmannstr.", (short)11, "A" },
            new object[] { "Testmannstr.11", "Testmannstr.", (short)11, "" },
        };

        private AddressSeparationProcessor<GermanSimpleOutputFormat> _processor;

        #endregion Fields

        #region Methods

        [SetUp]
        public void Setup()
        {
            // arrange
            this._processor = new AddressSeparationProcessor<GermanSimpleOutputFormat>();
        }

        [TestCaseSource("TestAddressesNoInputManipulation")]
        [SetCulture("de-DE")]
        public void NoManipulation_ReturnCorrectValues(string input, string streetName, short? houseNumber, string houseNumberAffix)
        {
            // act
            OutputResult<GermanSimpleOutputFormat> result = _processor.Process(input);
            GermanSimpleOutputFormat address = result.ResolvedAddress;

            // assert
            Assert.AreEqual(streetName, address.StreetName);
            Assert.AreEqual(houseNumber, address.HouseNumber);
            Assert.AreEqual(houseNumberAffix, address.HouseNumberAffix);
        }

        [TestCaseSource("TestAddressesTrimManipulation")]
        [SetCulture("de-DE")]
        public void TrimManipulation_ReturnCorrectValues(string input, string streetName, short? houseNumber, string houseNumberAffix)
        {
            // arrange
            _processor.SetInputManipulation(new TrimInputManipulation());

            // act
            OutputResult<GermanSimpleOutputFormat> result = _processor.Process(input);
            GermanSimpleOutputFormat address = result.ResolvedAddress;

            // assert
            Assert.AreEqual(streetName, address.StreetName);
            Assert.AreEqual(houseNumber, address.HouseNumber);
            Assert.AreEqual(houseNumberAffix, address.HouseNumberAffix);
        }

        [TestCaseSource("TestAddressesRemoveParenthesisManipulation")]
        [SetCulture("de-DE")]
        public void RemoveParenthesisManipulation_ReturnCorrectValues(string input, string streetName, short? houseNumber, string houseNumberAffix)
        {
            // arrange
            _processor.SetInputManipulation(new RemoveParenthesisInputManipulation());

            // act
            OutputResult<GermanSimpleOutputFormat> result = _processor.Process(input);
            GermanSimpleOutputFormat address = result.ResolvedAddress;

            // assert
            Assert.AreEqual(streetName, address.StreetName);
            Assert.AreEqual(houseNumber, address.HouseNumber);
            Assert.AreEqual(houseNumberAffix, address.HouseNumberAffix);
        }

        [TestCaseSource("TestAddressesShortenManipulation")]
        [SetCulture("de-DE")]
        public void ShortenManipulation_ReturnCorrectValues(string input, string streetName, short? houseNumber, string houseNumberAffix)
        {
            // arrange
            _processor.SetInputManipulation(new ShortenGermanStreetInputManipulation());

            // act
            OutputResult<GermanSimpleOutputFormat> result = _processor.Process(input);
            GermanSimpleOutputFormat address = result.ResolvedAddress;

            // assert
            Assert.AreEqual(streetName, address.StreetName);
            Assert.AreEqual(houseNumber, address.HouseNumber);
            Assert.AreEqual(houseNumberAffix, address.HouseNumberAffix);
        }

        #endregion Methods
    }
}
