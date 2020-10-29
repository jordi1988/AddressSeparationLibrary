# Address Separation Library
Who doesn't know that? Some guy developed a database table with one column containing the whole address in one string. 
That database must be renewed. 

Now, you should do it better: the address should be separated into atomic values.  
> Here we go …

## Howto
Simply add this .NET Standard 2.0 library as a reference in your project. Create instance of `AddressSeparationProcessor` class, pass in options class of type `IProcessOptions`.

...

### Attributes
...

### Manipulations
...

### Options
...

### OutputFormat
...

## Features
- Currently supported locales/address formats
  - German (simple) / Deutsch (einfach)
- Easy to extend

## Next
These features may come in the future:
- Log in IProcessOptions
- NuGet package
- Excel AddIn
- Service for startup.cs
- Web application for separating addresses

## Contribution appreciated
Lets make this library complete by adding all locales/address formats of the world! Feel free to contribute!