using System;

namespace NDifference.Analysis
{
    /// <summary>
    /// The level at which the change was found.
    /// </summary>
    [Serializable]
    [Obsolete("Not used in the code, delete?")]
    public enum AnalysisLevel
    {
        /// <summary>
        /// Not set
        /// </summary>
        Unknown,
        /// <summary>
        /// Top level summary
        /// </summary>
        Summary,
        /// <summary>
        /// Inspecting an individual assembly
        /// </summary>
        Assembly,
        /// <summary>
        /// Inspecting an individual type
        /// </summary>
        Type,
        /// <summary>
        /// Inspecting a member of an individual type.
        /// </summary>
        Member
    }
}
