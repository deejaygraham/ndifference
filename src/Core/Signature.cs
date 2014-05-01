using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference
{
	/// <summary>
	/// A method signature.
	/// </summary>
	public class Signature
	{
		public string Name { get; set; }

		public int TypeParameterCount { get; set; }
	}

	public class SignatureOverloadResolver
	{
		public Signature FindBestMatch(Signature find, IEnumerable<Signature> choices)
		{
			Debug.Assert(find != null, "Find signature cannot be null");
			Debug.Assert(choices != null, "No choices supplied");

			Signature bestMatch = null;

			var candidates = choices.Where(x => x.Name == find.Name);

			if (candidates != null && candidates.Count() > 0)
			{
				if (candidates.Count() == 1)
				{
					bestMatch = candidates.First();
				}
				else
				{
					// now try to find the best match according to type, name of field etc.
				}
			}

			// fall back is to present all properties and ask user to pick.
			if (bestMatch == null)
				throw new Exception("No match found");

			return bestMatch;
		}
	}
}
