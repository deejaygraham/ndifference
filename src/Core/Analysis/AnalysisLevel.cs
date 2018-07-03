using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Analysis
{
    [Serializable]
    public enum AnalysisLevel
    {
        Unknown,
        Summary,
        Assembly,
        Type,
        Member
    }
}
