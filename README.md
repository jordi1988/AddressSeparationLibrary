# Address Separation Library
![.NET Standard 2.0](https://github.com/jordi1988/AddressSeparationLibrary/workflows/Address%20Separation%20.NET%20Core%203.1%20Release%20Build%20for%20Ubuntu/badge.svg)

Who doesn't know that? Some guy developed a database table with one column containing the whole address in one string. 
That database must be renewed. 

Now, you should do it better: the address should be separated into atomic values.  
> Here we go Ö

---

**Address Separation Library** is an extensible library written in C# for isolating/ dividing/ cutting/ breaking up an address into its parts. This is done by an Regex putting the matching groups into class properties. With the help of user-defined input and output *manipulation functions* it can be made even more powerful when processing an address.

## Table of contents
1. [Features](#Features)
2. [Usage](#Usage)
   1. [Example](#Example)
   2. [Cultures](#Cultures)
   3. [Manipulations](#Manipulations)
   4. [Options](#Options)
3. [Coming up next ...](#Coming-up-next)
4. [Contribution appreciated](#Contribution-appreciated)

## Features
- Separates a string into multiple atomic values
- Easy to extend with more cultures and user-defined manipulation functions
- Currently supported cultures/address formats
  - **[German, simple](/AddressSeparation/Cultures/de/GermanSimpleOutputFormat.cs)**: Matches simple german addresses in format `Streetname 123a`
  
## Usage
1. Simply add this [.NET Standard 2.0 library](/AddressSeparation/bin/Release/netstandard2.0/AddressSeparation.dll) as a reference in your project.
2. Choose your correct culture (e. g. [German, simple](/AddressSeparation/Cultures/de/GermanSimpleOutputFormat.cs)) or create a new one.
3. Create an instance of `AddressSeparationProcessor` class with your desired culture and process your string or your string array.

### Example
```csharp
static void Main(string[] args)
{
    var processor = new AddressSeparationProcessor<GermanSimpleOutputFormat>();
    var result = processor.Process('Teststraﬂe 123a');
    var address = result.ResolvedAddress;
        
    Console.WriteLine($"Name is {address.StreetName} with number {address.HouseNumber} and affix {address.HouseNumberAffix}");
}
```
*some console application*

### Cultures
Create new cultures by creating a class implementing `IOutputFormat` interface. You then need to pass in a regular expression with groups connected to your properties by `RegexGroupAttribute`. *Multiple attribute usage* is also allowed if more than one group is assigned to a single property. The first non-empty group will be assigned to the property. 

It is as simple as that:
```csharp
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
        public override short? HouseNumber { get; set; }

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
Input manipulation is for editing the *raw input address string* before any proccessing takes place.  
It is passed to the `AddressSeparationProcessor<T>` either in constructor or by calling `SetInputManipulation()` for single manipulation functions or `SetInputManipulationQueue()` for multiple manipulation functions.
  
```csharp
public class ShortenGermanStreetInputManipulation : IInputManipulation
{
        /// Shortens a German `Straﬂe` to `Str.`.
        public Func<string, string> Invoke =>
            (string raw) => raw?
                .Replace("Straﬂe", "Str.")?
                .Replace("straﬂe", "str.")?
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

### Options
*Reserved for later use.*

## Coming up next
These features may come in the future:
- NuGet package
- Excel AddIn
- Service for startup.cs
- Web application for separating addresses

## Contribution appreciated
Lets make this library complete by adding all cultures/address formats of the world! Feel free to contribute!