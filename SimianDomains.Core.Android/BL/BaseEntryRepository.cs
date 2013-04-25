
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

using Schema;

namespace SimianDomains.Core
{

	public class BaseEntryRepository : IEntryRepository
	{
		protected struct Reference
		{
			public Entry entry;
			public Sense sense;
			
			public Reference(Entry e, Sense s)
			{
				entry = e;
				sense = s;
			}
		};

		protected Dictionary<string, List<Entry>> entryIndex { get;  set; }
		protected Dictionary<string, Reference> refIndex { get; set; }

		public IEnumerable<EntryViewModel> FindByForm(string form)
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
							Reference r;
							if (refIndex.TryGetValue(syn, out r))
							{
								senseModel.Synonyms.Add(r.entry.form);
							}
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

		public string[] AllForms()
		{
			return entryIndex.Keys.Distinct().ToArray();
		}
	}
}

