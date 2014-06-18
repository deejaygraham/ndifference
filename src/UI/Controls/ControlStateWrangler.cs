using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NDifference.UI.Controls
{
	internal class ControlStateWrangler
	{
		public ControlStateWrangler()
		{
			this.Controls = new List<Control>();
		}

		public void Add(Control control)
		{
			this.Controls.Add(control);
		}

		public void Enable()
		{
			this.Controls.ForEach(c => c.Enabled = true);
		}

		public void Disable()
		{
			this.Controls.ForEach(c => c.Enabled = false);
		}

		public void Visible()
		{
			this.Controls.ForEach(c => c.Visible = true);
		}

		public void Invisible()
		{
			this.Controls.ForEach(c => c.Visible = false);
		}

		private List<Control> Controls { get; set; }

	}
}
