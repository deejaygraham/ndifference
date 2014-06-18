using NDifference.Inspectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NDifference.UnitTests.Inspectors.Types
{
	public class ConstantsObsoleteFacts
	{
		[Fact]
		public void ConstantsObsolete_Identifies_When_Constant_Made_Obsolete()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithConstant("string", "MyField", "\"Hello World\"");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithConstant("[Obsolete] public const string MyField = \"Hello World\";");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new ConstantsObsolete())
				.Build();

			Assert.Equal(1, delta.Changes.Count);
		}

		[Fact]
		public void ConstantsObsolete_Identifies_When_Constant_Made_Obsolete_With_Message()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithConstant("string", "MyField", "\"Hello World\"");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithConstant("[Obsolete(\"Do not use\")] public const string MyField = \"Hello World\";");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new ConstantsObsolete())
				.Build();

			Assert.Equal(1, delta.Changes.Count);
		}

		[Fact]
		public void ConstantsObsolete_Ignores_When_Constants_Not_Obsolete()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithConstant("string", "MyField", "\"Hello World\"");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithConstant("string", "MyField", "\"Hello World\"");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new ConstantsObsolete())
				.Build();

			Assert.Equal(0, delta.Changes.Count);
		}
	}
}
