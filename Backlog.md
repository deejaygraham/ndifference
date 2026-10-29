# Backlog

This file tracks work that remains after the .NET 10 modernization. Completed
setup tasks from the original .NET Framework repository have been removed; Git
history remains the record of that work.

## Extensibility

- Use a dedicated, user-writable plugin directory instead of discovering plugins
  in the application installation directory.
- Add plugin diagnostics that identify the plugin path and missing or incompatible
  dependency when activation fails.
- Define and document a compatibility/versioning policy for plugin contracts.

## Command line and automation

- Decide whether to turn `src/WalkingSkeleton` into a supported command-line
  application with stable arguments, exit codes, and machine-readable output.
- Add options for report format, inspector filtering, and consolidated reports.
- Add an end-to-end test that compares two fixture releases and validates the
  generated report set.

## Reporting and analysis

- Support filtering reports by namespace and assembly.
- Add assembly-dependency graph output.
- Review attribute and extension-method compatibility rules.
- Improve reporting for `params`, `in`, `out`, `ref`, and array signatures.

## Project maintenance

- Add a changelog and release checklist.
- Expand end-user help and contributor architecture documentation.
- Keep supported SDK, GitHub Actions, WiX, and package versions current.
