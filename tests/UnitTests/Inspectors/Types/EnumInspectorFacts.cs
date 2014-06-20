using NDifference.Inspectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NDifference.UnitTests
{
	public class EnumInspectorFacts
	{
		[Fact]
		public void EnumInspector_Identifies_When_Enum_Value_Added_At_End()
		{
			var oldEnumBuilder = CompilableEnumBuilder.PublicEnum()
										.Named("MyEnum")
										.WithValue("First")
										.WithValue("Second")
										.WithValue("Third");

			var newEnumBuilder = CompilableEnumBuilder.PublicEnum()
										.Named("MyEnum")
										.WithValue("First")
										.WithValue("Second")
										.WithValue("Third")
										.WithValue("Fourth");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldEnumBuilder)
				.To(newEnumBuilder)
				.InspectedBy(new EnumInspector())
				.Build();

			Assert.Equal(1, delta.Changes.Count);
		}

		[Fact]
		public void EnumInspector_Identifies_When_Enum_Value_Added_At_Beginning()
		{
			var oldEnumBuilder = CompilableEnumBuilder.PublicEnum()
										.Named("MyEnum")
										.WithValue("Second")
										.WithValue("Third");

			var newEnumBuilder = CompilableEnumBuilder.PublicEnum()
										.Named("MyEnum")
										.WithValue("First")
										.WithValue("Second")
										.WithValue("Third");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldEnumBuilder)
				.To(newEnumBuilder)
				.InspectedBy(new EnumInspector())
				.Build();

			Assert.Equal(1, delta.Changes.Count);
		}

		[Fact]
		public void EnumInspector_Identifies_When_Enum_Value_Added_In_Middle()
		{
			var oldEnumBuilder = CompilableEnumBuilder.PublicEnum()
										.Named("MyEnum")
										.WithValue("First")
										.WithValue("Third");

			var newEnumBuilder = CompilableEnumBuilder.PublicEnum()
										.Named("MyEnum")
										.WithValue("First")
										.WithValue("Second")
										.WithValue("Third");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldEnumBuilder)
				.To(newEnumBuilder)
				.InspectedBy(new EnumInspector())
				.Build();

			Assert.Equal(1, delta.Changes.Count);
		}

		[Fact]
		public void EnumInspector_Identifies_When_Enum_Value_Removed()
		{
			var oldEnumBuilder = CompilableEnumBuilder.PublicEnum()
										.Named("MyEnum")
										.WithValue("First")
										.WithValue("Second")
										.WithValue("Third");

			var newEnumBuilder = CompilableEnumBuilder.PublicEnum()
										.Named("MyEnum")
										.WithValue("First")
										.WithValue("Third");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldEnumBuilder)
				.To(newEnumBuilder)
				.InspectedBy(new EnumInspector())
				.Build();

			Assert.Equal(1, delta.Changes.Count);
		}

		[Fact]
		public void EnumInspector_Ignores_When_Enum_Value_Changed()
		{
			var oldEnumBuilder = CompilableEnumBuilder.PublicEnum()
										.Named("MyEnum")
										.WithValue("First", 1)
										.WithValue("Second", 2)
										.WithValue("Third", 3);

			var newEnumBuilder = CompilableEnumBuilder.PublicEnum()
										.Named("MyEnum")
										.WithValue("First", 0)
										.WithValue("Second", 2)
										.WithValue("Third", 3);

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldEnumBuilder)
				.To(newEnumBuilder)
				.InspectedBy(new EnumInspector())
				.Build();

			Assert.Equal(1, delta.Changes.Count);
		}

		[Fact]
		public void EnumInspector_Ignores_When_Enum_Unchanged()
		{
			var oldEnumBuilder = CompilableEnumBuilder.PublicEnum()
										.Named("MyEnum")
										.WithValue("First")
										.WithValue("Second")
										.WithValue("Third");

			var newEnumBuilder = CompilableEnumBuilder.PublicEnum()
										.Named("MyEnum")
										.WithValue("First")
										.WithValue("Second")
										.WithValue("Third");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldEnumBuilder)
				.To(newEnumBuilder)
				.InspectedBy(new EnumInspector())
				.Build();

			Assert.Equal(0, delta.Changes.Count);
		}

	}

}
