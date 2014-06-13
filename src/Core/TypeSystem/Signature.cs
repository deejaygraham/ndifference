using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.TypeSystem
{
	///// <summary>
	///// A method signature.
	///// </summary>
	//[DebuggerDisplay("sig {Name}(..)")]
	//public class Signature
	//{
	//	public string Name { get; set; }

	//	public int TypeParameterCount { get; set; }
	//}


	/// <summary>
	/// Name, number, modifiers and types of formal parameters, number of generic type parameters.
	/// </summary>
	[DebuggerDisplay("sig {Name}(..)")]
	[Serializable]
	public class Signature
	{
		public Signature()
		{
			this.FormalParameters = new List<Parameter>();
		}

		public Signature(string methodName)
			: this()
		{
			this.Name = methodName;
		}

		public Signature(string methodName, IList<Parameter> formalParameters)
		{
			this.Name = methodName;
			this.FormalParameters = new List<Parameter>(formalParameters);
		}

		public string Name { get; set; }

		public List<Parameter> FormalParameters { get; private set; }

		public void Add(Parameter formalParameter)
		{
			this.FormalParameters.Add(formalParameter);
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();

			builder.Append(this.Name);
			builder.Append(" (");

			for (int i = 0; i < this.FormalParameters.Count; ++i)
			{
				Parameter p = this.FormalParameters[i];

				builder.Append(p.Name.Type.Value);

				if (i < this.FormalParameters.Count - 1)
				{
					builder.Append(",");
				}
			}

			builder.Append(")");

			return builder.ToString();
		}

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
