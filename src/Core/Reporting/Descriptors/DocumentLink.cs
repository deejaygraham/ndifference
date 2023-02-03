﻿namespace NDifference.Reporting
{
    public class DocumentLink : IDocumentLink
	{
        public string Identifier { get; set; }

		public string LinkText { get; set; }

		public string LinkUrl { get; set; }

        public int DataItemCount { get { return 1; } }
    }
}
