using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NDifference.UI.Controls
{
	public class EmptyListView : ListView
	{
		const int DrawBackground = 20;

		public string MessageWhenEmpty
		{
			get;
			set;
		}

		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);

			if (m.Msg == DrawBackground)
			{
				if (this.Items.Count == 0)
				{
					using(Graphics g = this.CreateGraphics())
					{
						string msg = MessageWhenEmpty;

						if (!string.IsNullOrEmpty(msg))
						{
							int width = (this.Width - g.MeasureString(msg, this.Font).ToSize().Width) / 2;
							g.DrawString(msg, this.Font, SystemBrushes.ControlText, width, 30);
						}
					}
				}
			}
		}
	}
}
