using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NDifference.UI.Controls
{
	[System.ComponentModel.DefaultBindingProperty("FolderPath")]
	public partial class FolderSelect : UserControl
	{
		public event EventHandler FolderChanged;

		public FolderSelect()
		{
			InitializeComponent();
		}

		public string FolderPath
		{
			get
			{
				return this.watermarkedTextBox1.Text;
			}
			set
			{
				this.watermarkedTextBox1.Text = value;
			}
		}

		public string FolderLabel
		{
			get
			{
				return this.watermarkedTextBox1.WatermarkText;
			}
			set
			{
				this.watermarkedTextBox1.WatermarkText = value;
			}
		}


		private void btnBrowser_Click(object sender, EventArgs e)
		{
			using (FolderBrowserDialog dialog = new FolderBrowserDialog())
			{
				if (!string.IsNullOrEmpty(this.FolderPath))
				{
					dialog.SelectedPath = this.FolderPath;
				}

				if (DialogResult.OK == dialog.ShowDialog())
				{
					this.watermarkedTextBox1.Text = dialog.SelectedPath;

					if (this.FolderChanged != null)
					{
						this.FolderChanged(this, EventArgs.Empty);
					}
				}
			}
		}

	}
}
