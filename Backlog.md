# Setup

Create github page for project - done
Add travis.yml - done
Add batch and msbuild project - done
Add shared versioninfo.cs - done

Update readme.md with full description - done

Top level structure
src
tests
packages

Create wiki for documentation
Provide links to wiki from readme and pages

Create end user help documentation

Create developer guide
 how to get the source code
 how the code is laid out
 setting up the build system
 how to run a build and setup unit tests
 how to contribute - unit tests, documentation, checklist of things
Add solution - done
Add core project
Add msbuild project
Add gui project
Add console project
Add powershell project binding?

Version number
Tag version in git

Create change log
Announce availability
Release checklist

Handle extension methods
System.Runtime.CompilerServices.ExtensionAttribute

* Graph of Assembly dependencies
* Hashes
* Levenstein Distance for code

# Namespaces

Reflection
Analysis 
Exceptions
Reporting
Framework
Logging
Tasks 

# Events in Reflection 

Consider
IdentifiedEvent
InterfaceFound
ClassFound
StructFound
EnumFound
TypeFound
AssemblyFound


# Rules

Framework to load custom inspection rules

# Reporting

Framework to load custom reports

# Rule Implementation

## Product Inspection

* Removed assemblies
* Changed assemblies
* Added assemblies

## Assembly Inspection

* Changed target architecture
* Changed assembly references
* Removed types
* Added types
* Obsolete types
* Changed types

## Type Inspection

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
* Taxonomy changes - class to interface, concrete to abstract class etc.

# UI Issues

Add textbox or lable to show directory for from and to folders.
Use a different dialog to browse to directory so that copy/paste of path can be used.
If highlighting multiple entries on either list, then the Delete key should perform the same job as clicking the - (minus) button.
When clicking into a list of assemblies in the UI, allow Ctrl+A or add a select all button.

# Examples

using c#, vb.net, f# ???

