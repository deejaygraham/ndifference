//using NDifference.Inspectors;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit;

//namespace NDifference.UnitTests.Inspectors.Assemblies
//{
//	public class ArchitectureChangeInspectorFacts
//	{
//		[Fact]
//		public void ArchitectureChangeInspector_Identifies_When_x32_Changes_to_x64()
//		{
//			var oldClassBuilder = CompilableClassBuilder.PublicClass()
//										.Named("Account")
//										.WithProperty("public string Text { get; set; }");

//			var newClassBuilder = CompilableClassBuilder.PublicClass()
//										.Named("Account")
//										.WithProperty("public string Text { get; set; }");

//			var delta = IdentifiedChangeCollectionBuilder.Changes()
//				//.From(oldClassBuilder)
//				.To(newClassBuilder)
//				.InspectedBy(new ArchitectureChangeInspector())
//				.Build();

//			Assert.Single(delta.Changes);
//		}
//	}

//}
