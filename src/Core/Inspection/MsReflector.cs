using NDifference.TypeSystem;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;

namespace NDifference
{
	internal class MsReflector : IAssemblyReflector
	{
		private System.Reflection.Assembly assembly;

		public MsReflector(System.Reflection.Assembly assembly)
		{
			Debug.Assert(assembly != null, "Assembly cannot be null");

			this.assembly = assembly;
		}

		public IEnumerable<ITypeInfo> GetTypes()
		{
			return this.GetTypes(AssemblyReflectionOption.All);
		}

		public IEnumerable<ITypeInfo> GetTypes(AssemblyReflectionOption option)
		{
			foreach (Type t in this.assembly.GetTypes())
			{
				if (option == AssemblyReflectionOption.All || t.IsPublic)
				{
					yield return t.ToTypeInfo();
				}
			}

			yield break;
		}

		public IEnumerable<ITypeInfo> GetTypesImplementing(Type requiredInterface)
		{
			string interfaceName = requiredInterface.FullName;

			foreach (Type candidateType in this.assembly.GetConcreteTypes())
			{
				if (candidateType.FindInterfaces(new TypeFilter(MatchNameFilter), interfaceName).Length > 0)
				{
					yield return candidateType.ToTypeInfo();
				}
			}

			yield break;
		}

		private static bool MatchNameFilter(Type type, object interfaceName)
		{
			string match = interfaceName as string;

			return type.ToString() == match;
		}


		public IAssemblyInfo GetAssemblyInfo()
		{
			var info = new AssemblyInfo(this.assembly.GetName().Name, this.assembly.GetName().Version);

			info.RuntimeVersion = this.assembly.ImageRuntimeVersion;

			foreach (var reference in this.assembly.GetReferencedAssemblies())
			{
				info.Add(new AssemblyReference { Name = reference.Name, Version = reference.Version.ToString() });
			}

			return info;

		}
	}

	/// <summary>
	/// Reflected assembly extension methods.
	/// </summary>
	public static class AssemblyExtensions
	{
		/// <summary>
		/// Find all concrete types in a given assembly.
		/// </summary>
		/// <param name="assembly"></param>
		/// <returns></returns>
		public static Type[] GetConcreteTypes(this System.Reflection.Assembly assembly)
		{
			var typeList = new List<Type>();

			foreach (Type candidateType in assembly.GetTypes())
			{
				if (candidateType.IsAbstract || candidateType.IsInterface)
				{
					continue;
				}

				typeList.Add(candidateType);
			}

			return typeList.ToArray();
		}
	}
}
