﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace NDifference.SourceFormatting
{
	public class SourceCode : IXmlSerializable, IEquatable<SourceCode>
	{
		private List<SourceCodeTag> tags = new List<SourceCodeTag>();

		public void Add(SourceCode code)
		{
			foreach (SourceCodeTag tag in code.tags)
			{
				this.tags.Add(tag);
			}
		}

		public void Add(SourceCodeTag tag)
		{
			this.tags.Add(tag);
		}

		public System.Xml.Schema.XmlSchema GetSchema()
		{
			return null;
		}

		public void ReadXml(System.Xml.XmlReader reader)
		{
			/* nothing - output only */
		}

		public void WriteXml(System.Xml.XmlWriter writer)
		{
			foreach (SourceCodeTag tag in this.tags)
			{
				tag.WriteXml(writer);
			}
		}

		public override string ToString()
		{
			using (TextWriter writer = new StringWriter())
			{
				using (XmlWriter xmlWriter = new XmlTextWriter(writer))
				{
					this.WriteXml(xmlWriter);
					return writer.ToString();
				}
			}
		}

		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			SourceCode other = obj as SourceCode;

			if ((object)other == null)
			{
				return false;
			}

			return string.Compare(
				this.ToString(),
				other.ToString(),
				StringComparison.InvariantCulture) == 0;
		}

		public bool Equals(SourceCode other)
		{
			if ((object)other == null)
			{
				return false;
			}

			return string.Compare(
				this.ToString(),
				other.ToString(),
				StringComparison.InvariantCulture) == 0;
		}

		public static bool operator ==(SourceCode a, SourceCode b)
		{
			if (object.ReferenceEquals(a, b))
			{
				return true;
			}

			if (((object)a == null) || ((object)b == null))
			{
				return false;
			}

			return string.Compare(
				a.ToString(),
				b.ToString(),
				StringComparison.InvariantCulture) == 0;
		}

		public static bool operator !=(SourceCode a, SourceCode b)
		{
			return !(a == b);
		}

		//public void SaveTo(TextWriter writer)
		//{
		//	ObjectFlattener<SourceCode>.Flatten(this, writer);
		//}

		//public string ToXml()
		//{
		//	using (var writer = new StringWriter())
		//	{
		//		this.SaveTo(writer);
		//		return writer.ToString();
		//	}
		//}

		public ReadOnlyCollection<SourceCodeTag> Content
		{
			get
			{
				return new ReadOnlyCollection<SourceCodeTag>(this.tags);
			}
		}
	}
}
