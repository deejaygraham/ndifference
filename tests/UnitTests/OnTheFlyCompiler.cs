using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace NDifference.UnitTests
{
    /// <summary>
    /// Compiles code into an assembly to allow reflection on test code.
    /// </summary>
    /// <example>
    /// var ofc = new OnTheFlyCompiler();
    /// {
    ///		Assembly assembly = ofc.Compile(code);
    ///		
    ///		...stuff...
    ///		
    /// }
    /// </example>
    public sealed class OnTheFlyCompiler
	{
		private const string DllExtension = ".dll";
		private const string PdbExtension = ".pdb";

		public OnTheFlyCompiler()
		{
			//this.FileName = System.IO.Path.Combine(this.Folder, System.IO.Path.GetRandomFileName() + DllExtension);
            this.FileName = System.IO.Path.GetRandomFileName();
            var codeBase = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var localPath = new Uri(codeBase).LocalPath;

            this.Folder = Path.Combine(System.IO.Path.GetDirectoryName(localPath), System.IO.Path.GetRandomFileName());

            this.References = new HashSet<string>(new string[]
            {
                typeof(System.Object).GetTypeInfo().Assembly.Location,
                Path.Combine(Path.GetDirectoryName(typeof(System.Runtime.GCSettings).GetTypeInfo().Assembly.Location), "System.Runtime.dll")
            });
		}

		public HashSet<string> References { get; private set; }

		private string Folder { get; set; }

		public string FileName { get; set; }

        private string FullPath
        {
            get
            {
                return System.IO.Path.Combine(this.Folder, this.FileName + DllExtension);
            }
        }

		public IAssemblyDiskInfo Compile(IBuildToCode code)
		{
			return this.Compile(code.Build());
		}

		public IAssemblyDiskInfo Compile(string code)
		{
			Debug.Assert(!string.IsNullOrEmpty(code), "Code cannot be blank");

            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(code);

            MetadataReference[] references = this.References.Select(r => MetadataReference.CreateFromFile(r)).ToArray();

            CSharpCompilation compilation = CSharpCompilation.Create(
                this.FileName,
                syntaxTrees: new[] { syntaxTree },
                references: references,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using (var ms = new MemoryStream())
            {
                EmitResult result = compilation.Emit(ms);

                if (result.Success)
                {
                    // save to disk 
                    string tempFolder = System.IO.Path.GetDirectoryName(this.FullPath);

                    if (!Directory.Exists(tempFolder))
                        Directory.CreateDirectory(tempFolder);

                    using (Stream assembly = File.Open(this.FullPath, FileMode.Create))
                    {
                        ms.Seek(0, SeekOrigin.Begin);
                        ms.CopyTo(assembly);

                        assembly.Close();
                    }

                    AssemblyDiskInfoBuilder infoBuilder = new AssemblyDiskInfoBuilder();
                    return infoBuilder.BuildFromFile(this.FullPath);
                }

                IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic =>
                    diagnostic.IsWarningAsError ||
                    diagnostic.Severity == DiagnosticSeverity.Error);

                string message = string.Join(Environment.NewLine, failures.BuildErrorMessageList(code).ToArray());
                throw new InvalidOperationException("Compilation Failed:\n" + message);
            }
		}
	}

	public static class CompilerErrorExtensions
	{
		public static IEnumerable<string> BuildErrorMessageList(this IEnumerable<Diagnostic> allErrors, string code)
		{
			var errors = new List<string>();

			foreach (Diagnostic compilerError in allErrors)
			{
				errors.Add(compilerError.ToDevStudioFormat());
			}

			errors.Add("Code Below:");
			errors.Add(code);

			return errors;
		}

		public static string ToDevStudioFormat(this Diagnostic error)
		{
			return string.Format("{0}: {1}", error.Id, error.GetMessage());
		}
	}
}
