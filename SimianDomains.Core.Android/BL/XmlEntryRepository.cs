
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
			
			entryIndex = new Dictionary<string, List<Entry>>();
			foreach (Entry e in db.entry)
			{
				if (!entryIndex.ContainsKey(e.form))
					entryIndex.Add(e.form, new List<Entry>());
				entryIndex[e.form].Add(e);
			}
			
			refIndex = new Dictionary<string, Reference>();
			foreach (Entry e in db.entry)
			{
				foreach (Sense s in e.sense)
				{
					refIndex.Add(s.id, new Reference(e, s));
				}
			}
		}
	}
}

