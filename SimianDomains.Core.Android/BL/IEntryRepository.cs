
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimianDomains.Core
{
	public interface IEntryRepository
	{
		IEnumerable<EntryViewModel> FindByForm(string form);
		string[] AllForms();
	}
}

