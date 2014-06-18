using NDifference;
using NDifference.Analysis;
using NDifference.Framework;
using NDifference.Projects;
using NDifference.Reflection;
using System;
using System.IO;
using System.Reflection;

namespace WalkingSkeleton
{
	public class Program
	{
#if ASSEMBLY_LOAD_DEMO
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
#else

		static void Main(string[] args)
		{
			if (args.Length < 3)
			{
				Console.WriteLine("WalkingSkeleton <v1 folder> <v2 folder> <output>");
				return;
			}

			string firstFolder = args[0];
			string secondFolder = args[1];
			string outputPath = args[2];

			if (!Directory.Exists(firstFolder))
			{
				Console.WriteLine("Folder \'{0}\' does not exist", firstFolder);
				return;
			}

			if (!Directory.Exists(secondFolder))
			{
				Console.WriteLine("Folder \'{0}\' does not exist", secondFolder);
				return;
			}

			var previousVersion = new ProductIncrement() { Name = "v0.0.1" };

			previousVersion.AddRange(AssemblyDiskInfoBuilder.BuildFromFolder(firstFolder));

			var nextVersion = new ProductIncrement() { Name = "v0.0.2" };

			nextVersion.AddRange(AssemblyDiskInfoBuilder.BuildFromFolder(secondFolder));

			var project = ProjectBuilder.Default();
			project.Product.Clear();

			project.Product.Name = "Walking Skeleton";
			project.Product.Add(previousVersion);
			project.Product.Add(nextVersion);

			AnalysisWorkflow workflow = new AnalysisWorkflow(
				new FileFinder(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), FileFilterConstants.AssemblyFilter), 
				new CecilReflectorFactory());

			workflow.AnalysisStarting += (o, e) =>
				{
					Console.WriteLine("Analysis starting...");
				};

			workflow.PluginsLoading += (o, e) =>
			{
				Console.Write("Loading plugins...");
			};

			workflow.PluginsComplete += (o, e) =>
			{
				Console.Write("done.");
			};

			workflow.AssemblyComparisonStarting += (o, e) =>
				{
					Console.WriteLine(".");
				};

			workflow.AssemblyComparisonComplete += (o, e) =>
				{
					Console.WriteLine(".");
				};

			workflow.AnalysisComplete += (o, e) =>
			{
				Console.WriteLine("Done.");
			};

			workflow.Analyse(project);
		
		}

#endif

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
