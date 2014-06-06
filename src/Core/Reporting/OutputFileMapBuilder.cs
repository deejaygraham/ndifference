using NDifference.Analysis;
using NDifference.Projects;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace NDifference.Reporting
{
	public static class OutputFileMapBuilder
	{
		public static OutputFileMap BuildFor(string indexIdentifier, IEnumerable<IdentifiedChangeCollection> allChanges, Project project, IReportFormat format)
		{
			Debug.Assert(!String.IsNullOrEmpty(indexIdentifier), "Index id cannot be blank");
			Debug.Assert(allChanges != null, "No superficial changes");
			Debug.Assert(format != null, "No format specified");
			Debug.Assert(project != null, "No project specified");

			var map = new OutputFileMap
			{
				IndexFolder = project.Settings.IndexPath
			};

			string summaryPagePath = project.Settings.SuggestIndexPath(format.Extension);
			map.Add(indexIdentifier, summaryPagePath);

			foreach(var changeCollection in allChanges)
			{
				string parentPath = project.Settings.SuggestPath(changeCollection.Name, format.Extension);
				map.Add(changeCollection.Name, parentPath);

				foreach (var change in changeCollection.Changes)
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
				}
			}

			return map;
		}
	}
}
