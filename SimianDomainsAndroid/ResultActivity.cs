
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
	public class ResultActivity : ExpandableListActivity
	{
		public List<EntryViewModel> Entries {
			get;
			set;
		}

		public override bool OnChildClick (ExpandableListView parent, View v, int groupPosition, int childPosition, long id)
		{
			return base.OnChildClick (parent, v, groupPosition, childPosition, id);
		}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			var lv = ExpandableListView;
			lv.DividerHeight = 2;
			lv.Clickable = true;
			lv.SetGroupIndicator(null);
		}
	}
}

