using NDifference.Framework;
using NDifference.Reporting;
using System;
using System.Collections.Generic;

namespace NDifference.Projects
{
    public static class ProjectBuilder
	{
		public static Project Default()
		{
			Project defaultProject = new Project
			{
				Version = "1.0",
				Settings = DefaultSettings()
			};

			defaultProject.Product = new Product();
			defaultProject.Product.Add(new ProductIncrement());
			defaultProject.Product.Add(new ProductIncrement());

			return defaultProject;
		}

		public static Project FromFile(string path)
		{
			return ProjectReader.LoadFromFile(path);
		}

		public static ProjectSettings DefaultSettings()
		{
			var settings = new ProjectSettings
			{
				IndexName = "Summary",
				IndexTitle = "API Differences",
				ConsolidateAssemblyTypes = false,
				ReportFormat = "markdown",
				SubFolder = "api_change",

				SummaryTitle = "API Differences Report",
				HeadingText = "",
				OutputFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
			};

			settings.HeaderText = string.Empty;
			settings.FooterText = string.Empty;

			// classic styling...
            if (settings.ReportFormat == "html")
            {
                var styleBuilder = new DefaultHtmlStyle();
                settings.StyleTag = styleBuilder.ToString();
            }
            else
            {
                settings.StyleTag = string.Empty;
            }

			var ignoreList = new List<string>();

			//ignoreList.Add("ACI001");
			//ignoreList.Add("ACI002");
			//ignoreList.Add("ACI003");

			//ignoreList.Add("AI001");

			//ignoreList.Add("TCI001");
			//ignoreList.Add("TCI002");
			//ignoreList.Add("TCI003");
			//ignoreList.Add("TCI004");
			//ignoreList.Add("TCI005");

			//ignoreList.Add("TI00DEMO");
			//ignoreList.Add("TI00SEC");
			//ignoreList.Add("TI001");
			//ignoreList.Add("TI004");
			//ignoreList.Add("TI005");

			// finalizers
			//ignoreList.Add("TI006");
			//ignoreList.Add("TI007");

			settings.IgnoreInspectors = String.Join(";", ignoreList);
			
			return settings;
		}
	}
}
