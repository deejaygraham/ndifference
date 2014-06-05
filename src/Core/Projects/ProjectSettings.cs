using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Projects
{
	public class ProjectSettings
	{
		const string AppName = "NDifference";
		const string AppVersion = "0.0.0.1";
		const string GitHubUrl = "www.github.com";
		const string AccountName = "deejaygraham";

		public ProjectSettings()
		{
			this.ApplicationName = AppName;
			this.ApplicationLink = String.Format(CultureInfo.CurrentCulture, "http://{0}/{1}/{2}", GitHubUrl, AccountName, AppName);
			this.ApplicationVersion = AppVersion;
		}

		public string OutputFolder { get; set; }

		public string IndexName { get; set; }

		public string SubFolder { get; set; }

		public bool ConsolidateAssemblyTypes { get; set; }

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

		public string FooterText { get; set; }

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

		public string SuggestIndexPath(string extension)
		{
			Debug.Assert(!String.IsNullOrEmpty(extension), "Extension cannot be blank");

			return Path.Combine(this.OutputFolder, this.IndexName + extension);
		}

		public string SuggestPath(string fileName, string extension)
		{
			Debug.Assert(!String.IsNullOrEmpty(fileName), "File name cannot be blank");
			Debug.Assert(!String.IsNullOrEmpty(extension), "Extension cannot be blank");

			if (String.IsNullOrEmpty(this.SubFolder))
				return Path.Combine(this.OutputFolder, fileName + extension);

			return Path.Combine(this.OutputFolder, this.SubFolder, fileName + extension);
		}

		public string IndexPath
		{
			get
			{
				Debug.Assert(!String.IsNullOrEmpty(this.OutputFolder), "Output Folder not set");

				return this.OutputFolder;
			}
		}

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
				SummaryTitle = this.SummaryTitle
			};

			return persistableSettings;
		}

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
				SummaryTitle = persistableSettings.SummaryTitle
			};

			return settings;
		}

	}
}
