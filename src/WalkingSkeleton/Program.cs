using NDifference;
using NDifference.Reflection;
using System;
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
