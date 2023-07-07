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
        /// Breaking Change pre-existing, corresponds to Nag mode.
        /// </summary>
        LegacyBreakingChange = 4,
        /// <summary>
        /// Definitely will break dependent code.
        /// </summary>
        BreakingChange = 8
    }

}
