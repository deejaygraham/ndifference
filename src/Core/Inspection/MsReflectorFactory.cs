using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace NDifference
{
	internal class MsReflectorFactory : IAssemblyReflectorFactory
	{
		public IAssemblyReflector LoadAssembly(string path)
		{
			Debug.Assert(!string.IsNullOrEmpty(path), "Assembly name is blank");
			Debug.Assert(File.Exists(path), "Path to assembly does not exist");

			return new MsReflector(Assembly.LoadFile(path));
		}
	}
}
