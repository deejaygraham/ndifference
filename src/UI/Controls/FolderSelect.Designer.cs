namespace NDifference.UI.Controls
{
	partial class FolderSelect
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.watermarkedTextBox1 = new NDifference.UI.Controls.WatermarkedTextBox();
			this.btnBrowser = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// watermarkedTextBox1
			// 
			this.watermarkedTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.watermarkedTextBox1.Location = new System.Drawing.Point(0, 3);
			this.watermarkedTextBox1.Margin = new System.Windows.Forms.Padding(0);
			this.watermarkedTextBox1.Name = "watermarkedTextBox1";
			this.watermarkedTextBox1.Size = new System.Drawing.Size(268, 20);
			this.watermarkedTextBox1.TabIndex = 0;
			this.watermarkedTextBox1.WatermarkText = "<Watermark>";
			// 
			// btnBrowser
			// 
			this.btnBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBrowser.Location = new System.Drawing.Point(284, 1);
			this.btnBrowser.Name = "btnBrowser";
			this.btnBrowser.Size = new System.Drawing.Size(26, 23);
			this.btnBrowser.TabIndex = 1;
			this.btnBrowser.Text = "...";
			this.btnBrowser.UseVisualStyleBackColor = true;
			this.btnBrowser.Click += new System.EventHandler(this.btnBrowser_Click);
			// 
			// FolderSelect
			// 
			this.Controls.Add(this.btnBrowser);
			this.Controls.Add(this.watermarkedTextBox1);
			this.Name = "FolderSelect";
			this.Size = new System.Drawing.Size(310, 30);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private NDifference.UI.Controls.WatermarkedTextBox watermarkedTextBox1;
		private System.Windows.Forms.Button btnBrowser;
	}
}
