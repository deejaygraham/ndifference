using NDifference.Analysis;
using NDifference.Projects;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace NDifference.Reporting
{
	public class OutputFileMapBuilder
	{
		public static OutputFileMapBuilder Map()
		{
			return new OutputFileMapBuilder();
		}

		public OutputFileMapBuilder()
		{
			this.map = new OutputFileMap();
		}

		private OutputFileMap map;

		private Project project;

		private IReportFormat format;

		public OutputFileMapBuilder UsingProject(Project p)
		{
			this.project = p;
			this.map.IndexFolder = project.Settings.IndexPath;

			return this;
		}

		public OutputFileMapBuilder As(IReportFormat fmt)
		{
			this.format = fmt;

			return this;
		}

		public OutputFileMapBuilder With(IDocumentLink link)
		{
			Debug.Assert(this.map != null, "Map not created");
			Debug.Assert(this.project != null, "Project not set");

			string pagePath = this.project.Settings.SuggestPath(link.LinkUrl.HtmlSafeTypeName(), this.format.Extension);
			map.Add(link.Identifier, pagePath);

			return this;
		}

		public OutputFileMapBuilder With(IdentifiedChange change)
		{
			object descriptor = change.Descriptor;

			if (descriptor != null)
			{
				IDocumentLink link = descriptor as IDocumentLink;

				if (link != null)
				{
					return this.With(link);
				}
			}

			return this;
		}

		public OutputFileMapBuilder With(IEnumerable<IdentifiedChangeCollection> changes)
		{
			foreach (var change in changes)
			{
				this.With(change);
			}

			return this;
		}

		public OutputFileMapBuilder With(IdentifiedChangeCollection change)
		{
			Debug.Assert(this.map != null, "Map not created");
			Debug.Assert(this.project != null, "Project not set");

			string parentPath = project.Settings.SuggestPath(change.Name.HtmlSafeTypeName(), format.Extension);
			map.Add(change.Identifier, parentPath);

			foreach (var c in change.Changes)
			{
				object descriptor = c.Descriptor;

				if (descriptor != null)
				{
					IDocumentLink link = descriptor as IDocumentLink;

					if (link != null)
					{
						string pagePath = project.Settings.SuggestPath(link.LinkUrl.HtmlSafeTypeName(), format.Extension);
						map.Add(link.Identifier, pagePath);
					}
				}
			}

			return this;
		}

		public OutputFileMapBuilder WithIndex(string indexIdentifier)
		{
			Debug.Assert(this.map != null, "Map not created");
			Debug.Assert(this.project != null, "Project not set");

			Debug.Assert(!String.IsNullOrEmpty(indexIdentifier), "Index id cannot be blank");

			string summaryPagePath = this.project.Settings.SuggestIndexPath(this.format.Extension);
			map.Add(indexIdentifier, summaryPagePath);

			return this;
		}

		public OutputFileMap Build()
		{
			return this.map;
		}
	}
}
