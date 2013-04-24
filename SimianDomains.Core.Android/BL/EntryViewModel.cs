
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimianDomains.Core
{
	[Serializable]
	public class EntryViewModel
	{
		public string Form { get; set; }
		public List<SenseViewModel> Senses { get; set; }
	}
}

