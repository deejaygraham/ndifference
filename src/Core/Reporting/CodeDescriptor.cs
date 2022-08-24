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
    public class CodeDescriptor : ICodeDescriptor
	{
		public ICoded Code { get; set; }

        public string Reason { get; set; }

        public int Columns
        {
            get
            {
                int columns = 1;

                if (!String.IsNullOrEmpty(this.TypeName))
                    columns++;

                if (!String.IsNullOrEmpty(this.AssemblyName))
                    columns++;

                return columns;
            }
        }

        public IEnumerable<string> ColumnNames
        {
            get
            {
                return new string[] { "Change" };
            }
        }

        public string TypeName { get; set; }

        public string AssemblyName { get; set; }
    }
}
