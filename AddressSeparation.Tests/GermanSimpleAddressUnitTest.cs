using AddressSeparation.Manipulations;
using AddressSeparation.Options;
using AddressSeparation.OutputFormats;
using NUnit.Framework;
using System.Collections.Generic;

namespace AddressSeparation.Tests
{
    public class GermanSimpleAddressUnitTest
    {
        #region Fields

        private static object[] CheckupAddresses =
        {
            // Streets only
            new object[] { "Vattmannstr.", "Vattmannstr.", null, "" },
            new object[] { "Vattmannstraﬂe", "Vattmannstraﬂe", null, "" },
            new object[] { "Vattmannstrasse", "Vattmannstrasse", null, "" },
            new object[] { "Berliner-Straﬂe", "Berliner-Straﬂe", null, "" },
            new object[] { "Berliner Straﬂe", "Berliner Straﬂe", null, "" },

            // Normal ones
            new object[] { "Vattmannstr. 11", "Vattmannstr. ", (short)11, "" },
            new object[] { "Vattmannstraﬂe 11", "Vattmannstraﬂe ", (short)11, "" },
            new object[] { "Berliner Strasse 11", "Berliner Strasse ", (short)11, "" },
            new object[] { "Berliner-Strasse 11", "Berliner-Strasse ", (short)11, "" },

            // With affix
            new object[] { "Vattmannstr. 11a", "Vattmannstr. ", (short)11, "A" },
            new object[] { "Vattmannstr. 11 A", "Vattmannstr. ", (short)11, "A" },
            new object[] { "Vattmannstraﬂe 11a", "Vattmannstraﬂe ", (short)11, "A" },
            new object[] { "Vattmannstraﬂe 11 A", "Vattmannstraﬂe ", (short)11, "A" },
            new object[] { "Berliner Strasse 11a", "Berliner Strasse ", (short)11, "A" },
            new object[] { "Berliner Strasse 11 A", "Berliner Strasse ", (short)11, "A" },
            new object[] { "Berliner-Strasse 11a", "Berliner-Strasse ", (short)11, "A" },
            new object[] { "Berliner-Strasse 11 A", "Berliner-Strasse ", (short)11, "A" },

            // With errors
            new object[] { "Vattmannstr.11a", "Vattmannstr.", (short)11, "A" },
            new object[] { "Vattmannstr.11 a", "Vattmannstr.", (short)11, "A" },
            new object[] { "Vattmannstr.11", "Vattmannstr.", (short)11, "" },
            //new object[] { "    Vattmannstraﬂe 11", "Vattmannstr.", (short)11, "" },
            //new object[] { "   Vattmannstraﬂe 11    ", "Vattmannstr.", (short)11, "" },
            //new object[] { "Vattmannstraﬂe 11    ", "Vattmannstr.", (short)11, "" },
        };

        private AddressSeparationProcessor<GermanSimpleAddressFormat> _processor;
        private GermanSimpleAddressProcessOptions _options;

        #endregion Fields

        #region Methods

        [SetUp]
        public void Setup()
        {
            // arrange
            var inputManipulationQueue = new Queue<IInputManipulation>();
            // inputManipulationQueue.Enqueue(new TrimInputManipulation());
            // inputManipulationQueue.Enqueue(new ShortenGermanStreetInputManipulation());

            this._options = new GermanSimpleAddressProcessOptions();
            this._processor = new AddressSeparationProcessor<GermanSimpleAddressFormat>(this._options, inputManipulationQueue);
        }

        [TestCaseSource("CheckupAddresses")]
        public void Addresses_WithoutInputManipulation_ReturnCorrectValues(string input, string streetName, short? houseNumber, string houseNumberAffix)
        {
            // act
            OutputResult<GermanSimpleAddressFormat> result = _processor.Process(input);
            GermanSimpleAddressFormat address = result.ResolvedAddress;

            // assert
            Assert.AreEqual(streetName, address.StreetName);
            Assert.AreEqual(houseNumber, address.HouseNumber);
            Assert.AreEqual(houseNumberAffix, address.HouseNumberAffix);
        }

        #endregion Methods
    }
}
