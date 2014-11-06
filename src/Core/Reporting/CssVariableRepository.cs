using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Reporting
{
	public class CssVariableRepository
	{
		Dictionary<string, string> variables = new Dictionary<string, string>();

		public void Declare(string variableName, string value)
		{
			string key = FormatAsVariable(variableName);

			if (!this.variables.ContainsKey(key))
			{
				this.variables.Add(key, value);
			}
			else
			{
				variables[key] = value;
			}
		}
		
		public string Process(string text)
		{
			var styleBuilder = new StringBuilder(text);

			foreach (string key in variables.Keys)
			{
				styleBuilder.Replace(key, variables[key]);
			}

			return styleBuilder.ToString();
		}
		
		private string FormatAsVariable(string key)
		{
			string prefix = "$(";
			string suffix = ")";

			if (!key.StartsWith(prefix) && !key.EndsWith(suffix))
				return String.Format("{0}{1}{2}", prefix, key, suffix);

			return key; 
		}
	}


}
