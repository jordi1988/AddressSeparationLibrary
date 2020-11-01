using AddressSeparation.Cultures.de;
using AddressSeparation.Manipulations.Input;
using AddressSeparation.Models;
using NUnit.Framework;

namespace AddressSeparation.UnitTests.Cultures
{
    internal class GermanSimpleAddressUnitTests
    {
        #region Fields

        private static object[] TestAddressesNoInputManipulation =
        {
            // Streets only
            new object[] { "Vattmannstr.", "Vattmannstr.", null, "" },
            new object[] { "Vattmannstra�e", "Vattmannstra�e", null, "" },
            new object[] { "Vattmannstrasse", "Vattmannstrasse", null, "" },
            new object[] { "Berliner-Stra�e", "Berliner-Stra�e", null, "" },
            new object[] { "Berliner Stra�e", "Berliner Stra�e", null, "" },

            // Normal ones
            new object[] { "Vattmannstr. 11", "Vattmannstr.", (short)11, "" },
            new object[] { "Vattmannstra�e 11", "Vattmannstra�e", (short)11, "" },
            new object[] { "Berliner Strasse 11", "Berliner Strasse", (short)11, "" },
            new object[] { "Berliner-Strasse 11", "Berliner-Strasse", (short)11, "" },

            // With affix
            new object[] { "Vattmannstr. 11a", "Vattmannstr.", (short)11, "A" },
            new object[] { "Vattmannstr. 11 A", "Vattmannstr.", (short)11, "A" },
            new object[] { "Vattmannstra�e 11a", "Vattmannstra�e", (short)11, "A" },
            new object[] { "Vattmannstra�e 11 A", "Vattmannstra�e", (short)11, "A" },
            new object[] { "Berliner Strasse 11a", "Berliner Strasse", (short)11, "A" },
            new object[] { "Berliner Strasse 11 A", "Berliner Strasse", (short)11, "A" },
            new object[] { "Berliner-Strasse 11a", "Berliner-Strasse", (short)11, "A" },
            new object[] { "Berliner-Strasse 11 A", "Berliner-Strasse", (short)11, "A" },

            // No whitespace
            new object[] { "Vattmannstr.11a", "Vattmannstr.", (short)11, "A" },
            new object[] { "Vattmannstr.11 a", "Vattmannstr.", (short)11, "A" },
            new object[] { "Vattmannstr.11", "Vattmannstr.", (short)11, "" },
        };

        private static object[] TestAddressesTrimManipulation =
        {
            new object[] { "    Vattmannstra�e 11", "Vattmannstra�e", (short)11, "" },
            new object[] { "   Vattmannstra�e 11    ", "Vattmannstra�e", (short)11, "" },
            new object[] { "Vattmannstra�e 11    ", "Vattmannstra�e", (short)11, "" },
            new object[] { "    Vattmannstra�e     11    ", "Vattmannstra�e", (short)11, "" },
        };

        private static object[] TestAddressesShortenManipulation =
        {
            // Streets only
            new object[] { "Vattmannstr.", "Vattmannstr.", null, "" },
            new object[] { "Vattmannstra�e", "Vattmannstr.", null, "" },
            new object[] { "Vattmannstrasse", "Vattmannstr.", null, "" },
            new object[] { "Berliner-Stra�e", "Berliner-Str.", null, "" },
            new object[] { "Berliner Stra�e", "Berliner Str.", null, "" },

            // Normal ones
            new object[] { "Vattmannstr. 11", "Vattmannstr.", (short)11, "" },
            new object[] { "Vattmannstra�e 11", "Vattmannstr.", (short)11, "" },
            new object[] { "Berliner Strasse 11", "Berliner Str.", (short)11, "" },
            new object[] { "Berliner-Strasse 11", "Berliner-Str.", (short)11, "" },

            // With affix
            new object[] { "Vattmannstr. 11a", "Vattmannstr.", (short)11, "A" },
            new object[] { "Vattmannstr. 11 A", "Vattmannstr.", (short)11, "A" },
            new object[] { "Vattmannstra�e 11a", "Vattmannstr.", (short)11, "A" },
            new object[] { "Vattmannstra�e 11 A", "Vattmannstr.", (short)11, "A" },
            new object[] { "Berliner Strasse 11a", "Berliner Str.", (short)11, "A" },
            new object[] { "Berliner Strasse 11 A", "Berliner Str.", (short)11, "A" },
            new object[] { "Berliner-Strasse 11a", "Berliner-Str.", (short)11, "A" },
            new object[] { "Berliner-Strasse 11 A", "Berliner-Str.", (short)11, "A" },

            // No whitespace
            new object[] { "Vattmannstr.11a", "Vattmannstr.", (short)11, "A" },
            new object[] { "Vattmannstr.11 a", "Vattmannstr.", (short)11, "A" },
            new object[] { "Vattmannstr.11", "Vattmannstr.", (short)11, "" },
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
