using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Reporting
{
	public class HtmlStyleReset
	{
		public override string ToString()
		{
			var styleBuilder = new StringBuilder();

			styleBuilder.AppendLine("<style type=\"text/css\">");

			styleBuilder.AppendLine("html,body,div,h1,h2,h3,h4,h5,h6,p,img,dl,dt,dd,ol,ul,li,table,caption,tbody,tfoot,thead,tr,th,td,form,fieldset,embed,object,applet { margin: 0; padding: 0; border: 0; }");

			styleBuilder.AppendLine("</style>");

			return styleBuilder.ToString();
		}
	}
}
