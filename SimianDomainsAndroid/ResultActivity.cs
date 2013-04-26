
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
	public class ResultActivity : ExpandableListActivity
	{
        private const string formKey = "Form";
        private const string senseKey = "Sense";

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView(Resource.Layout.Result);

            string form = FormFromIntent(Intent);
            Application app = (Application)Application;
            
            IList<IDictionary<string, object>> forms = new JavaList<IDictionary<string, object>>();
            IList<IList<IDictionary<string, object>>> senses = new JavaList<IList<IDictionary<string, object>>>();
		    foreach (EntryViewModel entry in app.SharedEntryRepository.FindByForm(form))
		    {
                JavaDictionary<string, object> formInformation = new JavaDictionary<string, object>();
                formInformation[formKey] = entry.Form;
                forms.Add(formInformation);

                JavaList<IDictionary<string, object>> senseInformation = new JavaList<IDictionary<string, object>>();
		        foreach (SenseViewModel sense in entry.Senses)
		        {
                    JavaDictionary<string, object> senseHolder = new JavaDictionary<string, object>();
                    senseHolder[senseKey] = sense.Gloss;
                    senseInformation.Add(senseHolder);
                }
				senses.Add(senseInformation);
		    }

            SimpleExpandableListAdapter expListAdapter =
                new SimpleExpandableListAdapter(
                        this,
                        forms,                          // Creating group List.
                        Resource.Layout.Group_Item,     // Group item layout XML.
                        new[] { formKey },              // the key of group item.
                        new[] { Resource.Id.group_item },// ID of each group item.-Data under the key goes into this TextView.
                        senses,                         // childData describes second-level entries.
                        Resource.Layout.Sub_Item,       // Layout for sub-level entries(second level).
                        new[] { senseKey },             // Keys in childData maps to display.
                        new[] { Resource.Id.sub_item }   // Data under the keys above go into these TextViews.
                    );
            SetListAdapter(expListAdapter);             // setting the adapter in the list.
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

