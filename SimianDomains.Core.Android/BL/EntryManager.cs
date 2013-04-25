
using System;
using System.Collections.Generic;
using Debug = System.Diagnostics.Debug;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Schema;

namespace SimianDomains.Core
{
	struct Reference
	{
		public Entry entry;
		public Sense sense;
		
		public Reference(Entry e, Sense s)
		{
			entry = e;
			sense = s;
		}
	};

	public class EntryManager
	{
		private Dictionary<string, List<Entry>> entryIndex;
		private Dictionary<string, Reference> refIndex;

		public EntryManager(Stream stream)
		{
			var mySerializer = new XmlSerializer(typeof(Database));
			var db = (Database)mySerializer.Deserialize(stream);

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
			
		public List<EntryViewModel> GetEntries(string form)
		{
			// TODO: This could probably be done more efficiently with a LINQ Query
			var entries = new List<EntryViewModel>();
			try
			{
				List<Entry> result;
				if (!entryIndex.TryGetValue(form, out result))
					return entries;

				foreach (Entry e in result)
				{
					var entryModel = new EntryViewModel { Form = e.form };
					foreach (Sense s in e.sense)
					{
						var senseModel = new SenseViewModel { Gloss = s.gloss, Category = s.category, Synonyms = new List<string>() };

						foreach (string syn in s.synonyms)
						{
							var r = refIndex[syn];
							senseModel.Synonyms.Add(r.entry.form);
						}
						entryModel.Senses.Add(senseModel);
					}
					entries.Add(entryModel);
				}
			}
			catch (Exception e)
			{
				Debug.Write(e);
			}

			return entries;
		}

	}
}

