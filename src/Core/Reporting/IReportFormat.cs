﻿using NDifference.SourceFormatting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Reporting
{
	/// <summary>
	/// Represents a discrete report format - pdf, html, maml etc.
	/// </summary>
	public interface IReportFormat : IUniquelyIdentifiable
	{
		/// <summary>
		/// Human readable name
		/// </summary>
		string FriendlyName { get; }

		/// <summary>
		/// File Extension
		/// </summary>
		string Extension { get; }

		/// <summary>
		/// Format a link in the correct way.
		/// </summary>
		/// <param name="link"></param>
		/// <returns></returns>
		string Format(IDocumentLink link);

		/// <summary>
		/// Formats source code in the correct way.
		/// </summary>
		/// <param name="source"></param>
		/// <returns></returns>
		string Format(ICoded source);

		string FormatLink(string url, string name);

		string FormatTitle(int size, string title, string id);

		string FormatTableHeader(IEnumerable<string> headings);

		string FormatTableRow(IEnumerable<string> cells);

		/// <summary>
		/// Does this format match the provided one?
		/// </summary>
		/// <param name="format"></param>
		/// <returns></returns>
		bool Supports(IReportFormat format);

		/// <summary>
		/// Does this format format this kind of document?
		/// </summary>
		/// <param name="format"></param>
		/// <returns></returns>
		bool Supports(string format);
	}
}
