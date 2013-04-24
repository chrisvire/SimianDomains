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
		private EntryManager entryManager;

		protected override void OnCreate (Bundle bundle)
		{
			var stream = Assets.Open("Akoose.xml");
			entryManager = new EntryManager(stream);

			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.buttonSubmit);
			
			button.Click += delegate {
				EditText text = FindViewById<EditText> (Resource.Id.editText1);

				var entries = entryManager.GetEntries(text.Text);
				var intent = ResultActivity.IntentFromEntries(this, entries);

				StartActivity(intent);
			};
		}



	}
}


