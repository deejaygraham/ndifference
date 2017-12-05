using NDifference.Framework;
using NDifference.Projects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NDifference.UnitTests
{
	public class ProjectReaderFacts
	{
		[Fact]
		public void ProjectReader_Reads_Project_As_Xml()
		{
			var project = ProjectBuilder.Default();
			project.Product.Clear();

			string xmlText = WriteProjectToString(project);

			Project readProject = ReadProjectFromString(xmlText);

			Assert.Equal(project.Id, readProject.Id);
			Assert.Equal(project.Version, readProject.Version);
		}

		[Fact]
		public void ProjectReader_Loads_Assemblies_Relative_To_Project()
		{
			string folder = "C:\\MyDocuments";

			var project = ProjectBuilder.Default();
			project.Product.Clear();

			project.FileName = Path.Combine(folder, "Example.okavango");

			project.Product.Name = "Example";

			var firstVersion = new ProductIncrement { Name = "1.0.0" };
			firstVersion.Add(new AssemblyDiskInfo(folder, "Old\\First.dll"));
			firstVersion.Add(new AssemblyDiskInfo(folder, "Old\\Second.dll"));
			firstVersion.Add(new AssemblyDiskInfo(folder, "Old\\Third.dll"));

			project.Product.Add(firstVersion);

			var secondVersion = new ProductIncrement { Name = "2.0.0" };
			secondVersion.Add(new AssemblyDiskInfo(folder, "New\\Second.dll"));
			secondVersion.Add(new AssemblyDiskInfo(folder, "New\\Third.dll"));
			secondVersion.Add(new AssemblyDiskInfo(folder, "New\\Fourth.dll"));
			secondVersion.Add(new AssemblyDiskInfo(folder, "New\\Fifth.dll"));

			project.Product.Add(secondVersion);

			string xmlText = WriteProjectToString(project);

			Project readProject = ReadProjectFromString(xmlText, folder);

			Assert.Equal(project.Product.ComparedIncrements.First.Assemblies.Count, readProject.Product.ComparedIncrements.First.Assemblies.Count);
			Assert.Equal(project.Product.ComparedIncrements.Second.Assemblies.Count, readProject.Product.ComparedIncrements.Second.Assemblies.Count);
		}

		[Fact]
		public void ProjectReader_Loads_Assemblies_With_Full_Paths_In_Order()
		{
			string folder = "C:\\MyDocuments";

			var project = ProjectBuilder.Default();
			project.Product.Clear();

			project.FileName = Path.Combine(folder, "Example.okavango");

			project.Product.Name = "Example";

			var firstVersion = new ProductIncrement { Name = "1.0.0" };
			firstVersion.Add(new AssemblyDiskInfo(folder, "Old\\First.dll"));
			firstVersion.Add(new AssemblyDiskInfo(folder, "Old\\Second.dll"));
			firstVersion.Add(new AssemblyDiskInfo(folder, "Old\\Third.dll"));

			project.Product.Add(firstVersion);

			var secondVersion = new ProductIncrement { Name = "2.0.0" };
			secondVersion.Add(new AssemblyDiskInfo(folder, "New\\Second.dll"));
			secondVersion.Add(new AssemblyDiskInfo(folder, "New\\Third.dll"));
			secondVersion.Add(new AssemblyDiskInfo(folder, "New\\Fourth.dll"));
			secondVersion.Add(new AssemblyDiskInfo(folder, "New\\Fifth.dll"));

			project.Product.Add(secondVersion);

			string xmlText = WriteProjectToString(project);

			Project readProject = ReadProjectFromString(xmlText, folder);

			var oldAssemblies = readProject.Product.ComparedIncrements.First.Assemblies;
			var newAssemblies = readProject.Product.ComparedIncrements.Second.Assemblies;

			Assert.Equal(Path.Combine(folder, "Old\\First.dll"), oldAssemblies[0].Path);
			Assert.Equal(Path.Combine(folder, "Old\\Second.dll"), oldAssemblies[1].Path);
			Assert.Equal(Path.Combine(folder, "Old\\Third.dll"), oldAssemblies[2].Path);
			Assert.Equal(Path.Combine(folder, "New\\Second.dll"), newAssemblies[0].Path);
			Assert.Equal(Path.Combine(folder, "New\\Third.dll"), newAssemblies[1].Path);
			Assert.Equal(Path.Combine(folder, "New\\Fourth.dll"), newAssemblies[2].Path);
			Assert.Equal(Path.Combine(folder, "New\\Fifth.dll"), newAssemblies[3].Path);
		}

		[Fact]
		public void ProjectReader_Loads_ConsolidateAssemblyTypes_Setting()
		{
			var project = new Project();

			project.Settings.ConsolidateAssemblyTypes = true;

			string xmlText = WriteProjectToString(project);

			Project readProject = ReadProjectFromString(xmlText);
			Assert.True(readProject.Settings.ConsolidateAssemblyTypes);
		}

		[Fact]
		public void ProjectReader_Loads_OutputFolder_Setting()
		{
			var project = new Project();

			project.Settings.OutputFolder = @"C:\MyFolder\\";

			string xmlText = WriteProjectToString(project);

			Project readProject = ReadProjectFromString(xmlText);
			Assert.Equal(project.Settings.OutputFolder, readProject.Settings.OutputFolder);
		}

		[Fact]
		public void ProjectReader_Loads_IndexName_Setting()
		{
			var project = new Project();

			project.Settings.IndexName = @"summary.html";

			string xmlText = WriteProjectToString(project);

			Project readProject = ReadProjectFromString(xmlText);
			Assert.Equal(project.Settings.IndexName, readProject.Settings.IndexName);
		}

		[Fact]
		public void ProjectReader_Loads_SubFolder_Setting()
		{
			var project = new Project();

			project.Settings.SubFolder = "api_changes";

			string xmlText = WriteProjectToString(project);

			Project readProject = ReadProjectFromString(xmlText);
			Assert.Equal(project.Settings.SubFolder, readProject.Settings.SubFolder);
		}

		[Fact]
		public void ProjectReader_Loads_HeadTag_Setting()
		{
			var project = new Project();

			project.Settings.HeadTag = "head tags";

			string xmlText = WriteProjectToString(project);

			Project readProject = ReadProjectFromString(xmlText);
			Assert.Equal(project.Settings.HeadTag, readProject.Settings.HeadTag);
		}

		[Fact]
		public void ProjectReader_Loads_StyleTag_Setting()
		{
			var project = new Project();

			project.Settings.StyleTag = "style goes here";

			string xmlText = WriteProjectToString(project);

			Project readProject = ReadProjectFromString(xmlText);
			Assert.Equal(project.Settings.StyleTag, readProject.Settings.StyleTag);
		}

		[Fact]
		public void ProjectReader_Loads_HeadingText_Setting()
		{
			var project = new Project();

			project.Settings.HeadingText = "Hello";

			string xmlText = WriteProjectToString(project);

			Project readProject = ReadProjectFromString(xmlText);
			Assert.Equal(project.Settings.HeadingText, readProject.Settings.HeadingText);
		}

		[Fact]
		public void ProjectReader_Loads_FooterText_Setting()
		{
			var project = new Project();

			project.Settings.FooterText = "<p>Copyright goes here</p>";

			string xmlText = WriteProjectToString(project);

			Project readProject = ReadProjectFromString(xmlText);
			Assert.Equal(project.Settings.FooterText, readProject.Settings.FooterText);
		}

		[Fact]
		public void ProjectReader_Loads_ReportFormat_Setting()
		{
			var project = new Project();

			project.Settings.ReportFormat = "Unsupported";

			string xmlText = WriteProjectToString(project);

			Project readProject = ReadProjectFromString(xmlText);
			Assert.Equal(project.Settings.ReportFormat, readProject.Settings.ReportFormat);
		}

		private string WriteProjectToString(Project project)
		{
			using (TextWriter writer = new StringWriter())
			{
				ProjectWriter.SaveTo(project, writer);
				return writer.ToString();
			}
		}

		private Project ReadProjectFromString(string xmlText)
		{
			return ReadProjectFromString(xmlText, null);
		}

		private Project ReadProjectFromString(string xmlText, string folder)
		{
			Project readProject = null;

			using (TextReader reader = new StringReader(xmlText))
			{
				if (String.IsNullOrEmpty(folder))
					readProject = ProjectReader.LoadFrom(reader);
				else
					readProject = ProjectReader.LoadFrom(reader, folder);
			}

			return readProject;
		}
	}
}
