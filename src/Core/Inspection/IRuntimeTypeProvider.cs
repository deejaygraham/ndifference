using System;
using System.Collections.Generic;

namespace NDifference.Inspection
{
  public interface IRuntimeTypeProvider
  {
    IEnumerable<Type> GetRuntimeTypesImplementing(Type requiredInterface);
  }
}
