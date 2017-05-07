using Android.App;
using Android.Widget;
using Android.OS;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
using Microsoft.Azure.Mobile.Distribute;
using System;
using System.Collections.Generic;
using System.Threading;

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
                   typeof(Analytics), typeof(Crashes),typeof(Distribute));
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


				try
			{
				var list = new List<byte[]>();
				while (true)
				{
					list.Add(new byte[1024]); // Change the size here.
					Thread.Sleep(100); // Change the wait time here.
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
			};




		}


		bool ShouldProcess(ErrorReport report)
		{

			// Check the report in here and return true or false depending on the ErrorReport.
			Crashes.ShouldAwaitUserConfirmation = () =>
			{
				return true; // Return true if the SDK should await user confirmation, otherwise false.
					};

			Crashes.NotifyUserConfirmation(UserConfirmation.Send);

			return true;
		}
	}
}

