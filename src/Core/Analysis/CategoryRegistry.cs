using System.Collections.Generic;
using System.Diagnostics;

namespace NDifference.Analysis
{
    public class CategoryRegistry
    {
        private Dictionary<int, Category> categories = new Dictionary<int, Category>();

        public CategoryRegistry()
        {
            Register(WellKnownAssemblyCategories.AddedReferences);
            Register(WellKnownAssemblyCategories.AddedTypes);
            Register(WellKnownAssemblyCategories.AssemblyInternal);
            Register(WellKnownAssemblyCategories.ChangedTypes);
            Register(WellKnownAssemblyCategories.ObsoleteTypes);
            Register(WellKnownAssemblyCategories.PotentiallyChangedTypes);
            Register(WellKnownAssemblyCategories.RemovedReferences);
            Register(WellKnownAssemblyCategories.RemovedTypes);

            Register(WellKnownSummaryCategories.RemovedAssemblies);
            Register(WellKnownSummaryCategories.ChangedAssemblies);
            Register(WellKnownSummaryCategories.PotentiallyChangedAssemblies);
            Register(WellKnownSummaryCategories.AddedAssemblies);
            Register(WellKnownSummaryCategories.BreakingChanges);

            Register(WellKnownTypeCategories.TypeInternal);
            Register(WellKnownTypeCategories.ConstantsRemoved);
            Register(WellKnownTypeCategories.ConstantsObsolete);
            Register(WellKnownTypeCategories.ConstantsChanged);
            Register(WellKnownTypeCategories.ConstantsAdded);
            Register(WellKnownTypeCategories.FieldsRemoved);
            Register(WellKnownTypeCategories.FieldsObsolete);
            Register(WellKnownTypeCategories.FieldsChanged);
            Register(WellKnownTypeCategories.FieldsAdded);
            Register(WellKnownTypeCategories.ConstructorsRemoved);
            Register(WellKnownTypeCategories.ConstructorsObsolete);
            Register(WellKnownTypeCategories.ConstructorsChanged);
            Register(WellKnownTypeCategories.ConstructorsAdded);
            Register(WellKnownTypeCategories.FinalizersRemoved);
            Register(WellKnownTypeCategories.FinalizersObsolete);
            Register(WellKnownTypeCategories.FinalizersChanged);
            Register(WellKnownTypeCategories.FinalizersAdded);
            Register(WellKnownTypeCategories.DelegatesRemoved);
            Register(WellKnownTypeCategories.DelegatesObsolete);
            Register(WellKnownTypeCategories.DelegatesChanged);
            Register(WellKnownTypeCategories.DelegatesAdded);
            Register(WellKnownTypeCategories.EventsRemoved);
            Register(WellKnownTypeCategories.EventsObsolete);
            Register(WellKnownTypeCategories.EventsChanged);
            Register(WellKnownTypeCategories.EventsAdded);
            Register(WellKnownTypeCategories.PropertiesRemoved);
            Register(WellKnownTypeCategories.PropertiesObsolete);
            Register(WellKnownTypeCategories.PropertiesChanged);
            Register(WellKnownTypeCategories.PropertiesAdded);
            Register(WellKnownTypeCategories.IndexersRemoved);
            Register(WellKnownTypeCategories.IndexersObsolete);
            Register(WellKnownTypeCategories.IndexersChanged);
            Register(WellKnownTypeCategories.IndexersAdded);
            Register(WellKnownTypeCategories.MethodsRemoved);
            Register(WellKnownTypeCategories.MethodsObsolete);
            Register(WellKnownTypeCategories.MethodsChanged);
            Register(WellKnownTypeCategories.MethodsAdded);
            Register(WellKnownTypeCategories.EnumValuesRemoved);
            Register(WellKnownTypeCategories.EnumValuesChanged);
            Register(WellKnownTypeCategories.EnumValuesAdded);
            Register(WellKnownTypeCategories.TypeDebug);
        }

        public void Register(Category c)
        {
            var key = c.Priority.Value;

            Debug.Assert(key > 0, "Category priority not set correctly");

            if (!this.categories.ContainsKey(key))
                this.categories.Add(key, c);
        }

        public Category ForPriority(int priority)
        {
            Debug.Assert(priority > 0, "Priority not set correctly");

            var key = priority;

            if (this.categories.ContainsKey(key))
                return this.categories[key];

            return null;
        }
    }
}
