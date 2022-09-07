namespace NDifference.Reporting
{
    public class DocumentLink : Descriptor, IDocumentLink
	{
        //public DocumentLink()
        //{
        //    this.ColumnNames = new string[]
        //    {
        //    };
        //}

        public string Identifier { get; set; }

		public string LinkText { get; set; }

		public string LinkUrl { get; set; }
	}
}
