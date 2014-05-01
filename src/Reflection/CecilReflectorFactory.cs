using Mono.Cecil;
using System;
using System.Diagnostics;
using System.IO;

namespace NDifference.Reflection
{
	public class CecilReflectorFactory : IAssemblyReflectorFactory
	{
		public IAssemblyReflector LoadAssembly(string path)
		{
			Debug.Assert(!string.IsNullOrEmpty(path), "Assembly name is blank");
			Debug.Assert(File.Exists(path), "Path to assembly does not exist");

			var resolver = new DefaultAssemblyResolver();

			resolver.AddSearchDirectory(Path.GetDirectoryName(path));

			var reader = new ReaderParameters()
			{
				AssemblyResolver = resolver
			};

			var assembly = AssemblyDefinition.ReadAssembly(path, reader);

			Debug.Assert(assembly != null, "Assembly not read from disk");

			return new CecilReflector(assembly);
		}
	}
}
