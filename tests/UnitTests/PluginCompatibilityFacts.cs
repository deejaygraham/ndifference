using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using NDifference.Exceptions;
using NDifference.Inspectors;
using NDifference.Plugins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Runtime.Versioning;
using Xunit;

namespace NDifference.UnitTests
{
	public class PluginCompatibilityFacts
	{
		[Fact]
		public void NetStandard20_Plugin_With_Private_Dependency_Is_Discovered_And_Activated()
		{
			using (var pluginDirectory = new TemporaryDirectory())
			{
				string suffix = Guid.NewGuid().ToString("N");
				string dependencyAssemblyName = "CompatibilityDependency_" + suffix;
				string pluginAssemblyName = "CompatibilityPlugin_" + suffix;
				string dependencyPath = Path.Combine(pluginDirectory.Path, dependencyAssemblyName + ".dll");
				string pluginPath = Path.Combine(pluginDirectory.Path, pluginAssemblyName + ".dll");

				CompileNetStandard20Assembly(
					dependencyAssemblyName,
					dependencyPath,
					@"namespace CompatibilityDependency
{
	public static class PluginNames
	{
		public static string GetDisplayName()
		{
			return ""External netstandard2.0 plugin"";
		}
	}
}");

				CompileNetStandard20Assembly(
					pluginAssemblyName,
					pluginPath,
					@"using CompatibilityDependency;
using NDifference.Analysis;
using NDifference.Inspectors;
using NDifference.TypeSystem;

namespace CompatibilityPlugin
{
	public sealed class ExternalInspector : ITypeInspector
	{
		private readonly string displayName;

		public ExternalInspector()
		{
			this.displayName = PluginNames.GetDisplayName();
		}

		public bool Enabled { get; set; }
		public string ShortCode { get { return ""EXT""; } }
		public string DisplayName { get { return this.displayName; } }
		public string Description { get { return ""Compatibility test plugin""; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
		}
	}
}",
					dependencyPath,
					typeof(IInspector).Assembly.Location);

				// Self-contained deployments place native runtime files such as this
				// beside managed assemblies. Discovery must not try to activate them.
				File.WriteAllText(
					Path.Combine(pluginDirectory.Path, "clretwrc.dll"),
					"Native runtime DLL placeholder");

				using var catalog = new PluginCatalog(
					new PluginDirectory(pluginDirectory.Path).CreateFinder());

				IInspector plugin = Assert.Single(catalog.Inspectors);

				Assert.Equal("External netstandard2.0 plugin", plugin.DisplayName);
				Assert.Equal("EXT", plugin.ShortCode);
				Assert.NotSame(AssemblyLoadContext.Default, AssemblyLoadContext.GetLoadContext(plugin.GetType().Assembly));
				Assert.False(AssemblyLoadContext.GetLoadContext(plugin.GetType().Assembly).IsCollectible);
				Assert.Same(typeof(IInspector).Assembly, plugin.GetType().GetInterfaces().Single(type => type == typeof(IInspector)).Assembly);

				TargetFrameworkAttribute targetFramework = plugin.GetType().Assembly.GetCustomAttribute<TargetFrameworkAttribute>();

				Assert.NotNull(targetFramework);
				Assert.Equal(".NETStandard,Version=v2.0", targetFramework.FrameworkName);
			}
		}

		[Theory]
		[InlineData(false, "Missing plugin dependency")]
		[InlineData(true, "incompatible binary format")]
		public void Dependency_Failure_Identifies_Plugin_Path_Type_And_Dependency(
			bool useIncompatibleDependency,
			string expectedDiagnostic)
		{
			using (var pluginDirectory = new TemporaryDirectory())
			{
				string suffix = Guid.NewGuid().ToString("N");
				string dependencyAssemblyName = "BrokenDependency_" + suffix;
				string pluginAssemblyName = "BrokenPlugin_" + suffix;
				string dependencyPath = Path.Combine(pluginDirectory.Path, dependencyAssemblyName + ".dll");
				string pluginPath = Path.Combine(pluginDirectory.Path, pluginAssemblyName + ".dll");

				CompileNetStandard20Assembly(
					dependencyAssemblyName,
					dependencyPath,
					@"namespace BrokenDependency
{
	public static class PluginNames
	{
		public static string GetDisplayName()
		{
			return ""Broken dependency plugin"";
		}
	}
}");

				CompileNetStandard20Assembly(
					pluginAssemblyName,
					pluginPath,
					@"using BrokenDependency;
using NDifference.Analysis;
using NDifference.Inspectors;
using NDifference.TypeSystem;

namespace BrokenPlugin
{
	public sealed class ExternalInspector : ITypeInspector
	{
		private readonly string displayName;

		public ExternalInspector()
		{
			this.displayName = PluginNames.GetDisplayName();
		}

		public bool Enabled { get; set; }
		public string ShortCode { get { return ""BROKEN""; } }
		public string DisplayName { get { return this.displayName; } }
		public string Description { get { return ""Broken dependency test plugin""; } }

		public void Inspect(ITypeInfo first, ITypeInfo second, IdentifiedChangeCollection changes)
		{
		}
	}
}",
					dependencyPath,
					typeof(IInspector).Assembly.Location);

				if (useIncompatibleDependency)
				{
					File.WriteAllText(dependencyPath, "Not a managed assembly");
				}
				else
				{
					File.Delete(dependencyPath);
				}

				PluginLoadException exception = Assert.Throws<PluginLoadException>(
					() => new PluginCatalog(
						new PluginDirectory(pluginDirectory.Path).CreateFinder()));

				Assert.Equal(pluginPath, exception.PluginPath);
				Assert.Equal("activate types from", exception.Operation);
				Assert.Contains(pluginPath, exception.Message);
				Assert.Contains("BrokenPlugin.ExternalInspector", exception.Message);
				Assert.Contains(dependencyAssemblyName, exception.Message);
				Assert.Contains(expectedDiagnostic, exception.Message);
				Assert.NotNull(exception.InnerException);
			}
		}

		private static void CompileNetStandard20Assembly(
			string assemblyName,
			string outputPath,
			string source,
			params string[] additionalReferences)
		{
			string frameworkSource = @"using System.Runtime.Versioning;
[assembly: TargetFramework("".NETStandard,Version=v2.0"", FrameworkDisplayName = "".NET Standard 2.0"")]";

			IEnumerable<MetadataReference> references = GetNetStandard20ReferenceAssemblies()
				.Concat(additionalReferences.Select(path => MetadataReference.CreateFromFile(path)));

			CSharpCompilation compilation = CSharpCompilation.Create(
				assemblyName,
				new[]
				{
					CSharpSyntaxTree.ParseText(frameworkSource),
					CSharpSyntaxTree.ParseText(source)
				},
				references,
				new CSharpCompilationOptions(
					OutputKind.DynamicallyLinkedLibrary,
					optimizationLevel: OptimizationLevel.Release));

			Directory.CreateDirectory(Path.GetDirectoryName(outputPath));

			using (FileStream output = File.Create(outputPath))
			{
				var result = compilation.Emit(output);

				if (!result.Success)
				{
					string diagnostics = string.Join(
						Environment.NewLine,
						result.Diagnostics
							.Where(diagnostic => diagnostic.Severity == DiagnosticSeverity.Error)
							.Select(diagnostic => diagnostic.ToString()));

					throw new InvalidOperationException("Plugin compilation failed:" + Environment.NewLine + diagnostics);
				}
			}
		}

		private static IEnumerable<MetadataReference> GetNetStandard20ReferenceAssemblies()
		{
			string packagesFolder = Environment.GetEnvironmentVariable("NUGET_PACKAGES");

			if (string.IsNullOrWhiteSpace(packagesFolder))
			{
				packagesFolder = Path.Combine(
					Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
					".nuget",
					"packages");
			}

			string referenceFolder = Path.Combine(
				packagesFolder,
				"netstandard.library",
				"2.0.3",
				"build",
				"netstandard2.0",
				"ref");

			if (!Directory.Exists(referenceFolder))
			{
				throw new DirectoryNotFoundException(
					"The .NET Standard 2.0 reference pack was not restored: " + referenceFolder);
			}

			return Directory
				.GetFiles(referenceFolder, "*.dll")
				.Select(path => MetadataReference.CreateFromFile(path))
				.ToArray();
		}

		private sealed class TemporaryDirectory : IDisposable
		{
			public TemporaryDirectory()
			{
				this.Path = System.IO.Path.Combine(
					System.IO.Path.GetTempPath(),
					"NDifference.PluginCompatibility",
					Guid.NewGuid().ToString("N"));

				Directory.CreateDirectory(this.Path);
			}

			public string Path { get; private set; }

			public void Dispose()
			{
				try
				{
					Directory.Delete(this.Path, true);
				}
				catch (IOException)
				{
					// Loaded plugin files remain locked on Windows until the test process exits.
				}
				catch (UnauthorizedAccessException)
				{
					// Loaded plugin files remain locked on Windows until the test process exits.
				}
			}
		}
	}
}
