using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using SimianDomains.Core;
using System.Collections.Generic;

namespace SimianDomainsAndroid
{
	[Activity (Label = "Simian Domains", MainLauncher = true)]
	public class Activity1 : Activity
	{
		protected AutoCompleteTextView SearchTextView
		{
            get { return FindViewById<AutoCompleteTextView>(Resource.Id.autocomplete_search); }
		}

		protected Button SubmitButton
        {
            get { return FindViewById<Button>(Resource.Id.buttonSubmit); }
		}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			var app = (Application)Application;
			var allForms = app.SharedEntryRepository.AllForms;
			var adapter = new ArrayAdapter(this, Resource.Layout.list_item, allForms);

			SearchTextView.Adapter = adapter;

			// Get our button from the layout resource,
			// and attach an event to it
			SubmitButton.Click += delegate {
				var form = SearchTextView.Text;
				var intent = ResultActivity.IntentFromForm(this, form);
				StartActivity(intent);
			};
		}
	}
}


