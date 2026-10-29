# NDifference

NDifference compares two releases of managed .NET assemblies and reports public API changes. It works from 
compiled assemblies, so the original source code is not required.

Analysis is performed at product, assembly, and type level. Built-in inspectors cover common compatibility 
changes, and custom inspectors and report writers can be loaded as plugins.

## Supported inspections

Product-level inspections include added, removed, and changed assemblies. Assembly-level inspections include 
target architecture, assembly references, and added, removed, obsolete, or changed types.

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
[GitHub Releases](https://github.com/deejaygraham/ndifference/releases). The installer contains a self-contained 
.NET 10 Windows Desktop application, so a separate .NET runtime is not required.

Start NDifference from the Windows Start menu, select the assemblies from the older and newer releases, choose an 
output folder, and run the comparison.

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

The core libraries can be built on Linux, but the complete solution and installer build require Windows because they 
contain WinForms and WiX projects.

Run the UI from source with:

```powershell
dotnet run --project src/UI/UI.csproj
```

## Command-line sample

`src/WalkingSkeleton` is an API usage sample rather than a supported command-line product. It compares every managed 
assembly found in two folders and writes Markdown reports to an output folder:

```powershell
dotnet run --project src/WalkingSkeleton/WalkingSkeleton.csproj -- `
  C:\releases\previous `
  C:\releases\current `
  C:\reports\api-difference
```

The generated entry point is `Summary.md`.

## Custom plugins

Custom inspector plugins should target `netstandard2.0` and reference the `netstandard2.0` build 
of `NDifference.Core`. This gives plugins the best chance of remaining compatible with future host runtime upgrades 
while the inspector contracts remain binary compatible. A plugin may target `net10.0`, but that
couples it to the current host runtime.

Plugins compiled for the former .NET Framework application must be recompiled; the old binaries cannot be loaded by 
the .NET 10 host. Plugin classes must be public, implement an NDifference plugin interface, and have a public parameterless
constructor. For example:

```csharp
using NDifference.Analysis;
using NDifference.Inspectors;
using NDifference.TypeSystem;

public sealed class CustomInspector : ITypeInspector
{
    public bool Enabled { get; set; } = true;
    public string ShortCode => "CUSTOM001";
    public string DisplayName => "Custom inspector";
    public string Description => "Applies a project-specific compatibility rule.";

    public void Inspect(
        ITypeInfo previous,
        ITypeInfo current,
        IdentifiedChangeCollection changes)
    {
        // Add identified changes here.
    }
}
```

Place the plugin DLL and its private dependencies beside `NDifference.UI.exe`. Do not deploy a private copy 
of `NDifference.Core.dll`; the host shares its contract assembly with plugins. Native DLLs in the application directory are
ignored during plugin discovery, while managed plugin load errors are reported.

## Versioning and releases

MinVer generates assembly and installer versions from Git tags. Release tags must be semantic versions with 
a `v` prefix, such as `v2.0.0` or `v2.1.0-rc.1`. Untagged commits receive a preview version automatically.

Pushing a matching tag runs `.github/workflows/release.yml`. The workflow builds and tests the solution, validates the embedded 
MSI payload and generated versions, creates a SHA-256 checksum, publishes a GitHub Release, and attempts to
create a provenance attestation. A temporary Sigstore/Rekor outage does not prevent the installer from being released.

## Continuous integration

[![CI](https://github.com/deejaygraham/ndifference/actions/workflows/ci-build.yml/badge.svg)](https://github.com/deejaygraham/ndifference/actions/workflows/ci-build.yml)

CI builds the Release configuration, validates that the installer contains its payload, and runs the test suite on `windows-latest`.

## Contributing

Issues and pull requests are welcome. Build the Release configuration and run the complete test suite before submitting a change.

## Acknowledgements

- [Mono.Cecil](https://www.mono-project.com/docs/tools+libraries/libraries/Mono.Cecil/)
  provides the static reflection engine.
- [xUnit.net](https://xunit.net/) provides the unit testing framework.
- The original inspiration was Microsoft's unsupported LibCheck tool.
