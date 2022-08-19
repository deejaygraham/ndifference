using System;

namespace NDifference.Analysis
{
    /// <summary>
    /// Severity of the discovered change
    /// </summary>
    [Serializable]
    [Flags]
    public enum Severity
    {
        /// <summary>
        /// Not known
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Won't break dependents
        /// </summary>
        NonBreaking = 1,
        /// <summary>
        /// Could break dependents
        /// </summary>
        PotentiallyBreakingChange = 2,
        /// <summary>
        /// Definitely will break depedent code.
        /// </summary>
        BreakingChange = 4
    }

}
