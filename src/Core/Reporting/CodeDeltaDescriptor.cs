using NDifference.SourceFormatting;

namespace NDifference.Reporting
{
    /// <summary>
    /// Describes a code level change from a "was" state to an 
    /// "is now" state. With optional descriptive text.
    /// </summary>
    public class CodeDeltaDescriptor : Descriptor, ICodeDeltaDescriptor
	{
        //public CodeDeltaDescriptor()
        //{
        //    this.ColumnNames = new string[]
        //    {
        //        "Was",
        //        "Is Now",
        //        "Reason"
        //    };
        //}

        // TODO make this string
        public ICoded Was { get; set; }

		// TODO make this string
		public ICoded IsNow { get; set; }
	}
}
