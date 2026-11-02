namespace NDifference.UI
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
      components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
      menuStrip1 = new System.Windows.Forms.MenuStrip();
      fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      newProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      openProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      saveProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      saveProjectAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      buildToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      buildToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      aboutNDifferenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      progressBar = new System.Windows.Forms.ProgressBar();
      btnStart = new System.Windows.Forms.Button();
      btnCancel = new System.Windows.Forms.Button();
      productNameErrorProvider = new System.Windows.Forms.ErrorProvider(components);
      previousVersionErrorProvider = new System.Windows.Forms.ErrorProvider(components);
      newVersionErrorProvider = new System.Windows.Forms.ErrorProvider(components);
      tcContents = new System.Windows.Forms.TabControl();
      tpAssemblies = new System.Windows.Forms.TabPage();
      folder2Label = new System.Windows.Forms.Label();
      folder1Label = new System.Windows.Forms.Label();
      asNewVersion = new NDifference.UI.Controls.AssemblySelection();
      asPreviousVersion = new NDifference.UI.Controls.AssemblySelection();
      tpInspectors = new System.Windows.Forms.TabPage();
      tvInspectors = new System.Windows.Forms.TreeView();
      progressLabel = new System.Windows.Forms.Label();
      fsOutputFolder = new NDifference.UI.Controls.FolderSelect();
      txtNewVersion = new NDifference.UI.Controls.WatermarkedTextBox();
      txtPreviousVersion = new NDifference.UI.Controls.WatermarkedTextBox();
      lblVersionInfo = new NDifference.UI.Controls.EmphasisLabel();
      txtProductName = new NDifference.UI.Controls.WatermarkedTextBox();
      lblProductInformation = new NDifference.UI.Controls.EmphasisLabel();
      viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      menuStrip1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)productNameErrorProvider).BeginInit();
      ((System.ComponentModel.ISupportInitialize)previousVersionErrorProvider).BeginInit();
      ((System.ComponentModel.ISupportInitialize)newVersionErrorProvider).BeginInit();
      tcContents.SuspendLayout();
      tpAssemblies.SuspendLayout();
      tpInspectors.SuspendLayout();
      SuspendLayout();
      // 
      // menuStrip1
      // 
      menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem, buildToolStripMenuItem, helpToolStripMenuItem });
      menuStrip1.Location = new System.Drawing.Point(0, 0);
      menuStrip1.Name = "menuStrip1";
      menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
      menuStrip1.Size = new System.Drawing.Size(892, 24);
      menuStrip1.TabIndex = 0;
      menuStrip1.Text = "menuStrip1";
      // 
      // fileToolStripMenuItem
      // 
      fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { newProjectToolStripMenuItem, openProjectToolStripMenuItem, toolStripSeparator2, saveProjectToolStripMenuItem, saveProjectAsToolStripMenuItem, toolStripSeparator1, exitToolStripMenuItem });
      fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      fileToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N;
      fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
      fileToolStripMenuItem.Text = "File";
      // 
      // newProjectToolStripMenuItem
      // 
      newProjectToolStripMenuItem.Name = "newProjectToolStripMenuItem";
      newProjectToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N;
      newProjectToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
      newProjectToolStripMenuItem.Text = "New Project";
      newProjectToolStripMenuItem.Click += newProjectToolStripMenuItem_Click;
      // 
      // openProjectToolStripMenuItem
      // 
      openProjectToolStripMenuItem.Name = "openProjectToolStripMenuItem";
      openProjectToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O;
      openProjectToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
      openProjectToolStripMenuItem.Text = "Open Project...";
      openProjectToolStripMenuItem.Click += openProjectToolStripMenuItem_Click;
      // 
      // toolStripSeparator2
      // 
      toolStripSeparator2.Name = "toolStripSeparator2";
      toolStripSeparator2.Size = new System.Drawing.Size(192, 6);
      // 
      // saveProjectToolStripMenuItem
      // 
      saveProjectToolStripMenuItem.Name = "saveProjectToolStripMenuItem";
      saveProjectToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S;
      saveProjectToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
      saveProjectToolStripMenuItem.Text = "Save Project...";
      saveProjectToolStripMenuItem.Click += saveProjectToolStripMenuItem_Click;
      // 
      // saveProjectAsToolStripMenuItem
      // 
      saveProjectAsToolStripMenuItem.Name = "saveProjectAsToolStripMenuItem";
      saveProjectAsToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
      saveProjectAsToolStripMenuItem.Text = "Save Project As...";
      saveProjectAsToolStripMenuItem.Click += saveProjectAsToolStripMenuItem_Click;
      // 
      // toolStripSeparator1
      // 
      toolStripSeparator1.Name = "toolStripSeparator1";
      toolStripSeparator1.Size = new System.Drawing.Size(192, 6);
      // 
      // exitToolStripMenuItem
      // 
      exitToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      exitToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4;
      exitToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
      exitToolStripMenuItem.Text = "Exit";
      exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
      // 
      // buildToolStripMenuItem
      // 
      buildToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { buildToolStripMenuItem1, viewToolStripMenuItem });
      buildToolStripMenuItem.Name = "buildToolStripMenuItem";
      buildToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
      buildToolStripMenuItem.Text = "Build";
      // 
      // buildToolStripMenuItem1
      // 
      buildToolStripMenuItem1.Name = "buildToolStripMenuItem1";
      buildToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F6;
      buildToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
      buildToolStripMenuItem1.Text = "Build";
      buildToolStripMenuItem1.Click += buildToolStripMenuItem1_Click;
      // 
      // helpToolStripMenuItem
      // 
      helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { aboutNDifferenceToolStripMenuItem });
      helpToolStripMenuItem.Name = "helpToolStripMenuItem";
      helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
      helpToolStripMenuItem.Text = "Help";
      // 
      // aboutNDifferenceToolStripMenuItem
      // 
      aboutNDifferenceToolStripMenuItem.Name = "aboutNDifferenceToolStripMenuItem";
      aboutNDifferenceToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
      aboutNDifferenceToolStripMenuItem.Text = "About NDifference";
      aboutNDifferenceToolStripMenuItem.Click += aboutNDifferenceToolStripMenuItem_Click;
      // 
      // progressBar
      // 
      progressBar.Location = new System.Drawing.Point(14, 706);
      progressBar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
      progressBar.Name = "progressBar";
      progressBar.Size = new System.Drawing.Size(864, 27);
      progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
      progressBar.TabIndex = 15;
      // 
      // btnStart
      // 
      btnStart.Location = new System.Drawing.Point(14, 740);
      btnStart.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
      btnStart.Name = "btnStart";
      btnStart.Size = new System.Drawing.Size(88, 27);
      btnStart.TabIndex = 16;
      btnStart.Text = "Build";
      btnStart.UseVisualStyleBackColor = true;
      btnStart.Click += btnStart_Click;
      // 
      // btnCancel
      // 
      btnCancel.Location = new System.Drawing.Point(791, 740);
      btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
      btnCancel.Name = "btnCancel";
      btnCancel.Size = new System.Drawing.Size(88, 27);
      btnCancel.TabIndex = 17;
      btnCancel.Text = "Cancel";
      btnCancel.UseVisualStyleBackColor = true;
      btnCancel.Click += btnCancel_Click;
      // 
      // productNameErrorProvider
      // 
      productNameErrorProvider.ContainerControl = this;
      // 
      // previousVersionErrorProvider
      // 
      previousVersionErrorProvider.ContainerControl = this;
      // 
      // newVersionErrorProvider
      // 
      newVersionErrorProvider.ContainerControl = this;
      // 
      // tcContents
      // 
      tcContents.Controls.Add(tpAssemblies);
      tcContents.Controls.Add(tpInspectors);
      tcContents.Location = new System.Drawing.Point(14, 159);
      tcContents.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
      tcContents.Name = "tcContents";
      tcContents.SelectedIndex = 0;
      tcContents.Size = new System.Drawing.Size(864, 440);
      tcContents.TabIndex = 18;
      // 
      // tpAssemblies
      // 
      tpAssemblies.Controls.Add(folder2Label);
      tpAssemblies.Controls.Add(folder1Label);
      tpAssemblies.Controls.Add(asNewVersion);
      tpAssemblies.Controls.Add(asPreviousVersion);
      tpAssemblies.Location = new System.Drawing.Point(4, 24);
      tpAssemblies.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
      tpAssemblies.Name = "tpAssemblies";
      tpAssemblies.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
      tpAssemblies.Size = new System.Drawing.Size(856, 412);
      tpAssemblies.TabIndex = 0;
      tpAssemblies.Text = "Assemblies";
      tpAssemblies.UseVisualStyleBackColor = true;
      // 
      // folder2Label
      // 
      folder2Label.AutoEllipsis = true;
      folder2Label.AutoSize = true;
      folder2Label.Location = new System.Drawing.Point(432, 30);
      folder2Label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      folder2Label.Name = "folder2Label";
      folder2Label.Size = new System.Drawing.Size(166, 15);
      folder2Label.TabIndex = 10;
      folder2Label.Text = "<Please select a target folder>";
      // 
      // folder1Label
      // 
      folder1Label.AutoEllipsis = true;
      folder1Label.AutoSize = true;
      folder1Label.Location = new System.Drawing.Point(0, 30);
      folder1Label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      folder1Label.Name = "folder1Label";
      folder1Label.Size = new System.Drawing.Size(170, 15);
      folder1Label.TabIndex = 9;
      folder1Label.Text = "<Please select a source folder>";
      // 
      // asNewVersion
      // 
      asNewVersion.AllowDrop = true;
      asNewVersion.Location = new System.Drawing.Point(435, 48);
      asNewVersion.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
      asNewVersion.Name = "asNewVersion";
      asNewVersion.Size = new System.Drawing.Size(413, 358);
      asNewVersion.TabIndex = 8;
      asNewVersion.ListChanged += asNewVersion_ListChanged_1;
      // 
      // asPreviousVersion
      // 
      asPreviousVersion.AllowDrop = true;
      asPreviousVersion.Location = new System.Drawing.Point(4, 48);
      asPreviousVersion.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
      asPreviousVersion.Name = "asPreviousVersion";
      asPreviousVersion.Size = new System.Drawing.Size(413, 358);
      asPreviousVersion.TabIndex = 7;
      asPreviousVersion.ListChanged += asPreviousVersion_ListChanged_1;
      // 
      // tpInspectors
      // 
      tpInspectors.Controls.Add(tvInspectors);
      tpInspectors.Location = new System.Drawing.Point(4, 24);
      tpInspectors.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
      tpInspectors.Name = "tpInspectors";
      tpInspectors.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
      tpInspectors.Size = new System.Drawing.Size(856, 412);
      tpInspectors.TabIndex = 1;
      tpInspectors.Text = "Inspectors";
      tpInspectors.UseVisualStyleBackColor = true;
      // 
      // tvInspectors
      // 
      tvInspectors.Location = new System.Drawing.Point(7, 7);
      tvInspectors.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
      tvInspectors.Name = "tvInspectors";
      tvInspectors.Size = new System.Drawing.Size(209, 59);
      tvInspectors.TabIndex = 0;
      // 
      // progressLabel
      // 
      progressLabel.AutoSize = true;
      progressLabel.Location = new System.Drawing.Point(14, 688);
      progressLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      progressLabel.Name = "progressLabel";
      progressLabel.Size = new System.Drawing.Size(16, 15);
      progressLabel.TabIndex = 19;
      progressLabel.Text = "...";
      // 
      // fsOutputFolder
      // 
      fsOutputFolder.FolderLabel = "Output Folder";
      fsOutputFolder.Location = new System.Drawing.Point(14, 606);
      fsOutputFolder.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
      fsOutputFolder.Name = "fsOutputFolder";
      fsOutputFolder.Size = new System.Drawing.Size(864, 35);
      fsOutputFolder.TabIndex = 8;
      // 
      // txtNewVersion
      // 
      txtNewVersion.Location = new System.Drawing.Point(454, 129);
      txtNewVersion.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
      txtNewVersion.Name = "txtNewVersion";
      txtNewVersion.Size = new System.Drawing.Size(367, 23);
      txtNewVersion.TabIndex = 5;
      txtNewVersion.WatermarkText = "New Version";
      txtNewVersion.TextChanged += txtNewVersion_TextChanged;
      txtNewVersion.Validated += txtNewVersion_Validated;
      // 
      // txtPreviousVersion
      // 
      txtPreviousVersion.Location = new System.Drawing.Point(14, 129);
      txtPreviousVersion.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
      txtPreviousVersion.Name = "txtPreviousVersion";
      txtPreviousVersion.Size = new System.Drawing.Size(378, 23);
      txtPreviousVersion.TabIndex = 4;
      txtPreviousVersion.WatermarkText = "Previous Version";
      txtPreviousVersion.TextChanged += txtPreviousVersion_TextChanged;
      txtPreviousVersion.Validated += txtPreviousVersion_Validated;
      // 
      // lblVersionInfo
      // 
      lblVersionInfo.AutoSize = true;
      lblVersionInfo.Location = new System.Drawing.Point(10, 111);
      lblVersionInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      lblVersionInfo.Name = "lblVersionInfo";
      lblVersionInfo.Size = new System.Drawing.Size(162, 15);
      lblVersionInfo.TabIndex = 3;
      lblVersionInfo.Text = "<b>Version</b> Information";
      // 
      // txtProductName
      // 
      txtProductName.Location = new System.Drawing.Point(14, 68);
      txtProductName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
      txtProductName.Name = "txtProductName";
      txtProductName.Size = new System.Drawing.Size(116, 23);
      txtProductName.TabIndex = 2;
      txtProductName.WatermarkText = "Product Name";
      txtProductName.TextChanged += txtProductName_TextChanged;
      txtProductName.Validated += txtProductName_Validated;
      // 
      // lblProductInformation
      // 
      lblProductInformation.AutoSize = true;
      lblProductInformation.Location = new System.Drawing.Point(10, 50);
      lblProductInformation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      lblProductInformation.Name = "lblProductInformation";
      lblProductInformation.Size = new System.Drawing.Size(166, 15);
      lblProductInformation.TabIndex = 1;
      lblProductInformation.Text = "<b>Product</b> Information";
      // 
      // viewToolStripMenuItem
      // 
      viewToolStripMenuItem.Name = "viewToolStripMenuItem";
      viewToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
      viewToolStripMenuItem.Text = "View Output";
      viewToolStripMenuItem.Click += viewToolStripMenuItem_Click;
      // 
      // MainForm
      // 
      AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      ClientSize = new System.Drawing.Size(892, 780);
      Controls.Add(progressLabel);
      Controls.Add(tcContents);
      Controls.Add(btnCancel);
      Controls.Add(btnStart);
      Controls.Add(progressBar);
      Controls.Add(fsOutputFolder);
      Controls.Add(txtNewVersion);
      Controls.Add(txtPreviousVersion);
      Controls.Add(lblVersionInfo);
      Controls.Add(txtProductName);
      Controls.Add(lblProductInformation);
      Controls.Add(menuStrip1);
      FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
      Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
      MaximizeBox = false;
      Name = "MainForm";
      Text = "NDifference";
      Load += MainForm_Load;
      menuStrip1.ResumeLayout(false);
      menuStrip1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)productNameErrorProvider).EndInit();
      ((System.ComponentModel.ISupportInitialize)previousVersionErrorProvider).EndInit();
      ((System.ComponentModel.ISupportInitialize)newVersionErrorProvider).EndInit();
      tcContents.ResumeLayout(false);
      tpAssemblies.ResumeLayout(false);
      tpAssemblies.PerformLayout();
      tpInspectors.ResumeLayout(false);
      ResumeLayout(false);
      PerformLayout();

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
    private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
  }
}

