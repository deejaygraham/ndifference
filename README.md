NDifference
===========

Tools for public API difference reporting on .Net assemblies

## What is it?
NDifference is a difference and reporting tool to allow developers to discover breaking changes in the public 
API of a .Net project between releases at the assembly level. It works with published assemblies via reflection 
and does not require the original source code.

It performs static analysis on .Net assemblies, providing built-in inspectors for the most common checks and 
supports loading of custom inspectors. Analysis is performed at the library, assembly and type level.

## Product Inspection
The following is a list of built-in product level inspections:

* Removed assemblies
* Changed assemblies
* Added assemblies

## Assembly Inspection
The following is a list of built-in assembly level inspections:

* Changed target architecture
* Changed assembly references
* Removed types
* Added types
* Obsolete types
* Changed types

## Type Inspection
The following is a list of built-in type level inspections:

* Constant values
* Enum values
* Abstraction
* Constructors
* Derivation
* Events
* Fields
* Finalizers
* Interface implementation
* Methods
* Properties
* Sealing
* Obsolete members
* Taxonomy

## Reporting
There is a built-in reporting capability for common file formats but custom reporting is supported too.

## Extensibility
Both the inspection rules and the report output are customisable.

## Prior Art
The original inspiration for this project came from Microsoft's unsupported 
[libcheck](http://www.microsoft.com/en-us/download/details.aspx?id=11287) tool.

## CI Status
The project builds on AppVeryor: [![Build status](https://ci.appveyor.com/api/projects/status/j669wh11qrhwbq6b?svg=true)](https://ci.appveyor.com/project/deejaygraham/ndifference).

## Get Involved!
Have an idea about improving the project? Please get involved. NDifference is an open source project (of course) and we accept 
contributions! Contribute a patch or let us know how to improve.

### Thank You's !
* The [Mono.Cecil](http://www.mono-project.com/Cecil) Project for the reflection engine
* The fine folks at [xunit](http://xunit.codeplex.com) for their unit testing framework.
* [AppVeyor](https://appveyor.com/) for the CI support.

