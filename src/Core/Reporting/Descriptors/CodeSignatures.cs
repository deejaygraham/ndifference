using System;
using System.Collections.Generic;
using NDifference.SourceFormatting;

namespace NDifference.Reporting
{
    public interface ICodeSignature : IDescriptor
    {
        ICoded Signature { get; }

        string Reason { get; }
    }

    /// <summary>
    /// Describes a code level change where the signature of a method, property etc.
    /// has changed from the previous value.
    /// </summary>
    public interface IChangedCodeSignature : IDescriptor
    {
        ICoded Was { get; }

        ICoded IsNow { get; }

        string Reason { get; }
    }

    /// <summary>
    /// Descriptor containing code only, used e.g. in a list of 
    /// properties added to a class where the signature is the 
    /// only thing of interest.
    /// </summary>
    public class RemovedSignature : ICodeSignature
    {

        public ICoded Signature { get; set; }

        public string Reason { get; set; }

        public int DataItemCount { get { return 1; } }
    }

    /// <summary>
    /// Descriptor containing code only, used e.g. in a list of 
    /// properties added to a class where the signature is the 
    /// only thing of interest.
    /// </summary>
    public class AddedSignature : ICodeSignature
    {
        public ICoded Signature { get; set; }

        public string Reason { get; set; }

        public int DataItemCount { get { return 1; } }
    }

    /// <summary>
    /// Describes a code level change from a "was" state to an 
    /// "is now" state. With optional descriptive text.
    /// </summary>
    public class ChangedCodeSignature : IChangedCodeSignature
    {
        // TODO make this string
        public ICoded Was { get; set; }

        // TODO make this string
        public ICoded IsNow { get; set; }

        public string Reason { get; set; }

        public int DataItemCount { get { return 3; } }
    }

    public class ObsoleteSignature : ICodeSignature
    {
        public ICoded Signature { get; set; }

        public string Reason { get; set; }

        public int DataItemCount { get { return 2; } }
    }
}
