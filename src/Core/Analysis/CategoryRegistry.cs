using System.Collections.Generic;
using System.Diagnostics;

namespace NDifference.Analysis
{
    public class CategoryRegistry
    {
        private Dictionary<int, Category> categories = new Dictionary<int, Category>();

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
