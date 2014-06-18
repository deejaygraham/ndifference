using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NDifference.UI.Controls
{
	public partial class EmphasisLabel : Label
	{
		const string boldOn = "b>";
		const string boldOff = "/b>";
		const string emphasisOn = "em>";
		const string emphasisOff = "/em>";

		public EmphasisLabel()
		{
			this.Chunks = new List<string>();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			Graphics g = pe.Graphics;

			using (Brush background = new SolidBrush(this.BackColor))
			{
				using (Brush textBrush = new SolidBrush(this.ForeColor))
				{
					g.FillRectangle(background, ClientRectangle);

					float x = 0F, y = 0F;

					bool bold = false;
					using (Font boldFont = new Font(this.Font, FontStyle.Bold))
					{
						foreach (string chunk in this.Chunks)
						{
							string text = chunk;

							if (chunk.StartsWith(boldOn))
							{
								text = chunk.Substring(boldOn.Length);

								bold = true;
							}
							else if (chunk.StartsWith(emphasisOn))
							{
								text = chunk.Substring(emphasisOn.Length);

								bold = true;
							}
							else if (chunk.StartsWith(boldOff))
							{
								text = chunk.Substring(boldOff.Length);
								bold = false;
							}
							else if (chunk.StartsWith(emphasisOff))
							{
								text = chunk.Substring(emphasisOff.Length);
								bold = false;
							}

							if (bold)
							{
								this.DrawString(text, g, boldFont, textBrush, x, y);
								SizeF offset = g.MeasureString(text, boldFont);

								x += offset.Width;
							}
							else
							{
								this.DrawString(text, g, this.Font, textBrush, x, y);
								SizeF offset = g.MeasureString(text, this.Font);

								x += offset.Width;
							}
						}
					}
				}
			}
		}

		private void DrawString(string text, Graphics g, Font font, Brush brush, float x, float y)
		{
			g.DrawString(text, font, brush, x, y);
		}

		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;

				string[] chunks = value.Split(new char[] { '<' });

				this.Chunks.Clear();

				foreach (string chunk in chunks)
				{
					this.Chunks.Add(chunk);
				}

			}
		}

		private List<string> Chunks
		{
			get;
			set;
		}
	}
}
