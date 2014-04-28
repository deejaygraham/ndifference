using Reflection;
using System;
using System.Reflection;

namespace WalkingSkeleton
{
	class Program
	{
		static void Main(string[] args)
		{
			// Load an assembly and return all names...
			AssemblyReflector ar = new AssemblyReflector();

			string assemblyPath = Assembly.GetAssembly(typeof(AssemblyReflector)).Location;

			foreach(var tn in ar.AllTypeNamesIn(assemblyPath))
			{
				Console.WriteLine(tn);
			}

			assemblyPath = Assembly.GetExecutingAssembly().Location;

			foreach (var tn in ar.AllTypeNamesIn(assemblyPath))
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
		private T _object;

		public TemplatedType(T o)
		{
			this._object = o;
		}
	}
}
