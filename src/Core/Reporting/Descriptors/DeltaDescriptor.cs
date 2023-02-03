namespace NDifference.Reporting
{
    /// <summary>
    /// Describes a change from a "was" state to an 
    /// "is now" state. With optional descriptive text.
    /// </summary>
    public class DeltaDescriptor : IDeltaDescriptor
	{
        public string Was { get; set; }

		public string IsNow { get; set; }

        public string Reason { get; set; }

        public int DataItemCount { get { return 3; } }

    }
}
