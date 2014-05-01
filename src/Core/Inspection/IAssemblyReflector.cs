using System;
using System.Collections.Generic;

namespace NDifference
{
	public interface IAssemblyReflector
	{
		IEnumerable<ITypeInfo> GetTypes();

		IEnumerable<ITypeInfo> GetTypes(AssemblyReflectionOption option);

		IEnumerable<ITypeInfo> GetTypesImplementing(Type requiredInterface);

		IAssemblyInfo GetAssemblyInfo();
	}
}
