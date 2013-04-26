
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
        private const string synonymKey = "Synonym";

		private SimianDomainsAndroid.Application App {
			get;
			set;
		}
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate (bundle);
			App = (SimianDomainsAndroid.Application) Application;

			SetContentView(Resource.Layout.Result);
			         
			var form = FormFromIntent(Intent);
            PopulateData (form);
		}

		void PopulateData (string form)
		{
			IList<IDictionary<string, object>> forms = new JavaList<IDictionary<string, object>> ();
			IList<IList<IDictionary<string, object>>> synonyms = new JavaList<IList<IDictionary<string, object>>> ();
			foreach (EntryViewModel entry in App.SharedEntryRepository.FindByForm (form)) {
				JavaList<IDictionary<string, object>> synonymInformation = new JavaList<IDictionary<string, object>> ();
				foreach (SenseViewModel sense in entry.Senses) {
					JavaDictionary<string, object> formInformation = new JavaDictionary<string, object> ();
					formInformation [formKey] = String.Format ("{0} - {1}, /{2}/", entry.Form, sense.Category, sense.Gloss);
					forms.Add (formInformation);
					JavaDictionary<string, object> synonymHolder = new JavaDictionary<string, object> ();
					foreach (string synonym in sense.Synonyms) {
						synonymHolder [synonymKey] = synonym;
						synonymInformation.Add (synonymHolder);
					}
					synonyms.Add (synonymInformation);
				}
			}
			SimpleExpandableListAdapter expListAdapter = new SimpleExpandableListAdapter (this, forms, // Creating group List.
			Resource.Layout.Group_Item, // Group item layout XML.
			new[] {
				formKey
			}, // the key of group item.
			new[] {
				Resource.Id.group_item
			}, // ID of each group item.-Data under the key goes into this TextView.
			synonyms, // childData describes second-level entries.
			Resource.Layout.Sub_Item, // Layout for sub-level entries(second level).
			new[] {
				synonymKey
			}, // Keys in childData maps to display.
			new[] {
				Resource.Id.sub_item
			}// Data under the keys above go into these TextViews.
			);
			SetListAdapter (expListAdapter);
			// setting the adapter in the list.
			int count = ExpandableListAdapter.GroupCount;
			for (int position = 0; position < count; position++) {
				ExpandableListView.ExpandGroup (position);
			}
		}

		public override bool OnChildClick (ExpandableListView parent, View v, int groupPosition, int childPosition, long id)
		{
			//return base.OnChildClick (parent, v, groupPosition, childPosition, id);
			JavaDictionary<string, object> child = (JavaDictionary<string, object>) ExpandableListAdapter.GetChild(groupPosition, childPosition);
			string synonym = (string) child[synonymKey];
			PopulateData(synonym);
			return true;
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

