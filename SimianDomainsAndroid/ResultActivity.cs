
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
		public List<EntryViewModel> Entries {
			get;
			set;
		}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Entries = EntryFromIntent(Intent);

			//SetContentView (Resource.Layout.SimpleDetail);

			//TextView tv = FindViewById<TextView> (Resource.Id.textView1);

			foreach (var e in Entries)
			{
				//tv.Append(e.Form);
			}
		}

		public static List<EntryViewModel> EntryFromIntent(Intent intent)
		{
			var reader = new StringReader(intent.GetStringExtra("entries"));

			var serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<EntryViewModel>));
			return (List<EntryViewModel>) serializer.Deserialize(reader);
		}
		
		public static Intent IntentFromEntries(Context context, List<EntryViewModel> entries)
		{
			var intent = new Intent(context, typeof(ResultActivity));

			var writer = new System.IO.StringWriter();
			var serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<EntryViewModel>));
			serializer.Serialize(writer, entries);
			intent.PutExtra("entries", writer.ToString());

			return intent;
		}

	}
}

