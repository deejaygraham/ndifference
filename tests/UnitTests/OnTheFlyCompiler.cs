using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NDifference.UnitTests
{
	/// <summary>
	/// Compiles code into an assembly to allow reflection on test code.
	/// </summary>
	/// <example>
	/// using (var ofc = new OnTheFlyCompiler())
	/// {
	///		Assembly assembly = ofc.Compile(code);
	///		
	///		...stuff...
	///		
	/// }
	/// </example>
	public sealed class OnTheFlyCompiler : IDisposable
	{
		private const string DllExtension = ".dll";
		private const string PdbExtension = ".pdb";

        private AssemblyDiskInfoBuilder infoBuilder = new AssemblyDiskInfoBuilder();

		public OnTheFlyCompiler(CodeDomProvider provider, CompilerParameters parameters)
		{
			this.FileName = System.IO.Path.Combine(this.Folder, System.IO.Path.GetRandomFileName() + DllExtension);

			this.Provider = provider;
			this.Parameters = parameters;

			this.References = new HashSet<string>(new string[] { "System.dll" });

			parameters.OutputAssembly = this.FileName;
		}

		public OnTheFlyCompiler()
			: this(new CSharpCodeProvider(
				new Dictionary<string, string> { { "CompilerVersion", "v4.0" } }),
				new CompilerParameters { GenerateExecutable = false, TreatWarningsAsErrors = false, WarningLevel = 4, IncludeDebugInformation = true })
		{
		}

		public bool OptimiseCode { get; set; }

		public bool Targetx64 { get; set; }

		public string CompilerVersion { get; set; }

		public HashSet<string> References { get; private set; }

		private CodeDomProvider Provider { get; set; }

		private CompilerParameters Parameters { get; set; }

		private string Folder
		{
			get
			{
				var codeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
				var localPath = new Uri(codeBase).LocalPath;

				return System.IO.Path.GetDirectoryName(localPath);
			}
		}

		private string FileName { get; set; }

		public IAssemblyDiskInfo Compile(IBuildToCode code)
		{
			return this.Compile(code.Build());
		}

		public IAssemblyDiskInfo Compile(string code)
		{
			Debug.Assert(this.Provider != null, "CodeDomProvider is not set");
			Debug.Assert(this.Parameters != null, "CompilerParameters not set");
			Debug.Assert(!string.IsNullOrEmpty(code), "Code cannot be blank");

			string compilerOptions = string.Empty;

			if (this.OptimiseCode)
				compilerOptions += "/optimize+ ";

			if (this.Targetx64)
				compilerOptions += "/platform:x64";

			this.Parameters.CompilerOptions = compilerOptions;
			this.Parameters.ReferencedAssemblies.AddRange(this.References.ToArray());

			var results = this.Provider.CompileAssemblyFromSource(this.Parameters, code);

			if (results.Errors.HasErrors)
			{
				throw new InvalidOperationException("Compilation Failed:\n" + string.Join(Environment.NewLine, results.Errors.BuildErrorMessageList(code).ToArray()));
			}

			return infoBuilder.BuildFromFile(this.FileName);
		}

		public void Dispose()
		{
			if (this.Provider != null)
			{
				this.Provider.Dispose();
				this.Provider = null;
			}

			this.FileName.DeleteIfExists();
			string debugDBName = this.FileName.GetCompanionFile(PdbExtension);
			debugDBName.DeleteIfExists();
		}
	}

	public static class CompilerErrorExtensions
	{
		public static IEnumerable<string> BuildErrorMessageList(this CompilerErrorCollection allErrors, string code)
		{
			var errors = new List<string>();

			foreach (CompilerError compilerError in allErrors)
			{
				errors.Add(compilerError.ToDevStudioFormat());
			}

			errors.Add("Code Below:");
			errors.Add(code);

			return errors;
		}

		public static string ToDevStudioFormat(this CompilerError error)
		{
			return string.Format("{0}({1},{2}): error {3}: {4}", error.FileName, error.Line, error.Column, error.ErrorNumber, error.ErrorText);
		}
	}
}
