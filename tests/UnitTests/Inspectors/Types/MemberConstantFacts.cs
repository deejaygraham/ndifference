using NDifference.Inspectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NDifference.UnitTests
{
	public class MemberConstantFacts
	{
		[Fact]
		public void ConstantsAdded_Identifies_When_Adding_New_Constant()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithConstant("string", "MyField", "\"Hello World\"");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new ConstantsAdded())
				.Build();

            Assert.Single(delta.Changes);
        }

        [Fact]
		public void ConstantsAdded_Identifies_When_Existing_Field_Made_Constant()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithField("string", "MyField", "\"Hello World\"");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithConstant("string", "MyField", "\"Hello World\"");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new ConstantsAdded())
				.Build();

            Assert.Single(delta.Changes);
        }

        [Fact]
		public void ConstantsAdded_Ignores_When_No_Constants()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new ConstantsAdded())
				.Build();

            Assert.Empty(delta.Changes);
        }

        [Fact]
		public void ConstantsAdded_Ignores_When_Constants_Same()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithConstant("int", "MyInt", "1")
										.WithConstant("string", "MyField", "\"Hello World\"");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithConstant("int", "MyInt", "1")
										.WithConstant("string", "MyField", "\"Hello World\"");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new ConstantsAdded())
				.Build();

            Assert.Empty(delta.Changes);
        }

        [Fact]
		public void ConstantsAdded_Ignores_When_Constant_Removed()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithConstant("int", "MyInt", "1")
										.WithConstant("string", "MyField", "\"Hello World\"");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithConstant("int", "MyInt", "1");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new ConstantsAdded())
				.Build();

            Assert.Empty(delta.Changes);
        }

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

            Assert.Single(delta.Changes);
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

            Assert.Single(delta.Changes);
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

            Assert.Empty(delta.Changes);
        }

        [Fact]
		public void ConstantsRemoved_Identifies_When_Removing_Constant()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithConstant("string", "MyField", "\"Hello World\"");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new ConstantsRemoved())
				.Build();

            Assert.Single(delta.Changes);
        }

        [Fact]
		public void ConstantsRemoved_Identifies_When_Const_Removed_From_Field()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithConstant("string", "MyField", "\"Hello World\"");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithField("string", "MyField", "\"Hello World\"");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new ConstantsRemoved())
				.Build();

            Assert.Single(delta.Changes);
        }

        [Fact]
		public void ConstantsRemoved_Ignores_When_No_Constants()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new ConstantsRemoved())
				.Build();

            Assert.Empty(delta.Changes);
        }

        [Fact]
		public void ConstantsRemoved_Ignores_When_Constants_Same()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithConstant("int", "MyInt", "1")
										.WithConstant("string", "MyField", "\"Hello World\"");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithConstant("int", "MyInt", "1")
										.WithConstant("string", "MyField", "\"Hello World\"");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new ConstantsRemoved())
				.Build();

            Assert.Empty(delta.Changes);
        }

        [Fact]
		public void ConstantsRemoved_Ignores_When_Constant_Added()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithConstant("string", "MyField", "\"Hello World\"");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithConstant("string", "MyField", "\"Hello World\"")
										.WithConstant("int", "MyInt", "1");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new ConstantsRemoved())
				.Build();

            Assert.Empty(delta.Changes);
        }

        [Fact]
		public void ConstantsChanged_Identifies_When_Constant_Changes_Type()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithConstant("int", "MyInt", "1")
										.WithConstant("string", "MyField", "\"Hello World\"");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithConstant("string", "MyField", "\"Hello World\"")
										.WithConstant("double", "MyInt", "1.0");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new ConstantsChanged())
				.Build();

            Assert.Single(delta.Changes);
        }

        [Fact]
		public void ConstantsChanged_Ignores_When_Constant_Name_Changes()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithConstant("int", "MyInt", "1")
										.WithConstant("string", "MyField", "\"Hello World\"");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithConstant("string", "MyField", "\"Hello World\"")
										.WithConstant("int", "MyOtherInt", "1");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new ConstantsChanged())
				.Build();

            Assert.Empty(delta.Changes);
        }

    }
}
