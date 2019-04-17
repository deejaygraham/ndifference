using System;

namespace NDifference.Analysis
{
    [Serializable]
    [Flags]
    public enum Severity
    {
        Unknown = 0,
        NonBreaking = 1,
        PotentiallyBreakingChange = 2,
        BreakingChange = 4
    }

}
