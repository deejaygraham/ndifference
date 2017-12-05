using NDifference.Inspectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NDifference.UnitTests.Inspectors.Types
{
	public class MemberFieldFacts
	{
		[Fact]
		public void FieldsAdded_Identifies_When_Adding_New_Field()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithField("string", "MyField", "\"Hello World\"");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new FieldsAdded())
				.Build();

            Assert.Single(delta.Changes);
        }

        [Fact]
		public void ConstantsAdded_Identifies_When_Existing_Constant_Made_Field()
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
				.InspectedBy(new FieldsAdded())
				.Build();

            Assert.Single(delta.Changes);
        }

        [Fact]
		public void FieldsAdded_Ignores_When_No_Fields()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new FieldsAdded())
				.Build();

            Assert.Empty(delta.Changes);
        }

        [Fact]
		public void FieldsAdded_Ignores_When_Fields_Unchanged()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithField("int", "MyInt", "1")
										.WithField("string", "MyField", "\"Hello World\"");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithField("int", "MyInt", "1")
										.WithField("string", "MyField", "\"Hello World\"");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new FieldsAdded())
				.Build();

            Assert.Empty(delta.Changes);
        }

        [Fact]
		public void FieldsAdded_Ignores_When_Field_Removed()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithField("int", "MyInt", "1")
										.WithField("string", "MyField", "\"Hello World\"");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithField("int", "MyInt", "1");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new FieldsAdded())
				.Build();

            Assert.Empty(delta.Changes);
        }

        [Fact]
		public void FieldsObsolete_Identifies_When_Field_Made_Obsolete()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithField("string", "MyField", "\"Hello World\"");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithField("[Obsolete] public string MyField = \"Hello World\";");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new FieldsObsolete())
				.Build();

            Assert.Single(delta.Changes);
        }

        [Fact]
		public void FieldsObsolete_Identifies_When_Field_Made_Obsolete_With_Message()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithField("string", "MyField", "\"Hello World\"");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithField("[Obsolete(\"Do not use\")] public string MyField = \"Hello World\";");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new FieldsObsolete())
				.Build();

            Assert.Single(delta.Changes);
        }

        [Fact]
		public void FieldsObsolete_Ignores_When_Fields_Not_Obsolete()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithField("string", "MyField", "\"Hello World\"");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithField("string", "MyField", "\"Hello World\"");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new FieldsObsolete())
				.Build();

            Assert.Empty(delta.Changes);
        }

        [Fact]
		public void FieldsRemoved_Identifies_When_Removing_Field()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithField("string", "MyField", "\"Hello World\"");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new FieldsRemoved())
				.Build();

            Assert.Single(delta.Changes);
        }

        [Fact]
		public void FieldsRemoved_Ignores_When_No_Fields()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new FieldsRemoved())
				.Build();

            Assert.Empty(delta.Changes);
        }

        [Fact]
		public void FieldsRemoved_Ignores_When_Fields_Unchanged()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithField("int", "MyInt", "1")
										.WithField("string", "MyField", "\"Hello World\"");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithField("int", "MyInt", "1")
										.WithField("string", "MyField", "\"Hello World\"");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new FieldsRemoved())
				.Build();

			Assert.Empty(delta.Changes);
		}

		[Fact]
		public void FieldsRemoved_Ignores_When_Field_Added()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithField("string", "MyField", "\"Hello World\"");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithField("string", "MyField", "\"Hello World\"")
										.WithField("int", "MyInt", "1");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new FieldsRemoved())
				.Build();

            Assert.Empty(delta.Changes);
        }

        [Fact]
		public void FieldsChanged_Identifies_When_Field_Changes_Type()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithField("int", "MyInt", "1")
										.WithField("string", "MyField", "\"Hello World\"");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithField("string", "MyField", "\"Hello World\"")
										.WithField("double", "MyInt", "1.0");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new FieldsChanged())
				.Build();

            Assert.Single(delta.Changes);
        }

        [Fact]
		public void FieldsChanged_Ignores_When_Field_Name_Changes()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithField("int", "MyInt", "1")
										.WithField("string", "MyField", "\"Hello World\"");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithField("string", "MyField", "\"Hello World\"")
										.WithField("int", "MyOtherInt", "1");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new FieldsChanged())
				.Build();

            Assert.Empty(delta.Changes);
        }

    }
}
