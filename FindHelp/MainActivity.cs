using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Net;
using System.IO;
using System.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections;
using Newtonsoft.Json.Linq;
using Android.Telephony;

namespace WeatherREST
{
	[Activity (Label = "Auto Help", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{		
		Button button;
		ListView listview;
		Spinner spinner;
		//TextView lblNotFound;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Main);
			button = FindViewById<Button> (Resource.Id.getWeatherButton1);
			listview = FindViewById<ListView> (Resource.Id.listView1);

			spinner = FindViewById<Spinner> (Resource.Id.spinner);
			var adapter = ArrayAdapter.CreateFromResource (
				this, Resource.Array.type_array, Android.Resource.Layout.SimpleSpinnerItem);

			adapter.SetDropDownViewResource (Android.Resource.Layout.SimpleSpinnerDropDownItem);
			spinner.Adapter = adapter;

			button.Click += SearchButton_Click;
		}

		protected void SearchButton_Click (Object sender, EventArgs e)
		{			
			Dictionary<string, string> apiCallParams = GetClientLocation ();
			string phoneToken = GetPhoneToken ();
			apiCallParams.Add ("phoneToken", phoneToken);

			string phoneId = GetPhoneID ();
			apiCallParams.Add ("phoneId", phoneId);

			string type = spinner.GetItemAtPosition (spinner.SelectedItemPosition).ToString();
			apiCallParams.Add ("type", type);

			string url = this.GetApiUrl (apiCallParams);

			GetNearLocations (url);
		}

		protected string GetPhoneID ()
		{
			//todo get phone id 
			return "undefined";
		}

		protected string GetPhoneToken ()
		{
			//todo get phone id 
			return "undefined";
		}

		protected Dictionary<string, string> GetClientLocation ()
		{
			//todo implement this
			Dictionary<string, string> location = new Dictionary<string, string> ();				
			location ["lat"] = "43.8550359";
			location ["lng"] = "25.970048";

			return location;
		}

		protected string GetApiUrl (IDictionary<string, string> dict)
		{			
			string url = Config.url;

			foreach (KeyValuePair<string, string> str in dict) {
				url += "&" + str.Key + "=" + str.Value;
			}

			return url;
		}

		protected void GetNearLocations (string url)
		{
			List<Shop> arrayShops = new List<Shop> ();

			HttpWebRequest webRequest = new HttpWebRequest (new Uri (url));
			webRequest.ContentType = "application/json";
			webRequest.Method = "GET";

			try {
				webRequest.BeginGetResponse ((ar) => {
					var request = (HttpWebRequest)ar.AsyncState;
					using (var response = (HttpWebResponse)request.EndGetResponse (ar)) {						
						using (Stream stream = response.GetResponseStream ()) {
							var reader = new JsonTextReader (new StreamReader (stream));
							var jArray = JArray.Load (reader);
							if (jArray.Count == 0) {								
								return;
							} 								

							for (int i = 0; i < jArray.Count; i++) {
								var data = jArray [i];

								Shop shop = new Shop ();

								shop.Name = (string)data.SelectToken ("name");
								shop.Phone = (string)data.SelectToken ("phone");
								shop.Distance = (string)data.SelectToken("distance");
								shop.Address = (string)data.SelectToken("address");

								arrayShops.Add (shop);
							}
							RunOnUiThread (() => {
								this.listview.Adapter = new ListViewAdapter (this, arrayShops);
							});							
						}
					}
				}, webRequest);
			} catch (Exception ex) {
				//todo process exceptions	
			} 
		} 
	}
}