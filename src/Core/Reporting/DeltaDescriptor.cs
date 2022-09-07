namespace NDifference.Reporting
{
    /// <summary>
    /// Describes a change from a "was" state to an 
    /// "is now" state. With optional descriptive text.
    /// </summary>
    public class DeltaDescriptor : Descriptor, IDeltaDescriptor
	{
        //public DeltaDescriptor()
        //{
        //    this.ColumnNames = new string[]
        //    {
        //        "Was",
        //        "Is Now",
        //        "Reason"
        //    };
        //}

        public string Was { get; set; }

		public string IsNow { get; set; }
	}
}
