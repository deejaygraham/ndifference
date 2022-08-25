namespace NDifference.SourceFormatting
{
    public class TypeNameTag : SourceCodeTag
	{
		public TypeNameTag()
		{
		}

		public TypeNameTag(string name)
			: base("tn", name) //.Replace("<", "&lt;").Replace(">", "&gt;"))
		{
		}
	}
}
