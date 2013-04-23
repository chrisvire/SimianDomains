using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Schemas;

class ModelTest
{
	public struct Reference
	{
		public Entry entry;
		public Sense sense;

		public Reference(Entry e, Sense s)
		{
			entry = e;
			sense = s;
		}
	};

	static void Main(string[] args)
	{
		string filename = args[0];
		string search = args[1];

		XmlSerializer mySerializer = new XmlSerializer(typeof(Database));
		FileStream myFileStream = new FileStream(filename, FileMode.Open);
		Database o = (Database)mySerializer.Deserialize(myFileStream);

		var entryIndex = new Dictionary<string, List<Entry>>();
		foreach (Entry e in o.entry)
		{
			if (!entryIndex.ContainsKey(e.form))
				entryIndex.Add(e.form, new List<Entry>());
			entryIndex[e.form].Add(e);
		}

		var refIndex = new Dictionary<string, Reference>();
		foreach (Entry e in o.entry)
		{
			foreach (Sense s in e.sense)
			{
				refIndex.Add(s.id, new Reference(e, s));
			}
		}

		List<Entry> result = entryIndex[search];
		foreach (Entry e in result)
		{
			foreach (Sense s in e.sense)
			{
				Console.WriteLine("{0} ({1}), {2}", e.form, s.gloss, s.category);
				foreach (string syn in s.synonyms)
				{
					var r = refIndex[syn];
					Console.WriteLine("    {0} ({1})", r.entry.form, r.sense.gloss);
				}
			}
		}
	}
};
