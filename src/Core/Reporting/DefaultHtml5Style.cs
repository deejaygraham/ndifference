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

			styleBuilder.AppendLine("<style type=\"text/css\">");
			styleBuilder.AppendLine("html,body,div,h1,h2,h3,h4,h5,h6,p,img,dl,dt,dd,ol,ul,li,table,caption,tbody,tfoot,thead,tr,th,td,form,fieldset,embed,object,applet { margin: 0; padding: 0; border: 0; }");

			styleBuilder.AppendLine("html, body { background-color: $(body-background-color); }");
			styleBuilder.AppendLine("body { font-family: $(body-font-family); font-size: $(body-font-size); color: $(body-color); }");
			styleBuilder.AppendLine("#container { padding: 1em; }");

			styleBuilder.AppendLine("header, #header { margin-bottom: 0; }");
			styleBuilder.AppendLine("header > hgroup, #header { background-color: $(header-background-color); color: $(header-color); padding: 1em; }");

			styleBuilder.AppendLine("section > p, nav { padding: 1em; color: $(section-color); background-color: $(section-background-color); }");
			styleBuilder.AppendLine("section, nav { margin-bottom: 2em; }");

			styleBuilder.AppendLine("article { padding: 1em; }");

			styleBuilder.AppendLine("table { border-collapse:collapse; border-width:0; empty-cells:show; font-size:1em; margin:0 0 1em; padding:0; width: 100%; }");
			styleBuilder.AppendLine("th, td { border: 1px solid $(table-border-color); padding:6px 12px; text-align:left; vertical-align:top; background-color:inherit; }");
			styleBuilder.AppendLine("th { background-color: #616161; }"); //b2d1e5; }");
			styleBuilder.AppendLine("td > p:last-child { margin:0; }");
			styleBuilder.AppendLine(".even { background-color: $(even-background-color); }");
			styleBuilder.AppendLine("caption { display: none; }");
			styleBuilder.AppendLine("code { font-family: $(code-font-family); }");

			styleBuilder.AppendLine("a:link { color: $(anchor-link-color); text-decoration: none; }");
			styleBuilder.AppendLine("a:visited { color: $(anchor-visited-color); }");
			styleBuilder.AppendLine("a:hover { text-decoration: underline; }");
			styleBuilder.AppendLine("a:active { text-decoration: underline; }");

			styleBuilder.AppendLine("footer > p, #footer > p { text-align: center; }");
			styleBuilder.AppendLine("footer, #footer { border-top: 1px solid #ccc; background-color: $(footer-background-color); padding: 0 1em 2em 0; }");

			styleBuilder.AppendLine("nav { margin: 0; }");
			styleBuilder.AppendLine("nav a:link, nav a:visited, #header a:link, #header a:visited { color: $(nav-link-color); }");
			styleBuilder.AppendLine("ul, li { display: inline; margin-right: 0.5em; }");

			styleBuilder.AppendLine("footer > nav, #footer > nav { background-color: $(footer-nav-background-color); color: $(footer-nav-color); }");
			styleBuilder.AppendLine("footer > nav a:link, footer > nav a:visited, #footer > nav a:link, #footer > nav a:visited { color: $(footer-nav-link-color); }");

			styleBuilder.AppendLine(".kw { color: $(kw-color); margin-left: 0.2em; margin-right: 0.2em; }");
			styleBuilder.AppendLine(".tn { color: $(tn-color); padding-left: 0.2em; padding-right: 0.2em; }");
			styleBuilder.AppendLine(".punc { color: $(punc-color); }");
			styleBuilder.AppendLine(".ident { color: $(ident-color); padding-left: 0.2em; padding-right: 0.2em; }");
			styleBuilder.AppendLine("table.information { border-left: 12px solid $(table-information-border-left-color); }");
			styleBuilder.AppendLine("table.warning { border-left: 12px solid $(table-warning-border-left-color); }");
			styleBuilder.AppendLine("table.error { border-left: 12px solid $(table-error-border-left-color); }");
			styleBuilder.AppendLine("table.critical { border-left: 12px solid $(table-critical-border-left-color); }");
			styleBuilder.AppendLine("</style>");

			Dictionary<string, string> replacements = new Dictionary<string, string>();

			string white = "#fff";
			string veryLightGrey = "#eee";
			string lightGrey = "#ccc";
			string darkGrey = "#333";
			string black = "#000";
			string blue = "#00f";
			string turqouise = "#2b91af";

			replacements.Add("$(body-background-color)", white);
			replacements.Add("$(body-color)", darkGrey);
			replacements.Add("$(body-font-family)", "helvetica,arial,sans-serif");
			replacements.Add("$(body-font-size)", "13px");
			replacements.Add("$(header-background-color)", "#3c8dc5");
			replacements.Add("$(header-color)", white);
			replacements.Add("$(section-color)", white);
			replacements.Add("$(section-background-color)", "#6fb7e9");
			replacements.Add("$(table-border-color)", lightGrey);
			replacements.Add("$(th-background-color)", "#616161");
			replacements.Add("$(even-background-color)", "#e9f2f9");
			replacements.Add("$(code-font-family)", "consolas, courier, monospace");
			replacements.Add("$(anchor-link-color)", "#3c8dc5");
			replacements.Add("$(anchor-visited-color)", "#3c8dc5");
			replacements.Add("$(footer-border-top)", lightGrey);
			replacements.Add("$(footer-background-color)", veryLightGrey);
			replacements.Add("$(nav-link-color)", white);
			replacements.Add("$(footer-nav-background-color)", veryLightGrey);
			replacements.Add("$(footer-nav-color)", "#3c8dc5");
			replacements.Add("$(footer-nav-link-color)", "#3c8dc5");
			replacements.Add("$(kw-color)", blue);
			replacements.Add("$(tn-color)", turqouise);
			replacements.Add("$(punc-color)", black);
			replacements.Add("$(ident-color)", black);
			replacements.Add("$(table-information-border-left-color)", "#00A1CB");
			replacements.Add("$(table-warning-border-left-color)", "#F18D05");
			replacements.Add("$(table-error-border-left-color)", "#E54028");
			replacements.Add("$(table-critical-border-left-color)", "#D70060");
			
			foreach (var key in replacements.Keys)
			{
				styleBuilder.Replace(key, replacements[key]);
			}

			return styleBuilder.ToString();
		}
	}
}
