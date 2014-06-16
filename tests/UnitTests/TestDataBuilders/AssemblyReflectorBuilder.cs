using NDifference.Reflection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.UnitTests.TestDataBuilders
{
	/// <summary>
	/// Builds AssemblyReflector object given some source code.
	/// </summary>
	public class AssemblyReflectorBuilder : IBuildable<IAssemblyReflector>
	{
		private string sourceCode;
		private bool targetx64;
		private string compilerVersion;
		private List<string> references = new List<string>();

		private IAssemblyReflectorFactory factory = new CecilReflectorFactory();

		public static AssemblyReflectorBuilder Introspection()
		{
			return new AssemblyReflectorBuilder();
		}

		public AssemblyReflectorBuilder Code(string code)
		{
			this.sourceCode = code;

			return this;
		}

		public AssemblyReflectorBuilder TargetsX64(bool x64)
		{
			this.targetx64 = x64;

			return this;
		}

		public AssemblyReflectorBuilder WithAssemblyReference(string reference)
		{
			this.references.Add(reference);

			return this;
		}

		public AssemblyReflectorBuilder TargetsCompilerVersion(string compilerVersion)
		{
			this.compilerVersion = compilerVersion;

			return this;
		}

		public IAssemblyReflector Build()
		{
			Debug.Assert(this.factory != null, "Reflection factory not set");
			Debug.Assert(!String.IsNullOrEmpty(this.sourceCode), "No source code set");

			using (OnTheFlyCompiler fly = new OnTheFlyCompiler())
			{
				fly.Targetx64 = this.targetx64;

				if (!String.IsNullOrEmpty(this.compilerVersion))
					fly.CompilerVersion = this.compilerVersion;

				this.references.ForEach(x => fly.References.Add(x));
				var info = fly.Compile(this.sourceCode);

				return this.factory.LoadAssembly(info.Path);
			}
		}
	}

}
