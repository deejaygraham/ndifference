using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Reporting
{
	public class DefaultHtmlStyle
	{
		public override string ToString()
		{
			var styleBuilder = new StringBuilder();

			styleBuilder.AppendLine(new HtmlStyleReset().ToString());

			styleBuilder.AppendLine(new HtmlTypographyStyling().ToString());
			styleBuilder.AppendLine(new HtmlColourStyling().ToString());
			styleBuilder.AppendLine(new HtmlLayoutStyling().ToString());

			var cssReplace = new CssVariableRepository();

			string white = "#fff";
			string veryLightGrey = "#eee";
			string lightGrey = "#ccc";
			string darkGrey = "#333";
			string black = "#000";
			string blue = "#00f";
			string turqouise = "#2b91af";

			cssReplace.Declare("$(body-background-color)", white);
			cssReplace.Declare("$(body-color)", darkGrey);
			cssReplace.Declare("$(body-font-family)", "helvetica,arial,sans-serif");
			cssReplace.Declare("$(body-font-size)", "13px");
			cssReplace.Declare("$(header-background-color)", "#3c8dc5");
			cssReplace.Declare("$(header-color)", white);
			cssReplace.Declare("$(section-color)", white);
			cssReplace.Declare("$(section-background-color)", "#6fb7e9");
			cssReplace.Declare("$(table-border-color)", lightGrey);
			cssReplace.Declare("$(th-background-color)", "#fff");
			cssReplace.Declare("$(even-background-color)", "#e9f2f9");
			cssReplace.Declare("$(code-font-family)", "consolas, courier, monospace");
			cssReplace.Declare("$(anchor-link-color)", "#3c8dc5");
			cssReplace.Declare("$(anchor-visited-color)", "#3c8dc5");
			cssReplace.Declare("$(footer-border-top)", lightGrey);
			cssReplace.Declare("$(footer-background-color)", veryLightGrey);
			cssReplace.Declare("$(nav-link-color)", white);
			cssReplace.Declare("$(footer-nav-background-color)", veryLightGrey);
			cssReplace.Declare("$(footer-nav-color)", "#3c8dc5");
			cssReplace.Declare("$(footer-nav-link-color)", "#3c8dc5");
			cssReplace.Declare("$(kw-color)", blue);
			cssReplace.Declare("$(tn-color)", turqouise);
			cssReplace.Declare("$(punc-color)", black);
			cssReplace.Declare("$(ident-color)", black);
			cssReplace.Declare("$(table-font-size)", "1em");
			cssReplace.Declare("$(table-information-border-left-color)", "#00A1CB");
			cssReplace.Declare("$(table-warning-border-left-color)", "#F18D05");
			cssReplace.Declare("$(table-error-border-left-color)", "#E54028");
			cssReplace.Declare("$(table-critical-border-left-color)", "#D70060");

			return cssReplace.Process(styleBuilder.ToString());
		}
	}
}
