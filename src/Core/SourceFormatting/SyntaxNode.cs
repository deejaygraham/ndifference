using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.SourceFormatting
{
	public class SyntaxNode
	{
		public SyntaxNode()
		{
			this.Children = new List<SyntaxNode>();
		}

		public SyntaxKind Kind { get; set; }

		public string Text { get; set; }

		public ICollection<SyntaxNode> Children
		{
			get;
			private set;
		}
	}
}
