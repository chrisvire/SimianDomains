using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Schemas;

class ModelTest
{
	public struct Reference
	{
		public entry entry;
		public sense sense;

		public Reference(entry e, sense s)
		{
			entry = e;
			sense = s;
		}
	};

	static void Main(string[] args)
	{
		string filename = args[0];
		string search = args[1];

		XmlSerializer mySerializer = new XmlSerializer(typeof(database));
		FileStream myFileStream = new FileStream(filename, FileMode.Open);
		database o = (database)mySerializer.Deserialize(myFileStream);

		var entryIndex = new Dictionary<string, List<entry>>();
		foreach (entry e in o.entry)
		{
			if (!entryIndex.ContainsKey(e.form))
				entryIndex.Add(e.form, new List<entry>());
			entryIndex[e.form].Add(e);
		}

		var refIndex = new Dictionary<string, Reference>();
		foreach (entry e in o.entry)
		{
			foreach (sense s in e.sense)
			{
				refIndex.Add(s.id, new Reference(e, s));
			}
		}

		List<entry> result = entryIndex[search];
		foreach (entry e in result)
		{
			foreach (sense s in e.sense)
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
