namespace NDifference.Reporting
{
    /// <summary>
    /// Two columns of information, the name of something and it's current value.
    /// </summary>
    public class NameValueDescriptor : Descriptor, INameValueDescriptor
	{
        public string Name { get; set; }

		public object Value { get; set; }
	}
}
