namespace AddressSeparation.OutputFormats
{
    /// <summary>
    /// American addresses consist of the following series of address elements:
    /// house number, prefix direction, prefix type, street name, street type, suffix direction, city, state, and ZIP Code
    /// </summary>
    /// <remarks>see https://desktop.arcgis.com/en/arcmap/10.3/guide-books/geocoding/what-is-an-address.htm#GUID-BD86A1CC-8E68-47B9-AD63-70D075013C8B</remarks>
    public class AmericanAddressFormat : ZoneAddress
    {
    }
}
