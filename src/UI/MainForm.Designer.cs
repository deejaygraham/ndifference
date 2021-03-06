﻿namespace NDifference.UI
{
	public partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">True if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (this.components != null))
			{
				this.components.Dispose();
			}

			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.saveProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveProjectAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.buildToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.buildToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.btnStart = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.productNameErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			this.previousVersionErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			this.newVersionErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			this.tcContents = new System.Windows.Forms.TabControl();
			this.tpAssemblies = new System.Windows.Forms.TabPage();
			this.folder2Label = new System.Windows.Forms.Label();
			this.folder1Label = new System.Windows.Forms.Label();
			this.asNewVersion = new NDifference.UI.Controls.AssemblySelection();
			this.asPreviousVersion = new NDifference.UI.Controls.AssemblySelection();
			this.tpInspectors = new System.Windows.Forms.TabPage();
			this.tvInspectors = new System.Windows.Forms.TreeView();
			this.progressLabel = new System.Windows.Forms.Label();
			this.fsOutputFolder = new NDifference.UI.Controls.FolderSelect();
			this.txtNewVersion = new NDifference.UI.Controls.WatermarkedTextBox();
			this.txtPreviousVersion = new NDifference.UI.Controls.WatermarkedTextBox();
			this.lblVersionInfo = new NDifference.UI.Controls.EmphasisLabel();
			this.txtProductName = new NDifference.UI.Controls.WatermarkedTextBox();
			this.lblProductInformation = new NDifference.UI.Controls.EmphasisLabel();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutNDifferenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.productNameErrorProvider)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.previousVersionErrorProvider)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.newVersionErrorProvider)).BeginInit();
			this.tcContents.SuspendLayout();
			this.tpAssemblies.SuspendLayout();
			this.tpInspectors.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.buildToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(765, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newProjectToolStripMenuItem,
            this.openProjectToolStripMenuItem,
            this.toolStripSeparator2,
            this.saveProjectToolStripMenuItem,
            this.saveProjectAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// newProjectToolStripMenuItem
			// 
			this.newProjectToolStripMenuItem.Name = "newProjectToolStripMenuItem";
			this.newProjectToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.newProjectToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
			this.newProjectToolStripMenuItem.Text = "New Project";
			this.newProjectToolStripMenuItem.Click += new System.EventHandler(this.newProjectToolStripMenuItem_Click);
			// 
			// openProjectToolStripMenuItem
			// 
			this.openProjectToolStripMenuItem.Name = "openProjectToolStripMenuItem";
			this.openProjectToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.openProjectToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
			this.openProjectToolStripMenuItem.Text = "Open Project...";
			this.openProjectToolStripMenuItem.Click += new System.EventHandler(this.openProjectToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(192, 6);
			// 
			// saveProjectToolStripMenuItem
			// 
			this.saveProjectToolStripMenuItem.Name = "saveProjectToolStripMenuItem";
			this.saveProjectToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.saveProjectToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
			this.saveProjectToolStripMenuItem.Text = "Save Project...";
			this.saveProjectToolStripMenuItem.Click += new System.EventHandler(this.saveProjectToolStripMenuItem_Click);
			// 
			// saveProjectAsToolStripMenuItem
			// 
			this.saveProjectAsToolStripMenuItem.Name = "saveProjectAsToolStripMenuItem";
			this.saveProjectAsToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
			this.saveProjectAsToolStripMenuItem.Text = "Save Project As...";
			this.saveProjectAsToolStripMenuItem.Click += new System.EventHandler(this.saveProjectAsToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(192, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// buildToolStripMenuItem
			// 
			this.buildToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buildToolStripMenuItem1});
			this.buildToolStripMenuItem.Name = "buildToolStripMenuItem";
			this.buildToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
			this.buildToolStripMenuItem.Text = "Build";
			// 
			// buildToolStripMenuItem1
			// 
			this.buildToolStripMenuItem1.Name = "buildToolStripMenuItem1";
			this.buildToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F6;
			this.buildToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
			this.buildToolStripMenuItem1.Text = "Build";
			this.buildToolStripMenuItem1.Click += new System.EventHandler(this.buildToolStripMenuItem1_Click);
			// 
			// progressBar
			// 
			this.progressBar.Location = new System.Drawing.Point(12, 612);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(741, 23);
			this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.progressBar.TabIndex = 15;
			// 
			// btnStart
			// 
			this.btnStart.Location = new System.Drawing.Point(12, 641);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(75, 23);
			this.btnStart.TabIndex = 16;
			this.btnStart.Text = "Build";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(678, 641);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 17;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// productNameErrorProvider
			// 
			this.productNameErrorProvider.ContainerControl = this;
			// 
			// previousVersionErrorProvider
			// 
			this.previousVersionErrorProvider.ContainerControl = this;
			// 
			// newVersionErrorProvider
			// 
			this.newVersionErrorProvider.ContainerControl = this;
			// 
			// tcContents
			// 
			this.tcContents.Controls.Add(this.tpAssemblies);
			this.tcContents.Controls.Add(this.tpInspectors);
			this.tcContents.Location = new System.Drawing.Point(12, 138);
			this.tcContents.Name = "tcContents";
			this.tcContents.SelectedIndex = 0;
			this.tcContents.Size = new System.Drawing.Size(741, 381);
			this.tcContents.TabIndex = 18;
			// 
			// tpAssemblies
			// 
			this.tpAssemblies.Controls.Add(this.folder2Label);
			this.tpAssemblies.Controls.Add(this.folder1Label);
			this.tpAssemblies.Controls.Add(this.asNewVersion);
			this.tpAssemblies.Controls.Add(this.asPreviousVersion);
			this.tpAssemblies.Location = new System.Drawing.Point(4, 22);
			this.tpAssemblies.Name = "tpAssemblies";
			this.tpAssemblies.Padding = new System.Windows.Forms.Padding(3);
			this.tpAssemblies.Size = new System.Drawing.Size(733, 355);
			this.tpAssemblies.TabIndex = 0;
			this.tpAssemblies.Text = "Assemblies";
			this.tpAssemblies.UseVisualStyleBackColor = true;
			// 
			// folder2Label
			// 
			this.folder2Label.AutoEllipsis = true;
			this.folder2Label.AutoSize = true;
			this.folder2Label.Location = new System.Drawing.Point(370, 26);
			this.folder2Label.Name = "folder2Label";
			this.folder2Label.Size = new System.Drawing.Size(150, 13);
			this.folder2Label.TabIndex = 10;
			this.folder2Label.Text = "<Please select a target folder>";
			// 
			// folder1Label
			// 
			this.folder1Label.AutoEllipsis = true;
			this.folder1Label.AutoSize = true;
			this.folder1Label.Location = new System.Drawing.Point(0, 26);
			this.folder1Label.Name = "folder1Label";
			this.folder1Label.Size = new System.Drawing.Size(155, 13);
			this.folder1Label.TabIndex = 9;
			this.folder1Label.Text = "<Please select a source folder>";
			// 
			// asNewVersion
			// 
			this.asNewVersion.AllowDrop = true;
			this.asNewVersion.Location = new System.Drawing.Point(373, 42);
			this.asNewVersion.Name = "asNewVersion";
			this.asNewVersion.Size = new System.Drawing.Size(354, 310);
			this.asNewVersion.TabIndex = 8;
			this.asNewVersion.ListChanged += new System.EventHandler(this.asNewVersion_ListChanged_1);
			// 
			// asPreviousVersion
			// 
			this.asPreviousVersion.AllowDrop = true;
			this.asPreviousVersion.Location = new System.Drawing.Point(3, 42);
			this.asPreviousVersion.Name = "asPreviousVersion";
			this.asPreviousVersion.Size = new System.Drawing.Size(354, 310);
			this.asPreviousVersion.TabIndex = 7;
			this.asPreviousVersion.ListChanged += new System.EventHandler(this.asPreviousVersion_ListChanged_1);
			// 
			// tpInspectors
			// 
			this.tpInspectors.Controls.Add(this.tvInspectors);
			this.tpInspectors.Location = new System.Drawing.Point(4, 22);
			this.tpInspectors.Name = "tpInspectors";
			this.tpInspectors.Padding = new System.Windows.Forms.Padding(3);
			this.tpInspectors.Size = new System.Drawing.Size(733, 355);
			this.tpInspectors.TabIndex = 1;
			this.tpInspectors.Text = "Inspectors";
			this.tpInspectors.UseVisualStyleBackColor = true;
			// 
			// tvInspectors
			// 
			this.tvInspectors.Location = new System.Drawing.Point(6, 6);
			this.tvInspectors.Name = "tvInspectors";
			this.tvInspectors.Size = new System.Drawing.Size(180, 52);
			this.tvInspectors.TabIndex = 0;
			// 
			// progressLabel
			// 
			this.progressLabel.AutoSize = true;
			this.progressLabel.Location = new System.Drawing.Point(12, 596);
			this.progressLabel.Name = "progressLabel";
			this.progressLabel.Size = new System.Drawing.Size(16, 13);
			this.progressLabel.TabIndex = 19;
			this.progressLabel.Text = "...";
			// 
			// fsOutputFolder
			// 
			this.fsOutputFolder.FolderLabel = "Output Folder";
			this.fsOutputFolder.FolderPath = "";
			this.fsOutputFolder.Location = new System.Drawing.Point(12, 525);
			this.fsOutputFolder.Name = "fsOutputFolder";
			this.fsOutputFolder.Size = new System.Drawing.Size(741, 30);
			this.fsOutputFolder.TabIndex = 8;
			// 
			// txtNewVersion
			// 
			this.txtNewVersion.Location = new System.Drawing.Point(389, 112);
			this.txtNewVersion.Name = "txtNewVersion";
			this.txtNewVersion.Size = new System.Drawing.Size(315, 20);
			this.txtNewVersion.TabIndex = 5;
			this.txtNewVersion.WatermarkText = "New Version";
			this.txtNewVersion.TextChanged += new System.EventHandler(this.txtNewVersion_TextChanged);
			this.txtNewVersion.Validated += new System.EventHandler(this.txtNewVersion_Validated);
			// 
			// txtPreviousVersion
			// 
			this.txtPreviousVersion.Location = new System.Drawing.Point(12, 112);
			this.txtPreviousVersion.Name = "txtPreviousVersion";
			this.txtPreviousVersion.Size = new System.Drawing.Size(325, 20);
			this.txtPreviousVersion.TabIndex = 4;
			this.txtPreviousVersion.WatermarkText = "Previous Version";
			this.txtPreviousVersion.TextChanged += new System.EventHandler(this.txtPreviousVersion_TextChanged);
			this.txtPreviousVersion.Validated += new System.EventHandler(this.txtPreviousVersion_Validated);
			// 
			// lblVersionInfo
			// 
			this.lblVersionInfo.AutoSize = true;
			this.lblVersionInfo.Location = new System.Drawing.Point(9, 96);
			this.lblVersionInfo.Name = "lblVersionInfo";
			this.lblVersionInfo.Size = new System.Drawing.Size(138, 13);
			this.lblVersionInfo.TabIndex = 3;
			this.lblVersionInfo.Text = "<b>Version</b> Information";
			// 
			// txtProductName
			// 
			this.txtProductName.Location = new System.Drawing.Point(12, 59);
			this.txtProductName.Name = "txtProductName";
			this.txtProductName.Size = new System.Drawing.Size(100, 20);
			this.txtProductName.TabIndex = 2;
			this.txtProductName.WatermarkText = "Product Name";
			this.txtProductName.TextChanged += new System.EventHandler(this.txtProductName_TextChanged);
			this.txtProductName.Validated += new System.EventHandler(this.txtProductName_Validated);
			// 
			// lblProductInformation
			// 
			this.lblProductInformation.AutoSize = true;
			this.lblProductInformation.Location = new System.Drawing.Point(9, 43);
			this.lblProductInformation.Name = "lblProductInformation";
			this.lblProductInformation.Size = new System.Drawing.Size(140, 13);
			this.lblProductInformation.TabIndex = 1;
			this.lblProductInformation.Text = "<b>Product</b> Information";
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutNDifferenceToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem.Text = "Help";
			// 
			// aboutNDifferenceToolStripMenuItem
			// 
			this.aboutNDifferenceToolStripMenuItem.Name = "aboutNDifferenceToolStripMenuItem";
			this.aboutNDifferenceToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
			this.aboutNDifferenceToolStripMenuItem.Text = "About NDifference";
			this.aboutNDifferenceToolStripMenuItem.Click += new System.EventHandler(this.aboutNDifferenceToolStripMenuItem_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(765, 676);
			this.Controls.Add(this.progressLabel);
			this.Controls.Add(this.tcContents);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnStart);
			this.Controls.Add(this.progressBar);
			this.Controls.Add(this.fsOutputFolder);
			this.Controls.Add(this.txtNewVersion);
			this.Controls.Add(this.txtPreviousVersion);
			this.Controls.Add(this.lblVersionInfo);
			this.Controls.Add(this.txtProductName);
			this.Controls.Add(this.lblProductInformation);
			this.Controls.Add(this.menuStrip1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "NDifference";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.productNameErrorProvider)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.previousVersionErrorProvider)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.newVersionErrorProvider)).EndInit();
			this.tcContents.ResumeLayout(false);
			this.tpAssemblies.ResumeLayout(false);
			this.tpAssemblies.PerformLayout();
			this.tpInspectors.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private NDifference.UI.Controls.EmphasisLabel lblProductInformation;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openProjectToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private NDifference.UI.Controls.WatermarkedTextBox txtProductName;
		private NDifference.UI.Controls.EmphasisLabel lblVersionInfo;
		private NDifference.UI.Controls.WatermarkedTextBox txtPreviousVersion;
		private NDifference.UI.Controls.WatermarkedTextBox txtNewVersion;
		private NDifference.UI.Controls.FolderSelect fsOutputFolder;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ToolStripMenuItem saveProjectToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveProjectAsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newProjectToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem buildToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem buildToolStripMenuItem1;
		private System.Windows.Forms.ErrorProvider productNameErrorProvider;
		private System.Windows.Forms.ErrorProvider previousVersionErrorProvider;
		private System.Windows.Forms.ErrorProvider newVersionErrorProvider;
		private System.Windows.Forms.TabControl tcContents;
		private System.Windows.Forms.TabPage tpAssemblies;
		private System.Windows.Forms.TabPage tpInspectors;
		private System.Windows.Forms.TreeView tvInspectors;
		private Controls.AssemblySelection asNewVersion;
		private Controls.AssemblySelection asPreviousVersion;
		private System.Windows.Forms.Label progressLabel;
		private System.Windows.Forms.Label folder2Label;
		private System.Windows.Forms.Label folder1Label;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutNDifferenceToolStripMenuItem;
	}
}

