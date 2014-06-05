using NDifference.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Projects
{
	/// <summary>
	/// Project object used to store options for configuration of one product.
	/// Can be persisted to file or created on the fly and discarded.
	/// </summary>
	public sealed class Project
	{
		private Identifier identifier = new Identifier();

		public Project()
		{
			this.Version = "1.0";

			this.Product = new Product();

			this.Settings = new ProjectSettings();
		}

		/// <summary>
		/// The name of the file where the project was read from. May be empty
		/// if project created on the fly.
		/// </summary>
		public string FileName { get; set; }

		/// <summary>
		/// The version of the project on file.
		/// </summary>
		public string Version { get; set; }

		/// <summary>
		/// A unique ID for this project.
		/// </summary>
		public string Id
		{
			get
			{
				return this.identifier;
			}

			set
			{
				this.identifier = value;
			}
		}

		public Product Product { get; set; }

		public ProjectSettings Settings { get; set; }

		public PersistableProject ToPersistableFormat()
		{
			var persistableFormat = new PersistableProject
			{
				Identifier = this.Id,
				Version = this.Version
			};

			persistableFormat.ProductName = this.Product.Name;

			if (this.Product.Increments.Count > 1)
			{
				persistableFormat.SourceName = this.Product.ComparedIncrements.First.Name;
				persistableFormat.TargetName = this.Product.ComparedIncrements.Second.Name;

				string baseFolder = string.IsNullOrEmpty(this.FileName) ? string.Empty : Path.GetDirectoryName(this.FileName);

				bool writeAbsolutePaths = string.IsNullOrEmpty(baseFolder);

				foreach (var assembly in this.Product.ComparedIncrements.First.Assemblies)
				{
					string include = writeAbsolutePaths ? assembly.Path : PathExtensions.MakeRelativePath(baseFolder, assembly.Path);
					persistableFormat.SourceAssemblies.Add(include);
				}

				foreach (var assembly in this.Product.ComparedIncrements.Second.Assemblies)
				{
					string include = writeAbsolutePaths ? assembly.Path : PathExtensions.MakeRelativePath(baseFolder, assembly.Path);
					persistableFormat.TargetAssemblies.Add(include);
				}
			}

			persistableFormat.Settings = this.Settings.ToPersistableFormat();

			return persistableFormat;
		}

		public static Project FromPersistableFormat(PersistableProject persistableFormat, string baseFolder)
		{
			Debug.Assert(persistableFormat != null, "Project file object is null");

			var project = new Project
			{
				Id = persistableFormat.Identifier,
				Version = persistableFormat.Version
			};

			project.Product.Name = persistableFormat.ProductName;

			var firstVersion = new ProductIncrement();

			if (!String.IsNullOrEmpty(persistableFormat.SourceName))
			{
				firstVersion.Name = persistableFormat.SourceName;
			}

			foreach (var file in persistableFormat.SourceAssemblies)
			{
				if (!string.IsNullOrEmpty(file))
				{
					string fullPath = file;

					if (!string.IsNullOrEmpty(baseFolder))
					{
						fullPath = baseFolder.MakeAbsolutePath(file);
					}

					firstVersion.Add(AssemblyDiskInfo.BuildFrom(fullPath));
				}
			}

			project.Product.Add(firstVersion);

			var secondVersion = new ProductIncrement();

			if (!String.IsNullOrEmpty(persistableFormat.TargetName))
			{
				secondVersion.Name = persistableFormat.TargetName;
			}

			foreach (var file in persistableFormat.TargetAssemblies)
			{
				if (!string.IsNullOrEmpty(file))
				{
					string fullPath = file;

					if (!string.IsNullOrEmpty(baseFolder))
					{
						fullPath = baseFolder.MakeAbsolutePath(file);
					}

					secondVersion.Add(AssemblyDiskInfo.BuildFrom(fullPath));
				}
			}

			project.Product.Add(secondVersion);

			project.Settings = ProjectSettings.FromPersistableFormat(persistableFormat.Settings);

			return project;
		}
	}

}
