
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Schema;

namespace SimianDomains.Core
{
	public class XmlEntryRepository : BaseEntryRepository
	{
		public XmlEntryRepository (Stream xmlStream)
		{
			var mySerializer = new XmlSerializer(typeof(Database));
			var db = (Database)mySerializer.Deserialize(xmlStream);
			
			EntryIndex = new Dictionary<string, List<Entry>>();
			foreach (Entry e in db.entry)
			{
				if (!EntryIndex.ContainsKey(e.form))
					EntryIndex.Add(e.form, new List<Entry>());
				EntryIndex[e.form].Add(e);
			}
			
			RefIndex = new Dictionary<string, Reference>();
			foreach (Entry e in db.entry)
			{
				foreach (Sense s in e.sense)
				{
					RefIndex.Add(s.id, new Reference(e, s));
				}
			}
		}
	}
}

