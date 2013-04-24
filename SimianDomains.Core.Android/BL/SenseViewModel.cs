
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimianDomains.Core
{
	[Serializable]
	public class SenseViewModel
	{
		public string Gloss { get; set; }
		public string Category { get; set; }
		public List<string> Synonyms { get; set; }
	}
}

