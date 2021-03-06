﻿using NDifference.Framework;
using NDifference.Projects;
using System;
using System.IO;
using Xunit;

namespace NDifference.UnitTests
{
	public class ProjectWriterFacts
	{
		[Fact]
		public void ProjectWriter_Writes_Project_As_Xml()
		{
			var project = ProjectBuilder.Default();
			project.Product.Clear();

			string xmlText = WriteProjectToString(project);

			Assert.Contains("<NDifferenceProject", xmlText);
			Assert.Contains("Version=", xmlText);
			Assert.Contains("ID=", xmlText);
		}

		[Fact]
		public void ProjectWriter_Writes_Product_Details()
		{
			var project = ProjectBuilder.Default();
			project.Product.Clear();

			project.Product.Name = "Example";

			string xmlText = WriteProjectToString(project);

			Assert.Contains("<ProductName>Example</ProductName>", xmlText);
		}

		[Fact]
		public void ProjectWriter_Writes_ProductVersion_Details()
		{
			var project = ProjectBuilder.Default();
			project.Product.Clear();

			project.Product.Name = "Example";
			project.Product.Add(new ProductIncrement { Name = "1.0.0" });
			project.Product.Add(new ProductIncrement { Name = "2.0.0" });

			string xmlText = WriteProjectToString(project);

			Assert.Contains("<SourceName>1.0.0</SourceName>", xmlText);
			Assert.Contains("<SourceAssemblies", xmlText);
			Assert.Contains("<TargetName>2.0.0</TargetName>", xmlText);
			Assert.Contains("<TargetAssemblies", xmlText);
		}

		[Fact]
		public void ProjectWriter_Files_Are_Written_Relative_To_Source_Path()
		{
            string baseFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

			var project = ProjectBuilder.Default();
			project.Product.Clear();

            project.FileName = Path.Combine(baseFolder, "Example.ndiff");

			project.Product.Name = "Example";

			var firstVersion = new ProductIncrement { Name = "1.0.0" };
            firstVersion.Add(new AssemblyDiskInfo(baseFolder, "Old", "First.dll"));
            firstVersion.Add(new AssemblyDiskInfo(baseFolder, "Old", "Second.dll"));
            firstVersion.Add(new AssemblyDiskInfo(baseFolder, "Old", "Third.dll"));

			project.Product.Add(firstVersion);

			var secondVersion = new ProductIncrement { Name = "2.0.0" };
            secondVersion.Add(new AssemblyDiskInfo(baseFolder, "New", "Second.dll"));
            secondVersion.Add(new AssemblyDiskInfo(baseFolder, "New", "Third.dll"));
            secondVersion.Add(new AssemblyDiskInfo(baseFolder, "New", "Fourth.dll"));
            secondVersion.Add(new AssemblyDiskInfo(baseFolder, "New", "Fifth.dll"));

			project.Product.Add(secondVersion);

			string xmlText = WriteProjectToString(project);

			Assert.Contains("<SourceName>1.0.0</SourceName>", xmlText);
            Assert.Contains(string.Format("<SourceFolder>{0}{1}Old</SourceFolder>", baseFolder, Path.DirectorySeparatorChar), xmlText);
            Assert.Contains("<SourceAssemblies>", xmlText);
			Assert.Contains("<Include>First.dll</Include>", xmlText);
            Assert.Contains("<Include>Second.dll</Include>", xmlText);
            Assert.Contains("<Include>Third.dll</Include>", xmlText);

			Assert.Contains("<TargetName>2.0.0</TargetName>", xmlText);
            Assert.Contains(string.Format("<TargetFolder>{0}{1}New</TargetFolder>", baseFolder, Path.DirectorySeparatorChar), xmlText);
            Assert.Contains("<TargetAssemblies>", xmlText);
            Assert.Contains("<Include>Second.dll</Include>",  xmlText);
            Assert.Contains("<Include>Third.dll</Include>", xmlText);
            Assert.Contains("<Include>Fourth.dll</Include>",  xmlText);
            Assert.Contains("<Include>Fifth.dll</Include>", xmlText);
		}

		[Fact]
		public void ProjectWriter_All_Files_In_Single_Folder_Includes_Source_Target_Folder()
		{
            string baseFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

			var project = ProjectBuilder.Default();
			project.Product.Clear();

			project.FileName = Path.Combine(baseFolder, "Example.ndiff");

			project.Product.Name = "Example";

			var firstVersion = new ProductIncrement { Name = "1.0.0" };
            firstVersion.Add(new AssemblyDiskInfo(baseFolder, "Old", "First.dll"));
            firstVersion.Add(new AssemblyDiskInfo(baseFolder, "Old", "Second.dll"));
            firstVersion.Add(new AssemblyDiskInfo(baseFolder, "Old", "Third.dll"));
                                                          
			project.Product.Add(firstVersion);

			var secondVersion = new ProductIncrement { Name = "2.0.0" };
            secondVersion.Add(new AssemblyDiskInfo(baseFolder, "New", "Second.dll"));
            secondVersion.Add(new AssemblyDiskInfo(baseFolder, "New", "Third.dll"));
            secondVersion.Add(new AssemblyDiskInfo(baseFolder, "New", "Fourth.dll"));
            secondVersion.Add(new AssemblyDiskInfo(baseFolder, "New", "Fifth.dll"));

			project.Product.Add(secondVersion);

			string xmlText = WriteProjectToString(project);

            Assert.Contains(baseFolder, xmlText);
            Assert.Contains("SourceFolder", xmlText);
            Assert.Contains("TargetFolder", xmlText);
        }

        [Fact]
		public void ProjectWriter_Output_Includes_Section_For_Settings()
		{
			string xmlText = WriteProjectToString(ProjectBuilder.Default());

			Assert.Contains("<Settings", xmlText);
			Assert.Contains("<OutputFolder", xmlText);
			Assert.Contains("<IndexName", xmlText);
			Assert.Contains("<SubFolder", xmlText);
			Assert.Contains("<ConsolidateAssemblyTypes", xmlText);
			Assert.Contains("<StyleTag", xmlText);
			Assert.Contains("<HeadTag", xmlText);
			Assert.Contains("<HeadingText", xmlText);
			Assert.Contains("<FooterText", xmlText);
			Assert.Contains("<ReportFormat", xmlText);
		}

		[Fact]
		public void ProjectWriter_Output_Includes_OutputFolder_Setting()
		{
			var project = ProjectBuilder.Default();
			project.Product.Clear();

            string baseFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            project.Settings.OutputFolder = baseFolder;

			string xmlText = WriteProjectToString(project);

            Assert.Contains(baseFolder, xmlText);
		}

		[Fact]
		public void ProjectWriter_Output_Includes_ConsolidateAssemblyTypes_Setting()
		{
			var project = ProjectBuilder.Default();
			project.Product.Clear();

			project.Settings.ConsolidateAssemblyTypes = true;

			string xmlText = WriteProjectToString(project);

			Assert.Contains("<ConsolidateAssemblyTypes>true</ConsolidateAssemblyTypes>", xmlText);
			Assert.DoesNotContain("<ConsolidateAssemblyTypes>false</ConsolidateAssemblyTypes>", xmlText);
		}

		private string WriteProjectToString(Project project)
		{
			using (TextWriter writer = new StringWriter())
			{
				ProjectWriter.SaveTo(project, writer);
				return writer.ToString();
			}
		}
	}
}
