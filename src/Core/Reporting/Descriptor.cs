using System.Collections.Generic;

namespace NDifference.Reporting
{
    public abstract class Descriptor
    {
        public string Reason { get; set; }

        //public string TypeName { get; set; }

        //public string AssemblyName { get; set; }

        //public bool Consolidated { get; set; }

        //private List<string> columnNames = new List<string>();

        //public IEnumerable<string> ColumnNames
        //{
        //    get
        //    {
        //        if (this.Consolidated)
        //        {
        //            var completeList = new List<string>(columnNames);
        //            completeList.Add("Type");
        //            completeList.Add("Assembly");

        //            return completeList;
        //        }

        //        return columnNames;
        //    }
        //    set
        //    {
        //        if (value == null)
        //            columnNames = new List<string>();
        //        else
        //            columnNames = new List<string>(value);
        //    }
        //}
    }
}
