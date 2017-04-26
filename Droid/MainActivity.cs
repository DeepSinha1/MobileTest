using Android.App;
using Android.Widget;
using Android.OS;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
using System;
using System.Collections.Generic;

namespace MobileTest.Droid
{
	[Activity(Label = "MobileTest", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			MobileCenter.Start("8d6a239d-6428-4725-a02b-6028645a8560",
                   typeof(Analytics), typeof(Crashes));
			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button>(Resource.Id.myButton);
			Button myTestButton = FindViewById<Button>(Resource.Id.myTestButton);

			button.Click += delegate {
				Analytics.TrackEvent("Button Click");
				button.Text = $"{count++} clicks!";
				if (count == 5)
				{
					throw new NotImplementedException();
				}
			};

			myTestButton.Click += delegate {

				Analytics.TrackEvent("Test Button Click", new Dictionary<string, string>
				{
					["Event Name"] = "Button Click",
					["Button Name"] = myTestButton.Text
				});
			};


		}
	}
}

