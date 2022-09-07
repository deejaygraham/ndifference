using System;

namespace NDifference.Reporting
{
    [Obsolete("Value Descriptor not used")]
    public class ValueDescriptor : Descriptor, IValueDescriptor
	{
        //public ValueDescriptor()
        //{
        //    this.ColumnNames = new string[]
        //    {
        //        "Value"
        //    };
        //}

		public object Value { get; set; }
	}
}
