using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.UI
{
	public class ProjectCleanlinessTracker
	{
		public ProjectCleanlinessTracker(MainForm form)
		{
			form.ProjectLoaded += (s, e) => { this.IsDirty = false; };

			form.ProjectChanged += (s, e) => { this.IsDirty = true; };

			form.ProjectSaved += (s, e) => { this.IsDirty = false; };
		}

		public bool IsDirty { get; private set; }
	}
}
