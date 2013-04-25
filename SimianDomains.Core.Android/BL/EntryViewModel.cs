
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimianDomains.Core
{
	[Serializable]
	public class EntryViewModel
	{
		public EntryViewModel ()
		{
			Senses = new List<SenseViewModel>();
		}
		public string Form { get; set; }
		public List<SenseViewModel> Senses { get; set; }
	}
}

