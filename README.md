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

### Custom inspector compatibility

Custom inspector plugins should target `netstandard2.0` and reference the `netstandard2.0` build 
of `NDifference.Core`. This keeps plugins compatible with future NDifference host runtime upgrades, provided 
the inspector contracts remain binary compatible. Plugins may target `net10.0`, but doing so couples them to the
current host runtime and is not recommended.

## Prior Art
The original inspiration for this project came from Microsoft's unsupported 
[libcheck](http://www.microsoft.com/en-us/download/details.aspx?id=11287) tool.

## Build It
Get the source (fork or just use the download button at the top right) and run ```dotnet build```.

### Versioning and releases

Assembly and installer versions are generated from Git release tags. Create release tags using semantic 
versions with a `v` prefix, for example `v1.0.4`. The first release of the .NET 10
version is `v2.0.0`. Builds from untagged commits receive a preview version 
automatically. The build requires full Git history; the GitHub Actions workflow is configured to fetch it.

### Windows installer

The WiX 6 installer is part of `NDifference.sln`. A Release build publishes the Windows UI as a self-contained 
`win-x64` application and creates `installer/bin/Release/NDifference.Installer.msi`. The MSI includes the .NET 10
Windows Desktop runtime, so target machines do not need a separate runtime install.


## Get Involved!
Have an idea about improving the project? Please get involved. NDifference is an open source project (of course) and we accept 
contributions! Contribute a patch or let us know how to improve.

### Thank You's !
* The [Mono.Cecil](http://www.mono-project.com/Cecil) Project for the reflection engine
* The fine folks at [xunit](https://xunit.github.io/) for their unit testing framework.


