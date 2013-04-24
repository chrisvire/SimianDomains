using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using SimianDomains.Core;

namespace SimianDomainsAndroid
{
	[Activity (Label = "Simian Domains", MainLauncher = true)]
	public class Activity1 : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.buttonSubmit);
			
			button.Click += delegate {
				EditText text = FindViewById<EditText> (Resource.Id.editText1);

				var em = new EntryManager();
				var entry = em.GetEntry(text.Text);

				var result = new ResultActivity();
				result.Entry = entry;

				//TODO: StartActivity with ResultActivity and somehow pass entry to the Activity (maybe using an Intent?)

			};
		}
	}
}


