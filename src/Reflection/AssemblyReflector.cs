using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Reflection
{
	public class AssemblyReflector
	{
		public IEnumerable<string> AllTypeNamesIn(string assembly)
		{
			Debug.Assert(!String.IsNullOrEmpty(assembly), "Assembly name is blank");
			Debug.Assert(File.Exists(assembly), "Assembly does not exist");

			var definition = AssemblyDefinition.ReadAssembly(assembly);

			foreach(var typeDef in definition.MainModule.GetTypes().Where(x => !x.IsInternalName()))
				yield return typeDef.FriendlyName();

			yield break;
		}
	}
}
