using Mono.Cecil;
using NDifference;
using NDifference.Plugins;
using NDifference.Reflection;
using NDifference.Reporting;
using NDifference.TypeSystem;
using NDifference.UnitTests;
using System;
using System.IO;
using System.Reflection;

namespace WalkingSkeleton
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var factory = new CecilReflectorFactory();
			var reflector = factory.LoadAssembly(Assembly.GetAssembly(typeof(CecilReflector)).Location);

			foreach(var tn in reflector.GetTypes())
			{
				Console.WriteLine(tn);
			}

			reflector = factory.LoadAssembly(Assembly.GetExecutingAssembly().Location);

			foreach (var tn in reflector.GetTypes())
			{
				Console.WriteLine(tn);
			}

			reflector = factory.LoadAssembly(Assembly.GetAssembly(typeof(PocoType)).Location);

			foreach (var tn in reflector.GetTypes())
			{
				Console.WriteLine(tn);
			}

			IFileFinder finder = new FileFinder(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), FileFilterConstants.AssemblyFilter);

			var coreAssembly = Assembly.GetAssembly(typeof(PocoType));
			var testAssembly = Assembly.GetAssembly(typeof(PocoTypeFacts));
			var reflAssembly = Assembly.GetAssembly(typeof(CecilReflector));
			var monoAssembly = Assembly.GetAssembly(typeof(DefaultAssemblyResolver));

			var aInspectors = new AssemblyInspectorPluginDiscoverer(finder);
			aInspectors.Ignore(Path.GetFileName(coreAssembly.Location));
			aInspectors.Ignore(Path.GetFileName(testAssembly.Location));
			aInspectors.Ignore(Path.GetFileName(reflAssembly.Location));
			aInspectors.Ignore(Path.GetFileName(monoAssembly.Location));

			aInspectors.Find();

			var tInspectors = new TypeInspectorPluginDiscoverer(finder);
			tInspectors.Ignore(Path.GetFileName(coreAssembly.Location));
			tInspectors.Ignore(Path.GetFileName(testAssembly.Location));
			tInspectors.Ignore(Path.GetFileName(reflAssembly.Location));
			tInspectors.Ignore(Path.GetFileName(monoAssembly.Location));

			tInspectors.Find();

			var reportingPlugins = new ReportingPluginDiscoverer(finder);
			reportingPlugins.Ignore(Path.GetFileName(coreAssembly.Location));
			reportingPlugins.Ignore(Path.GetFileName(testAssembly.Location));
			reportingPlugins.Ignore(Path.GetFileName(reflAssembly.Location));
			reportingPlugins.Ignore(Path.GetFileName(monoAssembly.Location));

			IReportingRepository rr = new ReportingRepository();

			rr.AddRange(reportingPlugins.Find());

			foreach (var fmt in rr.ReportFormats.SupportedFormats)
			{
				Console.WriteLine(fmt);
			}

			Console.ReadKey();
		}
	}

	/// <summary>
	/// Example template class to exercise extension methods.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class TemplatedType<T>
	{
		private T exampleObject;

		public TemplatedType(T o)
		{
			this.exampleObject = o;
		}
	}
}
