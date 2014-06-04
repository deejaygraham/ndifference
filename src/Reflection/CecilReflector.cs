using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

namespace NDifference.Reflection
{
	public class CecilReflector : IAssemblyReflector
	{
		private const string InternalModuleType = "<Module>";

		private AssemblyDefinition assemblyDefinition;

		public CecilReflector(AssemblyDefinition assemblyContent)
		{
			this.assemblyDefinition = assemblyContent;
		}

		public IEnumerable<ITypeInfo> GetTypes()
		{
			return this.GetTypes(AssemblyReflectionOption.All);
		}

		public IEnumerable<ITypeInfo> GetTypes(AssemblyReflectionOption option)
		{
			var typeList = new List<ITypeInfo>();

			var modules = this.assemblyDefinition.Modules;

			foreach (var module in modules)
			{
			    var types = module.GetTypes();

			    foreach (var type in types)
			    {
					if (type.IsInternalType())
					{
						continue;
					}

					TypeTaxonomy kind = type.ToTaxonomy();

					// don't support this type
					if (kind == TypeTaxonomy.Unknown)
					{
						continue;
					}
						 
					if (option == AssemblyReflectionOption.All || type.IsPublic)
					{
						typeList.Add(type.ToTypeInfo());
					}
			    }
			}

			return typeList;
		}

		public IEnumerable<ITypeInfo> GetTypesImplementing(Type requiredInterface)
		{
			var modules = this.assemblyDefinition.Modules;

			foreach (var module in modules)
			{
				var types = module.GetTypes();

				foreach (var type in types)
				{
					if (!type.HasInterfaces)
						continue;

					TypeTaxonomy kind = type.ToTaxonomy();

					if (kind != TypeTaxonomy.Class || type.IsAbstract)
						continue;

					foreach (var i in type.Interfaces)
					{
						if (i.FullName == requiredInterface.FullName)
						{
							yield return type.ToTypeInfo();
						}
					}
				}
			}

			yield break;
		}

		public IAssemblyInfo GetAssemblyInfo()
		{
			var info = new AssemblyInfo(this.assemblyDefinition.Name.Name, this.assemblyDefinition.Name.Version);

			var module = this.assemblyDefinition.MainModule;

			info.Architecture = string.Format(CultureInfo.InvariantCulture, "{0} {1}",
				module.Architecture,
				module.Attributes);

			info.RuntimeVersion = module.Runtime.ToString();

			foreach (var reference in module.AssemblyReferences)
			{
				info.Add(new AssemblyReference { Name = reference.Name, Version = reference.Version.ToString() });
			}

			return info;
		}

	}
}
