using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;

namespace NDifference
{
	/// <summary>
	/// Information gathered about an assembly. 
	/// </summary>
	[DebuggerDisplay("{Name}")]
	public sealed class InspectedAssembly
	{
		private List<AssemblyReference> references = new List<AssemblyReference>();
		private List<ITypeInfo> objectModel = new List<ITypeInfo>();

		public InspectedAssembly(string name, string version)
			: this(name, new Version(version))
		{
		}

		public InspectedAssembly(string name, Version version)
		{
			this.Name = name;
			this.Version = version;
		}

		public string Name { get; private set; }

		public Version Version { get; private set; }

		public string RuntimeVersion { get; set; }

		public string Architecture { get; set; }

		public ReadOnlyCollection<AssemblyReference> References
		{
			get
			{
				return new ReadOnlyCollection<AssemblyReference>(this.references);
			}
		}

		public void Add(AssemblyReference ar)
		{
			this.references.Add(ar);
		}

		public int TypeCount
		{
			get { return this.objectModel.Count; }
		}

		public void Add(ITypeInfo discoveredType)
		{
			this.objectModel.Add(discoveredType);
		}

		////public ICollection<FullyQualifiedName> TypesIn(string namespaceName)
		////{
		////	return null; // this.namespaceTypeLookup.ContentFor(namespaceName);
		////}

		////public ITypeInfo Lookup(FullyQualifiedName typeName)
		////{
		////	Debug.Assert(typeName != null, "Type name cannot be blank");

		////	return this.objectModel.FirstOrDefault(x => x.FullName == typeName);
		////}

		////public ICollection<FullyQualifiedName> TypeDifferencesFrom(InspectedAssembly other)
		////{
		////	if (other == null)
		////	{
		////		throw new ArgumentNullException("other", "Introspection object cannot be null");
		////	}

		////	return this.objectModel.TypeDifferencesFrom(other.objectModel);
		////}

		////public ICollection<FullyQualifiedName> TypesInCommonWith(InspectedAssembly other)
		////{
		////	if (other == null)
		////	{
		////		throw new ArgumentNullException("other", "Introspection object cannot be null");
		////	}

		////	return this.objectModel.TypesInCommonWith(other.objectModel);
		////}

		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentUICulture, "{0} Version={1}", this.Name, this.Version);
		}

		public override int GetHashCode()
		{
			return this.Name.GetHashCode() ^ this.Version.GetHashCode();
		}
	}
}
