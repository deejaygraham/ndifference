using NDifference.Analysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace NDifference.Reporting
{
    public class MarkdownReportWriter : IReportWriter
    {
        private readonly ReportAsMarkdown _format = new ReportAsMarkdown();

        public IEnumerable<IReportFormat> SupportedFormats
        {
            get
            {
                return new IReportFormat[] { _format };
            }
        }

        public FileMap Map { get; set; }

		public void Write(IdentifiedChangeCollection changes, IReportOutput output, IReportFormat format)
        {
			Debug.Assert(changes != null, "changes object must be set");
			Debug.Assert(output != null, "output object must be set");

			TextWriter text = new StringWriter();

			try
			{
				if (this.Map != null)
				{
					string folder = Path.GetDirectoryName(output.Path);

					if (!Directory.Exists(folder))
					{
						Directory.CreateDirectory(folder);
					}
				}

				Encoding utf8 = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);

                using (MarkdownWriter mdw = MarkdownWriter.Create(text))
                {
                    var metadata = new Dictionary<string, string>
                    {
                        { "title", changes.Heading },
                        { "id", changes.Identifier },
                        { "layout", "layouts/overview.njk" }
                    };

                    mdw.WriteFrontMatter(metadata);
                    mdw.Write("<!-- markdownlint-disable-file -->");

                    if (!String.IsNullOrEmpty(changes.HeadingBlock))
                    {
                        mdw.Write(changes.HeadingBlock);
                    }

                    // content.Add("Summary Table");
                    mdw.Write(format.FormatTitle(2, "Summary of changes", null));

                    mdw.Write(format.FormatTableHeader(new string[] { "Item", "Value" }));

					foreach (var key in changes.SummaryBlocks.Keys)
                    {
                        mdw.Write(format.FormatTableRow(new string[] { key, changes.SummaryBlocks[key] }));
                    }

                    CategoryRegistry registry = new CategoryRegistry();
                    registry.Register(WellKnownAssemblyCategories.AddedReferences);
                    registry.Register(WellKnownAssemblyCategories.AddedTypes);
                    registry.Register(WellKnownAssemblyCategories.AssemblyInternal);
                    registry.Register(WellKnownAssemblyCategories.ChangedTypes);
                    registry.Register(WellKnownAssemblyCategories.ObsoleteTypes);
                    registry.Register(WellKnownAssemblyCategories.PotentiallyChangedTypes);
                    registry.Register(WellKnownAssemblyCategories.RemovedReferences);
                    registry.Register(WellKnownAssemblyCategories.RemovedTypes);

                    registry.Register(WellKnownSummaryCategories.RemovedAssemblies);
                    registry.Register(WellKnownSummaryCategories.ChangedAssemblies);
                    registry.Register(WellKnownSummaryCategories.PotentiallyChangedAssemblies);
                    registry.Register(WellKnownSummaryCategories.AddedAssemblies);
                    registry.Register(WellKnownSummaryCategories.BreakingChanges);

                    registry.Register(WellKnownTypeCategories.TypeInternal);
                    registry.Register(WellKnownTypeCategories.ConstantsRemoved);
                    registry.Register(WellKnownTypeCategories.ConstantsObsolete);
                    registry.Register(WellKnownTypeCategories.ConstantsChanged);
                    registry.Register(WellKnownTypeCategories.ConstantsAdded);
                    registry.Register(WellKnownTypeCategories.FieldsRemoved);
                    registry.Register(WellKnownTypeCategories.FieldsObsolete);
                    registry.Register(WellKnownTypeCategories.FieldsChanged);
                    registry.Register(WellKnownTypeCategories.FieldsAdded);
                    registry.Register(WellKnownTypeCategories.ConstructorsRemoved);
                    registry.Register(WellKnownTypeCategories.ConstructorsObsolete);
                    registry.Register(WellKnownTypeCategories.ConstructorsChanged);
                    registry.Register(WellKnownTypeCategories.ConstructorsAdded);
                    registry.Register(WellKnownTypeCategories.FinalizersRemoved);
                    registry.Register(WellKnownTypeCategories.FinalizersObsolete);
                    registry.Register(WellKnownTypeCategories.FinalizersChanged);
                    registry.Register(WellKnownTypeCategories.FinalizersAdded);
                    registry.Register(WellKnownTypeCategories.DelegatesRemoved);
                    registry.Register(WellKnownTypeCategories.DelegatesObsolete);
                    registry.Register(WellKnownTypeCategories.DelegatesChanged);
                    registry.Register(WellKnownTypeCategories.DelegatesAdded);
                    registry.Register(WellKnownTypeCategories.EventsRemoved);
                    registry.Register(WellKnownTypeCategories.EventsObsolete);
                    registry.Register(WellKnownTypeCategories.EventsChanged);
                    registry.Register(WellKnownTypeCategories.EventsAdded);
                    registry.Register(WellKnownTypeCategories.PropertiesRemoved);
                    registry.Register(WellKnownTypeCategories.PropertiesObsolete);
                    registry.Register(WellKnownTypeCategories.PropertiesChanged);
                    registry.Register(WellKnownTypeCategories.PropertiesAdded);
                    registry.Register(WellKnownTypeCategories.IndexersRemoved);
                    registry.Register(WellKnownTypeCategories.IndexersObsolete);
                    registry.Register(WellKnownTypeCategories.IndexersChanged);
                    registry.Register(WellKnownTypeCategories.IndexersAdded);
                    registry.Register(WellKnownTypeCategories.MethodsRemoved);
                    registry.Register(WellKnownTypeCategories.MethodsObsolete);
                    registry.Register(WellKnownTypeCategories.MethodsChanged);
                    registry.Register(WellKnownTypeCategories.MethodsAdded);
                    registry.Register(WellKnownTypeCategories.EnumValuesRemoved);
                    registry.Register(WellKnownTypeCategories.EnumValuesChanged);
                    registry.Register(WellKnownTypeCategories.EnumValuesAdded);
                    registry.Register(WellKnownTypeCategories.TypeDebug);


                    // find a well known category that corresponds to this priority 
                    // in the category registry.
                    foreach (var priorityGroup in changes.Changes.GroupBy(x => x.Priority).Reverse())
                    {

                        int priority = priorityGroup.Key;

                        var category = registry.ForPriority(priority);

                        mdw.WriteNewLine();

                        mdw.Write(format.FormatTitle(2, category.Name, null));

                        mdw.Write(category.FullDescription);

                        if (category.Headings != null && category.Headings.Length > 0)
                        {
                            mdw.Write(format.FormatTableHeader(category.Headings));
                        }

                        // order changes...
                        //var ordered = new List<IdentifiedChange>(change);
                        //ordered.Sort(new IdentifiedChangeComparer());

                        foreach (var change in priorityGroup)
                        {
                            RenderChange(change, mdw, output);
//                            mdw.WriteNewLine();
                        }

                        mdw.WriteNewLine();

                        // string href = "#" + cat.Identifier;
                        //                       string categoryLink = string.Format("[{0}]({1})", cat.Name, href);
                        //                       string occurrenceLink = string.Format("[{0}]({1})", list.Count, href);

                        //		mdw.WriteTableRow(new string[] { categoryLink, occurrenceLink });
                        //priorityGroup.

                        // RenderCategory(cat, list, mdw, output);
                    }

                    // write each category
                    //               foreach (var cat in changes.Categories.OrderBy(x => x.Priority.Value))
                    //               {
                    //                   var list = changes.ChangesInCategory(cat.Name);

                    //                   if (list.Any())
                    //                   {
                    //		// write out links to each item in the page below:
                    //                       string href = "#" + cat.Identifier;
                    //                       string categoryLink = string.Format("[{0}]({1})", cat.Name, href);
                    //                       string occurrenceLink = string.Format("[{0}]({1})", list.Count, href);

                    //		mdw.WriteTableRow(new string[] { categoryLink, occurrenceLink });
                    //	}
                    //}

                    //               foreach (var cat in changes.Categories.OrderBy(x => x.Priority.Value))
                    //               {
                    //                   var list = changes.ChangesInCategory(cat.Name);

                    //                   if (list.Any())
                    //                       RenderCategory(cat, list, mdw, output);
                    //               }

                    var uncatChanges = changes.UnCategorisedChanges();

                    if (uncatChanges.Any())
                    {
                        var uncat = new Category
                        {
                            Priority = new CategoryPriority(999), Description = "Uncategorised changes",
                            Name = "Uncategorised Changes"
                        };

                        //RenderCategory(uncat, uncatChanges, mdw, output);
                    }
                }

                string content = text.ToString();

                output.Execute(content);
            }
			finally
			{
				if (text != null)
				{
					text.Dispose();
					text = null;
				}
			}
		}
		//private void RenderCategory(Category cat, IEnumerable<IdentifiedChange> changes, MarkdownWriter mdw, IReportOutput output)
		//{
		//	mdw.WriteNewLine();
	
  //          if (changes.Any())
		//	{
		//		mdw.WriteHeading(cat.Name, 2);

		//		mdw.Write(cat.FullDescription);

  //              if (cat.Headings != null && cat.Headings.Length > 0)
  //              {
  //                  mdw.Write(format.cat.Headings);
  //              }

  //              // order changes...
  //              var ordered = new List<IdentifiedChange>(changes);
  //              ordered.Sort(new IdentifiedChangeComparer());

  //              foreach (var change in ordered)
  //              {
  //                  RenderChange(change, mdw, output);
  //              }

  //              mdw.WriteNewLine();
  //          }

  //          mdw.WriteNewLine();
		//}

		private void RenderChange(IdentifiedChange change, MarkdownWriter mdw, IReportOutput output)
		{
			object descriptor = change.Descriptor;

			if (descriptor != null)
			{
				RenderDescriptor(change, descriptor, mdw, output);
			}
///			else if (!String.IsNullOrEmpty(change.Description))
//			{
//				mdw.WriteTableRow(/*change.Inspector, */new string[] { change.Description });
//			}
		}

        private void Render(IDocumentLink link, MarkdownWriter mdw, IReportFormat format, IReportOutput output, FileMap map)
        {
            if (link != null)
            {
                if (map != null)
                {
                    //mdw.Write(fomatlink, output, this.Map);
                }
            }
        }

        private void RenderDescriptor(IdentifiedChange change, object descriptor, MarkdownWriter mdw, IReportOutput output)
        {
            IDocumentLink link = descriptor as IDocumentLink;

            if (link != null)
            {
                if (this.Map != null)
                {
                    mdw.WriteTableRow(link, output, this.Map);
                }
            }
            else
            {
                ICodeDescriptor code = descriptor as ICodeDescriptor;

                if (code != null)
                {
                    mdw.WriteTableRow(code, this._format, change.TypeName, change.AssemblyName);
                }
                else
                {
                    INameValueDescriptor nvd = descriptor as INameValueDescriptor;

                    if (nvd != null)
                    {
                        mdw.WriteTableRow(nvd, this._format, change.TypeName, change.AssemblyName);
                    }
                    else
                    {
                        INamedDeltaDescriptor nd = descriptor as INamedDeltaDescriptor;

                        if (nd != null)
                        {
                            mdw.WriteTableRow(nd, this._format, change.TypeName, change.AssemblyName);
                        }
                        else
                        {
                            IValueDescriptor vd = descriptor as IValueDescriptor;

                            if (vd != null)
                            {
                                mdw.WriteTableRow(vd, this._format, change.TypeName, change.AssemblyName);
                            }
                            else
                            {
                                INamedDeltaDescriptor ndd = descriptor as INamedDeltaDescriptor;

                                if (ndd != null)
                                {
                                    mdw.WriteTableRow(ndd, this._format);
                                }
                                else
                                {
                                    IDeltaDescriptor delta = descriptor as IDeltaDescriptor;

                                    if (delta != null)
                                    {
                                        mdw.WriteTableRow(delta, this._format, change.TypeName, change.AssemblyName);
                                    }
                                    else
                                    {
                                        ICodeDeltaDescriptor codedelta = descriptor as ICodeDeltaDescriptor;

                                        if (codedelta != null)
                                        {
                                            mdw.WriteTableRow(codedelta, this._format, change.TypeName, change.AssemblyName);
                                        }
                                        else
                                        {
                                            INameDescriptor named = descriptor as INameDescriptor;

                                            if (named != null)
                                            {
                                                mdw.WriteTableRow(named, this._format, change.TypeName, change.AssemblyName);
                                            }
                                            else
                                            {
                                                Debug.Assert(false, "No Descriptor for this");
                                            }
                                        }
                                    }
                                    //else
                                    //{
                                    //	INameValueDescriptor textDesc = descriptor as INameValueDescriptor;

                                    //	if (textDesc != null)
                                    //	{
                                    //		mdw.WriteTableRow(change.Inspector, textDesc, this._format);
                                    //	}
                                    //}
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
