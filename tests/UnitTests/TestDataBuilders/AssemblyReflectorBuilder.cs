using NDifference.Reflection;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace NDifference.UnitTests
{
    /// <summary>
    /// Builds AssemblyReflector object given some source code.
    /// </summary>
    public class AssemblyReflectorBuilder : IBuildable<IAssemblyReflector>
	{
		private string sourceCode;
        private string fileName;
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

        public AssemblyReflectorBuilder Named(string name)
        {
            this.fileName = name;

            return this;
        }
        public AssemblyReflectorBuilder WithAssemblyReference(string reference)
		{
			this.references.Add(reference);

			return this;
		}

		public IAssemblyReflector Build()
		{
			Debug.Assert(this.factory != null, "Reflection factory not set");
			Debug.Assert(!String.IsNullOrEmpty(this.sourceCode), "No source code set");

            OnTheFlyCompiler fly = new OnTheFlyCompiler();
			{
				this.references.ForEach(x => fly.References.Add(x));

                if (!String.IsNullOrEmpty(this.fileName))
                {
                    fly.FileName = this.fileName;
                }

				var info = fly.Compile(this.sourceCode);

				return this.factory.LoadAssembly(info.Path);
			}
		}
	}

}
