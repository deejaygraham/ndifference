using NDifference.Inspectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NDifference.UnitTests.Inspectors.Types
{
	public class MemberEventsFacts
	{
		[Fact]
		public void EventsAdded_Identifies_When_Adding_New_Event()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithEvent("public event EventHandler SaveComplete;");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new EventsAdded())
				.Build();

			Assert.Equal(1, delta.Changes.Count);
		}

		[Fact]
		public void EventsAdded_Ignores_When_No_Events()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new EventsAdded())
				.Build();

			Assert.Equal(0, delta.Changes.Count);
		}

		[Fact]
		public void EventsAdded_Ignores_When_Events_Unchanged()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithEvent("public event EventHandler SaveComplete;");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithEvent("public event EventHandler SaveComplete;");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new EventsAdded())
				.Build();

			Assert.Equal(0, delta.Changes.Count);
		}

		[Fact]
		public void EventsAdded_Ignores_When_Event_Removed()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithEvent("public event EventHandler OpenComplete;")
										.WithEvent("public event EventHandler SaveComplete;");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithEvent("public event EventHandler SaveComplete;");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new EventsAdded())
				.Build();

			Assert.Equal(0, delta.Changes.Count);
		}

		[Fact]
		public void EventsObsolete_Identifies_When_Event_Made_Obsolete()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithEvent("public event EventHandler SaveComplete;");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithEvent("[Obsolete] public event EventHandler SaveComplete;");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new EventsObsolete())
				.Build();

			Assert.Equal(1, delta.Changes.Count);
		}

		[Fact]
		public void EventsObsolete_Identifies_When_Event_Made_Obsolete_With_Message()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithEvent("public event EventHandler SaveComplete;");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithEvent("[Obsolete(\"Do not use\")] public event EventHandler SaveComplete;");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new EventsObsolete())
				.Build();

			Assert.Equal(1, delta.Changes.Count);
		}

		[Fact]
		public void EventsObsolete_Ignores_When_Events_Not_Obsolete()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithEvent("public event EventHandler SaveComplete;");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithEvent("public event EventHandler SaveComplete;");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new EventsObsolete())
				.Build();

			Assert.Empty(delta.Changes);
		}

		[Fact]
		public void EventsRemoved_Identifies_When_Removing_Event()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithEvent("public event EventHandler SaveComplete;");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new EventsRemoved())
				.Build();

			Assert.Single(delta.Changes);
		}

		[Fact]
		public void EventsRemoved_Ignores_When_No_Events()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new EventsRemoved())
				.Build();

			Assert.Empty(delta.Changes);
		}

		[Fact]
		public void EventsRemoved_Ignores_When_Events_Unchanged()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithEvent("public event EventHandler SaveComplete;");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithEvent("public event EventHandler SaveComplete;");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new EventsRemoved())
				.Build();

			Assert.Empty(delta.Changes);
		}

		[Fact]
		public void EventsRemoved_Ignores_When_Event_Added()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithEvent("public event EventHandler OpenComplete;");


			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithEvent("public event EventHandler OpenComplete;")
										.WithEvent("public event EventHandler SaveComplete;");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new EventsRemoved())
				.Build();

			Assert.Empty(delta.Changes);
		}

		[Fact]
		public void EventsChanged_Identifies_When_Event_Changes_Type()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithEvent("public event EventHandler OpenComplete;");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithEvent("public event EventHandler<EventArgs> OpenComplete;");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new EventsChanged())
				.Build();

			Assert.Single(delta.Changes);
		}

		[Fact]
		public void EventsChanged_Ignores_When_Event_Name_Changes()
		{
			var oldClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithEvent("public event EventHandler OpenComplete;");

			var newClassBuilder = CompilableClassBuilder.PublicClass()
										.Named("Account")
										.WithEvent("public event EventHandler OpenComplete2;");

			var delta = IdentifiedChangeCollectionBuilder.Changes()
				.From(oldClassBuilder)
				.To(newClassBuilder)
				.InspectedBy(new EventsChanged())
				.Build();

			Assert.Empty(delta.Changes);
		}

	}

}
