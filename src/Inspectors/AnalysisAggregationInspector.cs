using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NDifference.Analysis;
using System.Diagnostics;

namespace NDifference.Inspectors
{
    public class AnalysisAggregationInspector : IAnalysisInspector
    {
        public bool Enabled { get; set; }

        public string DisplayName { get { return "Analyis Aggregation"; } }

        public string Description { get { return "Aggregates results for types and assemblies"; } }

        public string ShortCode {  get { return "AAI_01"; } }

        public void Inspect(AnalysisResult result)
        {
            // look at each assembly identified as having a change - look for changes relating to that and modify the values. 
            // 

            foreach(var a in result.AssemblyLevelChanges)
            {
                foreach(var c in a.Changes.Where(x => x.Category.Identifier == WellKnownAssemblyCategories.PotentiallyChangedTypes.Identifier))
                {
                    if (result.TypeLevelChanges.Any(t => AssembliesMatch(t.SummaryBlocks["Assembly"], a.Name)))
                    {
                        c.Category = WellKnownAssemblyCategories.ChangedTypes;
                    }
                }
            }
        }

        private bool AssembliesMatch(string assembly, string summaryAssembly)
        {
            Debug.Assert(summaryAssembly.EndsWith(".dll"), "Assembly must be a full file name");
            Debug.Assert(!assembly.EndsWith(".dll"), "Summary must be a file name without extension");

            return (assembly + ".dll") == summaryAssembly;
        }
    }
}
