![Address Separation Logo](/AddressSeparation/Assets/logo.png)

# Address Separation Library
![build](https://github.com/jordi1988/AddressSeparationLibrary/workflows/build/badge.svg)

Who doesn't know that? Some guy developed a database table with one column containing the whole address in one string. 
That database must be renewed. 

Now, you should do it better: the address should be separated into atomic values.  
> Here we go …

---

**Address Separation Library** is an extensible library written in C# for isolating/ dividing/ cutting/ breaking up an address into its parts. This is done by an Regex putting the matching groups into class properties. With the help of user-defined input and output *manipulation functions* it can be made even more powerful when processing an address.

## Table of contents
1. [Features](#Features)
2. [Library usage](#Usage)
   1. [Example](#Example)
   2. [Output formats](#Output-formats)
   3. [Manipulations](#Manipulations)
   4. [Helper](#Helper)
3. [Excel add-in](#excel-add-in)
4. [Coming up next ...](#Coming-up-next)
5. [Contribution appreciated](#Contribution-appreciated)

## Features
- Separates a string into multiple atomic values
- Easy to extend with more output formats and user-defined manipulation functions
- No external dependencies
- Currently supported output formats
  - **[German, simple](/AddressSeparation/OutputFormats/de/GermanSimpleOutputFormat.cs)**: Matches simple German addresses in format `Streetname 123a`
  
## Usage
1. Simply grab this library from [NuGet](https://www.nuget.org/packages/AddressSeparation/) or download the binary from [GitHub](../../releases) and add it as a reference to your project.
2. Choose your correct output format (e. g. [German, simple](/AddressSeparation/OutputFormats/de/GermanSimpleOutputFormat.cs)) or create a new one.
3. Create an instance of `AddressSeparationProcessor` class with your desired output format and process your string/string array.

### Example
```csharp
static void Main(string[] args)
{
    var processor = new AddressSeparationProcessor<GermanSimpleOutputFormat>();
    var result = processor.Process("Teststraße 123a");
    var address = result.ResolvedAddress;
        
    Console.WriteLine($"
        Name is {address.StreetName} with number {address.HouseNumber} and affix {address.HouseNumberAffix}");
}
```
*some console application*

### Output formats `IOutputFormat`
Create new output formats by creating a class implementing `IOutputFormat` interface. You then need to pass in a regular expression with groups connected to your properties by `RegexGroupAttribute`. *Multiple attribute usage* is also allowed if more than one group is assigned to a single property. The first non-empty group will be assigned to the property. 

It is as simple as that:
```csharp
[DisplayName("German, simple")]
[Description("Matches german addresses in format `Streetname 123a`")]
public class GermanSimpleOutputFormat : IOutputFormat
{
        // Regex processing your address
        public Regex MatchingRegex => new Regex(
            @"^(?(?=.*\d)((\D+))\s?(\d+)\s*(\D){0,2}|(.*))$",
            RegexOptions.IgnoreCase
        );

        // Matches group 1 (or 4 if 1 is empty) for the street name.
        // Has a user-defined output manipulation function for trimming group 1
        [RegexGroup(1, typeof(TrimOutputManipulation))]
        [RegexGroup(4)]
        public string StreetName { get; set; }

        // Matches group 2 for the house number with nullable short.
        [RegexGroup(2)]
        public short? HouseNumber { get; set; }

        // Matches group 3 for the affix of the house number.
        // Has a user-defined output manipulation function for transforming the affix to uppercase.
        [RegexGroup(3, typeof(ToUpperOutputManipulation))]
        public string HouseNumberAffix { get; set; }
}
```
*GermanSimpleOutputFormat.cs (original class edited for brevity)* 

### Manipulations
Manipulations are divided into input and output manipulations. Manipulation classes can either implement `IInputManipulation` or `IOutputManipulation`.  

#### Input manipulation `IInputManipulation`
Input manipulation is for editing the *raw input address string* before any processing takes place.  
It is passed either to the `AddressSeparationProcessor<T>` constructor or set afterwards by `SetInputManipulation()`.
  
```csharp
public class ShortenGermanStreetInputManipulation : IInputManipulation
{
        /// Shortens a German `Straße` to `Str.`.
        public Func<string, string> Invoke =>
            (string raw) => raw?
                .Replace("Straße", "Str.")?
                .Replace("straße", "str.")?
                .Replace("Strasse", "Str.")?
                .Replace("strasse", "str.");
}
```
*ShortenGermanStreetInputManipulation.cs (original class edited for brevity)* 

```csharp
var shortenFunc = new ShortenGermanStreetInputManipulation();

// Like this    
var processor = new AddressSeparationProcessor<GermanSimpleOutputFormat>(shortenFunc);

// Or like that
processor.SetInputManipulation(shortenFunc);
```
*some console application*

#### Output manipulation `IOutputManipulation<T>`
Output manipulation is for editing the *found value inside a group* and is processed before releasing the output. Therefore the class type implementing the interface must be placed in `RegexGroupAttribute`.
**`<T>` must be of the same type as of the property** it will be assigned to.

```csharp
// can only be applied to string properties
public class ToUpperOutputManipulation : IOutputManipulation<string>
{
    // Value of group to be manipulated.
    public string Invoke(string value)
    {
        return value?.ToUpper();
    }
}
```
*ToUpperOutputManipulation.cs (original class edited for brevity)* 

```csharp
public class GermanSimpleOutputFormat : IOutputFormat
{
    ...

    [RegexGroup(3, typeof(ToUpperOutputManipulation))]
    public string HouseNumberAffix { get; set; }
}
```
*GermanSimpleOutputFormat.cs (original class edited for brevity)* 

### Helper
`OutputFormatHelper` class can find all output formats in this library or in your program. Get a list including display name and description (read from `DisplayNameAttribute` and `DescriptionAttribute`) by calling:
```csharp
// find all pre-defined output formats
IEnumerable<OutputFormatMapper> libraryOutputFormats = OutputFormatHelper.GetOutputFormats();

// find your own output formats
var myAssembly = Assembly.GetExecutingAssembly();
IEnumerable<OutputFormatMapper> myOutputFormats = OutputFormatHelper.GetOutputFormats(myAssembly);
```

If you are unsure which output format **fits your needs**, try calling `FindMatchingOutputFormats` of `OutputFormatHelper` and get suggestions based on a given input address.


## Excel add-in
![Screenshot of Excel add-in](/AddressSeparation.ExcelAddin/Assets/ExcelAddInScreenshot.png)  
The Excel add-in always uses the current version of the library. It can be installed using the setup.exe, which can be [downloaded within the ZIP file](../../releases) from the releases tab. You can create your own installer by publishing the `AddressSeparation.ExcelAddIn` project.


## Coming up next
These features may come in the future:
- Options/ input manipulation functions/ custom output formats for Excel Add in
- More output formats

## Contribution appreciated
Let's make this library complete by adding all output/address formats of the world! Feel free to contribute!
