﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference
{
	public class Progress
	{
		public static readonly int MinimumProgressValue = 0;
		public static readonly int MaximumProgressValue = 100;

		private int pcValue;

        public Progress(string description)
        {
            this.Description = description;
        }

        public Progress(string description, int current, int total)
            : this(description)
        {
            if (total > 0)
            {
                this.Percent = (100 * current) / total;
            }
        }
		public int Percent
		{
			get
			{
				return this.pcValue;
			}
			set
			{
				this.pcValue = Math.Max(MinimumProgressValue, value);

				if (value > MaximumProgressValue)
				{
					this.pcValue = value % MaximumProgressValue;
				}
			}
		}

		public string Description { get; set; }
	}

}
