# Setup

Create github page for project
Add travis.yml
Add batch and msbuild project 
Add shared versioninfo.cs

Update readme.md with full description.

Add solution
Add core project
Add msbuild project
Add gui project
Add console project

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

