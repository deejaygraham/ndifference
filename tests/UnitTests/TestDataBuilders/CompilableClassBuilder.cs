using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NDifference.UnitTests
{
	/// <summary>
	/// Generates C# code for a single class.
	/// </summary>
	public class CompilableClassBuilder : IBuildToCode
	{
		private string className;
		private List<string> events = new List<string>();
		private List<string> constructors = new List<string>();
		private List<string> properties = new List<string>();
		private List<string> methods = new List<string>();
		private List<string> fields = new List<string>();
		private List<string> constants = new List<string>();
		private bool isAbstract = false;
		private bool isInternal = false;
		private bool isSealed = false;
		private bool obsolete = false;
		private string obsoleteText = string.Empty;
		private string baseClass = string.Empty;
		private List<string> interfaces = new List<string>(); // base class and interfaces

		public static CompilableClassBuilder PublicClass()
		{
			return new CompilableClassBuilder();
		}

		public static CompilableClassBuilder InternalClass()
		{
			return new CompilableClassBuilder
			{
				isInternal = true
			};
		}

		public CompilableClassBuilder Default()
		{
			this.className = "Account";

			this.properties.Add("public string Name { get { return \"hello\"; } }");
			this.methods.Add("public void HelloWorld() { }");

			return this;
		}

		public CompilableClassBuilder Named(string cn)
		{
			this.className = cn;

			return this;
		}

		public CompilableClassBuilder Implementing(string interfaceName)
		{
			this.interfaces.Add(interfaceName);

			return this;
		}

		public CompilableClassBuilder DerivedFrom(string baseClassName)
		{
			this.baseClass = baseClassName;

			return this;
		}

		public CompilableClassBuilder WithConstructor(string constructorCode, bool makeObsolete = false)
		{
            string code = makeObsolete ? GenerateObsoleteAttribute("This Constructor is Obsolete") + constructorCode : constructorCode;
            this.constructors.Add(code);

			return this;
		}

		public CompilableClassBuilder WithDefaultConstructor(bool makeObsolete = false)
        {
            string constructorCode = string.Format("public {0}() {{ }}", this.className);

            return WithConstructor(constructorCode, makeObsolete);
		}

		public CompilableClassBuilder WithStaticConstructor()
		{
			this.constructors.Add(string.Format("static {0}() {{ }}", this.className));

			return this;
		}

		public CompilableClassBuilder WithFinalizer()
		{
			// not really a constructor 
			this.constructors.Add(string.Format("~{0}() {{ }}", this.className));

			return this;
		}

		public CompilableClassBuilder WithEvent(string eventCode, bool makeObsolete = false)
		{
            string code = makeObsolete ? GenerateObsoleteAttribute("This Event is Obsolete") + eventCode : eventCode;
			this.events.Add(code);

			return this;
		}

		public CompilableClassBuilder WithProperty(string propertyCode, bool makeObsolete = false)
		{
            string code = makeObsolete ? GenerateObsoleteAttribute("This Property is Obsolete") + propertyCode : propertyCode;
			this.properties.Add(code);

			return this;
		}

		public CompilableClassBuilder WithProperty(string propertyType, string propertyName, bool makeObsolete = false)
        {
            string propertyCode = string.Format("public {0} {1} {{ get; set; }}", propertyType, propertyName);
			return WithProperty(propertyCode, makeObsolete);
		}

		public CompilableClassBuilder WithField(string fieldCode, bool makeObsolete = false)
		{
            string code = makeObsolete ? GenerateObsoleteAttribute("This Field is Obsolete") + fieldCode : fieldCode;
			this.fields.Add(code);

			return this;
		}

		public CompilableClassBuilder WithField(string typeofField, string fieldName, string fieldValue, bool makeObsolete = false)
		{
			string fieldCode = string.Format("public {0} {1} = {2};", typeofField, fieldName, fieldValue);
			return WithField(fieldCode, makeObsolete);
		}

		public CompilableClassBuilder WithConstant(string constantCode, bool makeObsolete = false)
		{
            string code = makeObsolete ? GenerateObsoleteAttribute("This Constant is Obsolete") + constantCode : constantCode;
			this.constants.Add(code);

			return this;
		}

		public CompilableClassBuilder WithConstant(string typeofConstant, string constantName, string constantValue, bool makeObsolete = false)
        {
            string constantCode = string.Format("public const {0} {1} = {2};", typeofConstant, constantName, constantValue);
            return WithConstant(constantCode, makeObsolete);
		}

		public CompilableClassBuilder WithMethod(string methodCode, bool makeObsolete = false)
        {
            string code = makeObsolete ? GenerateObsoleteAttribute("This Method is Obsolete") + methodCode : methodCode;
            this.methods.Add(code);

			return this;
		}

		public CompilableClassBuilder IsObsolete()
		{
			this.obsolete = true;

			return this;
		}

		public CompilableClassBuilder IsObsolete(string text)
		{
			this.obsolete = true;
			this.obsoleteText = text;

			return this;
		}

		public CompilableClassBuilder IsInternal()
		{
			this.isInternal = true;

			return this;
		}

		public CompilableClassBuilder IsAbstract()
		{
			this.isAbstract = true;

			return this;
		}

		public CompilableClassBuilder IsSealed()
		{
			this.isSealed = true;

			return this;
		}

		public string Build()
		{
			var builder = new StringBuilder();

			builder.AppendLine("\t// Class ");

			if (this.obsolete)
			{
				builder.Append(GenerateObsoleteAttribute(this.obsoleteText));
			}

			builder.Append("\t");

            string visibility = this.isInternal ? "internal " : "public ";
			builder.Append(visibility);

			builder.AppendFormat(
				"{0}{1}class {2} ",
				this.isAbstract ? "abstract " : string.Empty,
				this.isSealed ? "sealed " : string.Empty,
				this.className);

			if (!string.IsNullOrEmpty(this.baseClass) || this.interfaces.Count > 0)
			{
				builder.Append(" : ");
			}

			if (!string.IsNullOrEmpty(this.baseClass))
			{
				builder.Append(this.baseClass);

				if (this.interfaces.Any())
				{
					builder.Append(", ");
				}
			}

			if (this.interfaces.Any())
			{
				builder.Append(string.Join(", ", this.interfaces));
			}

			builder.AppendLine();
			builder.AppendLine("\t{");

			this.events.GenerateMemberCode(builder, "Events");
			this.constants.GenerateMemberCode(builder, "Constants");
			this.fields.GenerateMemberCode(builder, "Fields");
			this.constructors.GenerateMemberCode(builder, "Constructors");
			this.properties.GenerateMemberCode(builder, "Properties");
			this.methods.GenerateMemberCode(builder, "Methods");

			builder.AppendLine("\t}");
			builder.AppendLine("\t// End of Class ");

			return builder.ToString();
		}

        private string GenerateObsoleteAttribute(string reason = null)
        {
            var builder = new StringBuilder();

			builder.Append("\t[Obsolete");
            if (!string.IsNullOrEmpty(reason))
            {
                builder.AppendFormat("(\"{0}\")", reason);
            }

            builder.AppendLine("]");

            return builder.ToString();
        }
	}
}
