using System;

namespace NDifference.Reporting
{
    /// <summary>
    /// Describes a change that includes a name, previous value and current value.
    /// </summary>
    [Obsolete("Use DeltaDescriptor with Description field")]
	public class NamedDeltaDescriptor : Descriptor, INamedDeltaDescriptor
    {
        //public NamedDeltaDescriptor()
        //{
        //    this.ColumnNames = new string[]
        //    {
        //        "Name",
        //        "Was",
        //        "IsNow",
        //        "Reason"
        //    };
        //}

        public string Name { get; set; }

		public string Was { get; set; }

		public string IsNow { get; set; }
	}
}
