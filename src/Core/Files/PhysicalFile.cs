using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference
{
	public class PhysicalFile : IFile
	{
		public PhysicalFile(string fullPath)
		{
			this.FullPath = fullPath;
			this.Name = Path.GetFileName(fullPath);
			this.Folder = Path.GetDirectoryName(fullPath);
		}

		public PhysicalFile(FileInfo info)
			: this (info.FullName)
		{
		}

		public string Name { get; private set; }

		public string Folder { get; private set; }

		public string FullPath { get; private set; }

		public string RelativeTo(IFile other)
		{
			return other.FullPath.MakeRelativePath(this.FullPath);
		}
	}
}
