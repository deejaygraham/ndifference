namespace NDifference.Reporting
{
    /// <summary>
    /// Single column of information
    /// </summary>
    public class NameDescriptor : Descriptor, INameDescriptor
    {
        public string Name { get; set; }
	}
}
