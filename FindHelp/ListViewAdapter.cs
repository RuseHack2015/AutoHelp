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

namespace WeatherREST
{
 public class ListViewAdapter : ArrayAdapter<Shop>
 {
  Activity context;

  public ListViewAdapter(Activity context, List<Shop> list)
   : base(context, 0, list)
  {
   this.context = context;
  }

  public override View GetView(int position, View convertView, ViewGroup parent)
  {
   //var item = this.TransHedList[position];
   var item = this.GetItem (position);
   View view = convertView;
   if (view == null)
			view = context.LayoutInflater.Inflate (Resource.Layout.ListViewTemplate, null);
			TextView textView1 = view.FindViewById<TextView> (Resource.Id.textView1);
			TextView textView2 = view.FindViewById<TextView> (Resource.Id.textView2);
			TextView textView3 = view.FindViewById<TextView> (Resource.Id.textView3);
			textView1.Text = String.Format("{0} ( distance {1}km )", item.Name, item.Distance);
			textView2.Text = String.Format("phone: {0} ", item.Phone);
			textView3.Text = String.Format("address: {0}", item.Address);
   return view;
  }
 }
}
