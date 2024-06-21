using NDifference.Analysis;
using NDifference.Inspection;
using NDifference.Inspectors;
using NDifference.Reflection;
using NDifference.TypeSystem;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NDifference.UnitTests
{
	public class IdentifiedChangeCollectionBuilder : IBuildable<IdentifiedChangeCollection>
	{
		private AssemblyReflectorBuilder oldBuilder = new AssemblyReflectorBuilder();

		private AssemblyReflectorBuilder newBuilder = new AssemblyReflectorBuilder();

		private List<IInspector> inspectors = new List<IInspector>();

		private IAssemblyReflectorFactory introspectorFactory = new CecilReflectorFactory();

		public static IdentifiedChangeCollectionBuilder Changes()
		{
			return new IdentifiedChangeCollectionBuilder();
		}

		public IdentifiedChangeCollectionBuilder From(string oldCode)
		{
			this.oldBuilder.Code(oldCode);

			return this;
		}

		public IdentifiedChangeCollectionBuilder From(IBuildToCode oldCode)
		{
			this.oldBuilder.Code(BoilerplateCodeBuilder.BuildFor(oldCode));

			return this;
		}

		public IdentifiedChangeCollectionBuilder HadAssemblyReference(string reference)
		{
			this.oldBuilder.WithAssemblyReference(reference);

			return this;
		}

		public IdentifiedChangeCollectionBuilder To(string newCode)
		{
			this.newBuilder.Code(newCode);

			return this;
		}

		public IdentifiedChangeCollectionBuilder To(IBuildToCode newCode)
		{
			this.newBuilder.Code(BoilerplateCodeBuilder.BuildFor(newCode));

			return this;
		}

		public IdentifiedChangeCollectionBuilder NowHasAssemblyReference(string reference)
		{
			this.newBuilder.WithAssemblyReference(reference);

			return this;
		}

		public IdentifiedChangeCollectionBuilder InspectedBy(IInspector inspector)
		{
			this.inspectors.Add(inspector);

			return this;
		}

		public IdentifiedChangeCollectionBuilder IntrospectedBy(IAssemblyReflectorFactory factory)
		{
			this.introspectorFactory = factory;

			return this;
		}

		public IdentifiedChangeCollection Build()
		{
			Debug.Assert(this.introspectorFactory != null, "Introspector not set");
			Debug.Assert(this.inspectors != null, "Inspectors not set");
			Debug.Assert(this.inspectors.Any(), "Inspectors not set");

			var oldVersion = this.oldBuilder.Build();
			var newVersion = this.newBuilder.Build();

			IdentifiedChangeCollection collection = new IdentifiedChangeCollection();

			// now try strategies to find correct interface to call..

			foreach(var inspector in this.inspectors)
			{ 
				IAssemblyInspector ai = inspector as IAssemblyInspector;

				if (ai != null)
				{
					ai.Inspect(oldVersion.GetAssemblyInfo(), newVersion.GetAssemblyInfo(), collection);
				}
				else
				{
					ITypeCollectionInspector tci = inspector as ITypeCollectionInspector;

					if (tci != null)
					{
						tci.Inspect(CombinedObjectModel.BuildFrom(oldVersion.GetTypes(), newVersion.GetTypes()), collection);
					}

					else
					{
						ITypeInspector ti = inspector as ITypeInspector;

						if (ti == null)
						{
							throw new Exception("Unsupported inspector for unit testing");
						}

						var oldTypes = oldVersion.GetTypes();
						var newTypes = newVersion.GetTypes();

						var commonTypes = oldTypes.InCommonWith(newTypes);
						var fqn = commonTypes.First();

						var comparer = new TypeNameComparer();

						var firstType = oldTypes.FindMatchFor(fqn, comparer);
						var secondType = newTypes.FindMatchFor(fqn, comparer);

						ti.Inspect(firstType, secondType, collection);
					}
				}
			}

			return collection;
		}
	}

}
