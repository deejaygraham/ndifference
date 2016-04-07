using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NDifference.UI.Controls
{
	public class WatermarkedTextBox : TextBox
	{
		private string _watermarkText;

		public WatermarkedTextBox()
		{
			this._watermarkText = "<Watermark>";
		}

		public string WatermarkText
		{
			get { return this._watermarkText; }
			set
			{
				this._watermarkText = value;

				if (!string.IsNullOrEmpty(this._watermarkText))
				{
					SetWatermark(this._watermarkText);
				}
			}
		}
		
		private void SetWatermark(string watermark)
		{
			NativeMethods.SendMessage(this.Handle, EM_SETCUEBANNER, (IntPtr)0, watermark);
		}

		const uint ECM_FIRST = 0x1500;
		const uint EM_SETCUEBANNER = ECM_FIRST + 1;

		private static class NativeMethods
		{
			[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
			public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);
		}
	}
}
