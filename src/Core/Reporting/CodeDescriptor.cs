using System;
using System.Collections.Generic;
using NDifference.SourceFormatting;

namespace NDifference.Reporting
{
    /// <summary>
    /// Descriptor containing code only, used e.g. in a list of 
    /// properties added to a class where the signature is the 
    /// only thing of interest.
    /// </summary>
    public class CodeDescriptor : Descriptor, ICodeDescriptor
	{
        //public CodeDescriptor()
        //{
        //    this.ColumnNames = new string[]
        //    {
        //        "Signature",
        //        "Reason"
        //    };
        //}

        public ICoded Code { get; set; }
    }
}
