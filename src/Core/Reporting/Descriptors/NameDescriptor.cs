namespace NDifference.Reporting
{
    /// <summary>
    /// Single column of information
    /// </summary>
    public class NameDescriptor : INameDescriptor
    {
        public string Name { get; set; }

        public string Reason { get; set; }

        public int DataItemCount { get { return 2; } }
    }
}
