﻿using NDifference.Analysis;
using NDifference.Framework;
using NDifference.Inspectors;
using NDifference.Projects;
using NDifference.Reflection;
using NDifference.Reporting;
using NDifference.UI.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NDifference.UI
{
	public partial class MainForm : Form
	{
		private ControlStateWrangler _dataEntryState = new ControlStateWrangler();

		private ControlStateWrangler _progressState = new ControlStateWrangler();

		private Project _project = null;

		private ProjectCleanlinessTracker _tracker;

		public event EventHandler ProjectLoaded;

		public event EventHandler ProjectChanged;

		public event EventHandler ProjectSaved;

		public MainForm()
		{
			InitializeComponent();

			this._tracker = new ProjectCleanlinessTracker(this);
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			this.tvInspectors.Dock = DockStyle.Fill;
			this.tvInspectors.CheckBoxes = true;
			this.tvInspectors.AfterCheck += (s, arg) =>
			{
				if (arg.Action != TreeViewAction.Unknown)
				{
					if (arg.Node.Nodes.Count > 0)
					{
						foreach (TreeNode node in arg.Node.Nodes)
						{
							node.Checked = arg.Node.Checked;
						}
					}
				}
			};


			this._project = ProjectBuilder.Default();

			this.InitialiseUIFromProject(this._project);

			this._dataEntryState.Add(this.txtProductName);
			this._dataEntryState.Add(this.txtPreviousVersion);
			this._dataEntryState.Add(this.txtNewVersion);
			this._dataEntryState.Add(this.tpAssemblies);
			this._dataEntryState.Add(this.tpInspectors);
			this._dataEntryState.Add(this.fsOutputFolder);
			this._dataEntryState.Add(this.btnStart);

			this._progressState.Add(this.progressLabel);
			this._progressState.Add(this.progressBar);
			this._progressState.Add(this.btnCancel);

			this._progressState.Invisible();
		}

		private void btnStart_Click(object sender, EventArgs e)
		{
			Build();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}


		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool okToContinue = SaveCurrentProjectIfRequired();

			if (okToContinue)
			{
				string loadProject = PromptForFileNameToLoadProject();

				if (!string.IsNullOrEmpty(loadProject))
				{
					try
					{
						this._project = ProjectReader.LoadFromFile(loadProject);
						InitialiseUIFromProject(this._project);

						if (this.ProjectLoaded != null)
						{
							this.ProjectLoaded(this, EventArgs.Empty);
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show("Unable to open project.\r\n" + ex.GetBaseException().Message);
					}
				}
			}
		}

		private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var newProject = UpdateProjectFromUI();
                newProject.CopyMetaFrom(this._project);
                this._project = newProject;

                if (string.IsNullOrEmpty(this._project.FileName))
                {
                    string saveProject = PromptForFileNameToSaveProject(this._project.Product.Name + "-" + this._project.Product.ComparedIncrements.First.Name + "-" + this._project.Product.ComparedIncrements.Second.Name);

                    if (!string.IsNullOrEmpty(saveProject))
                    {
                        this._project.FileName = saveProject;
                    }
                }

                if (!string.IsNullOrEmpty(this._project.FileName))
                {
                    ProjectWriter.Save(this._project, this._project.FileName);

                    if (this.ProjectSaved != null)
                    {
                        this.ProjectSaved(this, EventArgs.Empty);
                    }
                }
            }
			catch (DirectoryNotFoundException dnf)
            {
                MessageBox.Show(dnf.Message);
            }
		}

		private void saveProjectAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
            try
            {
                var newProject = UpdateProjectFromUI();
                newProject.CopyMetaFrom(this._project);
                this._project = newProject;

                string saveProject = PromptForFileNameToSaveProject(this._project.Product.Name + "-" + this._project.Product.ComparedIncrements.First.Name + "-" + this._project.Product.ComparedIncrements.Second.Name);

                if (!string.IsNullOrEmpty(saveProject))
                {
                    this._project.FileName = saveProject;
                    ProjectWriter.Save(this._project, this._project.FileName);

                    if (this.ProjectSaved != null)
                    {
                        this.ProjectSaved(this, EventArgs.Empty);
                    }
                }
            }
			catch (DirectoryNotFoundException dnf)
            {
                MessageBox.Show(dnf.Message);
            }
		}

		private void InitialiseUIFromProject(Project project)
		{
			this.txtProductName.Text = project.Product.Name;

			var oldRelease = project.Product.ComparedIncrements.First;
			var newRelease = project.Product.ComparedIncrements.Second;

			this.txtPreviousVersion.Text = oldRelease.Name;
			this.txtNewVersion.Text = newRelease.Name;

			this.asPreviousVersion.MessageWhenEmpty = "Add \"previous\" assemblies here";
			this.asPreviousVersion.Initialise(oldRelease.Assemblies);

			this.asNewVersion.MessageWhenEmpty = "Add \"new\" assemblies here";
			this.asNewVersion.Initialise(newRelease.Assemblies);

			this.fsOutputFolder.FolderPath = project.Settings.OutputFolder;
			
			var finder = new FileFinder(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), FileFilterConstants.AssemblyFilter);

            this.tvInspectors.BeginUpdate();

            this.tvInspectors.Nodes.Clear();

            try
            {
                InspectorRepository ir = new InspectorRepository();

                ir.Find(finder);
                InspectorFilter filter = new InspectorFilter(project.Settings.IgnoreInspectors);

			    ir.Filter(filter);

			    var aciNode = this.tvInspectors.Nodes.Add("Assembly Collection Inspectors");

			    PopulateNodeChildren(aciNode, ir.AssemblyCollectionInspectors);

			    var aiNode = this.tvInspectors.Nodes.Add("Assembly Inspectors");
			    PopulateNodeChildren(aiNode, ir.AssemblyInspectors);

			    var tciNode = this.tvInspectors.Nodes.Add("Type Collection Inspectors");
			    PopulateNodeChildren(tciNode, ir.TypeCollectionInspectors);

			    var tiNode = this.tvInspectors.Nodes.Add("Type Inspectors");
			    PopulateNodeChildren(tiNode, ir.TypeInspectors);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.tvInspectors.ExpandAll();

            this.tvInspectors.EndUpdate();
        }

        private void PopulateNodeChildren(TreeNode node, IEnumerable<IInspector> inspectors)
		{
			foreach (var i in inspectors)
			{
				node.Nodes.Add(
					new TreeNode(i.ShortCode + " - " + i.DisplayName) 
					{ 
						Tag = i.ShortCode, 
						Checked = i.Enabled 
					});
			}

			if (inspectors.All(x => x.Enabled))
				node.Checked = true;
		}

		private IEnumerable<string> BuildIgnoreList(TreeView tv)
		{
			var list = new List<string>();

			// now look at what inspectors have been selected...
			foreach (TreeNode n in tv.Nodes)
			{
				foreach (TreeNode child in n.Nodes)
				{
					if (!child.Checked && child.Tag != null)
					{
						string shortCode = child.Tag.ToString();

						if (!String.IsNullOrEmpty(shortCode))
						{
							list.Add(shortCode);
						}
					}
				}
			}

			return list;
		}

		private Project UpdateProjectFromUI()
		{
			Project refreshedProject = ProjectBuilder.Default();
			refreshedProject.Product.Clear();

			refreshedProject.Product.Name = txtProductName.Text;

			var oldVersion = new ProductIncrement { Name = this.txtPreviousVersion.Text };

            var infoBuilder = new AssemblyDiskInfoBuilder();

			foreach (var file in this.asPreviousVersion.SelectedAssemblies)
			{
				try
				{

					var info = infoBuilder.BuildFromFile(file);
					oldVersion.Add(info);
				}
				catch(FileNotFoundException)
				{
					// don't care if the file doesn't exist.
				}
			}

			var newVersion = new ProductIncrement { Name = this.txtNewVersion.Text };
            
			foreach (var file in this.asNewVersion.SelectedAssemblies)
			{
				try
				{
					var info = infoBuilder.BuildFromFile(file);
					newVersion.Add(info);
				}
				catch (FileNotFoundException)
				{
					// don't care if the file doesn't exist.
				}
			}

			refreshedProject.Product.Add(oldVersion);
			refreshedProject.Product.Add(newVersion);

			refreshedProject.Settings.OutputFolder = this.fsOutputFolder.FolderPath;

			var list = new List<string>();

			// now look at what inspectors have been selected...
			foreach (TreeNode n in this.tvInspectors.Nodes)
			{
				foreach (TreeNode child in n.Nodes)
				{
					if (!child.Checked && child.Tag != null)
					{
						string shortCode = child.Tag.ToString();

						if (!String.IsNullOrEmpty(shortCode))
						{
							list.Add(shortCode);
						}
					}
				}
			}

			if (list.Any())
			{
				refreshedProject.Settings.IgnoreInspectors = String.Join(";", list);
			}

			return refreshedProject;
		}

		private List<string> ValidateProject(Project project)
		{
			List<string> validationErrors = new List<string>();

			if (string.IsNullOrEmpty(project.Product.Name))
			{
				validationErrors.Add("Please give the project a name.");
			}

			var first = project.Product.ComparedIncrements.First;
			var second = project.Product.ComparedIncrements.Second;

			if (string.IsNullOrEmpty(first.Name))
			{
				validationErrors.Add("Please name the \"old\" version of the product.");
			}

			if (!first.Assemblies.Any())
			{
				validationErrors.Add("You have not selected an old version of your assemblies to analyse.");
			}

			if (string.IsNullOrEmpty(second.Name))
			{
				validationErrors.Add("Please name the \"new\" version of the product.");
			}

			if (!second.Assemblies.Any())
			{
				validationErrors.Add("You have not selected a new version of your assemblies to analyse.");
			}

			if (string.IsNullOrEmpty(project.Settings.OutputFolder))
			{
				validationErrors.Add("Output folder is not set.");
			}

			return validationErrors;
		}

		private void txtProductName_TextChanged(object sender, EventArgs e)
		{
			if (this.ProjectChanged != null)
			{
				this.ProjectChanged(this, EventArgs.Empty);
			}
		}

		private void txtPreviousVersion_TextChanged(object sender, EventArgs e)
		{
			if (this.ProjectChanged != null)
			{
				this.ProjectChanged(this, EventArgs.Empty);
			}
		}

		private void txtNewVersion_TextChanged(object sender, EventArgs e)
		{
			if (this.ProjectChanged != null)
			{
				this.ProjectChanged(this, EventArgs.Empty);
			}
		}

		private void cmbReportFormat_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.ProjectChanged != null)
			{
				this.ProjectChanged(this, EventArgs.Empty);
			}
		}

		private void asPreviousVersion_ListChanged(object sender, EventArgs e)
		{
			if (this.ProjectChanged != null)
			{
				this.ProjectChanged(this, EventArgs.Empty);
			}
		}

		private void asNewVersion_ListChanged(object sender, EventArgs e)
		{
			if (this.ProjectChanged != null)
			{
				this.ProjectChanged(this, EventArgs.Empty);
			}
		}

		private string PromptForFileNameToLoadProject()
		{
			using (OpenFileDialog dialog = new OpenFileDialog())
			{
				dialog.Title = "Open Project File";
				dialog.Filter = FileFilterConstants.ProjectFileDialogFilter;

				if (DialogResult.OK == dialog.ShowDialog())
				{
					return dialog.FileName;
				}

				return null;
			}
		}

		private string PromptForFileNameToSaveProject(string suggestedName = null)
		{
			using (SaveFileDialog dialog = new SaveFileDialog())
			{
				dialog.Title = "Save Project File";
				dialog.Filter = FileFilterConstants.ProjectFileDialogFilter;

				if (!string.IsNullOrEmpty(suggestedName))
				{
					dialog.FileName = suggestedName;
				}

				if (DialogResult.OK == dialog.ShowDialog())
				{
					return dialog.FileName;
				}

				return null;
			}
		}

		private DialogResult AskToSaveProject()
		{
			return MessageBox.Show("Save changes to this project?",
				"NDifference",
				MessageBoxButtons.YesNoCancel,
				MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button1);
		}

		private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool okToContinue = SaveCurrentProjectIfRequired();

			if (okToContinue)
			{
				this._project = ProjectBuilder.Default();

				InitialiseUIFromProject(this._project);

				if (this.ProjectLoaded != null)
				{
					this.ProjectLoaded(this, EventArgs.Empty);
				}
			}
		}

		private bool SaveCurrentProjectIfRequired()
		{
			if (!this._tracker.IsDirty)
				return true;

			DialogResult result = AskToSaveProject();

			if (result == DialogResult.Yes)
			{
				var newProject = UpdateProjectFromUI();
				newProject.CopyMetaFrom(this._project);
				this._project = newProject;

				if (string.IsNullOrEmpty(this._project.FileName))
				{
					string saveProject = PromptForFileNameToSaveProject(this._project.Product.Name + "-" + this._project.Product.ComparedIncrements.First.Name + "-" + this._project.Product.ComparedIncrements.Second.Name);

					if (!string.IsNullOrEmpty(saveProject))
					{
						this._project.FileName = saveProject;
					}
					else
					{
						// not saving...
						return false;
					}
				}

				if (!string.IsNullOrEmpty(this._project.FileName))
				{
					ProjectWriter.Save(this._project, this._project.FileName);

					if (this.ProjectSaved != null)
					{
						this.ProjectSaved(this, EventArgs.Empty);
					}

				}
			}
			else if (result == DialogResult.Cancel)
			{
				// quit.
				return false;
			}

			return true;
		}

		private void buildToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			Build();
		}

		private void Build()
		{
            this.fileToolStripMenuItem.Enabled = this.buildToolStripMenuItem.Enabled = false;
            this._dataEntryState.Disable();
            this._progressState.Visible();

            this.progressBar.Minimum = 0;
            this.progressBar.Value = 0;
            this.progressBar.Maximum = 100;

            try
            {
			    var newProject = UpdateProjectFromUI();
			    newProject.CopyMetaFrom(this._project);
			    this._project = newProject;

			    var errors = this.ValidateProject(this._project);

			    if (errors.Any())
			    {
                    this._progressState.Invisible();

                    this.fileToolStripMenuItem.Enabled = this.buildToolStripMenuItem.Enabled = true;
                    this._dataEntryState.Enable();

                    StringBuilder messageBuilder = new StringBuilder();

                    foreach (var error in errors)
                    {
                        messageBuilder.AppendLine(error);
                    }

                    MessageBox.Show(messageBuilder.ToString());
				    return;
			    }

			    // auto save...
			    if (this._tracker.IsDirty && !string.IsNullOrEmpty(this._project.FileName) && File.Exists(this._project.FileName))
			    {
				    ProjectWriter.Save(this._project, this._project.FileName);
			    }

				IProgress<Progress> progressIndicator = new Progress<Progress>(value =>
				{
					if (!String.IsNullOrEmpty(value.Description))
					{
						this.progressLabel.Visible = true;
						this.progressLabel.Text = value.Description;
					}
					else
						this.progressLabel.Visible = false;

					// this.progressBar.Value = value.Percent;
				});

				Task t = new Task(() =>
				{
					progressIndicator.Report(new Progress("Starting..."));

					IFileFinder finder = new FileFinder(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), FileFilterConstants.AssemblyFilter);

					AnalysisWorkflow analysis = new AnalysisWorkflow(
						finder,
						new CecilReflectorFactory());

					progressIndicator.Report(new Progress("Loading Plugins..."));

					InspectorRepository ir = new InspectorRepository();
					ir.Find(finder);

					InspectorFilter filter = new InspectorFilter(this._project.Settings.IgnoreInspectors);

					ir.Filter(filter);

					progressIndicator.Report(new Progress("Starting Analysis..."));

					var result = analysis.RunAnalysis(this._project, ir, progressIndicator);

                    // now modify results.
                    foreach(var r in result.TypeLevelChanges)
                    {
                        // if no changes .. set assembly level changes...
                    }

                    foreach(var a in result.AssemblyLevelChanges)
                    {
                        // if no assembly level changes set changed assemblies to zero.
                    }

					IReportingRepository rr = new ReportingRepository();
					rr.Find(finder);

					IReportingWorkflow reporting = new ReportingWorkflow();

					progressIndicator.Report(new Progress("Starting Reports..."));

					reporting.RunReports(this._project, rr, result, progressIndicator);

				});

				Task t2 = t.ContinueWith((antecedent) =>
					{
						this._progressState.Invisible();

						this.fileToolStripMenuItem.Enabled = this.buildToolStripMenuItem.Enabled = true;
						this._dataEntryState.Enable();
					},
					TaskScheduler.FromCurrentSynchronizationContext());

				t.Start();
			}
			catch(Exception ex)
			{
                MessageBox.Show(ex.GetBaseException().Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		
		private void txtProductName_Validated(object sender, EventArgs e)
		{
			string validationMessage = string.IsNullOrEmpty(this.txtProductName.Text) ? "Please provide a name for the product" : string.Empty;

			this.productNameErrorProvider.SetError(this.txtProductName, validationMessage);
		}

		private void txtPreviousVersion_Validated(object sender, EventArgs e)
		{
			string validationMessage = string.IsNullOrEmpty(this.txtPreviousVersion.Text) ? "Please provide a name for the previous version" : string.Empty;

			this.previousVersionErrorProvider.SetError(this.txtPreviousVersion, validationMessage);
		}

		private void txtNewVersion_Validated(object sender, EventArgs e)
		{
			string validationMessage = string.IsNullOrEmpty(this.txtNewVersion.Text) ? "Please provide a name for the new version" : string.Empty;

			this.newVersionErrorProvider.SetError(this.txtNewVersion, validationMessage);
		}

		private void asNewVersion_ListChanged_1(object sender, EventArgs e)
		{
			this.folder2Label.Text = asNewVersion.Folder;
		}

		private void asPreviousVersion_ListChanged_1(object sender, EventArgs e)
		{
			this.folder1Label.Text = asPreviousVersion.Folder;
		}

		private void aboutNDifferenceToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AboutBox about = new AboutBox();

			about.ShowDialog(this);
		}
	}
}
