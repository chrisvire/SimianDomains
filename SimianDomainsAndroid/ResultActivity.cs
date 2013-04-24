
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

using SimianDomains.Core;

namespace SimianDomainsAndroid
{
	[Activity (Label = "ResultActivity")]			
	public class ResultActivity : Activity
	{
		public List<EntryViewModel> Entries {
			get;
			set;
		}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.SimpleDetail);

			TextView tv = FindViewById<TextView> (Resource.Id.textView1);

			foreach (var e in Entries)
			{
				tv.Append(e.Form);
			}
			
		}
	}
}

