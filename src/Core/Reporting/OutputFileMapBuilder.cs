using NDifference.Analysis;
using NDifference.Projects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Reporting
{
	public static class OutputFileMapBuilder
	{
		public static OutputFileMap BuildFor(IdentifiedChangeCollection assemblyChanges, Project project, IReportFormat format)
		{
			Debug.Assert(assemblyChanges != null, "No superficial changes");
			Debug.Assert(format != null, "No format specified");
			Debug.Assert(project != null, "No project specified");

			var map = new OutputFileMap
			{
				IndexFolder = project.Settings.IndexPath
			};

			string summaryPagePath = project.Settings.SuggestIndexPath(format.Extension);
			map.Add(assemblyChanges.Identifier, summaryPagePath);

			foreach(var change in assemblyChanges.Changes)
			{
				object descriptor = change.Descriptor;

				if (descriptor != null)
				{
					IDocumentLink link = descriptor as IDocumentLink;

					if (link != null)
					{
						string pagePath = project.Settings.SuggestPath(link.LinkUrl, format.Extension);
						map.Add(link.Identifier, pagePath);
					}
				}

				if (change.Descriptor != null)
				{

				}
			}

			return map;
		}
	}
}
