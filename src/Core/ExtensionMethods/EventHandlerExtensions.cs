using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference
{
	/// <summary>
	/// Extension methods for EventHandler objects
	/// </summary>
	public static class EventHandlerExtensions
	{
		/// <summary>
		/// Fire an event with no arguments
		/// </summary>
		/// <param name="self">The event</param>
		/// <param name="sender">The sender of the event</param>
		public static void Fire(this EventHandler self, object sender)
		{
			EventHandler copyOf = self;

			if (copyOf != null)
			{
				copyOf(sender, EventArgs.Empty);
			}
		}

		/// <summary>
		/// Fire an event with argument of type T
		/// </summary>
		/// <typeparam name="T">The type of the argument</typeparam>
		/// <param name="self">The event</param>
		/// <param name="sender">The sender of the event</param>
		/// <param name="args">The argument</param>
		public static void Fire<T>(this EventHandler<T> self, object sender, T args) where T : EventArgs
		{
			EventHandler<T> copyOf = self;

			if (copyOf != null)
			{
				copyOf(sender, args);
			}
		}
	}

}
