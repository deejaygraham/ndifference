using NDifference;
using NDifference.Plugins;
using NDifference.Reflection;
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

			var aInspectors = new AssemblyInspectorPluginDiscoverer(finder);
			aInspectors.Ignore("NDifference.dll");
			aInspectors.Ignore("NDifference.UnitTests.dll");

			aInspectors.Find();

			var tInspectors = new TypeInspectorPluginDiscoverer(finder);
			tInspectors.Ignore("NDifference.dll");
			tInspectors.Ignore("NDifference.UnitTests.dll");

			tInspectors.Find();

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
