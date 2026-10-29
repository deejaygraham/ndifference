# NDifference

NDifference compares two releases of managed .NET assemblies and reports public
API changes. It works from compiled assemblies, so the original source code is
not required.

Analysis is performed at product, assembly, and type level. Built-in inspectors
cover common compatibility changes, and custom inspectors and report writers can
be loaded as plugins.

## Supported inspections

Product-level inspections include added, removed, and changed assemblies.
Assembly-level inspections include target architecture, assembly references,
and added, removed, obsolete, or changed types.

Type-level inspections cover:

- constants and enum values
- abstraction, derivation, and sealing
- constructors and finalizers
- events, fields, methods, and properties
- interface implementation
- obsolete members and type taxonomy changes

Reports can be generated as Markdown or HTML.

## Install and run

Download the `win-x64` MSI from the project's
[GitHub Releases](https://github.com/deejaygraham/ndifference/releases). The
installer contains a self-contained .NET 10 Windows Desktop application, so a
separate .NET runtime is not required.

Start NDifference from the Windows Start menu, select the assemblies from the
older and newer releases, choose an output folder, and run the comparison.

## Build from source

The full solution requires:

- Windows 10 or later
- the .NET 10 SDK selected by `global.json`
- Git history and tags, which MinVer uses to generate versions

Restore, build, and test with:

```powershell
dotnet restore NDifference.sln
dotnet build NDifference.sln --configuration Release --no-restore
dotnet test tests/UnitTests/UnitTests.csproj --configuration Release --no-build
```

The Release build creates:

- the self-contained application in `artifacts/publish/win-x64/`
- the WiX 6 installer at
  `installer/bin/Release/NDifference.Installer.msi`

The core libraries can be built on Linux, but the complete solution and
installer build require Windows because they contain WinForms and WiX projects.

Run the UI from source with:

```powershell
dotnet run --project src/UI/UI.csproj
```

## Command-line sample

`src/WalkingSkeleton` is an API usage sample rather than a supported command-line
product. It compares every managed assembly found in two folders and writes
Markdown reports to an output folder:

```powershell
dotnet run --project src/WalkingSkeleton/WalkingSkeleton.csproj -- `
  C:\releases\previous `
  C:\releases\current `
  C:\reports\api-difference
```

The generated entry point is `Summary.md`.

## Custom plugins

Custom inspector plugins should target `netstandard2.0` and reference the
`netstandard2.0` build of `NDifference.Core`. This gives plugins the best chance
of remaining compatible with future host runtime upgrades while the inspector
contracts remain binary compatible. A plugin may target `net10.0`, but that
couples it to the current host runtime.

Plugins compiled for the former .NET Framework application must be recompiled;
the old binaries cannot be loaded by the .NET 10 host. Plugin classes must be
public, implement an NDifference plugin interface, and have a public parameterless
constructor. For example:
