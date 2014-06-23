using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.IO;

namespace NDifference.UI.Controls
{
	public partial class AssemblySelection : UserControl
	{
		public event EventHandler ListChanged;

		public AssemblySelection()
		{
			InitializeComponent();

			this.AllowDrop = true;

			this.DragEnter += AssemblySelection_DragEnter;
			this.DragDrop += AssemblySelection_DragDrop;
			this.lvAssemblies.View = View.Details;

			this.lvAssemblies.Columns.Add("Name", 250, HorizontalAlignment.Left);
			this.lblFolder.Text = string.Empty;
		}

		void AssemblySelection_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				e.Effect = DragDropEffects.Copy;
			}
		}

		void AssemblySelection_DragDrop(object sender, DragEventArgs e)
		{
			string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

			if (files.Length > 0)
			{
				this.lblFolder.Text = System.IO.Path.GetDirectoryName(files[0]);
			}

			lvAssemblies.BeginUpdate();

			foreach (string file in files)
			{
				lvAssemblies.Items.Add(new AssemblyListViewItem(file));
			}

			lvAssemblies.EndUpdate();
			
			if (this.ListChanged != null)
			{
				this.ListChanged(this, EventArgs.Empty);
			}
		}
		
		public ReadOnlyCollection<string> SelectedAssemblies
		{
			get
			{
				List<string> files = new List<string>();

				foreach (var item in lvAssemblies.Items)
				{
					AssemblyListViewItem alvi = item as AssemblyListViewItem;

					if (alvi != null)
					{
						files.Add(alvi.FullPath);
					}
				}

				return new ReadOnlyCollection<string>(files);
			}
		}

		public void Initialise(IEnumerable<IAssemblyDiskInfo> files)
		{
			//lvAssemblies.Clear();
			lvAssemblies.BeginUpdate();
			
			lvAssemblies.Items.Clear();

			// duplicates ???
			foreach (var file in files)
			{
				lvAssemblies.Items.Add(new AssemblyListViewItem(file.Path));
			}

			lvAssemblies.EndUpdate();

			var first = files.FirstOrDefault();
			
			if (first != null)
			{
				this.Path = first.Path;
				this.lblFolder.Text = this.Path;
			}

			btnRemove.Enabled = false;
		}

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			using (FolderBrowserDialog dialog = new FolderBrowserDialog())
			{
				if (!string.IsNullOrEmpty(this.Path))
				{
					dialog.SelectedPath = this.Path;
				}
				else
				{
					dialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				}

				if (DialogResult.OK == dialog.ShowDialog())
				{
					this.Path = dialog.SelectedPath;

					lvAssemblies.BeginUpdate();

					FileFinder finder = new FileFinder { Filter = FileFilterConstants.AssemblyFilter, Folder = this.Path };

					foreach (var file in finder.Find())
					{
						lvAssemblies.Items.Add(new AssemblyListViewItem(file.FullPath));
					}

					lvAssemblies.EndUpdate();

					this.lblFolder.Text = this.Path;

					if (this.ListChanged != null)
					{
						this.ListChanged(this, EventArgs.Empty);
					}
				}
			}
		}

		private string Path
		{
			get;
			set;
		}


		private void btnAdd_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog dialog = new OpenFileDialog())
			{
				dialog.Multiselect = true;
				dialog.Filter = FileFilterConstants.AssemblyFileDialogFilter;

				if (DialogResult.OK == dialog.ShowDialog())
				{
					lvAssemblies.BeginUpdate();

					// duplicates ???
					foreach (var file in dialog.FileNames)
					{
						lvAssemblies.Items.Add(new AssemblyListViewItem(file));
					}

					if (dialog.FileNames.Length > 0)
					{
						this.lblFolder.Text = System.IO.Path.GetDirectoryName(dialog.FileNames[0]);
					}

					lvAssemblies.EndUpdate();

					if (this.ListChanged != null)
					{
						this.ListChanged(this, EventArgs.Empty);
					}
				}
			}
		}

		private void bntRemove_Click(object sender, EventArgs e)
		{
			if (lvAssemblies.SelectedItems.Count > 0)
			{
				lvAssemblies.BeginUpdate();

				foreach (ListViewItem item in lvAssemblies.SelectedItems)
				{
					item.Remove();
				}

				lvAssemblies.EndUpdate();

				if (this.ListChanged != null)
				{
					this.ListChanged(this, EventArgs.Empty);
				}
			}
		}

		private void lvAssemblies_SelectedIndexChanged(object sender, EventArgs e)
		{
			btnRemove.Enabled = lvAssemblies.SelectedItems.Count > 0;
		}
	}


	[Serializable]
	public class AssemblyListViewItem : ListViewItem
	{
		public AssemblyListViewItem(string fullPath)
		{
			this.FullPath = fullPath;
			this.Text = System.IO.Path.GetFileName(this.FullPath);
		}

		public string FullPath
		{
			get;
			set;
		}
	}
}
