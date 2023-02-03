namespace NDifference.Reporting
{
    /// <summary>
    /// Two columns of information, the name of something and it's current value.
    /// </summary>
    public class NameValueDescriptor : INameValueDescriptor
	{
        public string Name { get; set; }

		public object Value { get; set; }

        public string Reason { get; set; }

        public int DataItemCount { get { return 3; } }
    }
}
