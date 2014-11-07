using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Reporting
{
	public class DefaultHtml5Style
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
			string veryLightGrey = "#F9F9F9"; // "#eee";
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

			cssReplace.Declare("$(table-zebra-stripe-color)", veryLightGrey);
			cssReplace.Declare("$(table-head-color)", veryLightGrey);
			cssReplace.Declare("$(table-border-color)", "#ddd");

			return cssReplace.Process(styleBuilder.ToString());
		}
	}

	public class HtmlTypographyStyling
	{
		public override string ToString()
		{
			var styleBuilder = new StringBuilder();
			styleBuilder.AppendLine("<style type=\"text/css\">");

			styleBuilder.AppendLine("body { font-family: $(body-font-family); font-size: $(body-font-size); }");

			styleBuilder.AppendLine("table { font-size: $(table-font-size); }");

			styleBuilder.AppendLine("code { font-family: $(code-font-family); }");

			styleBuilder.AppendLine("</style>");

			return styleBuilder.ToString();
		}
	}

	public class HtmlColourStyling
	{
		public override string ToString()
		{
			var styleBuilder = new StringBuilder();
			styleBuilder.AppendLine("<style type=\"text/css\">");

			styleBuilder.AppendLine("html, body { background-color: $(body-background-color); }");
			styleBuilder.AppendLine("body { color: $(body-color); }");

			styleBuilder.AppendLine("header > hgroup, #header { background-color: $(header-background-color); color: $(header-color); }");

			styleBuilder.AppendLine("section > p, nav { color: $(section-color); background-color: $(section-background-color); }");

			styleBuilder.AppendLine("th, td { background-color:inherit; }");
			styleBuilder.AppendLine("th { background-color: $(th-background-color); }"); 
			styleBuilder.AppendLine(".even { background-color: $(even-background-color); }");

			styleBuilder.AppendLine("a:link { color: $(anchor-link-color); }");
			styleBuilder.AppendLine("a:visited { color: $(anchor-visited-color); }");

			styleBuilder.AppendLine("footer, #footer { border-top: 1px solid $(footer-border-top); background-color: $(footer-background-color); }");

			styleBuilder.AppendLine("nav a:link, nav a:visited, #header a:link, #header a:visited { color: $(nav-link-color); }");

			styleBuilder.AppendLine("footer > nav, #footer > nav { background-color: $(footer-nav-background-color); color: $(footer-nav-color); }");
			styleBuilder.AppendLine("footer > nav a:link, footer > nav a:visited, #footer > nav a:link, #footer > nav a:visited { color: $(footer-nav-link-color); }");

			styleBuilder.AppendLine(".kw { color: $(kw-color); }");
			styleBuilder.AppendLine(".tn { color: $(tn-color); }");
			styleBuilder.AppendLine(".punc { color: $(punc-color); }");
			styleBuilder.AppendLine(".ident { color: $(ident-color); }");
			styleBuilder.AppendLine("table.information { border-left: 12px solid $(table-information-border-left-color); }");
			styleBuilder.AppendLine("table.warning { border-left: 12px solid $(table-warning-border-left-color); }");
			styleBuilder.AppendLine("table.error { border-left: 12px solid $(table-error-border-left-color); }");
			styleBuilder.AppendLine("table.critical { border-left: 12px solid $(table-critical-border-left-color); }");

			// zebra stripe tables.
			styleBuilder.AppendLine("tbody tr:nth-child(even) { background-color: $(table-zebra-stripe-color); }");
			styleBuilder.AppendLine(".diff-table td { border: 1px solid $(table-border-color); }");
			styleBuilder.AppendLine(".diff-table th { border: 1px solid $(table-border-color); background-color: $(table-head-color); }");

			styleBuilder.AppendLine("</style>");

			return styleBuilder.ToString();
		}
	}

	public class HtmlLayoutStyling
	{
		public override string ToString()
		{
			var styleBuilder = new StringBuilder();
			styleBuilder.AppendLine("<style type=\"text/css\">");

			styleBuilder.AppendLine("#container { padding: 1em; }");
			styleBuilder.AppendLine("header, #header { margin-bottom: 0; }");
			styleBuilder.AppendLine("header > hgroup, #header { padding: 2em; }");

			styleBuilder.AppendLine("section > p, nav { padding: 1em; }");
			styleBuilder.AppendLine("section, nav { margin-bottom: 2em; }");

			styleBuilder.AppendLine("article { padding: 1em; }");

			styleBuilder.AppendLine("table { border-collapse:collapse; border-width:0; empty-cells:show; margin:0 0 1em; padding:0; width: 100%; }");
			styleBuilder.AppendLine("th, td { padding:6px 12px; text-align:left; vertical-align:top; }");
			styleBuilder.AppendLine("td > p:last-child { margin:0; }");
			styleBuilder.AppendLine("caption { display: none; }");

			styleBuilder.AppendLine("a:link { text-decoration: none; }");
			styleBuilder.AppendLine("a:hover { text-decoration: underline; }");
			styleBuilder.AppendLine("a:active { text-decoration: underline; }");

			styleBuilder.AppendLine("footer > p, #footer > p { text-align: center; }");
			styleBuilder.AppendLine("footer, #footer { padding: 0 1em 2em 0; }");

			styleBuilder.AppendLine("nav { margin: 0; }");
			styleBuilder.AppendLine("ul, li { display: inline; margin-right: 0.5em; }");

			styleBuilder.AppendLine(".kw { margin-left: 0.2em; margin-right: 0.2em; }");
			styleBuilder.AppendLine(".tn { padding-left: 0.2em; padding-right: 0.2em; }");
			styleBuilder.AppendLine(".ident { padding-left: 0.2em; padding-right: 0.2em; }");

			styleBuilder.AppendLine(".diff-container { padding: 2em; }");

			styleBuilder.AppendLine("</style>");

			return styleBuilder.ToString();
		}
	}

	public class HtmlStyleDebug
	{
		public override string ToString()
		{
			var styleBuilder = new StringBuilder();
			styleBuilder.AppendLine("<style type=\"text/css\">");

			styleBuilder.AppendLine("div:empty, span:empty,");
			styleBuilder.AppendLine("li:empty, p:empty,");
			styleBuilder.AppendLine("td:empty, th:empty {padding: 0.5em; background: yellow;}");

			styleBuilder.AppendLine("*[style], font, center {outline: 5px solid red;}");
			styleBuilder.AppendLine("*[class=\"\"], *[id=\"\"] {outline: 5px dotted red;}");

			styleBuilder.AppendLine("img[alt=\"\"] {border: 3px dotted red;}");
			styleBuilder.AppendLine("img:not([alt]) {border: 5px solid red;}");
			styleBuilder.AppendLine("img[title=\"\"] {outline: 3px dotted fuchsia;}");
			styleBuilder.AppendLine("img:not([title]) {outline: 5px solid fuchsia;}");

			styleBuilder.AppendLine("table:not([summary]) {outline: 5px solid red;}");
			styleBuilder.AppendLine("table[summary=\"\"] {outline: 3px dotted red;}");
			styleBuilder.AppendLine("th {border: 2px solid red;}");
			styleBuilder.AppendLine("th[scope=\"col\"], th[scope=\"row\"] {border: none;}");

			styleBuilder.AppendLine("a[href]:not([title]) {border: 5px solid red;}");
			styleBuilder.AppendLine("a[title=\"\"] {outline: 3px dotted red;}");
			styleBuilder.AppendLine("a[href=\"#\"] {background: lime;}");
			styleBuilder.AppendLine("a[href=\"\"] {background: fuchsia;}");

			styleBuilder.AppendLine("</style>");

			return styleBuilder.ToString();
		}
	}

}
