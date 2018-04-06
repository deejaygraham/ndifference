using NDifference.Analysis;
using NDifference.Inspection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Inspectors
{
    public interface IAnalysisInspector : IInspector
    {
        void Inspect(AnalysisResult result);
    }
}
