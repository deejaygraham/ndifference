using NDifference.Analysis;
using NDifference.Projects;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace NDifference.Reporting
{
	public class FileMapBuilder
	{
		public static FileMapBuilder Map()
		{
			return new FileMapBuilder();
		}

		public FileMapBuilder()
		{
			this.map = new FileMap();
		}

		private FileMap map;

		private Project project;

		private IReportFormat format;

		public FileMapBuilder UsingProject(Project p)
		{
			this.project = p;

			return this;
		}

		public FileMapBuilder As(IReportFormat fmt)
		{
			this.format = fmt;

			return this;
		}

		public FileMapBuilder With(IDocumentLink link)
		{
			Debug.Assert(this.map != null, "Map not created");
			Debug.Assert(this.project != null, "Project not set");

			string pagePath = this.project.Settings.SuggestPath(link.LinkUrl.PathSafeTypeName(), this.format.Extension);
			map.Add(link.Identifier, new PhysicalFile(pagePath));

			return this;
		}

		public FileMapBuilder With(IdentifiedChange change)
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

		public FileMapBuilder With(IEnumerable<IdentifiedChangeCollection> changes)
		{
			foreach (var change in changes)
			{
				this.With(change);
			}

			return this;
		}

		public FileMapBuilder With(IdentifiedChangeCollection change)
		{
			Debug.Assert(this.map != null, "Map not created");
			Debug.Assert(this.project != null, "Project not set");

			string parentPath = project.Settings.SuggestPath(change.Name.PathSafeTypeName(), format.Extension);
			map.Add(change.Identifier, new PhysicalFile(parentPath));

			foreach (var c in change.Changes)
			{
				object descriptor = c.Descriptor;

				if (descriptor != null)
				{
					IDocumentLink link = descriptor as IDocumentLink;

					if (link != null)
					{
						string pagePath = project.Settings.SuggestPath(link.LinkUrl.PathSafeTypeName(), format.Extension);
						map.Add(link.Identifier, new PhysicalFile(pagePath));
					}
				}
			}

			return this;
		}

		public FileMapBuilder WithIndex(string indexIdentifier)
		{
			Debug.Assert(this.map != null, "Map not created");
			Debug.Assert(this.project != null, "Project not set");

			Debug.Assert(!String.IsNullOrEmpty(indexIdentifier), "Index id cannot be blank");

			string summaryPagePath = this.project.Settings.SuggestIndexPath(this.format.Extension);
			map.Add(indexIdentifier, new PhysicalFile(summaryPagePath));

			return this;
		}

		public FileMap Build()
		{
			return this.map;
		}
	}
}
