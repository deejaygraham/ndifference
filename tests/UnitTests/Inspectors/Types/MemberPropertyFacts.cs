using NDifference.Inspectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NDifference.UnitTests.Inspectors.Types
{
	public class MemberPropertyFacts
	{
		[Fact]
		public void PropertiesRemoved_Identifies_Single_Property_Removed()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithProperty("public string Text { get; set; }");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new PropertiesRemoved())
				.Build();

			Assert.Equal(1, delta.Changes.Count);
		}
		
        [Fact]
        public void PropertiesAdded_Identifies_Single_Property_Added()
        {
            var oldClassBuilder = CompilableClassBuilder.PublicClass()
                                        .Named("Account");

            var newClassBuilder = CompilableClassBuilder.PublicClass()
                                        .Named("Account")
                                        .WithProperty("public string Text { get; set; }");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new PropertiesAdded())
				.Build();

			Assert.Equal(1, delta.Changes.Count);
		}

        [Fact]
		public void PropertiesChanged_Identifies_Removing_Public_Setter()
        {
            var oldClassBuilder = CompilableClassBuilder.PublicClass()
                                        .Named("Account")
                                        .WithProperty("public string Text { get; set; }");

            var newClassBuilder = CompilableClassBuilder.PublicClass()
                                        .Named("Account")
                                        .WithProperty("public string Text { get; private set; }");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new PropertiesChanged())
				.Build();

			Assert.Equal(1, delta.Changes.Count);
		}

        
        [Fact]
		public void PropertiesChanged_Identifies_Removing_Public_Getter()
        {
            var oldClassBuilder = CompilableClassBuilder.PublicClass()
                                        .Named("Account")
                                        .WithProperty("public string Text { get; set; }");

            var newClassBuilder = CompilableClassBuilder.PublicClass()
                                        .Named("Account")
                                        .WithProperty("public string Text { private get; set; }");


			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new PropertiesChanged())
				.Build();

			Assert.Equal(1, delta.Changes.Count);
		}

        
        [Fact]
        public void PropertiesChanged_Identifies_Type_Change()
        {
            var oldClassBuilder = CompilableClassBuilder.PublicClass()
                                        .Named("Account")
                                        .WithProperty("public string Text { get; set; }");

            var newClassBuilder = CompilableClassBuilder.PublicClass()
                                        .Named("Account")
                                        .WithProperty("public int Text { get; set; }");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new PropertiesChanged())
				.Build();

			Assert.Equal(1, delta.Changes.Count);
		}

		[Fact]
		public void PropertyInspectors_Identify_Changing_Setter_To_Private()
		{
            var oldClassBuilder = CompilableClassBuilder.PublicClass()
                                        .Named("Account")
                                        .WithProperty("public string Text { get; set; }");

            var newClassBuilder = CompilableClassBuilder.PublicClass()
                                        .Named("Account")
                                        .WithProperty("public string Text { get; private set; }");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new PropertiesAdded())
				.InspectedBy(new PropertiesChanged())
				.InspectedBy(new PropertiesRemoved())
				.Build();

			Assert.Equal(1, delta.Changes.Count);
		}
	}
}
