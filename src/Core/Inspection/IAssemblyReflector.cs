using NDifference.TypeSystem;
using System;
using System.Collections.Generic;

namespace NDifference
{
	/// <summary>
	/// Provides reflection information on types and general assembly.
	/// </summary>
	public interface IAssemblyReflector
	{
		IEnumerable<ITypeInfo> GetTypes();

		IEnumerable<ITypeInfo> GetTypes(AssemblyReflectionOption option);

		IEnumerable<ITypeInfo> GetTypesImplementing(Type requiredInterface);

		IAssemblyInfo GetAssemblyInfo();
	}
}
