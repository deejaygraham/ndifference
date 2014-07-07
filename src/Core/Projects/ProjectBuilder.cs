﻿using NDifference.Framework;
using NDifference.Reporting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
				ConsolidateAssemblyTypes = false,
				ReportFormat = "html",
				SubFolder = "api_change",

				SummaryTitle = "API Differences Report",
				HeadingText = "This report details changes in the public API. It shows additions, modifications and removals of assemblies between the two versions. The links on this page detail which types have been added, removed or changed in each assembly.",

				OutputFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
			};

			settings.FooterText = String.Format(CultureInfo.CurrentCulture, 
				"<div id=\"footer\"><p>Generated by <a href=\"{0}\" title=\"go to website\">{1} {2}</a> {3} {4}</p></div>",
				settings.ApplicationLink,
				settings.ApplicationName,
				settings.ApplicationVersion,
				DateTime.Now.ToLongDateString(),
				DateTime.Now.ToLongTimeString());

			// classic styling...

			var styleBuilder = new DefaultHtmlStyle();

			settings.StyleTag = styleBuilder.ToString();

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
