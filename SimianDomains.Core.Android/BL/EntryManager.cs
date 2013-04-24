
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Entry = SimianDomains.Core.Entry;

namespace SimianDomains.Core
{
	public class EntryManager
	{
		public EntryViewModel GetEntry(string form)
		{
			// TODO: Use Model.cs classed to query for the data from the XML
			return new EntryViewModel { Form = "Foo" };
		}

	}
}

