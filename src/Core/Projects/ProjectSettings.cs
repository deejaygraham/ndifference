using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace NDifference.Projects
{
	/// <summary>
	/// Configurable options avaialable from a project.
	/// </summary>
	public class ProjectSettings
	{
		const string AppName = "NDifference";
		const string AppVersion = "0.0.0.1";
		const string GitHubUrl = "www.github.com";
		const string AccountName = "deejaygraham";

		const int MaxLineLength = 80;

		public ProjectSettings()
		{
			this.ApplicationName = AppName;
			this.ApplicationLink = String.Format(CultureInfo.CurrentCulture, "http://{0}/{1}/{2}", GitHubUrl, AccountName, AppName);
			this.ApplicationVersion = AppVersion;

			this.FromIndex = 0;
			this.ToIndex = 1;
		}

		/// <summary>
		/// Top level folder path where the reports are generated 
		/// </summary>
		public string OutputFolder { get; set; }

		/// <summary>
		/// File name of the entry point of the report hierarchy (e.g. index.html)
		/// </summary>
		public string IndexName { get; set; }

		/// <summary>
		/// Optional sub folder name for any reports beyond the index. Should be a name only, not a rooted path.
		/// </summary>
		public string SubFolder { get; set; }

		/// <summary>
		/// Put all type reports for an assembly in a single file ?
		/// </summary>
		public bool ConsolidateAssemblyTypes { get; set; }

		/// <summary>
		/// Index of product increment compare as "from". Defaults to 0
		/// </summary>
		public int FromIndex { get; set; }

		/// <summary>
		/// Index of product increment compare as "to". Defaults to 1
		/// </summary>
		public int ToIndex { get; set; }

		/// <summary>
		/// Extra content in the <head /> section of an html document
		/// </summary>
		public string HeadTag { get; set; }

		/// <summary>
		/// Extra content in the <head /> section. Use inline css or link to external file.
		/// </summary>
		public string StyleTag { get; set; }

		/// <summary>
		/// Title on summary page.
		/// </summary>
		public string SummaryTitle { get; set; }

		/// <summary>
		/// Heading block in summary page.
		/// </summary>
		public string HeadingText { get; set; }

		/// <summary>
		/// Footer to put on every page
		/// </summary>
		public string FooterText { get; set; }

		/// <summary>
		/// What format the report should be in (e.g. Html).
		/// </summary>
		public string ReportFormat { get; set; }

		/// <summary>
		/// The name of our application (used to generate documentation).
		/// </summary>
		public string ApplicationName { get; private set; }

		/// <summary>
		/// The link to the app's web page (used to generate documentation).
		/// </summary>
		public string ApplicationLink { get; private set; }

		/// <summary>
		/// The current version of the app (used to generate documentation).
		/// </summary>
		public string ApplicationVersion { get; private set; }

		/// <summary>
		/// Semi-colon delimited list of inspectors to exclude from the analysis.
		/// </summary>
		public string IgnoreInspectors { get; set; }

		/// <summary>
		/// Suggest the path (based on settings) to write the index file.
		/// </summary>
		/// <param name="extension"></param>
		/// <returns></returns>
		public string SuggestIndexPath(string extension)
		{
			Debug.Assert(!String.IsNullOrEmpty(extension), "Extension cannot be blank");

			return Path.Combine(this.OutputFolder, this.IndexName + extension);
		}

		/// <summary>
		/// Suggest a path (based on settings) to write a report file.
		/// </summary>
		/// <param name="fileName"></param>
		/// <param name="extension"></param>
		/// <returns></returns>
		public string SuggestPath(string fileName, string extension)
		{
			Debug.Assert(!String.IsNullOrEmpty(fileName), "File name cannot be blank");
			Debug.Assert(!String.IsNullOrEmpty(extension), "Extension cannot be blank");

			try
			{
				if (String.IsNullOrEmpty(this.SubFolder))
					return Path.Combine(this.OutputFolder, fileName + extension);

				return Path.Combine(this.OutputFolder, this.SubFolder, fileName + extension);
			}
			catch (ArgumentException)
			{
				return null;
			}
		}

		/// <summary>
		/// Folder where index file is written
		/// </summary>
		public string IndexPath
		{
			get
			{
				Debug.Assert(!String.IsNullOrEmpty(this.OutputFolder), "Output Folder not set");

				return this.OutputFolder;
			}
		}

		/// <summary>
		/// Folder for sub reports - resolves to indexpath if subfolder property is not set.
		/// </summary>
		public string SubPath
		{
			get
			{
				Debug.Assert(!String.IsNullOrEmpty(this.OutputFolder), "Output Folder not set");

				if (String.IsNullOrEmpty(this.SubFolder))
					return this.IndexPath;

				return Path.Combine(this.OutputFolder, this.SubFolder);
			}
		}

		/// <summary>
		/// Format into persistable state, ready for saving to disk.
		/// </summary>
		/// <returns></returns>
		public PersistableProjectSettings ToPersistableFormat()
		{
			var persistableSettings = new PersistableProjectSettings
			{
				ApplicationName = this.ApplicationName,
				ApplicationVersion = this.ApplicationVersion,
				ApplicationLink = this.ApplicationLink,
				ConsolidateAssemblyTypes = this.ConsolidateAssemblyTypes,
				FooterText = this.FooterText,
				HeadingText = this.HeadingText,
				HeadTag = this.HeadTag,
				IndexName = this.IndexName,
				OutputFolder = this.OutputFolder,
				ReportFormat = this.ReportFormat,
				StyleTag = this.StyleTag,
				Subfolder = this.SubFolder,
				SummaryTitle = this.SummaryTitle,
				FromIndex = this.FromIndex,
				ToIndex = this.ToIndex,
				IgnoreInspectors = this.IgnoreInspectors
			};

			return persistableSettings;
		}

		/// <summary>
		/// Re-hydrate from a persisted state.
		/// </summary>
		/// <param name="persistableSettings"></param>
		/// <returns></returns>
		public static ProjectSettings FromPersistableFormat(PersistableProjectSettings persistableSettings)
		{
			Debug.Assert(persistableSettings != null, "Settings are null");

			var settings = new ProjectSettings
			{
				ConsolidateAssemblyTypes = persistableSettings.ConsolidateAssemblyTypes,
				FooterText = persistableSettings.FooterText,
				HeadingText = persistableSettings.HeadingText,
				HeadTag = persistableSettings.HeadTag,
				IndexName = persistableSettings.IndexName,
				OutputFolder = persistableSettings.OutputFolder,
				ReportFormat = persistableSettings.ReportFormat,
				StyleTag = persistableSettings.StyleTag,
				SubFolder = persistableSettings.Subfolder,
				SummaryTitle = persistableSettings.SummaryTitle,
				FromIndex = persistableSettings.FromIndex,
				ToIndex = persistableSettings.ToIndex,
				IgnoreInspectors = persistableSettings.IgnoreInspectors
			};

			return settings;
		}

		public IEnumerable<string> GenerateMetaBlocks()
		{
			var blocks = new List<string>();

			string autoGenerationMessage = AutogenerateDateTimeText();

			blocks.Add(autoGenerationMessage);

			HeadTagContent(blocks);

			StyleTagContent(blocks);

			return blocks;
		}

		private void StyleTagContent(List<string> blocks)
		{
			if (String.IsNullOrEmpty(this.StyleTag))
			{
				blocks.Add("<!-- No custom style content defined -->");
			}
			else
			{
				blocks.Add("<!-- Custom style content -->");
				blocks.Add(this.StyleTag.SplitLongLines(MaxLineLength));
				blocks.Add("<!-- End of custom style content -->");
			}
		}

		private void HeadTagContent(List<string> blocks)
		{
			if (String.IsNullOrEmpty(this.HeadTag))
			{
				blocks.Add("<!-- No custom head content defined -->");
			}
			else
			{
				blocks.Add("<!-- Custom head content -->");
				blocks.Add(this.HeadTag);
				blocks.Add("<!-- End of custom head content -->");
			}
		}

		private string AutogenerateDateTimeText()
		{
			return String.Format("<!-- Generated by {0} {1} {2} {3} -->",
						 this.ApplicationName,
						 this.ApplicationVersion,
						 DateTime.Now.ToLongDateString(),
						 DateTime.Now.ToLongTimeString());
		}

		public IEnumerable<string> GenerateFooterBlocks()
		{
			var blocks = new List<string>();

			if (String.IsNullOrEmpty(this.FooterText))
			{
				blocks.Add("<!-- No footer block defined -->");
			}
			else
			{
				blocks.Add("<!-- Footer block -->");
				blocks.Add(this.FooterText.SplitLongLines(MaxLineLength));
				blocks.Add("<!-- End of Footer block -->");
			}

			return blocks;
		}

		public void CopyMetaFrom(ProjectSettings other)
		{
			if (other == null)
				return;

			this.IndexName = other.IndexName;
			this.HeadTag = other.HeadTag;
			this.HeadingText = other.HeadingText;
			this.FooterText = other.FooterText;

			this.ReportFormat = other.ReportFormat;
			this.StyleTag = other.StyleTag;
			this.SubFolder = other.SubFolder;
			this.SummaryTitle = other.SummaryTitle;
			this.FromIndex = other.FromIndex;
			this.ToIndex = other.ToIndex;

			// this.IgnoreInspectors = other.IgnoreInspectors;
		}
	}
}
