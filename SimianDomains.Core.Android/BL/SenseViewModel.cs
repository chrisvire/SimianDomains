
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

namespace SimianDomains.Core
{
	public class SenseViewModel
	{
		public string Gloss { get; set; }
		public string Category { get; set; }
		public List<string> Synonyms { get; set; }
	}
}

