
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimianDomains.Core
{
	[Serializable]
	public class SenseViewModel
	{
		public SenseViewModel ()
		{
			Synonyms = new List<string>();
		}
		public string Gloss { get; set; }
		public string Category { get; set; }
		public List<string> Synonyms { get; set; }
	}
}

