
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
using System.IO;

namespace SimianDomainsAndroid
{
	[Activity (Label = "ResultActivity")]			
	public class ResultActivity : Activity
	{
		public IEnumerable<EntryViewModel> Entries {
			get;
			set;
		}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			var form = FormFromIntent(Intent);

			var app = (SimianDomainsAndroid.Application) Application;
			Entries = app.SharedEntryRepository.FindByForm(form);

			SetContentView (Resource.Layout.Result);

			TextView tv = FindViewById<TextView> (Resource.Id.textView1);
			
			foreach (var e in Entries)
			{
				tv.Append(e.Form + System.Environment.NewLine);
			}
		}
		 
		public static string FormFromIntent(Intent intent)
		{
			return intent.GetStringExtra("form");

		}
		
		public static Intent IntentFromForm(Context context, string form)
		{
			var intent = new Intent(context, typeof(ResultActivity));

			intent.PutExtra("form", form);

			return intent;
		}
	}
}

