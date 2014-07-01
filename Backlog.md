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
Add core project - done
Add msbuild project
Add gui project - done
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
* Hashes - done
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

Framework to load custom inspection rules - done

# Reporting

Framework to load custom reports - done

# Rule Implementation

## Product Inspection

* Removed assemblies - done
* Changed assemblies - done
* Added assemblies - done

## Assembly Inspection

* Changed target architecture - done
* Changed assembly references - done
* Removed types - done
* Added types - done
* Obsolete types - done
* Changed types - done

## Type Inspection

* Constant values - done
* Enum values - done
* Abstraction - done
* Constructors - done
* Derivation - done
* Events - done
* Fields - done
* Finalizers - done
* Interface implementation - done
* Methods - done
* Properties - done
* Sealing - done
* Obsolete members - done
* Taxonomy changes - class to interface, concrete to abstract class etc. - done

# UI Issues

Add textbox or lable to show directory for from and to folders. - done
Use a different dialog to browse to directory so that copy/paste of path can be used.
If highlighting multiple entries on either list, then the Delete key should perform the same job as clicking the - (minus) button. - done
When clicking into a list of assemblies in the UI, allow Ctrl+A or add a select all button. - done

Task libraries
Completion token to cancel 
Closing the app?

Rules for attribute changes
Reports by namespace and/or by assembly - full object model to query.

# Examples

using c#, vb.net, f# ???


generate fragments of files used for sandcastle ui

Do unit tests on built code and on itypeinfos?

handle params arguments
handle arrays[]


