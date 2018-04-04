using NDifference.Analysis;
using NDifference.Framework;
using NDifference.Projects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NDifference.Reporting
{
	public class ReportingWorkflow : IReportingWorkflow
	{
		public event EventHandler<CancellableEventArgs> ReportsStarting;

		public event EventHandler<FileProgessEventArgs> ReportStarting;

		public event EventHandler ReportComplete;

		public event EventHandler ReportsComplete;

		public void RunReports(Project project, IReportingRepository reportWriters, AnalysisResult results, IProgress<ProgressValue> progressIndicator)
		{
			try
			{
				//project.Settings.ConsolidateAssemblyTypes = true;
				
				var cancellable = new CancellableEventArgs();
				this.ReportsStarting.Fire(this, cancellable);

				if (cancellable.CancelAction)
				{
					return;
				}

				var writer = reportWriters.Find("html");

				if (writer != null)
				{
					IReportFormat format = writer.SupportedFormats.First();

					progressIndicator.Report(new ProgressValue { Description = "Building File Map" });

					var builder = FileMapBuilder.Map()
						.UsingProject(project)
						.As(format)
						.WithIndex(results.Summary.Identifier)
						.With(results.Summary);

					builder.With(results.AssemblyLevelChanges);

					if (!project.Settings.ConsolidateAssemblyTypes)
						builder.With(results.TypeLevelChanges);

					writer.Map = builder.Build();

					if (!String.IsNullOrEmpty(project.Settings.SubFolder) && Directory.Exists(project.Settings.SubPath))
					{
						Directory.GetFiles(project.Settings.SubPath, "*" + format.Extension).ToList().ForEach(x => File.Delete(x));
					}

					if (!project.Settings.ConsolidateAssemblyTypes)
					{
						// write in reverse order...so that files should exist when we write a link..
						foreach (var typeChange in results.TypeLevelChanges)
						{
							IReportOutput typeOutput = new FileOutput(Path.Combine(project.Settings.SubPath, typeChange.Name.HtmlSafeTypeName() + format.Extension));

							progressIndicator.Report(new ProgressValue { Description = "Generating report for " + typeChange.Name });

							this.ReportStarting.Fire(this, new FileProgessEventArgs { FileName = typeChange.Name });

							writer.Write(typeChange, typeOutput, format);

							this.ReportComplete.Fire(this);
						}
					}

					foreach (var dllChange in results.AssemblyLevelChanges)
					{
						IReportOutput dllOutput = new FileOutput(Path.Combine(project.Settings.SubPath, dllChange.Name + format.Extension));

						this.ReportStarting.Fire(this, new FileProgessEventArgs { FileName = dllChange.Name });

						progressIndicator.Report(new ProgressValue { Description = "Generating report for " + dllChange.Name });

						writer.Write(dllChange, dllOutput, format);

						// need to write out everything relating to this assembly in one place...

						//if (project.Settings.ConsolidateAssemblyTypes)
						//{
						//	foreach (var typeChange in results.TypeLevelChanges.Where(x => x.Changes.)
						//	{
						//		writer.Write(typeChange, dllOutput, format);
						//	}
						//}

						this.ReportComplete.Fire(this);
					}

					// finally write out summary...
					IReportOutput output = new FileOutput(Path.Combine(project.Settings.OutputFolder, project.Settings.IndexName + format.Extension));

					this.ReportStarting.Fire(this, new FileProgessEventArgs { FileName = project.Settings.IndexName });

					progressIndicator.Report(new ProgressValue { Description = "Generating summary report" });
					
					writer.Write(results.Summary, output, format);

					SiteMapTopic siteMap = new SiteMapTopic
					{
						Id = results.Summary.Identifier,
						Title = results.Summary.Name,
						Link = Path.Combine(project.Settings.OutputFolder, project.Settings.IndexName + format.Extension)
					};

					foreach (var dllChange in results.AssemblyLevelChanges)
					{
						siteMap.Children.Add(new SiteMapTopic
							{
								Id = dllChange.Identifier,
								Title = dllChange.Name,
								Link = Path.Combine(project.Settings.SubPath, dllChange.Name + format.Extension),
								Indent = siteMap.Indent + 1
							});
					}

					foreach (var typeChange in results.TypeLevelChanges)
					{
						IDocumentLink parentLink = typeChange.Parents.LastOrDefault();

						if (parentLink != null)
						{
							SiteMapTopic assemblyTopic = siteMap.Find(parentLink.Identifier);

							if (assemblyTopic != null)
							{
								try
								{
									string fileName = typeChange.Name.Replace('<', '_').Replace('>', '_').Replace(',', '.');

									string link = Path.Combine(project.Settings.SubPath, fileName + format.Extension);

									assemblyTopic.Children.Add(new SiteMapTopic
									{
										Id = typeChange.Identifier,
										Title = typeChange.Name,
										Link = link,
										Indent = assemblyTopic.Indent + 1
									});
								}
								catch
								{
									// file name is badly mangled? 
								}
							}
						}
					}

					// sitemap file for sandcastle help file builder menu
					string siteMapFragment = Path.Combine(project.Settings.OutputFolder, project.Settings.IndexName + ".sitemap");

					File.WriteAllText(siteMapFragment, siteMap.ToString());

					this.ReportComplete.Fire(this);
				}
			}
			finally
			{
				this.ReportsComplete.Fire(this);
			}
		}
	}

	public class SiteMapTopic
	{
		public SiteMapTopic()
		{
			this.Children = new List<SiteMapTopic>();
			this.Indent = 0;
		}

		public string Title { get; set; }
		public string Link { get; set; }
		public string Id { get; set; }

		public int Indent { get; set; }

		public List<SiteMapTopic> Children { get; private set; }

		public SiteMapTopic Find(string id)
		{
			if (this.Id == id)
				return this;

			if (this.Children.Any())
			{
				foreach (var child in this.Children)
				{
					SiteMapTopic find = child.Find(id);

					if (find != null)
						return find;
				}
			}

			return null;
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();

			if (this.Indent > 0)
			{
				for(int i = 0; i < this.Indent; ++i)
				{
					builder.Append("\t");
				}
			}

			builder.AppendFormat("<siteMapNode title=\"{0}\" url=\"{1}\"", this.Title, this.Link);

			if (this.Children.Any())
			{
				builder.AppendLine(" >");

				foreach(var child in this.Children)
				{
					builder.Append(child.ToString());
				}

				if (this.Indent > 0)
				{
					for (int i = 0; i < this.Indent; ++i)
					{
						builder.Append("\t");
					}
				}

				builder.AppendLine("</siteMapNode>");
			}
			else
			{
				builder.AppendLine(" />");
			}

			return builder.ToString();
		}
	}
}
